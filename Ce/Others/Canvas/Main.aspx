<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>拖动</title>
    <link href="/Css/jquery.range.css" rel="stylesheet" />   
    <style type="text/css">
        * {
            margin: 0px;
            padding: 0px;
        }
        .MenuContain {
        padding-bottom:15px;       
        }
         .MenuContain input {
        margin:5px;
        
        }
        #TimerRange {
        margin-top:10px;
        margin-left:10px;
        
        }

        .container {
            /* 自定义开始 */
            margin: 0px auto;
            /* 自定义结束 */
            border:1px solid #787272;
            position: relative;
            overflow: hidden;
             background-image: url(/Imgs/bj.jpg);
            background-size: 100% 100%;
            background-repeat: no-repeat;
           
            width: 800px;
            height: 400px;
        }

        .elem {
            position: absolute;
        }

        #oImg {
            position: absolute;
            /*left: 50px;
	top: 50px;*/
            /*z-index: 1;*/
        }
        .prop {
        
        
        }
            .prop input,select {
            margin-top:5px;
            width:90px;
            height:30px;
            
            }
    </style>
    <script type="text/javascript" src="/JS/jquery-1.8.3.min.js"></script>
    <script type="text/javascript" src="JS/jquery.range.js"></script>
    <script type="text/javascript">
        /*绑定事件*/
        function addEvent(obj, sType, fn) {
            if (obj.addEventListener) {
                obj.addEventListener(sType, fn, false);
            } else {
                obj.attachEvent('on' + sType, fn);
            }
        };
        function removeEvent(obj, sType, fn) {
            if (obj.removeEventListener) {
                obj.removeEventListener(sType, fn, false);
            } else {
                obj.detachEvent('on' + sType, fn);
            }
        };
        function prEvent(ev) {
            var oEvent = ev || window.event;
            if (oEvent.preventDefault) {
                oEvent.preventDefault();
            }
            return oEvent;
        }
        /*添加滑轮事件*/
        function addWheelEvent(obj, callback) {
            if (window.navigator.userAgent.toLowerCase().indexOf('firefox') != -1) {
                addEvent(obj, 'DOMMouseScroll', wheel);
            } else {
                addEvent(obj, 'mousewheel', wheel);
            }
            function wheel(ev) {
                var oEvent = prEvent(ev),
                delta = oEvent.detail ? oEvent.detail > 0 : oEvent.wheelDelta < 0;
                callback && callback.call(oEvent, delta);
                return false;
            }
        };
        /*页面载入后*/
        window.onload = function () {
            var oImg = document.getElementById('oImg');
            /*拖拽功能*/
            (function () {
                addEvent(oImg, 'mousedown', function (ev) {
                    var oEvent = prEvent(ev),
                    oParent = oImg.parentNode,
                    disX = oEvent.clientX - oImg.offsetLeft,
                    disY = oEvent.clientY - oImg.offsetTop,
                    startMove = function (ev) {
                        if (oParent.setCapture) {
                            oParent.setCapture();
                        }
                        var oEvent = ev || window.event,
                        l = oEvent.clientX - disX,
                        t = oEvent.clientY - disY;                       
                        oImg.style.left = l + 'px';                        
                        oImg.style.top = t + 'px';
                        elemLeft.value = l;
                        elemRight.value = t;
                        oParent.onselectstart = function () {
                            return false;
                        }
                    }, endMove = function (ev) {
                        if (oParent.releaseCapture) {
                            oParent.releaseCapture();
                        }
                        oParent.onselectstart = null;
                        removeEvent(oParent, 'mousemove', startMove);
                        removeEvent(oParent, 'mouseup', endMove);
                    };
                    addEvent(oParent, 'mousemove', startMove);
                    addEvent(oParent, 'mouseup', endMove);
                    return false;
                });
            })();
            /*以鼠标位置为中心的滑轮放大功能*/
            (function () {
                addWheelEvent(oImg, function (delta) {
                    var ratioL = (this.clientX - oImg.offsetLeft) / oImg.offsetWidth,
                    ratioT = (this.clientY - oImg.offsetTop) / oImg.offsetHeight,
                    ratioDelta = !delta ? 1 + 0.1 : 1 - 0.1,
                    w = parseInt(oImg.offsetWidth * ratioDelta),
                    h = parseInt(oImg.offsetHeight * ratioDelta),
                    l = Math.round(this.clientX - (w * ratioL)),
                    t = Math.round(this.clientY - (h * ratioT));
                    with (oImg.style) {
                        width = w + 'px';
                        height = h + 'px';
                        left = l + 'px';
                        top = t + 'px';
                    }
                });
            })();
        };
        $(function () {           
            $('#newElem').change(function () {              
                var selElem = $('#newElem').find("option:selected").val();
                $("#" + selElem).focus();                
            });
            $("#elemLeft").keyup(function () {          
                $("#oImg").css("left", $("#elemLeft").val() + 'px');
            });
            $("#elemRight").keyup(function () {                             
                $("#oImg").css("top", $("#elemRight").val() + 'px');
            });
        });
       
    </script>
</head>
<body>
    <div class="prop">选元素：
       <select id="newElem">
           <option value="oImg" selected="selected">字体</option>
           <option value="oImg2" selected="selected">字体2</option>          
       </select>
       <div> 距离左：<input id="elemLeft" type="text" value="" />px</div>
       <div> 距离右：<input id="elemRight" type="text" value="" />px</div>
    </div>
    <div class="container">
        <div class="elem">
            <img id="oImg" alt="" src="/Imgs/font.png" />
        </div>
    </div>
    <div class="MenuContain">
        <input type="button" id="New" value="新建" />
        <input type="button" id="BgSet" value="背景设置" />
        <input type="file" id="bgupload" style="display:none"  />
        <input type="button" id="Button3" value="添加元素" />
        <input type="button" id="Button4" value="动画选择" />
        <input type="button" id="Button1" value="生成CSS" />
        <input type="button" id="g1" value="设为关键帧" />
    </div>
    <div id="TimerRange">   
		<input type="hidden" class="single-slider" value="23" />		
    </div>
    
    <script type="text/javascript">
        $(function () {
            //新建
            $("#New").click(function () {
                $(".container").empty();
                $(".container").height("300px");
                $(".container").width("500px");
            });
            //背景图片设置
           
            $("#BgSet").click(function () {
                $("#bgupload").click();
                $(".container").css("background-image",$("#bgupload").val());
            });

	$('.single-slider').jRange({
		from: 0,
		to: 100,
		step: 1,
		scale: [0,25,50,75,100],
		format: '%s',
		width: 300,
		showLabels: true,
		showScale: true
	});
	
	$("#g1").click(function(){
		var aa = $(".single-slider").val();
		alert(aa);
	});	
    });
        </script>
</body>
</html>
