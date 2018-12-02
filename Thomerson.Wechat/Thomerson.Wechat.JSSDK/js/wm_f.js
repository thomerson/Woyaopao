

(function () {

	var layout = document.querySelector( "#layout" );

	var pages = document.querySelectorAll( ".page" );

	var Height = layout.offsetHeight;

	var Width = layout.offsetWidth;


	var audio = document.querySelector( "audio" );

	var musicLogo = document.querySelector( ".music-logo" );

	var isStart = false;



	window.spHeight = Height;

	// Z.onTouchStart( pages[0], function () {
    //
	// 	if ( isStart == false ) {
    //
	// 		musicLogo.classList.add( "playing" );
    //
	// 		audio.src = "images/bg.mp3";
    //
	// 		audio.play();
    //
	// 	}
    //
	// 	isStart = true;
    //
	// } );

	Z.onTap( musicLogo, function () {

		musicLogo.classList.contains( "playing" ) ? audio.pause() : audio.play();

		musicLogo.classList.toggle( "playing" );

	} );





	// 第0页

	pages[0].onCut = function () {
        if ( isStart == false ) {

            musicLogo.classList.add( "playing" );

            //audio.src = "images/bg.mp3";

            audio.play();

        }

        isStart = true;

		setTimeout( function () {

			pages[0].classList.add( "animate" );

		}, 0 );

	};

	pages[0].onRemove = function () {

		this.classList.remove( "animate" );

	};





	// 第1页

	pages[1].onCut = function () {


		pages[1].classList.add( "animate" );

	};

	pages[1].onRemove = function () {

		this.classList.remove( "animate" );

	};



	// 第2页

	pages[2].onCut = function () {

		this.classList.add( "animate" );


	};

	pages[2].onRemove = function () {


		this.classList.remove( "animate" );


	};


	// 第3页

	pages[3].onCut = function () {

		this.classList.add( "animate" );

	};

	pages[3].onRemove = function () {

		this.classList.remove( "animate" );

	};



	//第4页

	pages[4].onCut = function () {

		this.classList.add( "animate" );

	};

	pages[4].onRemove = function () {

		this.classList.remove( "animate" );

	};





	//第5页

	pages[5].onCut = function () {

		this.classList.add( "animate" );

	};

	pages[5].onRemove = function () {

		this.classList.remove( "animate" );

	};






	setTimeout( function () {

		document.body.removeChild( document.querySelector( ".page-loading" ) );

		lib.ScreenSystem( document.getElementById( "layout" ) );

	}, 3000 );



})();

document.addEventListener( 'WeixinJSBridgeReady', function () {

    var WeixinJSBridge = window.WeixinJSBridge;



    // 发送给好友;

    WeixinJSBridge.on( 'menu:share:appmessage', function () {

        WeixinJSBridge.invoke( 'sendAppMessage', {

            "appid" : dataForWeixin.appId,

            "img_url" : dataForWeixin.picture,

            "img_width" : "120",

            "img_height" : "120",

            "link" : dataForWeixin.url,

            "desc" : dataForWeixin.desc,

            "title" : dataForWeixin.title

        }, function ( res ) {

        } );

    } );



    // 分享到朋友圈;

    WeixinJSBridge.on( 'menu:share:timeline', function () {

        WeixinJSBridge.invoke( 'shareTimeline', {

            "img_url" : dataForWeixin.picture,

            "img_width" : "120",

            "img_height" : "120",

            "link" : dataForWeixin.url,

            "desc" : dataForWeixin.desc,

            "title" : dataForWeixin.title

        }, function ( res ) {

        } );

    } );



    // 分享到微博;

    WeixinJSBridge.on( 'menu:share:weibo', function () {

        WeixinJSBridge.invoke( 'shareWeibo', {

            "content" : dataForWeixin.title + ' ' + dataForWeixin.url,

            "url" : dataForWeixin.url

        }, function ( res ) {

        } );

    } );



    // 分享到Facebook

    WeixinJSBridge.on( 'menu:share:facebook', function () {

        WeixinJSBridge.invoke( 'shareFB', {

            "img_url" : dataForWeixin.picture,

            "img_width" : "120",

            "img_height" : "120",

            "link" : dataForWeixin.url,

            "desc" : dataForWeixin.desc,

            "title" : dataForWeixin.title

        }, function ( res ) {

        } );

    } );

}, false );

