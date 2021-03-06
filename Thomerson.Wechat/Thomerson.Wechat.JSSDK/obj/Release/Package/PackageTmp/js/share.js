(function () {
    wx && $.getJSON('/Home/WxSignature?url=' + location.href.split('#')[0], function (data) {
        if (data && data.ErrMsg) {
            alert(data.ErrMsg);
            console.log(data.StackTrace);
            return false;
        }
        var config = {
            title: '精彩赛事，相约青浦西虹桥',
            link: location.href,
            desc: '精彩赛事，相约青浦西虹桥',
            imgUrl: '/images/share_icon.png',
            jsApiList: ['onMenuShareTimeline'
                , 'onMenuShareAppMessage'
                , 'onMenuShareQQ'
                , 'onMenuShareQZone', 'onMenuShareWeibo', 'showOptionMenu', 'closeWindow']
        }

        data && data.nonce && wx.config({
            debug: true, // 开启调试模式,调用的所有api的返回值会在客户端alert出来，
            //若要查看传入的参数，可以在pc端打开，参数信息会通过log打出，仅在pc端时才会打印。
            appId: data.appId, // 必填，公众号的唯一标识
            timestamp: data.timestamp, // 必填，生成签名的时间戳
            nonceStr: data.nonce, // 必填，生成签名的随机串
            signature: data.signature,// 必填，签名，见附录1
            jsApiList: config.jsApiList // 必填，需要使用的JS接口列表，所有JS接口列表见附录2
        });

        wx.ready(function () {
            wx.showOptionMenu();
            //分享到朋友圈
            wx.onMenuShareTimeline({
                title: config.title, // 分享标题
                link: config.link, // 分享链接
                imgUrl: config.imgUrl, // 分享图标
                success: function () {
                    // 用户确认分享后执行的回调函数
                    alert('分享成功');//分享+1
                },
                cancel: function () {
                    // 用户取消分享后执行的回调函数
                    alert('分享取消');//分享+1
                }
            });

            //分享给朋友
            wx.onMenuShareAppMessage({
                title: config.title, // 分享标题
                desc: config.desc, // 分享描述
                link: config.link, // 分享链接
                imgUrl: config.imgUrl, // 分享图标
                success: function () {
                    // 用户确认分享后执行的回调函数
                    alert('分享成功');//分享+1
                },
                cancel: function () {
                    // 用户取消分享后执行的回调函数
                    alert('分享取消');//分享+1
                }
            });

            //QQ
            wx.onMenuShareQQ({
                title: config.title, // 分享标题
                desc: config.desc, // 分享描述
                link: config.link, // 分享链接
                imgUrl: config.imgUrl, // 分享图标
                success: function () {
                    // 用户确认分享后执行的回调函数
                    alert('分享成功');//分享+1
                },
                cancel: function () {
                    // 用户取消分享后执行的回调函数
                    alert('分享取消');//分享+1
                }
            });
            //QQ微博
            wx.onMenuShareWeibo({
                title: config.title, // 分享标题
                desc: config.desc, // 分享描述
                link: config.link, // 分享链接
                imgUrl: config.imgUrl, // 分享图标
                success: function () {
                    // 用户确认分享后执行的回调函数
                    alert('分享成功');//分享+1
                },
                cancel: function () {
                    // 用户取消分享后执行的回调函数
                    alert('分享取消');//分享+1
                }
            });

            //QQ空间
            wx.onMenuShareQZone({
                title: config.title, // 分享标题
                desc: config.desc, // 分享描述
                link: config.link, // 分享链接
                imgUrl: config.imgUrl, // 分享图标
                success: function () {
                    // 用户确认分享后执行的回调函数
                    alert('分享成功');//分享+1
                },
                cancel: function () {
                    // 用户取消分享后执行的回调函数
                    alert('分享取消');//分享+1
                }
            });
        });


    });
})();