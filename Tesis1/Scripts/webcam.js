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
var imagen;
// Access webcam
async function init() {
    try {
        const stream = await navigator.mediaDevices.getUserMedia(constraints);
        handleSuccess(stream);
    } catch (e) {
        //errorMsgElement.innerHTML = `navigator.getUserMedia error:${e.toString()}`;
        console.log("Something went wrong!");
    }
}

// Success
function handleSuccess(stream) {
    window.stream = stream;
    video.srcObject = stream;
}

// Load init
init();

// Draw image

snap.addEventListener("click", function () {
    context.drawImage(video, 0, 0, 640, 480);
    imagen = canvas.toDataURL();
    enviarImagen(imagen);
});

//SIGNAL R
function enviarImagen(img) {
    $.connection.hub.start().done(function () {
        chat.server.send($(imagen));
    });
}
