Date.prototype.format = function (format) {
    var o = {
        "M+": this.getMonth() + 1, //month 
        "d+": this.getDate(), //day 
        "h+": this.getHours(), //hour 
        "m+": this.getMinutes(), //minute 
        "s+": this.getSeconds(), //second 
        "q+": Math.floor((this.getMonth() + 3) / 3), //quarter 
        "S": this.getMilliseconds() //millisecond 
    }

    if (/(y+)/.test(format)) {
        format = format.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    }

    for (var k in o) {
        if (new RegExp("(" + k + ")").test(format)) {
            format = format.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length));
        }
    }
    return format;
}




/////////过滤器
Vue.filter('jsondateconvert', function (value) {
   
    value=value||"";
        if ((value+'').indexOf('\/Date') == -1) {
            return value;
        }
        else {
            return eval(value.replace(/\/Date\((\d+)\)\//gi, "new Date($1)")).format('yyyy-MM-dd hh:mm:ss');
        }
    
  
})
//////


var Portal = {};
Portal.utility = {
    ////取网址里的参数
    GetQueryString: function (name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
        var r = window.location.search.substr(1).match(reg);
        if (r != null) return unescape(r[2]); return null;
        //if (r != null) return decodeURIComponent(r[2]); return null;
    },

    GetRootPath: function () {      
         
            var curWwwPath = window.document.location.href;
       
            var pathName = window.document.location.pathname;
            var pos = curWwwPath.indexOf(pathName);
          
            var localhostPaht = curWwwPath.substring(0, pos);
          
            var projectName = pathName.substring(0, pathName.substr(1).indexOf('/') + 1);
            return (localhostPaht + projectName);       

    }


};


Array.prototype.indexOf = function (val) {
    for (var i = 0; i < this.length; i++) {
        if (this[i] == val)
            return i;
    }
    return -1;
};

Array.prototype.remove = function (val) {
    var index = this.indexOf(val); if (index > -1) {
        this.splice(index, 1);
    }
};
Vue.http.headers.common['httpclient'] = "vue";
Vue.http.interceptors.push(function(request, next) {
    // continue to next interceptor
    next(function(response)  {
        // modify response
        var url = Portal.utility.GetRootPath + "/error.html";
        if (response.data.error == 200001) {
            //   top.window.location.href = url + "?err=" + response.data.message;
        };
    });
});

