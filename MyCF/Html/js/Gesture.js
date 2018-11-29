var myGesture;
var myADGesture;
var myRelatedGesture;
var myElement;
var myADElement;
var myRelatedElement;
var gestureStartX;

function gestureInit() {
    gesturePrepareTarget("paragraph", gestureListener);
    gesturePrepareTarget("adcontent", gestureListener);
    gesturePrepareTarget("related", gestureListener);

    myGesture = new MSGesture();
    myElement = document.getElementById("paragraph");
    myGesture.target = myElement;

    myADGesture = new MSGesture();
    myADElement = document.getElementById("adcontent");
    myADGesture.target = myADElement;

    myRelatedGesture = new MSGesture();
    myRelatedElement = document.getElementById("related");
    myRelatedGesture.target = myRelatedElement;
}

function onLoad(u) {
    gestureInit();
}

function gesturePrepareTarget(targetId, eventListener) {
    var target = document.getElementById(targetId);
    target.addEventListener("MSGestureStart", eventListener, false);
    target.addEventListener("MSGestureEnd", eventListener, false);
    target.addEventListener("MSGestureChange", eventListener, false);
    target.addEventListener("MSInertiaStart", eventListener, false);
    target.addEventListener("MSGestureTap", eventListener, false);
    target.addEventListener("MSGestureHold", eventListener, false);
    target.addEventListener("pointerdown", eventListener, false);
}

function getstureReset() {
    myGesture.reset();
    gestureStartX = 0;
}

function gestureListener(evt) {
    if (document.msFullscreenElement) {
        return;
    }
    if (evt.type == "pointerdown") {
        myGesture.addPointer(evt.pointerId);
        gestureStartX = evt.clientX;
    }
    else if (evt.type == "MSGestureStart") {
        gestureStartX = evt.clientX;
    }
    else if (evt.type == "MSGestureChange") {
        if (gestureStartX == "undefined") {
            gestureStartX = evt.clientX;
        }
        var translateX = evt.clientX - gestureStartX
        if (translateX < -60) {
            window.external.notify("gestures:goforward");
            gestureStartX = evt.clientX;
            myGesture.stop();
        }
        else if (translateX > 60) {
            window.external.notify("gestures:goback");
            gestureStartX = evt.clientX;
            myGesture.stop();
        }
    }
    else if (evt.type == "MSGestureEnd") {
        gestureStartX = evt.clientX;
        myGesture.reset();
    }
}
