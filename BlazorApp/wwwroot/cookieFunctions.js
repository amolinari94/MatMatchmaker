window.setCookie = (name, value) => {
    document.cookie = `${name}=${value}; path=/;`;
};

window.getCookie = function(name) {
    var match = document.cookie.match(new RegExp('(^| )' + name + '=([^;]+)'));
    if (match) {
        return match[2];
    }
    return null;
};

//this needs to be triggered at some point to delete on session end, or set to a time after login/inactivity
window.deleteCookie = function(name) {
    document.cookie = name + "=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
};