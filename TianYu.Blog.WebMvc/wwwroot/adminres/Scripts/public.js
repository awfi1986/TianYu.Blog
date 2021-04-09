(function ($) {
    $.admin = {
        selectValue: "", //返回id
        selectText: "", //返回值
        selectTextValue: "", //返回id和值
        isRefurbish: false,//是否刷新页面
        isCallback: false,//是否回调
        //控制checkbox选中或取消选中
        onSelectCbox: function (clickCbox, attrName, attrValue) {
            var bl = $(clickCbox).prop("checked") ? true : false;
            $("input[" + attrName + "='" + attrValue + "']").prop("checked", bl);
        },
        //反选
        onCounterSelectCbox: function (attrName, attrValue) {
            $.each($("input[" + attrName + "='" + attrValue + "']"), function (i, item) {
                $(item).prop("checked", $(item).prop("checked") ? false : true);
            });
        },
        //获取所有选中checkbox值
        getCboxVal: function (cboxList) {
            var objValue = "";
            $(cboxList).each(function () {
                if ($(this).prop("checked")) {
                    objValue += $(this).val() + ",";
                }
            });
            if (objValue != "") {
                objValue = objValue.substring(0, objValue.length - 1);
            }
            return objValue;
        },
        //获取所有checkbox值
        getCboxVals: function (cboxList, attrName) {
            var objValue = "";
            $(cboxList).each(function () {
                if ($(this).prop("checked")) {
                    objValue += $(this).prop(attrName) + ",";
                }
            });
            if (objValue != "") {
                objValue = objValue.substring(0, objValue.length - 1);
            }
            return objValue;
        },
        //keyPress、keyUp、keyBlur控制文本只能输入小数和整数
        keyPress: function (obj) {
            if (!obj.value.match(/^[\+\-]?\d*?\.?\d*?$/)) {
                obj.value = obj.t_value != undefined ? obj.t_value : "";
            }
            else {
                obj.t_value = obj.value;
            }
            if (obj.value.match(/^(?:[\+\-]?\d+(?:\.\d+)?)?$/)) {
                obj.o_value = obj.value;
            }
        },
        keyUp: function (obj) {
            if (!obj.value.match(/^[\+\-]?\d*?\.?\d*?$/)) {
                obj.value = obj.t_value != undefined ? obj.t_value : "";
            } else {
                obj.t_value = obj.value;
            }
            if (obj.value.match(/^(?:[\+\-]?\d+(?:\.\d+)?)?$/)) {
                obj.o_value = obj.value;
            }
        },
        keyBlur: function (obj) {
            if (!obj.value.match(/^(?:[\+\-]?\d+(?:\.\d+)?|\.\d*?)?$/)) {
                obj.value = obj.o_value != undefined ? obj.o_value : "";
            } else {
                if (obj.value.match(/^\.\d+$/)) {
                    obj.value = 0 + obj.value;
                }
                if (obj.value.match(/^\.$/)) {
                    obj.value = 0;
                    obj.o_value = obj.value;
                }
            }
        },
        //获取url参数
        getQueryString: function (name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
            var r = window.location.search.substr(1).match(reg);
            if (r != null) {
                return unescape(r[2]);
            }
            return null;
        },
        //获得唯一值
        getGuid: function () {
            var vguid = "";
            var vdate = new Date();
            vguid += vdate.getFullYear(); //获取年
            vguid += (vdate.getMonth() + 1); //获取月
            vguid += vdate.getDate(); //获取日
            vguid += vdate.getHours(); //获取小时
            vguid += vdate.getMinutes(); //获取分钟
            vguid += vdate.getSeconds(); //获取秒
            vguid += vdate.getMilliseconds(); //获取毫秒
            return vguid;
        },
        //获得当前时间
        getDate: function (fmt) {
            var myDate = new Date();
            var dyear = myDate.getYear();//获取当前年份(2位)     
            var cyear = myDate.getFullYear();//获取完整的年份(4位,1970-????)    
            var month = myDate.getMonth() + 1;//获取当前月份(0-11,0代表1月)    
            var date = myDate.getDate();//获取当前日(1-31)
            var day = myDate.getDay();//获取当前星期X(0-6,0代表星期天)     
            var time = myDate.getTime();//获取当前时间(从1970.1.1开始的毫秒数)    
            var hours = myDate.getHours();//获取当前小时数(0-23)    
            var minute = myDate.getMinutes();//获取当前分钟数(0-59)    
            var second = myDate.getSeconds();//获取当前秒数(0-59)    
            var millsecond = myDate.getMilliseconds();//获取当前毫秒数(0-999)    
            var ddate = myDate.toLocaleDateString();//获取当前日期     
            var dtime = myDate.toLocaleTimeString(); //获取当前时间    
            var ddatetime = myDate.toLocaleString();//获取日期与时间
            if (fmt) {
                if (fmt == "yyyy-MM-dd") return cyear + "-" + month + "-" + date;
                if (fmt == "yyyy-MM-dd HH:mm:ss") return cyear + "-" + month + "-" + date + " " + hours + ":" + minute + ":" + second;
                if (fmt == "yyyy-MM-dd HH:mm") return cyear + "-" + month + "-" + date + " " + hours + ":" + minute;
                if (fmt == "yyyy-MM-dd HH") return cyear + "-" + month + "-" + date + " " + hours;
                if (fmt = "yyyy-MM") return cyear + "-" + month;
                if (fmt == "MM-dd") return month + "-" + date;
                if (fmt == "HH:mm:ss") return hours + ":" + minute + ":" + second;
                if (fmt == "HH:mm") return hours + ":" + minute;
                return cyear + "-" + month + "-" + date;
            } else {
                return cyear + "-" + month + "-" + date;
            }
        },
        //Date(1464671903000)/ 格式的日期转换
        getFmtDate: function (dateStr, fmt) {
            if (dateStr == null) return '';
            if (dateStr.indexOf('T') != -1) {
                return  dateStr.replace('T',' ');
            }
            var myDate = new Date(parseInt(dateStr.slice(6)));
            var cyear = myDate.getFullYear();//获取完整的年份(4位,1970-????)    
            var month = myDate.getMonth() + 1;//获取当前月份(0-11,0代表1月)    
            var date = myDate.getDate();//获取当前日(1-31)  
            var hours = myDate.getHours();//获取当前小时数(0-23)    
            var minute = myDate.getMinutes();//获取当前分钟数(0-59)    
            var second = myDate.getSeconds();//获取当前秒数(0-59)     
            if (month < 10) month = '0' + month;
            if (date < 10) date = '0' + date;
            if (hours < 10) hours = '0' + hours;
            if (minute < 10) minute = '0' + minute;
            if (second < 10) second = '0' + second;
            if (fmt) {
                if (fmt == "yyyy-MM-dd") return cyear + "-" + month + "-" + date;
                if (fmt == "yyyy-MM-dd HH:mm:ss") return cyear + "-" + month + "-" + date + " " + hours + ":" + minute + ":" + second;
                if (fmt == "yyyy-MM-dd HH:mm") return cyear + "-" + month + "-" + date + " " + hours + ":" + minute;
                if (fmt == "yyyy-MM-dd HH") return cyear + "-" + month + "-" + date + " " + hours;
                if (fmt == "yyyy-MM") return cyear + "-" + month;
                if (fmt == "MM-dd") return month + "-" + date;
                if (fmt == "HH:mm:ss") return hours + ":" + minute + ":" + second;
                if (fmt == "HH:mm") return hours + ":" + minute;
                return cyear + "-" + month + "-" + date;
            } else {
                return cyear + "-" + month + "-" + date;
            }
        },
        //获取两个时间差
        getDateDiff: function (startTime, endTime, diffType) {
            startTime = startTime.replace(/-/g, "/");
            endTime = endTime.replace(/-/g, "/");
            diffType = diffType.toLowerCase();
            var sTime = new Date(startTime);
            var eTime = new Date(endTime);
            var divNum = 1;
            switch (diffType) {
                case "ss":
                    divNum = 1000;
                    break;
                case "mm":
                    divNum = 1000 * 60;
                    break;
                case "hh":
                    divNum = 1000 * 3600;
                    break;
                case "dd":
                    divNum = 1000 * 3600 * 24;
                    break;
                default:
                    break;
            }
            return parseInt((eTime.getTime() - sTime.getTime()) / parseInt(divNum));
        },
        //验证手机号码是否正确
        IsMobile: function (mobile) {
            var myreg = /^(((14[0-9]{1})|(16[0-9]{1})|(17[0-9]{1})|(13[0-9]{1})|(15[0-9]{1})|(19[0-9]{1})|(18[0-9]{1}))+\d{8})$/;
            if (!myreg.test(mobile)) {
                return false;
            }
            return true;
        },
        //小写金额转大写金额     
        onSmallToBig: function (n) {
            var fraction = ['角', '分'];
            var digit = ['零', '壹', '贰', '叁', '肆', '伍', '陆', '柒', '捌', '玖'];
            var unit = [['元', '万', '亿'], ['', '拾', '佰', '仟']];
            var head = n < 0 ? '欠' : '';
            n = Math.abs(n);
            var s = '';
            for (var i = 0; i < fraction.length; i++) {
                s += (digit[Math.floor(n * 10 * Math.pow(10, i)) % 10] + fraction[i]).replace(/零./, '');
            }
            s = s || '整';
            n = Math.floor(n);
            for (var i = 0; i < unit[0].length && n > 0; i++) {
                var p = '';
                for (var j = 0; j < unit[1].length && n > 0; j++) {
                    p = digit[n % 10] + unit[1][j] + p;
                    n = Math.floor(n / 10);
                }
                s = p.replace(/(零.)*零$/, '').replace(/^$/, '零') + unit[0][i] + s;
            }
            return head + s.replace(/(零.)*零元/, '元').replace(/(零.)+/g, '零').replace(/^整$/, '零元整');
        },
        //数值转金额 
        numToMoney: function (num, len = 2) {
            var v = $.trim(num);
            if (v.length == 0) return '';
            return (parseFloat(v) / 100).toFixed(2);
        },
        //金额转数值
        moneyToNum: function (money, len = 2) {
            var v = $.trim(money);
            if (v.length == 0) return '';
            return (parseFloat(v) * 100).toFixed(0);
        }
    };
    $.verify = {
        phones: function (value) {
            if ($.trim(value).length > 0) {
                var reg = /^1([38][0-9]|4[579]|5[0-3,5-9]|6[6]|7[0135678]|9[89])\d{8}$/;
                if (!reg.test(value)) {
                    return '您的输入有误，请重新输入(中国11位手机号)';
                }
            }
        },
        money: function (value) {
            if (value != null && value.length > 0) {
                var reg = /^\d+(\.\d{1,2})?$/;
                if (!reg.test(value)) {
                    return '只能输入金额，最多两位小数';
                }
            }
        },
        number: function (value) {
            if (value != null && value.length > 0) {
                var reg = /^\d+$/;
                if (!reg.test(value)) {
                    return '只能输入数字';
                }
            }
        },
        rate: function (value) {
            if (value != null && value.length > 0) {
                var reg = /^\d{1}\.\d{1,2}$/;
                if (!reg.test(value)) {
                    return '只能输入两位小数的数字';
                }
            }
        }
    };
})(jQuery);
 
var common = {
    Ajax: {
        Get: function (data, url, successfn, errorfn, option) {

            //默认成功回调函数
            var defaultSuccessfn = function (data) { console.log(data.d); }

            //默认失败回调函数
            var defaultErrorfn = function (x) {
                console.log(x.responseText);

                if (x.responseText == 'nologon' || x.status == 401) {
                    alert("您还未登录，请先登录！");
                    setInterval(function () { window.location.href = '/Member/User/Login'; }, 3000);
                }
            }

            //默认Ajax配置
            var defaultOption = {
                type: "GET",
                url: url,
                data: data,
                contentType: "application/json",
                dataType: "json",
                success: $.isFunction(successfn) ? successfn : defaultSuccessfn,
                error: $.isFunction(errorfn) ? errorfn : defaultErrorfn
            }

            //合并传入的配置
            $.extend(defaultOption, option);

            //执行ajax请求
            $.ajax(defaultOption);
        },
        Post: function (data, url, successfn, errorfn, option) {

            //默认成功回调函数
            var defaultSuccessfn = function (data) { console.log(data); }

            //默认失败回调函数
            var defaultErrorfn = function (x) {
                console.log(x.responseText);

                if (x.responseText == 'nologon' || x.status == 401) {
                    alert("您还未登录，请先登录！");
                    setInterval(function () { window.location.href = '/Member/User/Login'; }, 3000);
                }
            }

            //默认Ajax配置
            var defaultOption = {
                type: "POST",
                url: url,
                data: data,
                contentType: "application/json",
                dataType: "json",
                success: $.isFunction(successfn) ? successfn : defaultSuccessfn,
                error: $.isFunction(errorfn) ? errorfn : defaultErrorfn
            }

            //合并传入的配置
            $.extend(defaultOption, option);

            //执行ajax请求
            $.ajax(defaultOption);
        }
    },
    Message: {
        Alert: function (txt) {
            alert(txt);
        }
    },
    //Javascript 格式化金额
    //格式化：
    fmoney: function (s, n) {
        n = n > 0 && n <= 20 ? n : 2;
        s = parseFloat((s / 100 + "").replace(/[^\d\.-]/g, "")).toFixed(n) + "";
        var l = s.split(".")[0].split("").reverse(),
            r = s.split(".")[1];
        t = "";
        for (i = 0; i < l.length; i++) {
            t += l[i] + ((i + 1) % 3 == 0 && (i + 1) != l.length ? "," : "");
        }
        return t.split("").reverse().join("") + "." + r;
    }
}
//功能说明：初始化省份城市下拉框
//参数说明： parentId： 父ID
//           bindControlId：要邦定的下拉框ID
//           selectValue：设置选择中值 
//创建人  ：蔡启虹
//创建时间：2014-04-17
function InitArea(parentId, bindControlId, selectValue) {
    $.ajax({
        type: "POST",
        contentType: "application/json;utf-8",
        url: "/api/WA/Area/GetCity",
        data: "{parentId:'" + parentId + "'}",
        async: false,
        success: function (data) {

            $("#" + bindControlId).empty();
            $("#" + bindControlId).append('<option value="">--------请选择--------</option>');

            var html = [];
            $(data).each(function (index, item) {
                if (selectValue == item.Name || selectValue == item.Code) {
                    html.push('<option value=' + item.Code + ' selected>' + item.Name + '</option>');
                }
                else {
                    html.push('<option value=' + item.Code + '>' + item.Name + '</option>');
                }
            });
            $("#" + bindControlId).append(html.join(""));
        },
        error: function (x) { alert(x.responseText); }
    });
}

(function ($) {
    $.fn.serializeJson = function () {
        var serializeObj = {};
        var array = this.serializeArray();
        var str = this.serialize();
        $(array).each(function () {
            if (serializeObj[this.name]) {
                if ($.isArray(serializeObj[this.name])) {
                    serializeObj[this.name].push(this.value);
                } else {
                    serializeObj[this.name] = [serializeObj[this.name], this.value];
                }
            } else {
                serializeObj[this.name] = this.value;
            }
        });
        return serializeObj;
    };
})(jQuery); 