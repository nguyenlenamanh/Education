/******/ (function(modules) { // webpackBootstrap
/******/ 	// The module cache
/******/ 	var installedModules = {};
/******/
/******/ 	// The require function
/******/ 	function __webpack_require__(moduleId) {
/******/
/******/ 		// Check if module is in cache
/******/ 		if(installedModules[moduleId])
/******/ 			return installedModules[moduleId].exports;
/******/
/******/ 		// Create a new module (and put it into the cache)
/******/ 		var module = installedModules[moduleId] = {
/******/ 			exports: {},
/******/ 			id: moduleId,
/******/ 			loaded: false
/******/ 		};
/******/
/******/ 		// Execute the module function
/******/ 		modules[moduleId].call(module.exports, module, module.exports, __webpack_require__);
/******/
/******/ 		// Flag the module as loaded
/******/ 		module.loaded = true;
/******/
/******/ 		// Return the exports of the module
/******/ 		return module.exports;
/******/ 	}
/******/
/******/
/******/ 	// expose the modules object (__webpack_modules__)
/******/ 	__webpack_require__.m = modules;
/******/
/******/ 	// expose the module cache
/******/ 	__webpack_require__.c = installedModules;
/******/
/******/ 	// __webpack_public_path__
/******/ 	__webpack_require__.p = "/";
/******/
/******/ 	// Load entry module and return exports
/******/ 	return __webpack_require__(0);
/******/ })
/************************************************************************/
/******/ ([
/* 0 */
/***/ (function(module, exports, __webpack_require__) {

	module.exports = __webpack_require__(1);


/***/ }),
/* 1 */
/***/ (function(module, exports) {

	'use strict';
	
	var css3Support = function () {
	    var transform,
	        transformInCss,
	        transition,
	        transitionend,
	        oldFlex,
	        webkitVendor = 'Webkit',
	        elem = document.createElement('div').style;
	
	    function testProp(n) {
	        if (typeof elem[n] === 'string') {
	            return n;
	        }
	
	        n = webkitVendor + n.charAt(0).toUpperCase() + n.slice(1);
	
	        if (typeof elem[n] === 'string') {
	            return n;
	        }
	    }
	
	    function testOldFlex() {
	        var ret;
	
	        elem.display = '-webkit-box';
	        elem.display = '-webkit-flex';
	        elem.display = 'flex';
	
	        ret = elem.display;
	
	        elem.display = '';
	
	        if (ret === '-webkit-box') {
	            return true;
	        }
	
	        return false;
	    }
	
	    function testTrans() {
	        transform = testProp('transform');
	        transition = testProp('transition');
	
	        if (transform == 'WebkitTransform') {
	            transformInCss = '-webkit-transform';
	        } else {
	            transformInCss = transform;
	        }
	
	        if (transition == 'WebkitTransition') {
	            transitionend = 'webkitTransitionEnd';
	        } else {
	            transitionend = 'transitionend';
	        }
	    }
	
	    testTrans();
	    oldFlex = testOldFlex();
	
	    return {
	        transform: transform,
	        transformInCss: transformInCss,
	        transition: transition,
	        transitionend: transitionend,
	        oldFlex: oldFlex
	    };
	}();
	
	var rootFontSize = function () {
	    var idealWidth = 750,
	        remoffset = idealWidth / 100,
	        doc = document,
	        rootElem = doc.documentElement,
	        style,
	        timer,
	        remsize = [],
	        delay = 200;
	
	    function set() {
	        var orientationVal = window.orientation,
	            isLandscape = +!!(orientationVal === 90 || orientationVal === -90),
	            size = remsize[isLandscape];
	
	        if (size === undefined) {
	            size = rootElem.getBoundingClientRect().width / remoffset;
	            remsize[isLandscape] = size;
	        }
	
	        style.innerHTML = 'html{font-size: ' + size + 'px}';
	    }
	
	    function change() {
	        timer && clearTimeout(timer);
	        timer = setTimeout(function () {
	            timer = null;
	            set();
	        }, delay);
	    }
	
	    function getRatio() {
	        var body = doc.body,
	            ele = doc.createElement('div'),
	            fz = 1200,
	            ret;
	        ele.style.cssText = 'font-size:' + fz + 'px;display: none';
	        body.appendChild(ele);
	        ret = fz / parseInt(window.getComputedStyle(ele).getPropertyValue('font-size'));
	        body.removeChild(ele);
	        return ret;
	    }
	
	    function init() {
	        var ratio = getRatio();
	        remoffset /= ratio;
	
	        style = doc.createElement('style');
	        doc.head.appendChild(style);
	
	        set();
	        window.addEventListener('orientationchange', change);
	    }
	
	    return {
	        init: init
	    };
	}();
	
	var mobileSidebar = function () {
	    var mask = document.getElementById('J-wrapper-mask');
	    if (!mask) {
	        return;
	    }
	    var directions = ['left', 'right'],
	        sidebars = {},
	        body,
	        currentSidebar;
	
	    function handleSidebar(direction) {
	        var sidebar = sidebars[direction];
	        sidebar.trigger.addEventListener('click', function () {
	            if (currentSidebar) {
	                return;
	            }
	            body.classList.add(sidebar.className);
	            currentSidebar = direction;
	        });
	
	        mask.addEventListener('click', function () {
	            if (!currentSidebar) {
	                return;
	            }
	            body.classList.remove(sidebars[currentSidebar].className);
	            currentSidebar = null;
	        });
	
	        mask.addEventListener('touchmove', function (e) {
	            e.preventDefault();
	        });
	    }
	
	    function initSidebars() {
	        directions.forEach(function (direction) {
	            sidebars[direction] = {
	                elem: document.getElementById('J-msidebar-' + direction),
	                trigger: document.getElementById('J-msidebar-' + direction + '-trigger'),
	                className: 'body--' + direction + 'expanded'
	            };
	        });
	    }
	
	    function init() {
	        body = document.body;
	        initSidebars();
	        handleSidebar('left');
	        handleSidebar('right');
	    }
	
	    return {
	        init: init
	    };
	}();
	
	// var localLang = function () {
	//     var select = document.getElementById( 'J-lang-select' );
	//     if ( !select ) {
	//         return;
	//     }
	//
	//     function init() {
	//         select.addEventListener( 'change', function () {
	//             window.location = this.value;
	//         });
	//     }
	//
	//     return {
	//         init: init
	//     };
	// } ();
	
	function CollapsibleList(ele, options) {
	    var smooth = options.smooth || false;
	
	    this.trigger = ele.trigger;
	    this.content = ele.content;
	    this.contentSelectedClass = options.contentSelectedClass;
	    this.triggerSelectedClass = options.triggerSelectedClass;
	
	    if (smooth) {
	        this.rcontent = ele.rcontent;
	        this.contentStopClass = options.contentStopClass;
	        this.nextState = -1;
	    }
	
	    this.state = -1;
	    this._init(smooth);
	}
	
	CollapsibleList.prototype = {
	    _reflow: function _reflow() {
	        var content = this.content,
	            contentStopClass = this.contentStopClass;
	
	        content.style.height = content.getBoundingClientRect().height + 'px';
	        content.classList.add(contentStopClass);
	        content.clientWidth;
	        content.classList.remove(contentStopClass);
	    },
	    _smoothToggle: function _smoothToggle() {
	        var triggerSelectedClass = this.triggerSelectedClass,
	            contentSelectedClass = this.contentSelectedClass,
	            trigger = this.trigger,
	            content = this.content,
	            rcontent = this.rcontent,
	            state = this.state,
	            nextState = this.nextState,
	            action;
	
	        if (state === 0) {
	            this._reflow();
	            if (nextState === 1) {
	                action = 'remove';
	                content.style.height = 0;
	                this.nextState = -1;
	            } else {
	                action = 'add';
	                content.style.height = rcontent.getBoundingClientRect().height + 'px';
	                this.nextState = 1;
	            }
	        } else {
	            if (state === 1) {
	                action = 'remove';
	
	                this._reflow();
	
	                content.classList.remove(contentSelectedClass);
	                content.style.display = 'block';
	
	                this.state = 0;
	                this.nextState = -1;
	
	                content.style.height = 0;
	            } else {
	                action = 'add';
	                content.style.height = 0;
	                content.style.display = 'block';
	
	                this.state = 0;
	                this.nextState = 1;
	
	                content.style.height = rcontent.getBoundingClientRect().height + 'px';
	            }
	        }
	
	        triggerSelectedClass && trigger.classList[action](triggerSelectedClass);
	    },
	    _toggle: function _toggle() {
	        var action,
	            triggerSelectedClass = this.triggerSelectedClass;
	
	        if (this.state == 1) {
	            action = 'remove';
	            this.state = -1;
	        } else {
	            action = 'add';
	            this.state = 1;
	        }
	
	        triggerSelectedClass && this.trigger.classList[action](triggerSelectedClass);
	        this.content.classList[action](this.contentSelectedClass);
	    },
	    _transitionEnd: function _transitionEnd() {
	        var that = this,
	            contentStopClass = this.contentStopClass;
	
	        this.content.addEventListener(css3Support.transitionend, function (e) {
	            if (e.propertyName !== 'height') {
	                return;
	            }
	
	            if (that.nextState === 1) {
	                this.classList.add(that.contentSelectedClass);
	            }
	
	            this.classList.add(contentStopClass);
	            this.style.height = '';
	            this.style.display = '';
	            this.clientWidth;
	            this.classList.remove(contentStopClass);
	
	            that.state = that.nextState;
	        });
	    },
	
	    _init: function _init(smooth) {
	
	        var that = this;
	
	        if (smooth) {
	            this._transitionEnd();
	            this._toggle = this._smoothToggle;
	        }
	
	        this.trigger.addEventListener('click', function () {
	            that._toggle();
	        });
	    }
	};
	
	var sLvList = function () {
	    var listWrap = document.getElementById('J-slv-nor-wrap');
	    if (!listWrap) {
	        return;
	    }
	
	    var list,
	        trigger,
	        listExpandedClass = 'securitylv-normal-wrap--expanded',
	        listStopClass = 'securitylv-normal-wrap--stop',
	        triggerExpandedClass = 'securitylv-panel--expanded';
	
	    function init() {
	        list = document.getElementById('J-slv-nor');
	        trigger = document.getElementById('J-slv-nor-trigger');
	
	        var opts = {
	            contentSelectedClass: listExpandedClass,
	            contentStopClass: listStopClass,
	            triggerSelectedClass: triggerExpandedClass
	        };
	
	        opts.smooth = isMobile;
	
	        new CollapsibleList({
	            trigger: trigger,
	            content: listWrap,
	            rcontent: list
	        }, opts);
	    }
	
	    return {
	        init: init
	    };
	}();
	
	var oplogList = function () {
	    var listWrap = document.querySelectorAll('.J-oplog-wrap');
	
	    if (!listWrap.length) {
	        return;
	    }
	
	    var list,
	        trigger,
	        listExpandedClass = 'oplog-wrap--expanded',
	        listStopClass = 'oplog-wrap--stop',
	        triggerExpandedClass = 'oplog-title--expanded';
	
	    function init() {
	        list = document.querySelectorAll('.J-oplog');
	        trigger = document.querySelectorAll('.J-oplog-trigger');
	
	        var opts = {
	            contentSelectedClass: listExpandedClass,
	            contentStopClass: listStopClass,
	            triggerSelectedClass: triggerExpandedClass,
	            smooth: true
	        },
	            i = 0,
	            listLen = list.length;
	
	        for (; i < listLen; i++) {
	            new CollapsibleList({
	                trigger: trigger[i],
	                content: listWrap[i],
	                rcontent: list[i]
	            }, opts);
	        }
	    }
	
	    return {
	        init: init
	    };
	}();
	
	var qrcodeTip = function () {
	    var tip = document.getElementById('J-sauth-qrtip');
	    if (!tip) {
	        return;
	    }
	
	    var trigger,
	        tipVisibleClass = 'setupauth-step__pcqr-dl--visible',
	        triggerExpandedClass = 'setupauth-step__pcqr-info--expanded';
	
	    function init() {
	        trigger = document.getElementById('J-sauth-qrtip-trigger');
	
	        var opts = {
	            contentSelectedClass: tipVisibleClass,
	            triggerSelectedClass: triggerExpandedClass
	        };
	
	        new CollapsibleList({
	            trigger: trigger,
	            content: tip
	        }, opts);
	    }
	
	    return {
	        init: init
	    };
	}();
	
	var qrcodeDownloadPop = function () {
	    var trigger = document.getElementById('J-sauth-qrpop-trigger');
	    if (!trigger) {
	        return;
	    }
	
	    var popContainer,
	        popDimmer,
	        isHiding = false,
	        popShowStyle = 'mqrpop-container--show',
	        popfadeInStyle = 'mqrpop-container--fadein';
	
	    function show() {
	        popContainer.classList.add(popShowStyle);
	        popContainer.clientWidth;
	        popContainer.classList.add(popfadeInStyle);
	    }
	
	    function fadeOut() {
	        if (isHiding) {
	            return;
	        }
	        isHiding = true;
	        popContainer.classList.remove(popfadeInStyle);
	    }
	
	    function hide() {
	        popContainer.classList.remove(popShowStyle);
	        isHiding = false;
	    }
	
	    function init() {
	        popContainer = document.getElementById('J-sauth-qrpop-container');
	        popDimmer = document.getElementById('J-sauth-qrpop-dimmer');
	
	        trigger.addEventListener('click', show);
	        popDimmer.addEventListener('click', fadeOut);
	
	        popContainer.addEventListener(css3Support.transitionend, function () {
	            if (!isHiding) {
	                return;
	            }
	            hide();
	        });
	    }
	
	    return {
	        init: init
	    };
	}();
	
	function CountDown(ele, options) {
	    var dur;
	    this.ele = ele;
	    options = options || {};
	    dur = options.duration || 0;
	    this.duration = dur;
	    this.liftoffTime = dur;
	    this.onExpiry = options.onExpiry || null;
	
	    this.set(dur);
	}
	
	CountDown.prototype = {
	    delay: 1000,
	    set: function set(liftoffTime) {
	        this.ele.innerHTML = liftoffTime;
	    },
	    start: function start() {
	        var that = this,
	            liftoffTime = this.liftoffTime,
	            onExpiry = this.onExpiry;
	
	        setTimeout(function () {
	            liftoffTime--;
	            if (liftoffTime <= 0) {
	                onExpiry && onExpiry();
	            } else {
	                that.liftoffTime = liftoffTime;
	                that.set(liftoffTime);
	                that.start();
	            }
	        }, this.delay);
	    },
	    reset: function reset() {
	        var dur = this.duration;
	        this.liftoffTime = dur;
	        this.set(dur);
	    }
	};
	
	var redirectToHome = function () {
	    var display = document.getElementById('J-success-cdele');
	
	    if (!display) {
	        return;
	    }
	
	    function _onExpiry(url) {
	        window.location.href = url;
	    }
	
	    function init(options) {
	        options = options || {};
	
	        var url = options.url || '/',
	            delay = options.delay || 5,
	            cd = new CountDown(display, {
	            duration: delay,
	            onExpiry: function onExpiry() {
	                _onExpiry(url);
	            }
	        });
	        cd.start();
	    }
	
	    return {
	        init: init
	    };
	}();
	
	// redirectToHome && redirectToHome.init({url: '/'});
	
	var otpCode = function () {
	    var trigger = document.getElementById('J-getotp-trigger');
	    if (!trigger) {
	        return;
	    }
	
	    var cddisplay,
	        triggerCd,
	        sendinfo,
	        xhr,
	        cdduration = 60,
	        isTriggerDisabled = false,
	        triggerDisabledClass = 'content__form-getotp--disabled',
	        isSendinfoHasSendedClass = false,
	        infoSendedClass = 'content__sendinfo--sended';
	
	    function getcode(options) {
	        xhr && xhr.abort();
	        xhr = $.ajax(options);
	        xhr.then(function (data) {
	            if (data !== 'success') {
	                return $.Deferred().reject();
	            }
	
	            return;
	        }).done(function () {
	            isTriggerDisabled = true;
	
	            if (!isSendinfoHasSendedClass) {
	                sendinfo.classList.add(infoSendedClass);
	                isSendinfoHasSendedClass = true;
	            }
	
	            trigger.classList.add(triggerDisabledClass);
	            triggerCd.reset();
	            triggerCd.start();
	        }).always(function () {
	            xhr = null;
	        });
	    }
	
	    function init(options) {
	        cddisplay = document.getElementById('J-getotp-cd');
	        sendinfo = document.getElementById('J-sendinfo');
	
	        triggerCd = new CountDown(cddisplay, {
	            duration: cdduration,
	            onExpiry: function onExpiry() {
	                trigger.classList.remove(triggerDisabledClass);
	                isTriggerDisabled = false;
	            }
	        });
	
	        trigger.addEventListener('click', function () {
	            if (isTriggerDisabled) {
	                return;
	            }
	            getcode(options);
	        });
	    }
	    return {
	        init: init
	    };
	}();
	
	function FieldValidation(elem, validator, options) {
	    this.field = elem.field;
	    this.errMsgWrap = $(elem.errMsgWrap);
	    this.errMsgDisplays = [];
	    this.validator = validator;
	    this.state = [];
	    this.validatorKey = [];
	    options = options || {};
	    this.errStyle = options.errStyle || '';
	    this.errMsgSelector = options.errMsgSelector || '';
	    this.errMsgToggleStyle = options.errMsgToggleStyle || '';
	    this.events = options.events || 'change input';
	
	    this.init();
	}
	
	FieldValidation.prototype = {
	    ns: 'fv',
	    rules: {
	        required: function required() {
	            return arguments[1] !== '';
	        },
	        length: function length() {
	            var params = arguments[0],
	                length = arguments[1].length;
	
	            if (length < params.min || length > params.max) {
	                return false;
	            }
	
	            return true;
	        },
	        notequalto: function notequalto() {
	            var targetNs = this.ns + 'TargetNotEqualTo',
	                field = this.field[0],
	                target = this.getFieldTarget(arguments[0], field, targetNs, true);
	
	            return target.value !== arguments[1];
	        },
	        equalto: function equalto() {
	            var targetNs = this.ns + 'TargetEqualTo',
	                field = this.field[0],
	                target = this.getFieldTarget(arguments[0], field, targetNs, true);
	
	            return target.value == arguments[1];
	        },
	        phonenum: function phonenum() {
	            var params = {
	                min: 1
	            };
	            return this.rules.digits(params, arguments[1]);
	        },
	        email: function email() {
	            var regexp = new RegExp(/\S+@\S+\.\S+/);
	            return regexp.test(arguments[1]);
	        },
	        digits: function digits() {
	            var params = arguments[0],
	                min = params.min || 0,
	                max = params.max || '',
	                regexp = new RegExp('^\\d{' + min + ',' + max + '}$');
	            return regexp.test(arguments[1]);
	        },
	        otp: function otp() {
	            var field = this.field[0],
	                maxlength = field.maxLength,
	                params = {
	                min: maxlength,
	                max: maxlength
	            };
	            return this.rules.digits(params, arguments[1]);
	        },
	        smsotp: function smsotp() {
	            return this.rules.otp.call(this, arguments[0], arguments[1]);
	        },
	        authotp: function authotp() {
	            return this.rules.otp.call(this, arguments[0], arguments[1]);
	        },
	        emailotp: function emailotp() {
	            return this.rules.otp.call(this, arguments[0], arguments[1]);
	        },
	        pwdinvalidchar: function pwdinvalidchar() {
	            var i,
	                len,
	                code,
	                val = arguments[1],
	                invalidChars = this.rules.pwdinvalidcharcode,
	                invalidCharsGtCode = invalidChars.gt,
	                invalidCharsLtCode = invalidChars.lt;
	
	            for (i = 0, len = val.length; i < len; i++) {
	                code = val.charCodeAt(i);
	                if (code < invalidCharsLtCode || code > invalidCharsGtCode) {
	                    return false;
	                }
	            }
	
	            return true;
	        },
	        pwdinvalidcharcode: {
	            lt: 33,
	            gt: 126
	        },
	        pwdstrength: function pwdstrength() {
	            var field = this.field[0],
	                ns = this.ns,
	                meterNs = ns + 'Meter',
	                scoreNs = ns + 'Score',
	                meter = field[meterNs],
	                lvStyle = arguments[0].lvStyle,
	                val = arguments[1],
	                i,
	                len,
	                score = [0, 0, 0, 0],
	                code,
	                lastScore = field[scoreNs] || 0,
	                invalidChars = this.rules.pwdinvalidcharcode,
	                invalidCharsGtCode = invalidChars.gt,
	                invalidCharsLtCode = invalidChars.lt;
	
	            if (meter === undefined) {
	                meter = $(arguments[0].meter);
	                field[meterNs] = meter;
	            }
	
	            for (i = 0, len = val.length; i < len; i++) {
	                code = val.charCodeAt(i);
	                if (code >= 48 && code <= 57) {
	                    // digit
	                    score[0] = 1;
	                } else if (code >= 65 && code <= 90) {
	                    // upper-case letter
	                    score[1] = 1;
	                } else if (code >= 97 && code <= 122) {
	                    // lower-case
	                    score[2] = 1;
	                } else if (code >= invalidCharsLtCode && code <= invalidCharsGtCode) {
	                    score[3] = 1; // symbol
	                }
	            }
	
	            score = score.reduce(function (prev, curr) {
	                return prev += curr;
	            }, 0);
	
	            field[scoreNs] = score;
	
	            if (lastScore !== score) {
	                meter.removeClass(lvStyle[lastScore]);
	            }
	
	            meter.addClass(lvStyle[score]);
	            return field[scoreNs] > 0;
	        }
	    },
	    getFieldTarget: function getFieldTarget(params, field, targetNs, bindListener) {
	        var target = field[targetNs],
	            that = this;
	
	        if (target === undefined) {
	            target = params.target;
	            if (target === undefined) {
	                target = params;
	            } else {
	                target = $(target);
	                bindListener && target.on(this.events, function () {
	                    that.validate();
	                });
	                target = target[0];
	                field[targetNs] = target;
	            }
	        }
	        return target;
	    },
	    toggleErrorMsg: function toggleErrorMsg(display, isValid) {
	        display[(isValid ? 'remove' : 'add') + 'Class'](this.errMsgToggleStyle);
	    },
	    createErrorMsg: function createErrorMsg(index, msg) {
	        var div = document.createElement('div');
	        div.className = this.errMsgSelector;
	        div.innerHTML = msg;
	        div = $(div);
	        this.errMsgWrap.append(div);
	        this.errMsgDisplays[index] = div;
	        return div;
	    },
	    toggleErrorStyle: function toggleErrorStyle(isValid) {
	        this.field[(isValid ? 'remove' : 'add') + 'Class'](this.errStyle);
	    },
	    isValid: function isValid() {
	        return !this.state.some(function (state) {
	            return state === false;
	        });
	    },
	    validate: function validate() {
	        var that = this,
	            rules = that.rules,
	            validator = this.validator,
	            validatorKey = this.validatorKey,
	            isLastValid = true,
	            isValid = true,
	            value = this.field[0].value;
	
	        validator.forEach(function (_validator, index) {
	            var state = that.state[index],
	                result = rules[validatorKey[index]].call(that, _validator.params, value),
	                errorDisplay;
	
	            if (result !== state) {
	                errorDisplay = that.errMsgDisplays[index];
	                if (!errorDisplay) {
	                    errorDisplay = that.createErrorMsg(index, _validator.errMsg);
	                }
	                that.toggleErrorMsg(errorDisplay, result);
	            }
	
	            if (!result) {
	                isValid = result;
	            }
	
	            if (!state) {
	                isLastValid = state;
	            }
	
	            that.state[index] = result;
	        });
	
	        if (isValid !== isLastValid) {
	            this.toggleErrorStyle(isValid);
	        }
	
	        return isValid;
	    },
	    init: function init() {
	        var that = this,
	            validator = this.validator,
	            _validator = [];
	
	        Object.keys(validator).forEach(function (key, index) {
	            that.validatorKey[index] = key;
	            that.state[index] = true;
	            _validator[index] = validator[key];
	        });
	
	        this.validator = _validator;
	    }
	};
	
	var formValidation = function () {
	    var form = document.getElementById('J-validated-form');
	    if (!form) {
	        return;
	    }
	
	    var fields,
	        fieldsArr,
	        validatedFields = [],
	        errStyle = 'content__form-input-widget--err',
	        errMsgSelector = 'content__form-errmsg-item',
	        errMsgToggleStyle = 'content__form-errmsg-item--visible';
	
	    function oneSubmit() {
	        var isValid = true;
	
	        validatedFields.forEach(function (field) {
	            var result = field.validate();
	
	            if (!result) {
	                isValid = result;
	            }
	        });
	
	        if (!isValid) {
	            fieldsArr.forEach(function (field, index) {
	                var fieldInstance = validatedFields[index];
	                fields.slice(index, index + 1).on(fieldInstance.events, function () {
	                    fieldInstance.validate();
	                });
	            });
	
	            form.on('submit', function () {
	                return submit();
	            });
	
	            return isValid;
	        }
	    }
	
	    function submit() {
	        return !validatedFields.some(function (field) {
	            return field.isValid() === false;
	        });
	    }
	
	    function init() {
	        form = $(form);
	        fields = form.find('.J-validated-field');
	        fieldsArr = Array.prototype.slice.call(fields);
	
	        fieldsArr.forEach(function (field, i) {
	            var fieldData = JSON.parse(field.getAttribute('data-validate'));
	            validatedFields[i] = new FieldValidation({
	                field: fields.slice(i, i + 1),
	                errMsgWrap: fieldData.errMsgWrap
	            }, fieldData.validator, {
	                errStyle: errStyle,
	                errMsgSelector: errMsgSelector,
	                errMsgToggleStyle: errMsgToggleStyle
	            });
	        });
	
	        form[0].noValidate = true;
	
	        form.one('submit', function () {
	            return oneSubmit();
	        });
	    }
	    return {
	        state: function state() {
	            return submit();
	        },
	        init: init
	    };
	}();
	
	
	
	//localLang && localLang.init();
	sLvList && sLvList.init();
	formValidation && formValidation.init();

/***/ })
/******/ ]);
//# sourceMappingURL=extra.77933e9e.js.map