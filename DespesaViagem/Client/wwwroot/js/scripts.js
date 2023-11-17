function getUsername() {
    document.getElementById('btnObterUsername').click();
};

function travarCampos() {
    travaDataInicial();
    travaDataFinal();
    travaAdiantamento();
};

function travaDataInicial() {
    var dataInicial = document.getElementById('DataInicial');
    dataInicial.disabled = true;
    dataInicial.readonly = true;
    dataInicial.addEventListener('click', function () {
        dataInicial.disabled = true;
        alert('Esse campo s\u00F3 pode ser alterado com a viagem Aberta.');
    });
};

function travaDataFinal() {
    var dataFinal = document.getElementById('DataFinal');
    dataFinal.disabled = true;
    dataFinal.readonly = true;
    dataFinal.addEventListener('click', function () {
        dataFinal.disabled = true;
        alert('Esse campo s\u00F3 pode ser alterado com a viagem Aberta.');
    });
};

function travaAdiantamento() {
    var adiantamento = document.getElementById('Adiantamento');
    adiantamento.disabled = true;
    adiantamento.readonly = true;
    adiantamento.addEventListener('click', function () {
        adiantamento.disabled = true;
        alert('Esse campo s\u00F3 pode ser alterado com a viagem Aberta.');
    });
};

async function salvarArquivo(name, Content) {
    var link = document.createElement('a');
    link.download = name;
    link.href = "data:text/plain;charset=utf-8," + encodeURIComponent(Content)
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}

function mudarVisibilidadeCheckboxEstadosViagem() {
    let campos = document.getElementsByClassName('container-relatorio-opcoes')[0].querySelectorAll('input[type="checkbox"]') && document.querySelectorAll('input[id^="estado"]');

    campos.forEach((e) => e.disabled = !(e.disabled));
}

function salvarRelatorio(nomeArquivo, bytesBase64) {
    var link = document.createElement('a');
    link.download = nomeArquivo;
    link.href = "data:application/octet-stream;base64," + bytesBase64;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}