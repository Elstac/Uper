function customLine(x1,x2,y1,y2) {
    this.x1=x1;
    this.x2=x2;
    this.y1=y1;
    this.y2=y2;
}

var lastX;
var lastY;
var arr = new Array();

function drawLine(value) {
    line(value.x1, value.y1, value.x2, value.y2);
}

function mouseDragged() {
    arr.push(new customLine(mouseX, lastX, mouseY, lastY));
}

function draw() {
    background(51);

    arr.forEach(drawLine);

    lastX = mouseX;
    lastY = mouseY;
}

function setup() {
    createCanvas(400, 400);
}