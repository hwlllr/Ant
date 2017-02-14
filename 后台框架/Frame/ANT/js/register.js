if (typeof (ant) == "undefined") ant = {};

ant.Common = {
    //初始化页面标签功能
    "select2": function () {
        //Initialize Select2 Elements
        $(".select2").select2();

    },
    "date": function () {
        //时间控件绑定-hwl
        $('.setDate').daterangepicker({
            "showDropdowns": true,
            "singleDatePicker": true,
            "minDate": "2010/01/01",
            "maxDate": "2030/12/31"
        })
        $('.setDateList').daterangepicker({
            "showDropdowns": true,
            "autoApply": true,
            "minDate": "2010/01/01",
            "maxDate": "2030/12/31"
        })
    },
    "iCheck": function () {

        $('#chkall').on('ifChecked', function (event) {
            $('input[type="checkbox"]').iCheck('check');
        });
        $('#chkall').on('ifUnchecked', function (event) {
            $('input[type="checkbox"]').iCheck('uncheck');
        });
        //iCheck for checkbox and radio inputs
        $('input[type="checkbox"].minimal, input[type="radio"].minimal').iCheck({
            checkboxClass: 'icheckbox_minimal-blue',
            radioClass: 'iradio_minimal-blue'
        });

        $('input[type="checkbox"].flat-red, input[type="radio"].flat-red').iCheck({
            checkboxClass: 'icheckbox_flat-green',
            radioClass: 'iradio_flat-green'
        });
    },
    'page': function (ye,page) {//分页公共
        if (ye * 1 == 0) {
            ye = 1;
        }
        var start = page - 2;//起始页
        if (start < 1)
            start = 1;
        var end = page + 2;//末页

        if (end > ye)
            end = ye;
        var top = page - 1;//上一页
        var next = page + 1;//下一页

        var strpage = "";
        if (page == 1) {
            strpage += '{type:"disabled",name:"上一页",value:' + top + '},';
        } else {
            strpage += '{type:"show",name:"上一页",value:' + top + '},';
        }

        for (var i = start; i <= end; i++) {
            if (i == page) {
                strpage += '{type:"active",name:"' + i + '",value:' + i + '},';
            }
            else {
                strpage += '{type:"show",name:"' + i + '",value:' + i + '},';
            }
        }
        if (page == ye) {
            strpage += '{type:"disabled",name:"下一页",value:' + next + '}';
        }
        else {
            strpage += '{type:"show",name:"下一页",value:' + next + '}';
        }
        return eval("[" + strpage + "]");
    }

}

