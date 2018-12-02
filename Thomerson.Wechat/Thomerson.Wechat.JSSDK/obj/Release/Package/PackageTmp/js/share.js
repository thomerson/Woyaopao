(function () {
    wx && $.getJSON('/Home/WxSignature?url=' + location.href.split('#')[0], function (data) {
        if (data && data.ErrMsg) {
            alert(data.ErrMsg);
            console.log(data.StackTrace);
            return false;
        }
        var config = {
            title: '�������£���Լ����������',
            link: location.href,
            desc: '�������£���Լ����������',
            imgUrl: '/images/share_icon.png',
            jsApiList: ['onMenuShareTimeline'
                , 'onMenuShareAppMessage'
                , 'onMenuShareQQ'
                , 'onMenuShareQZone', 'onMenuShareWeibo', 'showOptionMenu', 'closeWindow']
        }

        data && data.nonce && wx.config({
            debug: true, // ��������ģʽ,���õ�����api�ķ���ֵ���ڿͻ���alert������
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
                    alert('����ɹ�');//����+1
                },
                cancel: function () {
                    // �û�ȡ�������ִ�еĻص�����
                    alert('����ȡ��');//����+1
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
                    alert('����ɹ�');//����+1
                },
                cancel: function () {
                    // �û�ȡ�������ִ�еĻص�����
                    alert('����ȡ��');//����+1
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
                    alert('����ɹ�');//����+1
                },
                cancel: function () {
                    // �û�ȡ�������ִ�еĻص�����
                    alert('����ȡ��');//����+1
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
                    alert('����ɹ�');//����+1
                },
                cancel: function () {
                    // �û�ȡ�������ִ�еĻص�����
                    alert('����ȡ��');//����+1
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
                    alert('����ɹ�');//����+1
                },
                cancel: function () {
                    // �û�ȡ�������ִ�еĻص�����
                    alert('����ȡ��');//����+1
                }
            });
        });


    });
})();