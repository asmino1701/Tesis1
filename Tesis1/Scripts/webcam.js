//Variables globales
const video = document.getElementById('video');
const canvas = document.getElementById('canvas');
const snap = document.getElementById("snap");
const constraints = {
    audio: true,
    video: {
        width: 1280,
        height: 720
    }
};

var chat = $.connection.chatHub;
var context = canvas.getContext('2d');
var imagenwc;
var json;
var temporizador;

// Acceder a la webcam
async function init() {
    try {
        const stream = await navigator.mediaDevices.getUserMedia(constraints);
        handleSuccess(stream);
    } catch (e) {
        //errorMsgElement.innerHTML = `navigator.getUserMedia error:${e.toString()}`;
        console.log("Ocurrió un error al iniciar la cámara");
    }
}

// Success
function handleSuccess(stream) {
    window.stream = stream;
    video.srcObject = stream;
}

// Load init
init();


//Funcion para capturar la imagen desde el streaming
function CapturarFrame() {
    context.drawImage(video, 0, 0, 640, 480);
    imagenwc = canvas.toDataURL();

    imagenwc = imagenwc.replace(/^data:image\/(png|jpg);base64,/, "");
    json = '{ "imageData" : "' + imagenwc + '" }';
    EnviarImagen(json);
}

//Funcion para enviar la imagen al servidor
function EnviarImagen(datos) {

    // Sending the image data to Server
    $.ajax({
        type: 'POST',
        url: 'Index.aspx/ImagenCls',
        data: datos,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            console.log("Imagen enviada al servidor.");
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log(errorThrown);
        }
    });
}
