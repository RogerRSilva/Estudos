// Criar campos dinamicamente com base na quantidade de colunas e linhas
function criaGrid(qtdLinhas, qtdColunas, containerGrid) {
    // Limpa o conteúdo existente do contêiner
    containerGrid.innerHTML = ''

    //cria um grid dinamico do zero
    for (var row = 1; row <= qtdLinhas; row++) {
        for (var col = 1; col <= qtdColunas; col++) {
            var gridItem = document.createElement("div");
            gridItem.setAttribute("id", "box-" + row + "-" + col); // Adiciona um atributo "id" com um valor único
            containerGrid.appendChild(gridItem);
        }
    }
}

//adiciona todo o conteúdo no campo flex
function adicionarConteudo(divId, texto, caminhoImagem, largura, altura) {
    var div = document.getElementById(divId);
    if (div) {
        // Criar um elemento com a classe que estiliza os textos e depois um elemento de parágrafo
        var containerTexto = document.createElement("div");
        containerTexto.setAttribute("class", "container-texto");
        var paragrafo = document.createElement("p");
        paragrafo.textContent = texto;

        //verifica se o texto excede o número de caracteres e se sim, oculta os caracteres excendentes
        document.addEventListener('DOMContentLoaded', function () {
            var limiteCaracteres = 199; // limite de caracteres

            if (paragrafo.textContent.length > limiteCaracteres) {
                var novoConteudo = paragrafo.textContent.substring(0, limiteCaracteres);
                paragrafo.textContent = novoConteudo;
            }
        });

        div.appendChild(containerTexto);
        containerTexto.appendChild(paragrafo);

        // Criar um elemento de imagem dentro do container de imagem
        var containerImg = document.createElement("div");
        containerImg.setAttribute("class", "container-imagem");
        var imagem = document.createElement("img");
        imagem.src = caminhoImagem;
        imagem.style.width = largura;
        imagem.style.height = altura;
        containerTexto.appendChild(containerImg);
        containerImg.appendChild(imagem);
    }
}

//função que serve para adicionar um símbolo em um container de simbolo
function adicionarSimbolo(divId, texto) {
    var div = document.getElementById(divId);
    if (div) {
        // Criar um elemento de com a classe que estiliza os textos e depois um elemento de parágrafo
        var containerSimbolo = document.createElement("div");
        containerSimbolo.setAttribute("class", "container-simbolo");
        var simbolo = document.createElement("b");
        simbolo.textContent = texto;
        div.appendChild(containerSimbolo);
        containerSimbolo.appendChild(simbolo);
    }
}