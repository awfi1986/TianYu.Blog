(function ($) {
    win = {
        winIndex: 0,
        error: function (message) {
            if (message == "" || message == undefined) message = "错误";
            layer.msg(message, { icon: 2 });
        },
        info: function (message) {
            if (message == "" || message == undefined) message = "提示";
            layer.msg(message, { icon: 7 });
        },
        alert: function (message) {
            if (message == "" || message == undefined) message = "错误";
            layer.alert(message, { icon: 7 });
        },
        alertOk: function (message) {
            if (message == "" || message == undefined) message = "成功";
            layer.alert(message, { icon: 1 });
        },
        fail: function (message) {
            if (message == "" || message == undefined) message = "操作失败";
            layer.msg(message, { icon: 7 });
        },
        success: function (message, callback, time) {
            if (message == "" || message == undefined) message = "操作成功";
            layer.msg(message, { icon: 1, time: time == undefined ? 2000 : time }, function () {
                if (callback!=undefined) callback();
            });
        },
        open: function (url, width, height, title, isMax) {
            if (title == undefined) title = "新窗体";
            win.winIndex = layer.open({
                type: 2,
                title: title,
                area: [width + 'px', height + 'px'],
                fixed: false, //不固定
                maxmin: false,
                content: url
            });
            if (isMax) layer.full(win.winIndex);
        },
        openNoMax: function (url, width, height, title) {
            if (title == undefined) title = "新窗体";
            win.winIndex = layer.open({
                type: 2,
                title: title,
                area: [width + 'px', height + 'px'],
                fixed: false, //不固定
                maxmin: false,
                content: url
            });
        },
        openMaxMin: function (url, width, height, title, isMax) {
            if (title == undefined) title = "新窗体";
            win.winIndex = layer.open({
                type: 2,
                title: title,
                area: [width + 'px', height + 'px'],
                fixed: false, //不固定
                maxmin: false,
                content: url
            });
            if (isMax) layer.full(win.winIndex);
        },
        close: function () {
            var index = parent.layer.getFrameIndex(window.name);
            parent.layer.close(index);
        },
        confirm: function (title, callback1, callback2) {
            var index = layer.confirm(title, {
                btn: ['确认', '取消']
            }, function () {
                callback1();
                layer.close(index);
            }, function () {
                if (callback2) callback2();
                layer.close(index);
            });
        },
        showLoading: function (msg) {
             if (msg == undefined) msg = "正在加载....";
             win.winIndex = layer.msg(msg, { icon: 16, time: 120000, width: 100 });
            //if (msg == undefined) msg = "正在加载....";
            //$(".mask1").show().find("span").html(msg);
        },
        hideLoading: function () {
            layer.close(win.winIndex);
            //$(".mask1").hide();
        },
        //获取所有选中checkbox值
        getChecked: function (cboxList) {
            var objValue = "";
            $(cboxList).each(function () {
                if ($(this).prop("checked")) {
                    objValue += $(this).val() + ",";
                }
            });
            if (objValue != "") objValue = objValue.substring(0, objValue.length - 1);
            return objValue;
        },
    }
})(jQuery);