function showMap() {
    document.getElementById('mapDiv').style.display = "inline";
    document.getElementById('removeBtn').style.display = "inline";

    document.getElementById('confirmBtn').onclick = function () { saveCnv() };
    document.getElementById('createBtn').style.display = "none";
}

function hideMap() {
    document.getElementById('confirmBtn').onclick = function () { };
    document.getElementById('createBtn').style.display = "inline";

    document.getElementById('mapDiv').style.display = "none";
    document.getElementById('removeBtn').style.display = "none";
}

function saveCnvToPng(formId) {
    var c = document.getElementById('defaultCanvas0');
    var dataURL = c.toDataURL('image/png');
    dataURL = dataURL.replace('data:image/png;base64,', '');

    var form = document.getElementById(formId);
    form.value = dataURL;
}