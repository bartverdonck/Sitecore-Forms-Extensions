$.validator.setDefaults({ ignore: ":hidden:not(.fxt-captcha)" });

/**
 * Google Recaptcha
 */
var reCaptchaArray = reCaptchaArray || [];
$.validator.unobtrusive.adapters.add("recaptcha", function (options) {
    options.rules["recaptcha"] = true;
    if (options.message) {
        options.messages["recaptcha"] = options.message;
    }
});

$.validator.addMethod("recaptcha", function (value, element, exclude) {
    return true;
});
var recaptchasRendered = false;
var loadReCaptchas = function () {
    for (var i = 0; i < reCaptchaArray.length; i++) {
        var reCaptcha = reCaptchaArray[i];
        if (reCaptcha.IsRendered === undefined) {
            reCaptcha.IsRendered = true;
            reCaptcha();
        }
    }
};

/**
 * File upload Content Type
 */
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

/**
 * File upload File Size
 */
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

// Date Time Span Validator
["timespan", "tsminagedate", "tsfuturedate", "tspastdate"].forEach(validationType =>{
  $.validator.unobtrusive.adapters.add(validationType, ['min', 'max', 'unit'], function(options) {
    options.rules[validationType] = [options.params.min, options.params.max, options.params.unit];
    options.messages[validationType] = options.message;
  });
  
  $.validator.addMethod(validationType, function (value, element, params) {
    if (!this.optional(element)) {
      var unit = params[2];
      var minvalue = params[0];
      var maxvalue = params[1];
  
      var valueToValidate = 0;
  
      switch (unit) {
        case 'days':
          valueToValidate = getDays(value);
          break;
        case 'months':
          valueToValidate = getMonths(value);
          break;
        case 'years':
          valueToValidate = getYears(value);
          break;
      }
  
      var isValid = true;
  
      if (typeof minvalue !== 'undefined' && valueToValidate < minvalue)
        isValid = false;
  
      if (typeof maxvalue !== 'undefined' && valueToValidate > maxvalue)
        isValid = false;
  
      return isValid;
    }
    return true;
  });
})

/*
$.validator.unobtrusive.adapters.add('timespan', ['min', 'max', 'unit'], function(options) {
  options.rules['timespan'] = [options.params.min, options.params.max, options.params.unit];
  options.messages['timespan'] = options.message;
});

$.validator.addMethod("timespan", timespanValidatorFunction);*/

function getDays(date) {
  var today = new Date();
  return Math.floor((today - new Date(date)) / (1000 * 60 * 60 * 24));
}

function getYears(date) {
  var today = new Date();
  var diffYears = today.getFullYear() - new Date(date).getFullYear();
  var temp = today;

  temp.setFullYear(temp.getFullYear() - diffYears);

  if (new Date(date) > temp)
    diffYears--;

  return diffYears;
}

function getMonths(date) {
  var today = new Date();
  var d = new Date(date);

  return (today.getFullYear() - d.getFullYear()) * 12 + today.getMonth() - d.getMonth();
}