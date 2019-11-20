var isMobile = function () {
	if ( /Android|iPhone|iPad|iPod|BlackBerry|IEMobile/i.test( navigator.userAgent ) ) {
		return true;
	}
	return false;
} ();

var importCSS = function () {
	var filepath = '/css/',
		fileprefix = 'acccenter-',
		filename = fileprefix + ( isMobile ? 'mobile' : 'pc' );

	function init( path, version ) {
		if ( path ) {
			filepath = path + filepath;
		}
		var str = '<link rel="stylesheet" href="' + filepath + filename + '.css';
		if ( version ) {
			str += '?ver=' + version;
		}
		str += '">';
		document.write( str );
	}

	return {
		init: init
	};
} ();

importCSS.init('', '0.000018');