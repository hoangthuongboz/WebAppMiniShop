HTMLCollection.prototype.click = function (f) {
    // body...
    for (var i = this.length - 1; i >= 0; i--) {
        this[i].onclick = f;
    }
};
NodeList.prototype.change = function (f) {
    // body...
    for (var i = this.length - 1; i >= 0; i--) {
        this[i].onchange = f;
    }
};
//alert(chk);
HTMLElement.prototype.change = function (f) {
    this.onchange = f;
};
//alert(document.getElementsByName('ids'));
NodeList.prototype.check = function (v) {
    for (var i = 0; i < this.length; i++) {
        this[i].checked = v;
    }
};
function confirmDelete() {
    return confirm('Are you sure Delete?');
}

function toData(o) {
    var a = new Array();
    for (var k in o) {
        a.push(k + '=' + o[k]);
    }
    return a.join('&');
}

var ajax = {};
ajax.post = function (o, url, f) {
    var xhr = new XMLHttpRequest();
    xhr.open('POST', url);
    xhr.setRequestHeader('Content-type', 'application/x-www-form-urlencoded');
    var data = toData(o);
    xhr.send(data);
    xhr.onload = function () {
        //alert(xhr.response);
        f.call(this, xhr.response);
    }
};