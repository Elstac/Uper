function customLine(x1,x2,y1,y2) {
    this.x1=x1;
    this.x2=x2;
    this.y1=y1;
    this.y2=y2;
}

var changeTicks = 0;
var changed = false;
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
        changed = true;
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
    mode = 'move';
    if (imgScale < 0.5)
        return;

    imgScale /= 2;
    invScale = Math.pow(imgScale, -1);
    clampImgPosition(2);
}

function zoomOut() {
    mode = 'move';
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
    //var c = document.getElementById('defaultCanvas0');
    //var dataURL = c.toDataURL('image/png');
    //dataURL = dataURL.replace('data:image/png;base64,', '');

    //var form = document.getElementById("mapInput");
    //form.value = dataURL;

    var json = JSON.stringify(arr);
    var form = document.getElementById("mapInput");

    form.value = json;
}

function loadCnv(json) {
    jsron = json.split('&quot;').join('"');
    arr = JSON.parse(jsron);
}

function draw() {
    background(51);
    image(bgimg, 0, 0, canvasHeight, canvasWidth, imgX, imgY, bgimg.width*imgScale, bgimg.height*imgScale);

    arr.forEach(drawLine);

    lastX = mouseX;
    lastY = mouseY;
}

function setup() {
    cnv = createCanvas(canvasWidth, canvasHeight);
    cnv.parent('mapHolder');
    bgimg = loadImage('../images/map.jpg');
}