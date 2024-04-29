window.setCookie = (name, value) => {
    document.cookie = `${name}=${value}; path=/;`;
};

window.document.getCookie = function(name) {
    var match = document.cookie.match(new RegExp('(^| )' + name + '=([^;]+)'));
    if (match) {
        return match[2];
    }
    return null;
};