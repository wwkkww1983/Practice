/*!
 * ljl@2018
 */
!function ($, window, undefined) {

    //var LocationHref = window.location.href.split("/");
    //if (LocationHref.length > 2) {
    //    var Host = LocationHref[0] + "//" + LocationHref[2];
    //    if (!top.$.cookie('userid') && location.href != Host + "/Login" && location.href != Host + "/Login/index") {
    //        location.href = "/Login";
    //    }
    //}

    var _top = top;
    //var _layer = top.layer;

    var _site = {
        t: new Date().getTime(),
        BASE_URL: 'http://localhost:61851/'
    };

    _site.encode = function (o) {
        return JSON.stringify(o);
    };
    _site.decode = function (str) {
        return JSON.parse(str);
    };

    _site.queryString = function (key, url) {
        url = url || location.search;
        var result = url.match(new RegExp("[\?\&]" + key + "=([^\&]+)", "i"));
        if (result == null || result.length < 1) {
            return "";
        }
        return result[1];
    };

    _site.api = function (api, data, callback, options) {

        var token = $.cookie('Token');
        if (!token) {
            top.location.href = '/login?returnUrl=' + window.location.href;
            return;
        }

        if (typeof data == 'function') {
            options = callback;
            callback = data;
            data = {}
        }
        var type = /^\s*get:/.test(api) ? 'get' : 'post';
        var url = _site.BASE_URL + api.replace(/^\s*(post:|get:)?\s*\/?/, '');

        return $.ajax($.extend({
            type: type,
            url: url,
            dataType: 'json',
            contentType: 'application/json',
            data: type == 'post' ? _site.encode(data) : data,
            beforeSend: function (xhr) {
                //var token = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJleHAiOjE1Njc4NDY2ODYsInVzZXIiOnsiVXNlcklEIjoiMSIsIlVzZXJOYW1lIjoi6LaF57qn566h55CG5ZGYIn19.U4mnFIJpioXChcKIBD29MLx1UL-NfPiK6hM1XVrw2U8';
                xhr.setRequestHeader('Authorization', 'Bearer ' + token);
            },
            success: function (ret) {
                if (ret.Code == 401) {
                    window.location.href = '/login?returnUrl=' + encodeURIComponent(window.location.href);
                }

                var flag;
                if (callback) flag = callback(ret);
                if (flag === false) return;
                if (flag === true && !ret.Code) return;
                if (api != "/api/Auth/GetCurrentUser" && ret.Message != "根据权限获取菜单成功！") {
                    _site.msg(ret);
                }
            },
            error: function (xhr) {
                var ret = { Code: xhr.status || 1, Message: xhr.statusText || xhr.responseText || 'error' };
                if (callback && callback(ret) === false) { }
                _site.msg(ret);
            }
        }, options));
    };
    _site.loader = function (api, beforeAjax) {
        var fn = function (param, success, error) {
            if (beforeAjax) {
                if (typeof beforeAjax == 'function')
                    param = $.extend(param, beforeAjax(param));
                else
                    param = $.extend(param, beforeAjax);
            }
            _site.api(api, param, function (ret) {
                if (!ret.Code) success(ret.Data);
                else error(ret);
                return true;
            });
        };

        return fn;
    };

    _site.comboboColumn = function (id) {
        var data = id;
        if (typeof id == 'string') {
            var el = $(id);
            if (el.length) {
                if (el.prop('tagName') == 'SELECT') {
                    data = {};
                    el.children().each(function () {
                        var op = $(this);
                        data[op.val()] = op.text();
                    });
                } else {
                    data = eval(el.val());
                }
            }
        }
        //alert(JSON.stringify(data));
        var fn = function (value, row, index) {
            value = value === false ? 0 : value === true ? 1 : value;
            if (data[value]) {
                return data[value];
            }
            return value;
        };

        return fn;
    };

    _site.filter = function (id) {
        var param = [];
        $(id).find('input[type=hidden]').each(function () {
            var that = $(this);
            var val = that.val();
            if (val) {
                //var el = that.parent().prev();
                var el = that.attr("filterCurrentDom") ? that : that.parent().prev();
                var p = { name: that.attr('name'), value: val, type: 'like' };
                if (el.hasClass('datebox') || el.hasClass('combo')) {
                    p.type = '=';
                }
                if (el.attr('filterType')) {
                    p.type = el.attr('filterType');
                }
                p.OrNames = el.attr('filterOrNames');
                param.push(p);
            }
        });

        return { filters: param };
    };

    _site.getFormData = function (id) {
        var param = {};
        var form = $(id);
        var valid = true;
        form.find('.validatebox-text:not(:disabled)').validatebox('validate');
        var invalidbox = form.find('.validatebox-invalid');
        invalidbox.filter(':not(:disabled):first').focus();
        if (invalidbox.length > 0) {
            valid = false;
            return;
        }
        form.find('input[type=hidden],select:visible,textarea').each(function () {
            var that = $(this);
            var val = that.val();
            var name = that.attr('relname') || that.attr('name');
            param[name] = val;
        });
        //form.find('select').each(function () {
        //    if (this.name && this.value)
        //        param[this.name] = param[this.name] || this.value;
        //});
        return param;
    };
    _site.setFormData = function (id, data) {
        var form = $(id);
        form.form('load', data);
        //var cls = ['tagbox', 'combobox', 'combotree', 'combogrid', 'combotreegrid', 'datetimebox', 'datebox', 'combo',
        //    'datetimespinner', 'timespinner', 'numberspinner', 'spinner',
        //    'slider', 'searchbox', 'numberbox', 'passwordbox', 'filebox', 'textbox', 'switchbutton'];
        //for (var name in data) {
        //    var val = data[name];
        //    var cc = form.find('[switchbuttonName="' + name + '"]');
        //    if (cc.length) {
        //        if (val) {
        //            cc.switchbutton('check');
        //        }
        //        else {
        //            cc.switchbutton('uncheck');
        //        }
        //    } else {
        //        var cc = form.find('[textboxName="' + name + '"],[sliderName="' + name + '"]');
        //        if (cc.length) {
        //            for (var i = 0; i < cls.length; i++) {
        //                var type = cls[i];
        //                var state = cc.data(type);
        //                if (state) {
        //                    if (state.options.multiple || state.options.range) {
        //                        cc[type]('setValues', val);
        //                    } else {
        //                        cc[type]('setValue', val);
        //                    }
        //                }
        //            }
        //        } else {
        //            form.find('input[name="' + name + '"]').val(val);
        //            form.find('textarea[name="' + name + '"]').val(val);
        //            form.find('select[name="' + name + '"]').val(val);
        //        }
        //    }
        //}
    };

    _site.export = function (id, api) {
        var grid = $(id);
        grid.datagrid('loading');
        var options = grid.datagrid('options');
        var param = {
            page: options.pageNumber,
            rows: options.pageSize,
            sort: options.sortName,
            order: options.sortOrder,
            columns: []
        };
        var columns = options.columns[0];
        for (var i = 0; i < columns.length; i++) {
            if (!columns[i].checkbox) {
                param.columns.push({
                    width: columns[i].width,
                    field: columns[i].field,
                    title: columns[i].title
                });
            }
        }

        //alert(JSON.stringify(param));

        grid.datagrid('loaded');
    };

    _site._layerList = [];
    _site.load = function (msg) {
        _top.layer.load();
    };
    _site.unload = function () {
        _top.layer.closeAll('loading');
    };
    _site.msg = function (message, code) {
        var o = message;
        if (typeof o == 'string')
            o = { Message: message, Code: code };
        //alert(o.Message);
        //$.messager.show({
        //    msg: o.Message,
        //    showType: 'show',
        //    icon: 'error'
        //});
        //layer.msg(o.Message, { time: 5000, icon: o.Code, offset: 'rb', anim: 4 });
        _top.layer.open({
            skin: 'layui-layer-msg',
            icon: o.Code === 0 ? 1 : o.Code === 1 ? 2 : 0,
            offset: 10,
            title: false,
            closeBtn: false,
            area: ['25%', ''],
            btn: false,
            shade: false,
            time: 3000,
            content: o.Message,
            anim: 4
        });
    };
    _site.confirm = function (msg, callback, icon) {
        layer.confirm(msg, {
            btn: ['确认', '取消'],
            title: "提示",
            icon: icon || 2,
            skin: 'lr-layer',
        }, function (index) {
            callback(true, index);
            layer.close(index);
        }, function (index) {
            callback(false, index);
            layer.close(index);
        });
    };
    _site.open = function (options) {
        options = $.extend({
            type: 2,
            shadeClose: false,
            shade: [0.2, "#fff"],
            max: true,
            reload: true,
            area: ["700px", "70%"],
        }, options);
        var success = [];
        if (options.fit) {
            success.push(function (layero, index) {
                _top.layer.iframeAuto(index);
            });
        }
        if (options.success)
            success.push(options.success);
        if (success.length > 0) {
            options.success = function (layero, index) {
                var iframeWin = _top[layero.find('iframe')[0]['name']];
                $.each(success, function (i, fn) {
                    fn.call(this, layero, index, iframeWin);
                });
            };
        }
        var end = options.end;
        options.end = function (data) {
            if (data === undefined) {
                _top.site._layerList.pop();
            }
            if (end) end.call(this, data);
        };
        var id = _top.layer.open(options);
        _top.site._layerList.push(id);
    };
    _site.close = function (value) {
        var index = _top.site._layerList.pop();
        if (!!index) {
            _top.layer.close(index, value);
        }
    };



    window.site = _site;

}(jQuery, window);