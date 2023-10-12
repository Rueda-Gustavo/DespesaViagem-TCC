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