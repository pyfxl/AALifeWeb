//trim()
String.prototype.trim = function () {
    return this.replace(/(^\s*)|(\s*$)/g, "");
}

//ltrim()
String.prototype.ltrim = function () {
    return this.replace(/(^\s*)/g, "");
}

//rtrim()
String.prototype.rtrim = function(){
    return this.replace(/(\s*$)/g, "");
}

//取价格dec
function getprice(obj) {
    obj.value = obj.value.replace(/[^0-9\.\-]/g, '');
    if (obj.value.split('.').length > 2 || obj.value.split('-').length > 2 || obj.value.indexOf('-') > 0) {
        obj.value = obj.value.substr(0, obj.value.length - 1);
    }
}

//取价格int
function getpriceint(obj) {
    obj.value = obj.value.replace(/[^0-9]/g, '');
}
