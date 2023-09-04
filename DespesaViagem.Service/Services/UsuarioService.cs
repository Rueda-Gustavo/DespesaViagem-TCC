using CSharpFunctionalExtensions;
using DespesaViagem.Infra.Interfaces;
using DespesaViagem.Services.Interfaces;
using DespesaViagem.Shared.Models.Core.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace DespesaViagem.Services.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IConfiguration _configuration;

        public UsuarioService(IUsuarioRepository usuarioRepository, IConfiguration configuration)
        {
            _usuarioRepository = usuarioRepository;
            _configuration = configuration;
        }
        public async Task<Result<string>> Login(string username, string password)
        {
            Usuario? usuario = await _usuarioRepository.ObterUsuario(username);
            if (usuario is null)
            {
                return Result.Failure<string>("Usuario ou senha incorretos.");
            }
            else if (!VerificarPasswordHash(password, usuario.PasswordHash, usuario.PasswordSalt))
            {
                return Result.Failure<string>("Usuario ou senha incorretos.");
            }

            string token = CriarTokenJwt(usuario);
            return Result.Success(token);
        }

        private bool VerificarPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using HMACSHA512 hmac = new(passwordSalt);
            var passwordConvertidaParaHash = 
                hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            //Através do valor aletório Salt obtido a senha é convertida para hash usando esse valor
            //para comparar se os valores binários armazenados no banco são iguais aos valores que
            //acabaram de ser convertidos a partir da senha digitada pelo usuário
            return passwordConvertidaParaHash.SequenceEqual(passwordHash);
        }

        private string CriarTokenJwt(Usuario usuario)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Username),
                new Claim(ClaimTypes.Role, usuario.TipoDeUsuario.ToString())                
            };

            SymmetricSecurityKey key = new(Encoding.UTF8
                .GetBytes(_configuration.GetSection("AppSettings:Token").Value!));

            SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha512Signature);

            JwtSecurityToken token = new(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            string jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public async Task<Result<bool>> TrocarSenha(int idUsuario, string newPassword)
        {
            Usuario? usuario = await _usuarioRepository.ObterUsuario(idUsuario);
            if (usuario is null)
            {
                return Result.Failure<bool>("Usuário não encontrado!");
            }

            CriarPasswordHash(newPassword, out byte[] passwordHash, out byte[] passwordSalt);

            usuario.PasswordHash = passwordHash;
            usuario.PasswordSalt = passwordSalt;

            await _usuarioRepository.Update(usuario);

            return Result.Success(true);
        }

        public static void CriarPasswordHash(string passoword, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using HMACSHA512 hmac = new();
            passwordSalt = hmac.Key;
            passwordHash = hmac
                .ComputeHash(Encoding.UTF8.GetBytes(passoword));
        }
    }
}
