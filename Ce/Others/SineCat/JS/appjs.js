//var stage = new createjs.Stage("gameView");
//stage.x = 200;
//stage.y = 200;
//var circle = new createjs.Shape();
//circle.graphics.beginFill("#FF0000").drawCircle(0,0,50);
//var text = new createjs.Text("Hello easeljs", "36px Arial", "#777");
//stage.addChild(circle);
//createjs.Tween.get(circle, { loop: true })
//.wait(1000)
//.to({scaleX:0.2,scaleY:0.2})
//.wait(1000)
//.to({ scaleX: 1, scaleY: 1 }, 1000, createjs.Ease.bounceInOut)
//createjs.Ticker.setFPS(20);
//createjs.Ticker.addEventListener("tick",stage);
//stage.update();

//var displayStatus;
//displayStatus = document.getElementById("status");
//var src = "/JS/1.mp3";

//createjs.Sound.alertnateExensions = ["mp3"];
//createjs.Sound.addEventListener("fileload", playSound);
//createjs.Sound.registerSound(src);
//displayStatus.innerHTML = "Waiting for load to complete";
//function playSound(event)
//{
//    soundInstance=createjs.Sound.play(event.src);
//    displayStatus.innerHTML = "Playing Source:" + event.src;
//}


var preload;
preload = new createjs.LoadQueue(false, "/Imgs/");
var plugin = {
    getPreloadHandlers: function (){
        return{
            types:["image"],
            callback:function(src){
                var id=src.toLowerCase().split("/").pop().split(".")[0];
                var img=document.getElementById(id);
                return {tag:img};
            }
        }
    }
}

preload.installPlugin(plugin);
preload.loadManifest([
    "1.png",
    "2.jpg"]);