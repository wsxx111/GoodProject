﻿/**
 * Created by user on 2015/12/31.
 */
var stage = new createjs.Stage("gameView");
createjs.Ticker.setFPS(30);
createjs.Ticker.addEventListener("tick", stage);

var gameView = new createjs.Container();
gameView.x = 30;
gameView.y = 30;
stage.addChild(gameView);

var circleArr = [[], [], [], [], [], [], [], [], []];
var currentCat;

function circleClicked(event) {
    if (event.target.getCircleType() != 3) {
        event.target.setCircleType(2);
        if (currentCat.indexX == 0 || currentCat.indexX == 8 || currentCat.indexY == 0 || currentCat.indexY == 8) {
            alert('游戏结束');
        }
        else {            
            var leftCircle = circleArr[currentCat.indexX - 1][currentCat.indexY];
            var rightCircle = circleArr[currentCat.indexX + 1][currentCat.indexY];
            var leftdownCircle = currentCat.indexY % 2 ? circleArr[currentCat.indexX][currentCat.indexY + 1] : circleArr[currentCat.indexX - 1][currentCat.indexY + 1];
            var rightdownCircle = currentCat.indexY % 2 ? circleArr[currentCat.indexX + 1][currentCat.indexY + 1] : circleArr[currentCat.indexX][currentCat.indexY + 1];
            var leftupCircle = currentCat.indexY % 2 ? circleArr[currentCat.indexX][currentCat.indexY - 1] : circleArr[currentCat.indexX - 1][currentCat.indexY - 1];
            var rightupCircle = currentCat.indexY % 2 ? circleArr[currentCat.indexX + 1][currentCat.indexY + 1] : circleArr[currentCat.indexX][currentCat.indexY - 1];

            if (leftCircle.getCircleType() == 1) {
                leftCircle.setCircleType(3);
                currentCat.setCircleType(1);
                currentCat = leftCircle;
            }
            else if (rightCircle.getCircleType() == 1) {
                rightCircle.setCircleType(3);
                currentCat.setCircleType(1);
                currentCat = rightCircle;
            }
            else if (leftdownCircle.getCircleType() == 1) {
                leftdownCircle.setCircleType(3);
                currentCat.setCircleType(1);
                currentCat = leftdownCircle;
            }
            else if (rightdownCircle.getCircleType() == 1) {
                rightdownCircle.setCircleType(3);
                currentCat.setCircleType(1);
                currentCat = rightdownCircle;
            }
            else if (leftupCircle.getCircleType() == 1) {
                leftupCircle.setCircleType(3);
                currentCat.setCircleType(1);
                currentCat = leftupCircle;
            }
            else if (rightupCircle.getCircleType() == 1) {
                rightupCircle.setCircleType(3);
                currentCat.setCircleType(1);
                currentCat = rightupCircle;
            }
            else {
                alert('你赢了');
            }
        }
    }
}
function addCircles() {
    for (var indexY = 0; indexY < 9; indexY++) {
        for (var indexX = 0; indexX < 9; indexX++) {
            var c = new Circle();
            gameView.addChild(c);
            circleArr[indexX][indexY] = c;
            c.indexX = indexX;
            c.indexY = indexY;
            c.x = indexY % 2 ? indexX * 55 + 25 : indexX * 55;
            c.y = indexY * 55;
            if (indexX == 4 && indexY == 4) {
                c.setCircleType(3);
                currentCat = c;
            }
            c.addEventListener("click", circleClicked);
        }
    }
}
addCircles();

