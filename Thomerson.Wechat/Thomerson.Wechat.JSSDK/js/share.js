(function () {
    function is_weixn() {
        var ua = navigator.userAgent.toLowerCase();
        return ua.indexOf('micromessenger') != -1;
    }

    is_weixn() && wx && $.getJSON('/Home/WxSignature?url=' + location.href.split('#')[0], function (data) {
        if (data && data.ErrMsg) {
            console.log(data.ErrMsg);
            console.log(data.StackTrace);
            return false;
        }
        var config = {
            title: '2018�������к�������',
            link: location.href,
            desc: '�������£���Լ����������',
            imgUrl: 'https://xhq.woyaopao.com/images/share_icon.png',
            jsApiList: ['onMenuShareTimeline'
                , 'onMenuShareAppMessage'
                , 'onMenuShareQQ'
                , 'onMenuShareQZone', 'onMenuShareWeibo', 'showOptionMenu', 'closeWindow']
        }

        data && data.nonce && wx.config({
            debug: false, // ��������ģʽ,���õ�����api�ķ���ֵ���ڿͻ���console.log������
            //��Ҫ�鿴����Ĳ�����������pc�˴򿪣�������Ϣ��ͨ��log���������pc��ʱ�Ż��ӡ��
            appId: data.appId, // ������ںŵ�Ψһ��ʶ
            timestamp: data.timestamp, // �������ǩ����ʱ���
            nonceStr: data.nonce, // �������ǩ���������
            signature: data.signature,// ���ǩ��������¼1
            jsApiList: config.jsApiList // �����Ҫʹ�õ�JS�ӿ��б�����JS�ӿ��б����¼2
        });

        wx.ready(function () {
            wx.showOptionMenu();
            //��������Ȧ
            wx.onMenuShareTimeline({
                title: config.title, // �������
                link: config.link, // ��������
                imgUrl: config.imgUrl, // ����ͼ��
                success: function () {
                    // �û�ȷ�Ϸ����ִ�еĻص�����
                    console.log('����ɹ�');//����+1
                },
                cancel: function () {
                    // �û�ȡ�������ִ�еĻص�����
                    console.log('����ȡ��');//����+1
                }
            });

            //���������
            wx.onMenuShareAppMessage({
                title: config.title, // �������
                desc: config.desc, // ��������
                link: config.link, // ��������
                imgUrl: config.imgUrl, // ����ͼ��
                success: function () {
                    // �û�ȷ�Ϸ����ִ�еĻص�����
                    console.log('����ɹ�');//����+1
                },
                cancel: function () {
                    // �û�ȡ�������ִ�еĻص�����
                    console.log('����ȡ��');//����+1
                }
            });

            //QQ
            wx.onMenuShareQQ({
                title: config.title, // �������
                desc: config.desc, // ��������
                link: config.link, // ��������
                imgUrl: config.imgUrl, // ����ͼ��
                success: function () {
                    // �û�ȷ�Ϸ����ִ�еĻص�����
                    console.log('����ɹ�');//����+1
                },
                cancel: function () {
                    // �û�ȡ�������ִ�еĻص�����
                    console.log('����ȡ��');//����+1
                }
            });
            //QQ΢��
            wx.onMenuShareWeibo({
                title: config.title, // �������
                desc: config.desc, // ��������
                link: config.link, // ��������
                imgUrl: config.imgUrl, // ����ͼ��
                success: function () {
                    // �û�ȷ�Ϸ����ִ�еĻص�����
                    console.log('����ɹ�');//����+1
                },
                cancel: function () {
                    // �û�ȡ�������ִ�еĻص�����
                    console.log('����ȡ��');//����+1
                }
            });

            //QQ�ռ�
            wx.onMenuShareQZone({
                title: config.title, // �������
                desc: config.desc, // ��������
                link: config.link, // ��������
                imgUrl: config.imgUrl, // ����ͼ��
                success: function () {
                    // �û�ȷ�Ϸ����ִ�еĻص�����
                    console.log('����ɹ�');//����+1
                },
                cancel: function () {
                    // �û�ȡ�������ִ�еĻص�����
                    console.log('����ȡ��');//����+1
                }
            });
        });


    });
})();