function customLine(x1,x2,y1,y2) {
    this.x1=x1;
    this.x2=x2;
    this.y1=y1;
    this.y2=y2;
}

function button(x,y,width,height,color,event) {
    this.x = x;
    this.y = y;
    this.width = width;
    this.height = height;
    this.color = color;
    this.event = event;

    this.isClicked = function () {
        if (mouseX > this.x && mouseX < this.x + this.width
            && mouseY > this.y && mouseY < this.y + this.height) {
            this.event();
        }
    }
}
var uiWidth = 550;
var uiHeight = 500;
var canvasWidth = 500;
var canvasHeight = 500;
var imgX = 0;
var imgY = 0;

var imgScale = 1.0;
var invScale = 1.0;

var UI = new Array();
var cnv;

var mode = 'move';

var bgimg;
var lastX;
var lastY;
var arr = new Array();

function drawLine(value) {
    var width = canvasWidth * imgScale;
    var height = canvasHeight * imgScale;
    if (value.x1 > width || value.x2 > width
        || value.y1 > height || value.y2 > height)
        return;

    line(value.x1 * invScale, value.y1 * invScale, value.x2 * invScale, value.y2 * invScale);
    stroke(0, 0, 255);
    strokeWeight(5);
}

function clamp(val, max, min) {
    return Math.max(Math.min(val, max), min);
}

function clampImgPosition(sc) {
    var lastx = imgX;
    var lasty = imgY;
    imgX = clamp(imgX, bgimg.width * (1 - imgScale), 0);
    imgY = clamp(imgY, bgimg.height * (1 - imgScale), 0);

    arr.forEach((point) => adjustPointPosition(point, (lastx - imgX) * 1.377 * sc, (lasty - imgY) * 1.377 * sc));
}

function mouseDragged() {
    if (mode === 'draw') {
        var avgX = (mouseX + lastX) / 2;
        var avgY = (mouseY + lastY) / 2;
        arr.push(new customLine(mouseX * imgScale, avgX * imgScale, mouseY * imgScale, avgY * imgScale));
        arr.push(new customLine(avgX * imgScale,  lastX * imgScale, avgY * imgScale, lastY * imgScale));
    }
    else if (mode === 'move') {
        moveImg(lastX - mouseX, lastY - mouseY);
    }
}

function moveImg(dirX, dirY) {
    var x = clamp(imgX + (dirX), bgimg.width * (1 - imgScale), 0) - imgX;
    var y = clamp(imgY + (dirY), bgimg.height * (1 - imgScale), 0) - imgY;
    imgX += x * 1.45 * imgScale;
    imgY += y * 1.45 * imgScale;
    arr.forEach((point) => adjustPointPosition(point, -x * imgScale, -y * imgScale));
}

function zoomIn() {
    if (imgScale < 0.5)
        return;

    imgScale /= 2;
    invScale = Math.pow(imgScale, -1);
    clampImgPosition(2);
}

function zoomOut() {
    if (imgScale >0.5)
        return;

    imgScale *= 2
    invScale = Math.pow(imgScale, -1);
    clampImgPosition(0.5);
}

function adjustPointScale(point, scale) {
    point.x1 *= scale;
    point.x2 *= scale;
    point.y1 *= scale;
    point.y2 *= scale;
}

function adjustPointPosition(point, changeX, changeY) {
    point.x1 += changeX;
    point.x2 += changeX;
    point.y1 += changeY;
    point.y2 += changeY;
}

function setDrawMode() {
    mode = 'draw';
}

function setMoveMode() {
    mode = 'move';
}

function saveCnv() {
    //var ctx = document.getElementById("defaultCanvas0").getContext("2d");
    //var imggData = ctx.getImageData(0, 0, 5, 5);
    //var data = { C: imggData };

    //httpPost('https://localhost:44384/home/test', 'json', data,
    //    function (result) {
    //        text('sended', 0, 0);
    //    },
    //    function (result) {
    //        text('error', 0, 0);
    //    });

    var c = document.getElementById('defaultCanvas0');
    var dataURL = c.toDataURL('image/png');
    dataURL = dataURL.replace('data:image/png;base64,', '');

    var data = { C: dataURL };
    httpPost('https://localhost:44384/home/test', 'json', data,
        function (result) {
            text('sended', 0, 0);
        },
        function (result) {
            text('error', 0, 0);
        });
}

function mouseClicked() {
    for (var i = 0; i < UI.length; i++) {
        UI[i].isClicked();
    }
}

function drawUI() {
    for (var i = 0; i < UI.length; i++) {
        fill(UI[i].color.r, UI[i].color.g, UI[i].color.b);
        rect(UI[i].x, UI[i].y, UI[i].width, UI[i].height);
    }
}

function draw() {
    background(51);
    image(bgimg, 0, 0, canvasHeight, canvasWidth, imgX, imgY, bgimg.width*imgScale, bgimg.height*imgScale);
    drawUI();

    arr.forEach(drawLine);

    lastX = mouseX;
    lastY = mouseY;
}

function setup() {
    cnv = createCanvas(uiWidth, uiHeight);
    cnv.parent('cnv-parent');
    bgimg = loadImage('../images/map.jpg');

    UI.push(new button(canvasWidth, 0, 50, 50, { r: 0, g: 255, b: 0 }, zoomIn));
    UI.push(new button(canvasWidth, 50, 50, 50, { r: 255, g: 0, b: 0 }, zoomOut));
    UI.push(new button(canvasWidth, 100, 50, 50, { r: 255, g: 0, b: 255 }, setMoveMode));
    UI.push(new button(canvasWidth, 150, 50, 50, { r: 125, g: 0, b: 255 }, setDrawMode));
    UI.push(new button(canvasWidth, canvasHeight - 50, 50, 50, { r: 0, g: 0, b: 255 }, saveCnv));
}