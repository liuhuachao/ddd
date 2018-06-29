// JavaScript Document

/* 
    author:liuhuachao    
    description:微信分享js-sdk
    updatedate:2018.06.27
    
    http://res.wx.qq.com/open/js/jweixin-1.2.0.js
*/


var appId;
var timestamp;
var nonceStr;
var signature;
var ShareURL = window.location.href;
var ShareImg = "http://m.chsgw.com/Public/Images/ShareIMG.png";
var ShareTitle = "";
if ($("head title").size() > 0) {
    ShareTitle = $(document).attr("title");
}
var ShareDesc = "尊贵赛鸽-有态度、有温度、有深度";
if ($("head meta[name='description']").size() > 0) {
    var Description = $("head meta[name='description']").attr("content");
    if (Description != "") {
        ShareDesc = Description;
    }
}

$(document).ready(function () {
    if (is_weixn()) {
        $.ajax({
            url: "/v1//weixin/jssdkshare",
            type: "GET",
            dataType: "json",            
            data: {
                ShareURL: ShareURL
            },
            timeout: 5000,
            success: function (result) {
                if (result.msg = "success") {
                    appId = result.data.appId;
                    timestamp = result.data.timestamp;
                    nonceStr = result.data.nonceStr;
                    signature = result.data.signature;
                    jweixinShare();
                }
            },
            error: function () {
                console.log("错误");
            },
            complete: function () {
                console.log('结束')
            }
        });
    }
});

function is_weixn() {
    var ua = navigator.userAgent.toLowerCase();
    if (ua.match(/MicroMessenger/i) == "micromessenger") {
        return true;
    }
    else {
        return false;
    }
}

function jweixinShare() {
    wx.checkJsApi({
        jsApiList: ["checkJsApi", "onMenuShareTimeline", "onMenuShareAppMessage", "onMenuShareQQ", "onMenuShareQZone"],
        success: function (res) {
        }
    });

    wx.config({
        debug: false,
        appId: appId,
        timestamp: timestamp,
        nonceStr: nonceStr,
        signature: signature,
        jsApiList: ["checkJsApi", "onMenuShareTimeline", "onMenuShareAppMessage", "onMenuShareQQ", "onMenuShareQZone"]
    });

    wx.ready(function () {
        // config信息验证后会执行ready方法，所有接口调用都必须在config接口获得结果之后，config是一个客户端的异步操作，所以如果需要在页面加载时就调用相关接口，则须把相关接口放在ready函数中调用来确保正确执行。对于用户触发时才调用的接口，则可以直接调用，不需要放在ready函数中。

        // 朋友圈
        wx.onMenuShareTimeline({
            title: ShareTitle,
            link: ShareURL,
            imgUrl: ShareImg,
            success: function (res) {
            },
            cancel: function (res) {
            }
        });

        // 微信好友
        wx.onMenuShareAppMessage({
            title: ShareTitle,
            desc: ShareDesc,
            link: ShareURL,
            imgUrl: ShareImg,
            type: 'link',
            dataUrl: '',
            success: function () {
            },
            cancel: function () {
            }
        });

        // QQ 好友
        wx.onMenuShareQQ({
            title: ShareTitle,
            desc: ShareDesc,
            link: ShareURL,
            imgUrl: ShareImg,
            success: function () {
            },
            cancel: function () {
            }
        });

        // QQ 空间
        wx.onMenuShareQZone({
            title: ShareTitle,
            desc: ShareDesc,
            link: ShareURL,
            imgUrl: ShareImg,
            success: function () {
            },
            cancel: function () {
            }
        });
    });

    wx.error(function (res) {
        // config信息验证失败会执行error函数，如签名过期导致验证失败，具体错误信息可以打开config的debug模式查看，也可以在返回的res参数中查看，对于SPA可以在这里更新签名。
    });
};