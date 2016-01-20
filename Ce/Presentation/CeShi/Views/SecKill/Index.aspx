<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="CeShi.Views.SecKill.Index" %>

<!doctype html>
<html>
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no" />
    <title><%=ProjName %></title>
    <link href="http://js.soufunimg.com/wireless_m/touch/css/css.css?version=1.177" rel="stylesheet" type="text/css">
    <link href="http://img1.soufun.com/message/images/spike/css/qscss.css" type="text/css" rel="stylesheet">
    <script src="http://img1.soufun.com/message/images/spike/js/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://img1.soufun.com/message/images/spike/js/checkUser.js?v=20131116"></script>

    <script type="text/javascript">
        var g = function (id) {
            return document.getElementById(id);
        }

        function spike() {
            g('frm_type').value = 'spike';
            g('frm_1').submit();
        }

        function _getcheckcode() {
            var phone = $('txt_phone').value;

            if (phone == '') {
                show_div_errorMsg1('手机号不能为空');
                return false
            }

            if (phone.length != 11 || phone.substring(0, 1) != "1" || !phone.match(/^[0-9]+$/)) {
                show_div_errorMsg1('手机号输入错误，请重新输入！');
                return false
            }

            $('frm_type').value = 'getcheckcode';
            frm_1.submit();

        }


        function show_div_errorMsg1(v) {
            $('div_errorMsg1').innerHTML = v;
            $('div_errorMsg1').style.display = '';
            $('div_errorMsg1').focus();
        }


        function showPop(id, t, tx) {
            var oPop = document.getElementById(id);
            var oShadow = document.getElementById('shadow');          
            var idfailspan = document.getElementById('idfailspan');         
            if (idfailspan) {
                idfailspan.innerHTML = tx;
            }

            oPop.style.display = 'block';
            oShadow.style.display = 'block';
        }

        function showPops(id, tx) {
            var oPop = document.getElementById(id);
            var oShadow = document.getElementById('shadow');
            var idsuspan = document.getElementById('idsuspan');
            if (idsuspan) {
                idsuspan.innerHTML = tx;
            }
            oPop.style.display = 'block';
            oShadow.style.display = 'block';
        }

        function startmarquee(lh, speed, delay, index) {
            var t;
            var p = false;
            var o = g("upUser" + index);
            o.innerHTML += o.innerHTML;
            if (o.scrollTop <= lh * 2) {
                o.innerHTML += o.innerHTML;
                o.innerHTML += o.innerHTML;
            }
            o.onmouseover = function () { p = true }
            o.onmouseout = function () { p = false }
            o.scrollTop = 0;
            function start() {
                t = setInterval(scrolling, speed);
                if (!p) o.scrollTop += 2;
            }
            function scrolling() {
                if (o.scrollTop % lh != 0) {
                    o.scrollTop += 2;
                    if (o.scrollTop >= o.scrollHeight / 2)
                        o.scrollTop = 0;
                }
                else {
                    clearInterval(t);
                    setTimeout(start, delay);
                }
            }
            setTimeout(start, delay);
        }
        //时间校正
        var vTime = 0;
        var timer;
        function serverTime(id, objid) {
            jQuery.ajax({
                type: "get",               
                url: "../SecKill/ReturnNowTime?id=" + id,             
                success: function (result) {                  
                    if (result == "-1") {
                        g(objid).innerHTML = ""; return;
                    }
                    if (result == "0") {
                        g(objid).innerHTML = "<span>0<em></em></span><em class='maohao'></em><span>0<em></em></span><em class='maohao'></em><span>0<em></em></span>"; window.location.reload();
                    }
                    var showMsg;
                    var times = result.split(",");
                    if (times[1] == "1")
                        showMsg = "";
                    countDown2(times[0], objid, showMsg)
                }
            });
        }

        function countDown2(endTime, objid, showMsg) {
            function showTime() {
                if (endTime > 0) {
                    var day = Math.floor(endTime / (60 * 60 * 24));
                    var hour = Math.floor(endTime / (3600)) - (day * 24);
                    var minute = Math.floor(endTime / (60)) - (day * 24 * 60) - (hour * 60);
                    var second = Math.floor(endTime) - (day * 24 * 60 * 60) - (hour * 60 * 60) - (minute * 60);
                    hour = 24 * day + hour;
                    endTime = endTime - 1;
                    hour += "";
                    var a = "";
                    var mTime = "";
                    if (hour.length == 1) hour = "0" + hour;
                    if (hour != null && hour.length > 0) {
                        for (var i = 0; i < hour.length; i++) {
                            mTime += "<span>" + hour[i] + "<em></em></span>";
                        }
                    }
                    mTime += "<em class='maohao'></em>"
                    minute += "";
                    if (minute.length == 1) minute = "0" + minute;
                    if (minute != null && minute.length > 0) {
                        for (var j = 0; j < minute.length; j++) {
                            mTime += "<span>" + minute[j] + "<em></em></span>";
                        }
                    }
                    mTime += "<em class='maohao'></em>"
                    second += "";
                    if (second.length == 1) second = "0" + second;
                    if (second != null && second.length > 0) {
                        for (var k = 0; k < second.length; k++) {
                            mTime += "<span>" + second[k] + "<em></em></span>";
                        }
                    }
                    g(objid).innerHTML = mTime;
                    vTime++;
                    if (vTime == 5) {
                        vTime = 0;
                        clearInterval(timer);
                        //校正时间
                        serverTime('<%=RunningId %>', 'timebox');
                    }
                }
                else {
                    g(objid).innerHTML = "";                   
                }
            }
            timer = setInterval(function () { showTime(); }, 1000);
        }
    </script>

</head>
<body>
    <!--sf topnav begin -->
    <div id="head" style="background: #517FBD;">
        <div style="height: 50px; border-bottom: #4371AF solid 1px;">
            <a id="wapxfsy_B01_02" class="flol" href="http://m.fang.com/bj.html" style="width: 70px; height: 30px; background: url(http://js.soufunimg.com/wireless_m/touch/img/ico-head.png) left bottom no-repeat; background-size: 70px 243px; display: block; margin: 10px 0px 0px 8px; z-index: 10; position: relative;" title="北京房产网"></a>

            <a id="wapxfxqy_B01_03" class="daohang awhite" href="javascript:void(0)" onclick="makeUpOrdown('more')" style="position: relative; z-index: 10;">导航</a>

            <a id="wapxfsy_B01_04" class="appd awhite" href="http://m.fang.com/clientindex.jsp?city=bj" style="">客户端</a>

            <div class="clear"></div>
        </div>
        <style>
            .nnav a {
                color: #fff!important;
            }
        </style>
        <div id="more" style="display: none;" class="nnav">
            <div style="border-top: #5F8EC6 solid 1px; border-bottom: #4371AF solid 1px;">
                <a id="wapxfxqy_B01_04" href="http://m.fang.com/xf/bj.html" style="width: 13%" title="北京楼盘">新房</a>
                <a id="wapxfxqy_B01_05" href="http://m.fang.com/esf/bj.html" style="width: 17%">二手房</a>
                <a id="wapxfxqy_B01_06" href="http://m.fang.com/zf/bj/" style="width: 13%">租房</a>
                <a id="wapxfxqy_B01_07" href="http://m.fang.com/jiaju/bj.html" style="width: 13%">家居</a>
                <a id="wapxfxqy_B01_08" href="http://m.fang.com/zixun/bj.html#tt" style="width: 12%;" title="北京房产新闻">资讯</a>
                <a id="wapxfxqy_B01_09" href="http://m.fang.com/ask/bj.html" style="width: 13%;" title="房产问答">问答<span style="margin-left: 8px"></span></a>
                <div class="clear"></div>
            </div>
            <div style="border-top: #5F8EC6 solid 1px; border-bottom: #4371AF solid 1px;">

                <a id="wapxfxqy_B01_10" href="http://m.fang.com/bbs/bj.html" style="width: 13%" title="北京房产论坛">论坛</a>

                <a id="wapxfxqy_B01_11" href="http://m.fang.com/kanfangtuan/bj.htm" style="width: 17%" title="北京看房团">看房团</a>

                <a id="wapxfxqy_B01_12" href="http://m.fang.com/tuangou/bj.htm" style="width: 13%" title="北京房产团购">团购</a>

                <a id="wapxfxqy_B01_13" href="http://m.fang.com/esf_sp/bj/" style="width: 13%">商铺</a>

                <a id="wapxfxqy_B01_14" href="http://m.fang.com/esf_bs/bj/" style="width: 12%;" title="北京别墅">别墅</a>

                <a id="wapxfxqy_B01_15" href="http://m.fang.com/pinggu/bj.html" style="width: 13%">评估</a>

                <div class="clear"></div>
            </div>
            <div style="border-top: #5F8EC6 solid 1px; border-bottom: #4371AF solid 1px;">
                <a id="wapxfxqy_B01_16" href="http://m.youtx.com/" style="width: 13%">短租</a>
                <a id="wapxfxqy_B01_17" href="http://m.fang.com/zf/?c=zf&amp;a=index&amp;city=bj&amp;purpose=%D0%B4%D7%D6%C2%A5" style="width: 17%">写字楼</a>
                <a id="wapxfxqy_B01_18" href="http://m.fang.com/tools/" style="width: 29%;">计算器</a>
                <a id="wapxfxqy_B01_19" href="http://m.fang.com/user.d?m=chat&amp;city=bj" style="width: 12%">消息<span id="msgnum1" class="sms-num none"></span></a>
                <a id="wapxfxqy_B01_20" href="javascript:void(0)" style="width: 13%;" onclick="makeUpOrdown('up')">收起<img src="http://js.soufunimg.com/wireless_m/touch/img/arr-more2.png" width="7" style="margin-left: 2px; margin-top: -3px"></a>
                <div class="clear"></div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        var a = '1';
        function makeUpOrdown(key) {
            if (key == 'up') {
                document.getElementById("more").style.display = "none";
                if (document.getElementById("msgnum0").innerHTML != '') {
                    document.getElementById("msgnum0").style.display = "block";
                }
                a = '1';
            } else if (key == 'more') {
                if (a == '1') {
                    document.getElementById("more").style.display = "block";
                    document.getElementById("msgnum0").style.display = "none";
                    a = '2';
                } else if (a == '2') {
                    document.getElementById("more").style.display = "none";
                    if (document.getElementById("msgnum0").innerHTML != '') {
                        document.getElementById("msgnum0").style.display = "block";
                    }
                    a = '1';
                }
            }
        }

        var mV1 = 'xfinfo';
        var idV1 = '1010736501';
        var cityV1 = 'bj';
        var storage_typeV1 = '';
        if (mV1 == 'xfinfo') {
            storage_typeV1 = 'xf_favorite';
        } else if (mV1 == 'esfdetail') {
            storage_typeV1 = 'esf_favorite';
        } else if (mV1 == 'zfdetail') {
            storage_typeV1 = 'zf_favorite';
        } else if (mV1 == 'xiaoquinfo') {
            storage_typeV1 = 'xiaoqu_favorite';
        }
        var shoucangn = "url(http://js.soufunimg.com/wireless_m/touch/img/shoucang_n.png) ";
        var shoucangh = "url(http://js.soufunimg.com/wireless_m/touch/img/shoucang_h.png) ";

        if (storage_typeV1 != '') {
            var all_favoriteV1 = localStorage.getItem(storage_typeV1) == null ? "" : localStorage.getItem(storage_typeV1);
            var favorite_list = all_favoriteV1.split("|");
            for (var i = 0; i < (favorite_list.length) ; i++) {
                var his_id = getparamV1(favorite_list[i], 'id');
                if (his_id == idV1) {
                    document.getElementById("favoritev1").innerHTML = '取消';
                    document.getElementById("favoritev1").style.backgroundImage = shoucangh;
                }
            }
        }

        function getById(eleid) {
            return document.getElementById(eleid);
        }

        function oparation() {

            var size = 50;
            var item = '';
            var all_favoriteV1 = localStorage.getItem(storage_typeV1) == null ? "" : localStorage.getItem(storage_typeV1);
            if (document.getElementById("favoritev1").innerHTML == '收藏') {
                if (mV1 == 'xfinfo') {
                    item += "city~" + cityV1 + ";";
                    item += "id~" + idV1 + ";";
                    item += "img~" + getById('storageimg').innerHTML + ";";
                    item += "title~" + (getById("title").innerHTML == null ? "" : getById("title").innerHTML) + ";";
                    item += "price~" + (getById("price").innerHTML == null ? "" : getById("price").innerHTML) + ";";
                    item += "district~" + (getById("district").innerHTML == null ? "" : getById("district").innerHTML) + ";";
                    item += "discount~" + (getById("divdiscount").innerHTML == null == null ? "" : getById("divdiscount").innerHTML) + "";

                    if (all_favoriteV1 == '') {
                        localStorage.setItem(storage_typeV1, item);
                        show_msg();
                        document.getElementById("favoritev1").innerHTML = '取消';
                        document.getElementById("favoritev1").style.backgroundImage = shoucangh;
                    } else {
                        var favorite_list = all_favoriteV1.split("|");
                        if (favorite_list.length >= size) {
                            alert('您收藏的新房已达到上限，系统已自动覆盖之前的房源信息');
                        }
                        var favorite = "";
                        for (var i = 0; i < (favorite_list.length >= size ? size : favorite_list.length) ; i++) {
                            var his_id = getparamV1(favorite_list[i], 'id');
                            if (his_id == idV1) {
                            } else {
                                favorite += favorite_list[i] + '|';
                            }
                        }
                        localStorage.setItem('xf_favorite', item + (favorite == '' ? '' : '|') + (favorite == '' ? "" : favorite.substring(0, (favorite.length - 1))));
                        show_msg();
                        document.getElementById("favoritev1").innerHTML = '取消';
                        document.getElementById("favoritev1").style.backgroundImage = shoucangh;
                    }

                }
            } else if (document.getElementById("favoritev1").innerHTML == '取消') {
                var favorite_listV1 = all_favoriteV1.split("|");
                var favorite = "";
                for (var i = 0; i < (favorite_listV1.length) ; i++) {
                    var his_id = getparamV1(favorite_listV1[i], 'id');
                    if (his_id == idV1) {

                    } else {
                        favorite += favorite_listV1[i] + '|';
                    }
                }
                localStorage.setItem(storage_typeV1, favorite == "" ? "" : favorite.substring(0, (favorite.length - 1)));
                show_hmsg();
                document.getElementById("favoritev1").innerHTML = '收藏';
                document.getElementById("favoritev1").style.backgroundImage = shoucangn;
            }

        }
        function getparamV1(str, name) {
            var paraString = str.split(";");
            var paraObj = {};
            for (i = 0; j = paraString[i]; i++) {
                paraObj[j.substring(0, j.indexOf("~")).toLowerCase()] = j.substring(j.indexOf("~") + 1, j.length);
            }
            return paraObj[name];
        }
    </script>
    <!--sf topnav end -->

    <%if (IsRunning)
      {%>
    <!--进行中头部   start-->
    <div class="qsheader">
        <h1 style="width: 210px; height: 50px; background: url(<%=RunningheadImgUrl %>) no-repeat 0 0; background-size: 210px 50px; margin: 18px auto 0; text-indent: -9999em; overflow: hidden;">抢市</h1>
        <h2>秒杀赢大奖</h2>
        <p class="time_tip">离下一个整点抢开始还有</p>
        <div class="timebox" id="timebox">
        </div>
        <div class="cloud"></div>
    </div>
    <!--进行中头部   end-->
    <%} %>


    <div class="qsbody">


        <!--刚结束 没超过10分钟   start-->
        <asp:Repeater ID="RJust" runat="server">
            <ItemTemplate>
                <div class="clear15"></div>
                <div class="qsbox pr">
                    <div class="over_tit pr"><%# DateTime.Parse(Eval("spikeTime").ToString()).ToString("HH:mm")%>场 <%= !string.IsNullOrEmpty(ProjName)?ProjName.Trim().Length>12 ?ProjName.Trim().Substring(0,12):ProjName.Trim():"" %><span class="over_tit_end">已结束</span><a class="js_btn"><img class="tb_jt" src="http://img1.soufun.com/message/images/spike/images/bjt.png" width="10" height="6"></a></div>
                    <div class="qscon pr" style="display: block;">
                        <img class="ico_img" src="http://img1.soufun.com/message/images/spike/images/ico_over.png" width="37" height="37" style="top: 10px; left: 0;">
                        <%#getImage(Eval("picUrl").ToString(), Eval("picLink").ToString())%>
                        <ul class="qs_ul">
                            <li>奖品：<span class="c_f90"><%# Eval("prize")%></span></li>
                            <li>秒杀时间：<%# DateTime.Parse(Eval("spikeTime").ToString()).ToString("yyyy-MM-dd HH:mm:ss")%></li>
                            <li>中奖名次：第<%#DealSeatNum(Eval("selNum").ToString().Trim())%>位</li>
                            <%#GetIscheckUser(Eval("Id").ToString())%>
                            <%-- <li><strong class="fl">中奖结果：</strong><div class="fl"><p><span class="c_f90">186</span>搜房网友<span class="c_f90">8888</span>成为幸运用户</p><p><span class="c_f90">186</span>搜房网友<span class="c_f90">8888</span>成为幸运用户</p><p><span class="c_f90">186</span>搜房网友<span class="c_f90">8888</span>成为幸运用户</p><p><span class="c_f90">186</span>搜房网友<span class="c_f90">8888</span>成为幸运用户</p></div></li>--%>
                        </ul>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>

        <!--刚结束 没超过10分钟   end-->

        <%if (IsRunning)
          {%>
        <!--进行中主体   start-->

        <span id="buttonspan">
            <%if (IsUserlogin)
              {%>
            <input id="bm_btn" class="qs_btn rbox17 linear_org" type="button" onclick="spike()" value="秒杀">
            <%}
              else
              {%>
            <input id="Button1" class="qs_btn rbox17 linear_org" type="button" onclick="showPop('bmpop')" value="报名">
            <%} %>
        </span>

        <div class="clear15"></div>
        <div class="qsbox pr">
            <img class="ico_img" src="http://img1.soufun.com/message/images/spike/images/ico_ing.png" width="37" height="37">
            <%= getImage(RunningpicUrl, RunningpicLink)%>
            <ul class="qs_ul">
                <li>奖品：<span class="c_f90"><%=Runningprize %></span></li>
                <li>秒杀时间：<%=DateTime.Parse(RunningspikeTime).ToString("yyyy-MM-dd HH:mm:ss")%></li>
                <li>中奖名次：第<%=DealSeatNum(RunningselNum)%>位</li>
            </ul>
        </div>
        <!--进行中主体   end-->
        <%} %>



        <!--下一场   start-->
        <asp:Repeater ID="RNext" runat="server">
            <ItemTemplate>
                <div class="clear15"></div>
                <div class="qsbox pr">
                    <img class="ico_img" src="http://img1.soufun.com/message/images/spike/images/ico_next.png" width="37" height="37">
                    <%#getImage(Eval("picUrl").ToString(), Eval("picLink").ToString())%>
                    <ul class="qs_ul">
                        <li>奖品：<span class="c_f90"><%# Eval("prize")%></span></li>
                        <li>秒杀时间：<%# DateTime.Parse(Eval("spikeTime").ToString()).ToString("yyyy-MM-dd HH:mm:ss")%></li>
                        <li>中奖名次：第<%#DealSeatNum(Eval("selNum").ToString().Trim())%>位</li>
                    </ul>
                </div>
            </ItemTemplate>
        </asp:Repeater>
        <!--下一场   end-->


        <!--已经结束   start-->
        <asp:Repeater ID="REndL" runat="server">
            <ItemTemplate>
                <div class="clear15"></div>
                <div class="qsbox pr">
                    <div class="over_tit pr"><%# DateTime.Parse(Eval("spikeTime").ToString()).ToString("HH:mm")%>场 <%= !string.IsNullOrEmpty(ProjName)?ProjName.Trim().Length>12 ?ProjName.Trim().Substring(0,12):ProjName.Trim():"" %><span class="over_tit_end">已结束</span><img class="tb_jt" src="http://img1.soufun.com/message/images/spike/images/bjt.png" width="10" height="6" style="cursor: pointer;" onclick="displayo('end_<%# Eval("Id")%>')"></div>
                    <div class="qscon pr" id='end_<%# Eval("Id")%>' style="display: none;">
                        <img class="ico_img" src="http://img1.soufun.com/message/images/spike/images/ico_over.png" width="37" height="37" style="top: 10px; left: 0;">
                        <%#getImage(Eval("picUrl").ToString(), Eval("picLink").ToString())%>
                        <ul class="qs_ul">
                            <li>奖品：<span class="c_f90"><%# Eval("prize")%></span></li>
                            <li>秒杀时间：<%# DateTime.Parse(Eval("spikeTime").ToString()).ToString("yyyy-MM-dd HH:mm:ss")%></li>
                            <li>中奖名次：第<%#DealSeatNum(Eval("selNum").ToString().Trim())%>位</li>
                            <%#GetIscheckUser(Eval("Id").ToString())%>
                        </ul>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
        <!--已经结束   end-->


    </div>



    <!--恭喜弹出框 begin-->
    <div class="pop_box rbox4" style="display: none; z-index: 12000;" id="popSuc">
        <p class="f22 mb10">
            恭喜!
        </p>
        <p class="f14">
            <%--您成为整点抢幸运用户！<br>--%>
            <span id="idsuspan">搜房小编会及时联系您领取奖品</span>
        </p>
        <div class="right">
        </div>
        <input class="linear_org rbox4 goq_btn" type="button" style="margin: 0 auto;" onclick="document.getElementById('popSuc').style.display = 'none'; document.getElementById('shadow').style.display = 'none'; "
            value="确定">
    </div>
    <!--恭喜弹出框 end-->

    <!--报歉弹出框 begin-->
    <div class="pop_box rbox4" style="display: none; z-index: 12000;" id="popFal">
        <p class="f22 mb10">
            很遗憾
        </p>
        <p class="f14" id="qErr">
            <%--本次秒杀时间为<span id="idfaitime"></span><br>--%>
            <span id="idfailspan">差一点就中了哦！</span>
        </p>
        <div class="err">
        </div>
        <div class="pop_btn_box">
            <input class="linear_org rbox4 goq_btn fl" type="button" value="继续抢" onclick="document.getElementById('popFal').style.display = 'none'; document.getElementById('shadow').style.display = 'none'; spike()" /><input class="linear_gry rbox4 noq_btn fl"
                type="button" value="取消" onclick="document.getElementById('popFal').style.display = 'none'; document.getElementById('shadow').style.display = 'none'" />
        </div>
    </div>
    <!--报歉弹出框 end-->




    <div class="shadow" id="shadow"></div>
    <div class="poplayer" id="bmpop">
        <div class="layer_con">
            <div class="layer_box rbox4">
                <form id="frm_1" name="frm_1" method="post" target="frm_2">
                    <input type="hidden" id="frm_type" name="frm_type" />
                    <input type="hidden" id="hprojid" name="hprojid" value="<%=ProjId %>" />
                    <input type="hidden" id="hactivityId" name="hactivityId" value="<%=RunningId %>" />
                    <p class="layer_tit">
                        请填写手机号报名
                    </p>
                    <div class="inputbox rbox4">
                        <label>
                            手机号:</label><input class="layer_text" name="txt_phone" id="txt_phone" type="text" />
                        <input type="hidden" name="hidPhone" id="hidPhone" value="<%=hidPhone %>" />
                    </div>


                    <%if (IsCheckCode)
                      {%>
                    <div style="width: 247px; margin: 11px auto 20px; overflow: hidden;">
                        <div class="inputbox rbox4 fl" style="width: 143px;">
                            <label>
                                验证码:</label><input class="layer_text" id="txt_code" name="txt_code" style="width: 76px;"
                                    type="text">
                        </div>

                        <input class="pop_btn fl linear_gry rbox4" style="margin-left: 0px; margin-top: 10px;" type="button" onclick="_getcheckcode()" value="获取验证码">
                    </div>
                    <%} %>



                    <p id="div_errorMsg1" class="f12" style="width: 247px; margin: 11px auto 20px; overflow: hidden; display: none; color: Red;">
                    </p>
                    <input class="pop_btn_bm linear_blue rbox4" style="margin: 11px auto 20px;" type="button" onclick="_checkUser()" value="报 名" />
                    <a href="#">
                        <div class="layer_close" id="close" onclick="document.getElementById('bmpop').style.display='none';document.getElementById('shadow').style.display='none'">
                        </div>
                    </a>
                </form>
                <iframe id="frm_2" name="frm_2" width="0" height="0" src=""></iframe>
            </div>
        </div>
    </div>


    <!--报歉弹出框1 begin-->
    <div class="pop_box rbox4" style="display: none; height: 130px; z-index: 10000; height: 220px;" id="baseErr">
        <p class="f22 mb10" id="div_errorMsg2">抱歉!</p>
        <p class="f14">
            <br>
        </p>
        <div class="err" style="cursor: pointer" onclick="document.getElementById('baseErr').style.display='none';document.getElementById('shadow').style.display='none'"></div>
        <div class="pop_btn_box" style="margin-left: 85px;">
            <input class="linear_gry rbox4 noq_btn fl" style="cursor: pointer" onclick="document.getElementById('baseErr').style.display = 'none'; document.getElementById('shadow').style.display = 'none'" type="button" value="取消">
        </div>
    </div>
    <!--报歉弹出框1 end-->

    <script>
        //var oBtn = document.getElementById('bm_btn');
        var oClose = document.getElementById('close');
        var oShadow = document.getElementById('shadow');
        var oPop = document.getElementById('bmpop');

        //    oBtn.onclick = function() {
        //        oShadow.style.display = 'block';
        //        oPop.style.display = 'block';
        //    }
        oClose.onclick = function () {
            oShadow.style.display = 'none';
            oPop.style.display = 'none';
        }

        function autoClose() {
            function close() {
                document.getElementById('baseErr').style.display = 'none';
                document.getElementById('shadow').style.display = 'none';
                document.getElementById('popSuc').style.display = 'none';

            }

            setTimeout(function () { close(); }, 3000);

        }

        function displayo(v) {
            var o = document.getElementById(v);
            if (o) {
                if (o.style.display == 'none') {
                    o.style.display = 'block'
                } else {
                    o.style.display = 'none'
                }
            }
        }

        function myrefresh() {
            window.location.reload();
        }


 <%if (IsRunning)
   {%>
        serverTime('<%=RunningId %>', 'timebox');
        <%} %>
 
 <%if (IsJustEnd)
   {%>
        setInterval('myrefresh()', 10000);
        <%} %>

    
    </script>

    <script type="text/javascript">
        var _dctc = _dctc || {};
        _dctc._account = _dctc._account || ['UA-24140496-1', 'UA-24140496-20'];
        _dctc.isNorth = _dctc.isNorth || 'Y';
        _dctc.bid = '1';
        (function () {
            var script = document.createElement('script'); script.type = 'text/javascript'; script.async = true;
            script.src = 'http://js.soufunimg.com/count/load.min.js';
            var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(script, s);
        })();
    </script>
    <%=errorMsg%>
</body>
</html>
