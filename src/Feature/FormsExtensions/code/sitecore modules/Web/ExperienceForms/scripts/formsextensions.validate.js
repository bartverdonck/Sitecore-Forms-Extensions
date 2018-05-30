$.validator.setDefaults({ ignore: "" });

$.validator.unobtrusive.adapters.add("recaptcha", function (options) {
    options.rules["recaptcha"] = true;
    if (options.message) {
        options.messages["recaptcha"] = options.message;
    }
});

$.validator.addMethod("recaptcha", function (value, element, exclude) {
    return true;
});

$.validator.unobtrusive.adapters.addSingleVal("contenttype", "allowedcontenttypes");

$.validator.addMethod("contenttype", function (value, element, allowedcontenttypes) {
    if (!this.optional(element)) {
        for (var i = 0; i < element.files.length; i++) {
            if (allowedcontenttypes.indexOf(element.files[i].type) < 0) {
                return false;
            }
        }
    }
    return true;
});

$.validator.unobtrusive.adapters.addSingleVal("filesize", "maxfilesize");

$.validator.addMethod("filesize", function (value, element, maxfilesize) {
    if (!this.optional(element)) {
        for (var i = 0; i < element.files.length; i++) {
            if (element.files[i].size > maxfilesize) {
                return false;
            }
        }
    }
    return true;
});