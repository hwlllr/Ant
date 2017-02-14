if (typeof (ls) == "undefined") ls = {};

//修改页面
ls.Upload = {
    //初始化图片上传  e上传元素, fileType文件类型, systemType所属系统, uploadFile上传成功显示的元素, fileName标识上传名称, onlyName标识保存名称-用来再度获取
    'uploadStart': function (e, uploadFile, fileName, onlyName) {
        
        $(e).uploadify({
            'formData': {
                'timestamp': '',
                'token': ''
            },
            //路径
            'swf': '/Frame/ANT/fonts/uploadify.swf',
            //处理程序
            'uploader': '/List/EditPortrait',
            //手指
            'buttonCursor': 'hand',
            //按钮名称
            'buttonText': '请选择上传的图片',
            //支持大小
            'fileSizeLimit': '10MB',
            //容器
            'queueID': 'queue',
            //提示支持格式
            'fileTypeDesc': '支持的格式：jpg,png,gif,jpge,ai,dwg',
            //同上
            'fileTypeExts': '*.jpg;*.jpge;*.gif;*.png;*.ai;*.dwg',
            //上传数量
            'queueSizeLimit': 12,
            //上传成功
            'onUploadSuccess': function (file, data, response) {
               eval("data=" + data);
                if (data.Success== true) {
                    var str = '<div class="col-md-3 imgdiv2"><div class="imgdiv"><div class="imgdivs"><span class="imgdelete glyphicon glyphicon-remove" onclick="ls.Upload.imgDelete(this)"></span><a target="_blank" href="' + data.FilePath + '"><img src="' + data.FilePath + '" /></a><input type="hidden" name="' + fileName + '" value="' + file.name + '" /><input type="hidden" name="' + onlyName + '" value="' + data.FilePath + '" /></div></div></div>';
                    $(uploadFile).append(str);
                } else {
                    alert(data.Message);
                }
            },
            //返回一个错误，选择文件的时候触发
            'onSelectError': function (file, errorCode, errorMsg) {
                debugger
                switch (errorCode) {
                    case -100:
                        alert("上传的文件数量已经超出系统限制的" + $(e).uploadify('settings', 'queueSizeLimit') + "个文件！");
                        break;
                    case -110:
                        alert("文件 [" + file.name + "] 大小超出系统限制的" + $(e).uploadify('settings', 'fileSizeLimit') + "大小！");
                        break;
                    case -120:
                        alert("文件 [" + file.name + "] 大小异常！");
                        break;
                    case -130:
                        alert("文件 [" + file.name + "] 类型不正确！");
                        break;
                }
            },
        });
    },
    //初始化文件上传
    'uploadFileStart': function (e, uploadFile, fileName, onlyName) {
        
        $(e).uploadify({
            'formData': {
                'timestamp': '',
                'token': ''
            },
            //路径
            'swf': '/Frame/ANT/fonts/uploadify.swf',
            //处理程序
            'uploader': '/FundsUpload/EditPortrait',
            //手指
            'buttonCursor': 'hand',
            //按钮名称
            'buttonText': '请选择上传的文件',
            //支持大小
            'fileSizeLimit': '20MB',
            //容器
            'queueID': 'queue',
            //提示支持格式
            'fileTypeDesc': '支持的格式：txt,xls,xlsx,doc,docx,ppt,pptx,pdf,jpg,png,gif,jpge,ai,dwg',
            //同上
            //'fileTypeExts': '*.jpg;*.jpge;*.gif;*.png;*.xlsx',(hanxinlu,2016-06-03)
            'fileTypeExts': '*.txt;*.xls;*.xlsx;*.doc;*.docx;*.ppt;*.pptx;*.pdf;*.jpg;*.jpge;*.gif;*.png;*.ai;*.dwg',
            //上传数量
            'queueSizeLimit': 12,
            //上传成功
            'onUploadSuccess': function (file, data, response) {
                eval("data=" + data);
                if (data.Success== true) {
                    var str = '<div><div><div><span class="glyphicon glyphicon-trash filedelete" onclick="ls.Upload.imgDelete(this)"></span><a target="_blank" href="' + data.FilePath + '">' + file.name + '</a><input type="hidden" name="' + fileName + '" value="' + file.name + '" /><input type="hidden" name="' + onlyName + '" value="' + data.FilePath + '" /></div></div></div>';
                    $(uploadFile).append(str);
                }
                else {
                    alert(data.Message);
                }
            },
            //返回一个错误，选择文件的时候触发
            'onSelectError': function (file, errorCode, errorMsg) {
                switch (errorCode) {
                    case -100:
                        alert("上传的文件数量已经超出系统限制的" + $(e).uploadify('settings', 'queueSizeLimit') + "个文件！");
                        break;
                    case -110:
                        alert("文件 [" + file.name + "] 大小超出系统限制的" + $(e).uploadify('settings', 'fileSizeLimit') + "大小！");
                        break;
                    case -120:
                        alert("文件 [" + file.name + "] 大小异常！");
                        break;
                    case -130:
                        alert("文件 [" + file.name + "] 类型不正确！");
                        break;
                }
            },
        });
    },
    //删除图片
    'imgDelete': function (e) {
        $(e).parent().parent().parent().remove();
    },
    //获取所有上传名称
    'fileNames': function (s) {
        var str = '';
        var list = $('input[name="' + s + '"]');
        list.each(function () {
            str += $(this).val() + 'HA分_割AH';
        })
        str = str.substr(0, str.length - 7);
        return str;
    }
}