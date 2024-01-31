//função que recebe as especificações dos blocos svg, o tamanho do container e as conexões
function criarContainer(container, tamanho, conexoes, conteudo, imagens) {
    //variavel que armazena o elemento svg
    var svg = document.createElementNS("http://www.w3.org/2000/svg", "svg");
    //variavel que reconhece a div que irá armazenar a tag svg
    var div = document.getElementById("container-diagrama");

    div.setAttribute("width", tamanho.width);
    div.setAttribute("height", tamanho.height);

    svg.setAttribute("viewBox", "0 0 " + (tamanho.width) + " " + (tamanho.height));
    svg.setAttribute("width", "100%");
    svg.setAttribute("height", "100%");
    svg.setAttribute("class", "svg-container");

    //cria todos os blocos contidos dentro do array "container" em svg
    for (var i = 0; i < container.length; i++) {
        var bloco = document.createElementNS("http://www.w3.org/2000/svg", "rect");

        bloco.setAttribute("x", container[i].posX);
        bloco.setAttribute("y", container[i].posY);
        bloco.setAttribute("width", container[i].width);
        bloco.setAttribute("height", container[i].height);
        bloco.setAttribute("fill", container[i].color);
        bloco.setAttribute("rx", 10);
        bloco.setAttribute("ry", 10);

        if (container[i].shape == "losang") {
            bloco.setAttribute("transform", "rotate(45 " + (container[i].posX + (container[i].width / 2)) + " " + (container[i].posY + (container[i].height / 2)) + ")");
        }
        else if (container[i].shape == "circle") {
            bloco.setAttribute("rx", "50%");
            bloco.setAttribute("ry", "50%");
        }

        svg.appendChild(bloco);
    }

    //váriaveis que definem as setas
    var def = document.createElementNS("http://www.w3.org/2000/svg", "defs");
    var mark = document.createElementNS("http://www.w3.org/2000/svg", "marker");
    var arrowHead = document.createElementNS("http://www.w3.org/2000/svg", "polygon");

    mark.setAttribute("id", "arrowhead");
    mark.setAttribute("markerWidth", 5);
    mark.setAttribute("markerHeight", 3.5);
    mark.setAttribute("orient", "auto");
    mark.setAttribute("refX", 3.5);
    mark.setAttribute("refY", 1.75);

    arrowHead.setAttribute("points", "0 0, 5 1.75, 0 3.5");
    arrowHead.setAttribute("fill", "black");

    //cria todas as conexões especificadas na página html
    for (var i = 0; i < conexoes.length; i++) {
        var arrowLine = document.createElementNS("http://www.w3.org/2000/svg", "line");

        var anguloPi = verificarAngulo(calcularAngulo(container[conexoes[i].from], container[conexoes[i].to]), conexoes[i].point2);

        criaLinha(arrowLine, container[conexoes[i].from], container[conexoes[i].to], anguloPi, conexoes[i].point1);

        arrowLine.setAttribute("stroke", "black");
        arrowLine.setAttribute("stroke-linecap", "round")
        arrowLine.setAttribute("stroke-width", 4);
        arrowLine.setAttribute("marker-end", "url(#arrowhead)");
        arrowLine.setAttribute("stroke-dasharray", conexoes[i].dotted);

        svg.appendChild(arrowLine);
    }

    insereConteudo(imagens, conteudo, container, svg);

    //amarra os elementos criados
    svg.appendChild(def);
    def.appendChild(mark);
    mark.appendChild(arrowHead);

    //amarra a tag svg na div
    div.appendChild(svg);
}

function insereConteudo(image, cont, blocos, svg) {
    for (var i = 0; i < cont.length; i++) {
        // Encontrar o bloco correspondente pelo ID
        var blocoId = blocos[i].id;

        if (blocoId == cont[i].idBox) {
            var containerCont = document.createElementNS("http://www.w3.org/2000/svg", "foreignObject");
            var contDiv = document.createElement("div");

            if (blocoId == image[i].idImg) {
                var imageCont = document.createElement("div");
                imageCont.style.maxWidth = "100%";
                imageCont.style.maxHeight = "100%";

                var imgDiv = document.createElement("img");

                imgDiv.src = image[i].img;
                imgDiv.style.width = imageCont.width;
                imgDiv.style.height = imageCont.height;
                imgDiv.style.maxWidth = (image[i].width) + "px";
                imgDiv.style.maxHeight = (image[i].height) + "px";
                imgDiv.style.padding = (image[i].paddingImg) + "px";

                // Centraliza a imagem dentro do bloco SVG
                imgDiv.style.display = "block";
                imgDiv.style.margin = image[i].marginImg;

                imageCont.appendChild(imgDiv);
                containerCont.appendChild(imageCont); // Adicione containerCont à tag svg

            }

            containerCont.setAttribute("x", blocos[i].posX);
            containerCont.setAttribute("y", blocos[i].posY);
            containerCont.setAttribute("width", blocos[i].width);
            containerCont.setAttribute("height", blocos[i].height);

            var txtDiv = document.createElement("div");


            if (blocos[i].height - image[i].height - (image[i].paddingImg * 2) == 0 && blocos[i].width - image[i].width - (image[i].paddingImg * 2) == 0) {

                txtDiv.style.height = "0px";
                txtDiv.style.width = "0px";
            }
            else if (blocos[i].height - image[i].height - (image[i].paddingImg * 2) > 0 && blocos[i].width - image[i].width - (image[i].paddingImg * 2) == 0) {
                txtDiv.style.height = (blocos[i].height - image[i].height) + "px";
                txtDiv.style.width = (image[i].width) + "px";
            }
            else if (blocos[i].height - image[i].height - (image[i].paddingImg * 2) == 0 && blocos[i].width - image[i].width - (image[i].paddingImg * 2) > 0) {
                txtDiv.style.height = (image[i].height) + "px";
                txtDiv.style.width = (blocos[i].width - image[i].width) + "px";
            }

            txtDiv.style.overflow = "hidden";

            contDiv.setAttribute("class", "container-texto");

            contDiv.style.fontSize = cont[i].fontSize;
            contDiv.style.color = cont[i].colorText;
            contDiv.style.fontFamily = "Arial";
            contDiv.style.marginInline = cont[i].paddingHorizontal;
            contDiv.style.marginTop = cont[i].paddingVertical;

            // Iterar sobre o array de texto atual
            for (var ii = 0; ii < cont[i].text.length; ii++) {
                // Criar um novo elemento de parágrafo para cada elemento do array de texto
                var paragrafo = document.createElement("p");
                paragrafo.style.margin = cont[i].marginParagraph + "px";
                paragrafo.textContent = cont[i].text[ii];
                paragrafo.style.textAlign = cont[i].textAlign;

                if (ii > 0) {
                    paragrafo.style.fontWeight = cont[i].fontWeightText;
                }
                else if (ii == 0) {
                    paragrafo.style.fontWeight = cont[i].fontWeightTittle;
                }

                // Adicionar o parágrafo ao elemento HTML desejado
                contDiv.appendChild(paragrafo);
            }

            txtDiv.appendChild(contDiv);
            containerCont.appendChild(txtDiv);
            svg.appendChild(containerCont); // Adicione containerCont à tag svg
        }
    }
}

//função que cria as conexões usando a posição dos blocos 1 e 2 e o ângulo entre eles
function criaLinha(arrowLine, bloco1, bloco2, angulo, pontoOrigem) {
    //calcula o ângulo
    var anguloOrigem = verificarAngulo(calcularAngulo(bloco1, bloco2), pontoOrigem);

    //O ângulo partindo do bloco 1 retorna um desses 4 valores correspondentes aos lados do bloco
    //Com base no valor retornado define de qual lado do bloco a seta irá partir
    if (anguloOrigem == 4) {
        arrowLine.setAttribute("x1", bloco1.posX + bloco1.width);
        arrowLine.setAttribute("y1", (bloco1.height / 2) + bloco1.posY);

        if (bloco1.shape == "losang") {
            arrowLine.setAttribute("x1", bloco1.posX + bloco1.width + (bloco1.width * 0.19));
            arrowLine.setAttribute("y1", (bloco1.height / 2) + bloco1.posY);
        }

    } else if (anguloOrigem == 1) {
        arrowLine.setAttribute("x1", (bloco1.width / 2) + bloco1.posX);
        arrowLine.setAttribute("y1", bloco1.posY + bloco1.height);

        if (bloco1.shape == "losang") {
            arrowLine.setAttribute("x1", (bloco1.width / 2) + bloco1.posX);
            arrowLine.setAttribute("y1", bloco1.posY + bloco1.height + (bloco1.height * 0.19));
        }

    } else if (anguloOrigem == 2) {
        arrowLine.setAttribute("x1", bloco1.posX);
        arrowLine.setAttribute("y1", (bloco1.height / 2) + bloco1.posY);

        if (bloco1.shape == "losang") {
            arrowLine.setAttribute("x1", bloco1.posX - (bloco1.width * 0.19));
            arrowLine.setAttribute("y1", (bloco1.height / 2) + bloco1.posY);
        }

    } else if (anguloOrigem == 3) {
        arrowLine.setAttribute("x1", (bloco1.width / 2) + bloco1.posX);
        arrowLine.setAttribute("y1", bloco1.posY);

        if (bloco1.shape == "losang") {
            arrowLine.setAttribute("x1", (bloco1.width / 2) + bloco1.posX);
            arrowLine.setAttribute("y1", bloco1.posY - (bloco1.height * 0.19));
        }

    }

    //O ângulo partindo do bloco 2 retorna um desses 4 valores correspondentes aos lados do bloco
    //Com base no valor retornado define de qual lado do bloco o final da seta irá tangenciar

    if (angulo == 4) {
        arrowLine.setAttribute("x2", bloco2.posX - 5);
        arrowLine.setAttribute("y2", (bloco2.height / 2) + bloco2.posY);

        if (bloco2.shape == "losang") {
            arrowLine.setAttribute("x2", bloco2.posX - (bloco2.width * 0.21));
            arrowLine.setAttribute("y2", (bloco2.height / 2) + bloco2.posY);
        }

    } else if (angulo == 3) {
        arrowLine.setAttribute("x2", (bloco2.width / 2) + bloco2.posX);
        arrowLine.setAttribute("y2", bloco2.posY + bloco2.height + 5);

        if (bloco2.shape == "losang") {
            arrowLine.setAttribute("x2", (bloco2.width / 2) + bloco2.posX);
            arrowLine.setAttribute("y2", bloco2.posY + bloco2.height + (bloco2.height * 0.21));
        }

    } else if (angulo == 2) {
        arrowLine.setAttribute("x2", bloco2.posX + bloco2.width + 5);
        arrowLine.setAttribute("y2", (bloco2.height / 2) + bloco2.posY);

        if (bloco2.shape == "losang") {
            arrowLine.setAttribute("x2", bloco2.posX + bloco2.width + (bloco2.width * 0.21));
            arrowLine.setAttribute("y2", (bloco2.height / 2) + bloco2.posY);
        }

    } else if (angulo == 1) {
        arrowLine.setAttribute("x2", (bloco2.width / 2) + bloco2.posX);
        arrowLine.setAttribute("y2", bloco2.posY - 5);

        if (bloco2.shape == "losang") {
            arrowLine.setAttribute("x2", (bloco2.width / 2) + bloco2.posX);
            arrowLine.setAttribute("y2", bloco2.posY - (bloco2.height * 0.21));
        }
    }
}

//calcula o ângulo entre o bloco 1 e o bloco 2 e retorna em radianos
function calcularAngulo(bloco1, bloco2) {
    var centro1 = { x: bloco1.posX + bloco1.width / 2, y: bloco1.posY + bloco1.height / 2 };
    var centro2 = { x: bloco2.posX + bloco2.width / 2, y: bloco2.posY + bloco2.height / 2 };
    var vetorX = centro2.x - centro1.x;
    var vetorY = centro2.y - centro1.y;
    var anguloRad = Math.atan2(vetorY, vetorX);
    return anguloRad;
}

//Verifica o ângulo em radiano, converte para graus e retorna 4 valores correspondentes aos 4 lados de um bloco
function verificarAngulo(anguloRad, ponto) {
    var anguloGraus = anguloRad * (180 / Math.PI);
    var anguloNormalizado = (anguloGraus + 360) % 360;
    if (ponto == null) {
        if (anguloNormalizado >= 31 && anguloNormalizado <= 150) {
            return 1;
        } else if (anguloNormalizado >= 151 && anguloNormalizado <= 210) {
            return 2;
        } else if (anguloNormalizado >= 211 && anguloNormalizado <= 330) {
            return 3;
        } else {
            return 4;
        }
    }
    else{
        return ponto;
    }
}
