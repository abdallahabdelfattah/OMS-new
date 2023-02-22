var localResources = {
    ar: {
        CharactersRemaining: "أحرف متبقية",
        DeleteConfirm: "هل أنت متأكد انك تريد الحذف؟",
        ErrorOccured: "لقد حدث خطأ",
        UnExpectedError: "لقد حدث خطأ غير متوقع برجاء المحاولة مرة أخرى",
        ChangeLangDescription: "حول إلى اللغة العربية",
        Arabic: "ع",
        Yes: 'نعم',
        No: 'لا',
        English: "إنجليزي",
        LoadingText: "جارى تحميل البيانات الخاصة بك.",
        Ok: "موافق",
        Cancel: "إلغاء",
        Close: "إغلاق"
    },
    en: {
        CharactersRemaining: "characters remaining",
        DeleteConfirm: "Are you sure you want to delete?",
        ErrorOccured: "Error Occured",
        UnExpectedError: "Unexpected Error Occured. Please try again later.",
        ChangeLangDescription: "Switch to English language",
        Arabic: "Arabic",
        English: "En",
        LoadingText: "Loading ...",
        Yes: 'Yes',
        No: 'No',
        Ok: "Ok",
        Cancel: "Cancel",
        Close: "Close"
    }
};

var $commons = (function () {

   

// ------------- Dates ------------------------------
    function parseDate(dateString, calendarType, dateFormat, lang){

        calendarType = calendarType || 'gregorian';
        dateFormat = (dateFormat || 'dd/mm/yyyy').toLowerCase();
        lang = lang || getCurrentLang();

        if(!$.calendars){
            return;
        }

         var calendar = $.calendars.instance(calendarType, lang);
         return calendar.parseDate(dateFormat, dateString);
    }

    function tryParseDate(dateString, dateFormat, calendarType, lang) {
        
        dateFormat = (dateFormat || 'dd/mm/yyyy').toLowerCase();
        lang = lang || getCurrentLang();

        if (!$.calendars) {
            return;
        }

        var parsedDate = null;
        var parseSuccess = false;
        
        try {
            var gregorianCalendar = $.calendars.instance(calendarType, lang);
            parsedDate = gregorianCalendar.parseDate(dateFormat, dateString);
            parseSuccess = true;
        } catch (e) {
        }


        return parsedDate;
    }

    function convertDates (dateObjToParse, fromCalName, toCalName) {
        var jd = dateObjToParse.toJD();
        return $.calendars.instance(toCalName).fromJD(jd);
    }
    
    function diffDates(startDateString, startDateWidgetSettings, endDateString, endDateWidgetSettings){

        var startDate = parseDate(startDateString, startDateWidgetSettings.calendarType, startDateWidgetSettings.dateFormat, startDateWidgetSettings.lang);
        var endDate = parseDate(endDateString, endDateWidgetSettings.calendarType, endDateWidgetSettings.dateFormat, endDateWidgetSettings.lang);

        var oneDay = 24 * 60 * 60 * 1000; // hours*minutes*seconds*milliseconds
        var diffDays = Math.round((endDate.toJSDate().getTime() - startDate.toJSDate().getTime())/(oneDay));

        return diffDays;

    }

    function tryDiffDates(startDateString, startDateCalType, endDateString, endDateCalType) {

        var startDate = tryParseDate(startDateString, 'dd/mm/yyyy', startDateCalType);
        var endDate = tryParseDate(endDateString, 'dd/mm/yyyy', endDateCalType);

        if (startDate == null) {
            throw "unable to parse start date";
        }

        if (endDate == null) {
            throw "unable to parse end date";
        }

        var oneDay = 24 * 60 * 60 * 1000; // hours*minutes*seconds*milliseconds
        var diffDays = Math.round((endDate.toJSDate().getTime() - startDate.toJSDate().getTime()) / (oneDay));

        return diffDays;

    }


    // ------------------------------- Localization and Culture Related Code -----------------------------

    function isArabic() {
        return getCurrentLang() === 'ar';
    }

    function isRtl() {
        return isArabic();
    }

    var cultureCookieName = 'user-culture';

    function getCurrentLang() {
        var lang = 'ar';
        var cultureFromCookie = readCookie(cultureCookieName);
        var langFromHtmlTag = $('html').attr('lang');

        if (cultureFromCookie !== undefined && cultureFromCookie !== "") {
            lang = (cultureFromCookie === 'ar-SA') ? 'ar' : 'en';
        } else if (langFromHtmlTag !== undefined && langFromHtmlTag !== "") {
            lang = langFromHtmlTag;
        }

        return lang;
    };

    function getLocalString(key) {
        return localResources[getCurrentLang()][key];
    };

    function getTimeSuffix(time) {

        if (isArabic())
            return time.replace(/am/g, " ص ").replace(/pm/g, " م ");
        else
            return time;
    };

    // ------------------- Application Behavior  --------------------------------------------------------------------

    function applyAutoPostBackAttribute() {
        $('[autopostback=true]').change(function () {
            $(this).closest('form').submit();
        });
    };

    function setCurrentCulture(cultureName) {
        if (cultureName) {
            setCookie(cultureCookieName, cultureName);
        }
    };

    function initLangSwitcher(elementId) {

        elementId = elementId || "lang-switcher";
        var langSwitcherAnchor = $("#" + elementId);
        var isIcon = false;

        if (!langSwitcherAnchor.length) {
            elementId = "lang-switcher-icon";
            langSwitcherAnchor = $("#" + elementId);
            isIcon = langSwitcherAnchor.length !== 0;
        }

        langSwitcherAnchor.attr("title",
            isArabic()
                ? localResources.en.ChangeLangDescription
                : localResources.ar.ChangeLangDescription);

        if (!isIcon) {
            langSwitcherAnchor.text(isArabic()
                ? localResources.en.English
                : localResources.ar.Arabic);
        }
        
        langSwitcherAnchor.click(function (e) {
            if (isArabic()) {
                setCookie(cultureCookieName, "en-US");
            } else {
                setCookie(cultureCookieName, "ar-SA");
            }

            document.location.reload(true);
            e.preventDefault();
        });
    };

    //allpy max length automatically from mvc vaidation attribute string
    function applyMaxLengthAttributes() {

        // for string values with stringlength attribute added
        $("input[data-val-length-max], textarea[data-val-length-max]").each(function () {
            var element = $(this);
            element.attr("maxlength", element.data().valLengthMax);
        });

        //for number values with number with range attribute added
        $("input[data-val-range-max][data-val-range-max], textarea[data-val-range-max][data-val-range-max]").each(function () {
            var element = $(this);
            element.attr("maxlength", element.data().valRangeMax.toString().length - 1);
        });
       
    };

    //character countdown in textarea
    function applyTextAreasCharCountDown() {
        $('textarea').keyup(function () {

            var textArea = $(this);

            var maxLength = textArea.attr('maxlength');
            if (!maxLength) {
                return;
            }

            //JavaScript is treating a newline as one character rather than two so when I try to insert a "max length" string into the database I get an error.
            //Detect how many newlines are in the textarea, then be sure to count them twice as part of the length of the input.
            var newlines = (textArea.val().match(/\n/g) || []).length;
            if (textArea.val().length + newlines > maxLength) {
                textArea.val(textArea.val().substring(0, maxLength - newlines));
            }

            var length = textArea.val().length;
            length = maxLength - length - newlines;

            if (!textArea.next('.textarea-countdown').length) {
                $('<span class="textarea-countdown help-block" style="direction:' +
                    (isRtl() ? 'rtl' : 'ltr') +
                    ';"><span>').insertAfter(textArea);
            }

            textArea.next('.textarea-countdown').text(length + ' ' + getLocalString('CharactersRemaining'));
        });
    };

    function initSubmitButtonOverlay() {
        $(':submit').click(function (evt) {

            if ($(this).hasClass("disable-overlay")) {
                return;
            }

            var form = $(this).parents('form:first');

            if (form && form.length && form.valid && form.valid()) {
                showOverlay(' ');
            } else {
                hideOverlay();
            }
        });

        $('a.enable-overlay').click(function (evt) {
            showOverlay(' ');
        });

    };

    function initRequiredFieldsMark() {

        $("[data-val-required][name], [data-val-requiredif][name]")
            .each(function () {
                $('label[for="' + $(this).attr("name").replace(".", "_") + '"]').not(":contains('*')")
                    .append('<span class="text-danger">*</span>');

            });

        //$('[data-val-required], [data-val-requiredif][name]')
        //    .each(function () {
        //        $('label[for="' + $(this).attr('name').replace(".", "_") + '"]:not(:has(>span))')
        //            .append('<span class="text-danger">*</span>');
        //    });


    };

    function printUrlContents(url) {

        if ($("#frmPrint").length) {
            $("#frmPrint").remove();
        }

        if (!url) {
            return false;
        }

        var html =
            '<iframe id="frmPrint" width="0" height="0" frameborder="0" name="frmPrint" src="' +
                url +
            '"></iframe>';

        $('body').after(html);

        var iframe = document.frames
            ? window.frames.frames['frmPrint']
            : document.getElementById('frmPrint');

        var ifWin = iframe.contentWindow || iframe;

        try {
            ifWin.focus();
            ifWin.print();
        } catch (e) {
            window.print(false);
            //or when you get Invalid calling object error for IE9 and above
            //set the browser into IE8 compatibility mode will work
        }

        return false;
    };

    function printElementContents(selector) {

        if ($(selector).length === 0) {
            return;
        }

        

        if ($("#frmPrint").length) {
            $("#frmPrint").remove();
        }

        var cssLinksText = '';
        $('link[rel="stylesheet"]')
            .each(function () {

               var outerHtml = $('<div>').append($(this).clone()).html();
               cssLinksText += outerHtml;
            });

        console.log(cssLinksText);

        var html = '';
        html += cssLinksText;
        html += $(selector).html();
        
        var iframeHtml =
            '<iframe id="frmPrint" width="0" height="0" frameborder="0" name="frmPrint"></iframe>';

        $('body').after(iframeHtml);

        var iframe = document.frames
            ? window.frames.frames['frmPrint']
            : document.getElementById('frmPrint');

        var ifWin = iframe.contentWindow || iframe;
        
        var dstDoc = iframe.contentDocument || iframe.contentWindow.document;
        dstDoc.write(html);
        dstDoc.close()

        try {
            ifWin.focus();
            ifWin.print();
            ifWin.colse();
        } catch (e) {
            // window.print(false);
            // or when you get Invalid calling object error for IE9 and above
            // set the browser into IE8 compatibility mode will work
        }

        return false;
    }

    function refreshFormValidation(selector) {
        var form = $(selector ? selector : 'form')
            .removeData("validator") // added by the raw jquery.validate plugin /
            .removeData("unobtrusiveValidation"); /* added by the jquery unobtrusive plugin*/
        if ($.validator && $.validator.unobtrusive) {
            $.validator.unobtrusive.parse(form);
        }        
    }

    function disableAllInputs(containerSelector) {
        if (containerSelector) {

            $(containerSelector).find('input[data-val-required],input[data-val-requiredif],select[data-val-required],select[data-val-requiredif]')
                .removeAttr('data-val-required');
            $(containerSelector).find('.text-danger').hide();
            $(containerSelector).find("input,textarea,select").not(".btn-default").not('.btn-primary').attr("disabled", "disabled");
            $('div.panel-heading-btn > input').hide();
        } else {

            $('input[data-val-required],input[data-val-requiredif],select[data-val-required],select[data-val-requiredif]')
                .removeAttr('data-val-required');
            $('.text-danger').hide();
            $("input,textarea,select").not(".btn-default").not('.btn-primary').attr("disabled", "disabled");
            $('div.panel-heading-btn > input').hide();
        }
        $(".date-picker-select").removeAttr("disabled");
    }

    function disableInputs(containerSelector) {
        disableAllInputs(containerSelector);
        $(document).ajaxStop(function () {
            disableAllInputs(containerSelector);
        });  
    }

    var InlineAlertCssClasses =
        {
            danger: "alert-danger",
            information: "alert-info",
            success: "alert-success",
            warning: "alert-warning"
        }

    function showMessageInlineAlert(containerSelector, messageType, message) {
        $(containerSelector).html('<div class="alert ' + messageType + '">' + message + '</div>');
    }

    function showSuccessInlineAlert(containerSelector, message) {
        showMessageInlineAlert(containerSelector, InlineAlertCssClasses.success, message);
    }

    function showErrorInlineAlert(containerSelector, message) {
        showMessageInlineAlert(containerSelector, InlineAlertCssClasses.danger, message);
    }

    function showInformationInlineAlert(containerSelector, message) {
        showMessageInlineAlert(containerSelector, InlineAlertCssClasses.information, message);
    }

    function showWarningInlineAlert(containerSelector, message) {
        showMessageInlineAlert(containerSelector, InlineAlertCssClasses.warning, message);
    }

    function initSelect(selector, data, selectedVal, isDisabled) {
        var ddl = $(selector);
       
        var selectItemOption = ddl.find('option:first');
        ddl.empty();
        if (selectItemOption.length) {
            ddl.append(selectItemOption);
        }

        if (isDisabled) {
            ddl.attr('disabled', 'disabled');
        }
      
        if (data && data.length) {
            ddl.removeAttr('disabled');
            $.each(data,
                function (index, d) {
                    ddl.append($("<option />").val(d.value).text(d.text));
                }
            );
        }

        if (selectedVal) {
            ddl.val(selectedVal);
        }

    }

    function initCheckBoxList(selector, data, propName) {
        var container = $(selector);

        container.empty();

        $.each(data,
            function (iteration, item) {
                if (data && data.length) {
                    container.append(
                        $(document.createElement('label')).attr({
                            'for': 'chk-' + iteration,
                            'class': 'checkbox-inline'
                        })
                            .append(
                            $(document.createElement("input")).attr({
                                type: 'checkbox',
                                id: propName,
                                name: propName,
                                value: item.value
                            }),
                            item.text
                            ));
                }
            });

    }

    function bindCheckBoxList(selector, data) {
        var container = $(selector);

        if (data && data.length) {
            $.each(data,
                function (i, e) {
                    $('input[type=checkbox][value=' + e + ']').prop('checked', true);
                });
        }
    }

    function showOverlay(loadingText) {

        loadingText = loadingText ? loadingText : getLocalString("LoadingText");

        if ($("#divOverlay").length) {
            $("#divOverlay").show();
            return;
        }

        var divOverlay = '<div id="divOverlay" class="loader-overlay" style="display:none">' +
            '<div class="loader-overlay-shape"></div>' +//'<img src="data:image/gif;base64,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" />' +
            '<div class="loader-overlay-text" id="dvOverLayText">' +
            loadingText +
            '</div>' +
            '</div>';

        $('body').after(divOverlay);
        $("#divOverlay").show();
    };

    function hideOverlay() {
        if ($("#divOverlay").length) {
            $("#divOverlay").hide();
        }
    };

    function showAlert(title, text, callback) {

        hideOverlay();

        var html =
            '<div id="divAlertModalMessage" class="modal fade">' +
            '<div class="modal-dialog">' +
            '<div class="modal-content">' +
            '<div class="modal-header">' +
            '<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>' +
            '<h4 class="modal-title">' +
            title +
            '</h4>' +
            '</div>' +
            '<div class="modal-body">' +
            text +
            '</div>' +
            '<div class="modal-footer">' +
            '<div class="text-left"><button type="button" class="btn btn-ok btn-default btn-lg btn-nor-primary">' +
            getLocalString("Close") +
            '</button></div>' +
            '</div>' +
            '</div>' +
            '</div>' +
            '</div>';

        $('body').after(html);

        $("#divAlertModalMessage button.btn-ok").click(function (e) {
            $("#divAlertModalMessage").modal('hide');

            if (isFunction(callback)) {
                callback();
            }
        });

        $("#divAlertModalMessage").on('show.bs.modal', function () {
            // z-index fix.
            // if you open modal alert from another modal dialogue.

            var zIndex = 1050 + (10 * $('.modal:visible').length);
            $(this).css('z-index', zIndex);

            setTimeout(function () {
                //$("#divAlertModalMessage").find('.modal-backdrop').not('.modal-stack').css('z-index', zIndex - 1).addClass('modal-stack');
                if ($("#divAlertModalMessage").length) {
                    $("#divAlertModalMessage").css('z-index', zIndex - 1);
                }
            }, 1);

        });

        $("#divAlertModalMessage").on("hidden.bs.modal",
            function () {
                $(this).data('bs.modal', null);
                $("#divAlertModalMessage").remove();
            });

        $("#divAlertModalMessage").modal('show');
    };

    function confirm(title, text, callback, okText, cancelText) {

        hideOverlay();

        if ($("#divConfirmMessage").length) {
            $(this).data('bs.modal', null);
            $("#divConfirmMessage").remove();
        }

        var html =
            '<div id="divConfirmMessage" class="modal fade">' +
            '<div class="modal-dialog">' +
            '<div class="modal-content">' +
            '<div class="modal-header">' +
            '<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>' +
            '<h4 class="modal-title">' +
            title +
            '</h4>' +
            '</div>' +
            '<div class="modal-body">' +
            text +
            '</div>' +
            '<div class="modal-footer">' +
            '<div class="text-left"><button type="button" class="btn btn-ok btn-lg btn-nor-primary">' +
            (okText || getLocalString("Ok")) +
            '</button><button type="button" class="btn btn-cancel btn-default btn-lg btn-nor-primary">' +
            (cancelText || getLocalString("Cancel")) +
            '</button></div>' +
            '</div>' +
            '</div>' +
            '</div>' +
            '</div>';

        $('body').after(html);

        $("#divConfirmMessage").modal('show');

        $("#divConfirmMessage button.btn-ok").on("click",
            function (e) {
                $("#divConfirmMessage").modal('hide');
                if (isFunction(callback)) {
                    callback();
                }
            });

        $("#divConfirmMessage button.btn-cancel").on("click",
            function (e) {
                $("#divConfirmMessage").modal('hide');
            });

        $("#divConfirmMessage").on('show.bs.modal', function () {
            // z-index fix.
            // if you open modal alert from another modal dialogue.

            var zIndex = 1050 + (10 * $('.modal:visible').length);
            $(this).css('z-index', zIndex);

            setTimeout(function () {
                //$("#divAlertModalMessage").find('.modal-backdrop').not('.modal-stack').css('z-index', zIndex - 1).addClass('modal-stack');
                if ($("#divAlertModalMessage").length) {
                    $("#divAlertModalMessage").css('z-index', zIndex - 1);
                }
                
            }, 1);

        });

        $("#divConfirmMessage").on("hidden.bs.modal",
            function () {
                $(this).data('bs.modal', null);
                $("#divConfirmMessage").remove();
            });

    };

    function confirmYesNo(title, text, callBack) {

        hideOverlay();

        var html =
            '<div id="divConfirmMessage" class="modal fade">' +
            '<div class="modal-dialog">' +
            '<div class="modal-content">' +
            '<div class="modal-header">' +
            '<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>' +
            '<h4 class="modal-title">' +
            title +
            '</h4>' +
            '</div>' +
            '<div class="modal-body">' +
            text +
            '</div>' +
            '<div class="modal-footer">' +
            '<div class="text-left">' +
            '<button type="button" class="btn btn-yes btn-lg btn-nor-primary">' +
            getLocalString("Yes") +
            '</button>' +
            '<button type="button" class="btn btn-no btn-lg btn-nor-primary">' +
            getLocalString("No") +
            '</button>' +
            '<button type="button" class="btn btn-cancel btn-default btn-lg btn-nor-primary">' +
            getLocalString("Cancel") +
            '</button>' +
            '</div></div>' +
            '</div>' +
            '</div>' +
            '</div>' +
            '</div>';

        $('body').after(html);

        $("#divConfirmMessage").modal('show');

        $("#divConfirmMessage button.btn-yes").on("click",
            function (e) {
                $("#divConfirmMessage").modal('hide');
                if (isFunction(callBack)) {
                    callBack(true);
                }
            });

        $("#divConfirmMessage button.btn-no").on("click",
            function (e) {
                $("#divConfirmMessage").modal('hide');
                if (isFunction(callBack)) {
                    callBack(false);
                }
            });

        $("#divConfirmMessage button.btn-cancel").on("click",
            function (e) {
                $("#divConfirmMessage").modal('hide');
            });

        $("#divConfirmMessage").on('show.bs.modal', function () {
            // z-index fix.
            // if you open modal alert from another modal dialogue.

            var zIndex = 1050 + (10 * $('.modal:visible').length);
            $(this).css('z-index', zIndex);

            setTimeout(function () {
                //$("#divAlertModalMessage").find('.modal-backdrop').not('.modal-stack').css('z-index', zIndex - 1).addClass('modal-stack');
                if ($("#divAlertModalMessage").length) {
                    $("#divAlertModalMessage").css('z-index', zIndex - 1);
                }
            }, 1);

        });

        $("#divConfirmMessage").on("hidden.bs.modal",
            function () {
                $(this).data('bs.modal', null);
                $("#divConfirmMessage").remove();
            });

    };

    function showModal(title, text, okButtonText, cancelButtonText, callback, hideOnOk) {

        hideOverlay();

        var cancelButtonHtml = cancelButtonText && $.trim(cancelButtonText) !== ''
            ? '<button type="button" class="btn btn-default btn-lg btn-nor-primary btn-primary btn-cancel">' + cancelButtonText
            : '';
        var html =
            '<div id="divModalContainer" class="modal fade">' +
            '<div class="modal-dialog">' +
            '<div class="modal-content">' +
            '<div class="modal-header">' +
            '<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>' +
            '<h4 class="modal-title">' +
            title +
            '</h4>' +
            '</div>' +
            '<div class="modal-body">' +
            text +
            '</div>' +
            '<div class="modal-footer">' +
            '<div class="text-left"><button type="button" class="btn-ok btn btn-lg btn-nor-primary">' +
            okButtonText +
            '</button>' +
            cancelButtonHtml +
            '</div>' +
            '</div>' +
            '</div>' +
            '</div>' +
            '</div>';

        $('body').after(html);

        $("#divModalContainer").modal('show');

        //refreshFormValidation('#frmDeActivateUserContent');

        $("#divModalContainer button.btn-ok").on("click",
            function (e) {

                if (hideOnOk === true) {
                    $("#divModalContainer").modal('hide');
                }

                if (isFunction(callback)) {
                    callback();
                }
            });

        $("#divModalContainer button.btn-cancel").on("click",
            function (e) {
                $("#divModalContainer").modal('hide');
            });

        $("#divModalContainer").on('show.bs.modal', function () {
            // z-index fix.
            // if you open modal alert from another modal dialogue.

            var zIndex = 1040 + (10 * $('.modal:visible').length);
            $(this).css('z-index', zIndex);

            setTimeout(function () {
                //$("#divAlertModalMessage").find('.modal-backdrop').not('.modal-stack').css('z-index', zIndex - 1).addClass('modal-stack');
                if ($("#divAlertModalMessage").length) {
                    $("#divAlertModalMessage").css('z-index', zIndex - 1);
                }
            }, 1);

        });

        $("#divModalContainer").on("hidden.bs.modal",
            function () {

                // Scrollbar fix
                // If you have a modal on your page that exceeds the browser height, then you can't scroll in it when closing an second modal.
                $('.modal:visible').length && $(document.body).addClass('modal-open');

                $(this).data('bs.modal', null);
                $("#divModalContainer").remove();

            });

    };

    function hideModal() {
        $("#divModalContainer").modal('hide');
        $("#divModalContainer").on("hidden.bs.modal",
            function () {

                // Scrollbar fix
                // If you have a modal on your page that exceeds the browser height, then you can't scroll in it when closing an second modal.
                $('.modal:visible').length && $(document.body).addClass('modal-open');

                $(this).data('bs.modal', null);
                $("#divModalContainer").remove();

            });
    };

    function confirmWithAjaxPost(title,
        confirmMsg,
        defaultSuccessMsg,
        defaultErrorMsg,
        url,
        dataObject,
        successCallBack) {

        hideOverlay();

        confirm(title,
            confirmMsg,
            function() {
                showOverlay(title);
                $.post(url,
                    dataObject,
                    function(data) {
                        hideOverlay();
                        if (data) {
                            if (data.success === true) {
                                if (data.successMessage || defaultSuccessMsg) {
                                    showAlert(title,
                                        data.successMessage || defaultSuccessMsg,
                                        function() {
                                            //showOverlay();
                                            if (isFunction(successCallBack)) {
                                                successCallBack(data);
                                            }
                                        });
                                } else if (isFunction(successCallBack)) {
                                    successCallBack(data);
                                }

                            } else if (data.errorMessage || defaultErrorMsg) {

                                showAlert(title, data.errorMessage || defaultErrorMsg);

                            }
                        }
                    });
            });
    }

    function showModalWithPartial(title, url, successCallback, closeCallback) {
        
        hideModal();
        hideOverlay();

        var html =
            '<div id="divModalContainer" class="modal fade">' +
            '<div class="modal-dialog">' +
            '<div class="modal-content">' +
            '<div class="modal-header">' +
            '<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>' +
            '<h4 class="modal-title">' +
            title +
            '</h4>' +
            '</div>' +
            '<div class="modal-body">' +
            '</div>' +
            '</div>' +
            '</div>' +
            '</div>';

        $('body').append(html);

        $(".modal-body").load(url,
            function () {

                hideOverlay();

                //$("#divModalContainer").modal('show');
                $("#divModalContainer").modal({
                    backdrop: 'static',
                    keyboard: false
                });

                initMvcFormPage();
                $commons.refreshFormValidation('#divModalContainer')

                var partialScripts = $.trim($(".modal-body").find("script[type='text/javascript']").last().html());

                if (partialScripts) {
                    var script = "<script type=\"text/javascript\"> " + partialScripts + " </script>";                    
                    $('body').append(script);//incorporates and executes inmediatelly
                }

                executePartialViewsScriptTags();
                $('.modal-backdrop').css('z-index', 1030);
            });

        $(document).off('submit').on('submit', '#divModalContainer form',
            function (e) {

                e.preventDefault();

                var form = this;
                var formData = new FormData(form);
                
                if(!$(form).valid()){

                    animateToFirstValidationError();
                    return;
                }

                $.ajax({    
                    url: form.action,
                    type: form.method,
                    //dataType: 'json',
                    processData: false,
                    contentType: false,
                    cache : false,
                    data: formData,
                    success: function (result) {

                        hideOverlay();

                        $(form).unbind();

                        if (result.success === true) {
                            $('#divModalContainer').modal('hide');
                            if (result.successMessage) {
                                showAlert(title,
                                    result.successMessage,
                                    function () {
                                        if (isFunction(successCallback)) {
                                            successCallback(result);
                                        }
                                    });
                            } else {
                                if (isFunction(successCallback)) {
                                    successCallback(result);
                                }
                            }

                        } else if (result.success === false) {
                            if (result.errorMessage && result.errorMessage.length) {
                                result.errorMessage = stringReplaceAll(result.errorMessage, ",", "</br>");

                                showAlert(title, result.errorMessage);

                                //$('.modal-body').html(result.errorMessage);

                            } else {
                                showAlert(title, getLocalString('ErrorOccured'));
                                //$('.modal-body').html(getLocalString('ErrorOccured'));
                            }

                        } else {

                            $('.modal-body').html(result);

                        }

                        initMvcFormPage();
                    },
                    error: function (result) {
                        $('.modal-body').html(getLocalString('ErrorOccured'));
                    }
                });
                return false;
            });

        $(document).on('click',
            '.btnCancel',
            function (e) {
                $("#divModalContainer").modal('hide');
            });

        $("#divModalContainer a.btn-cancel").on("click",
            function (e) {
                $("#divModalContainer").modal('hide');
            });

        $("#divModalContainer").on('show.bs.modal', function () {
            // z-index fix.
            // if you open modal alert from another modal dialogue.

            var zIndex = 1040 + (10 * $('.modal:visible').length);
            $(this).css('z-index', zIndex);

            setTimeout(function () {
                //$("#divAlertModalMessage").find('.modal-backdrop').not('.modal-stack').css('z-index', zIndex - 1).addClass('modal-stack');
                if ($("#divAlertModalMessage").length) {
                    $("#divAlertModalMessage").css('z-index', zIndex - 1);
                }
            }, 1);

        });

        $("#divModalContainer").on('hidden.bs.modal',
            function () {

                if (isFunction(closeCallback)) {
                    closeCallback();
                }

                if ($("#divModalContainer").find('.select2').length) {
                    var select2ddls = $("#divModalContainer").find('.select2-hidden-accessible');
                    select2ddls.each(function (i, item) {
                        $(item).select2("destroy");
                    });
                }

                // Scrollbar fix
                // If you have a modal on your page that exceeds the browser height, then you can't scroll in it when closing an second modal.
                $('.modal:visible').length && $(document.body).addClass('modal-open');

                $(this).data('bs.modal', null);
                $("#divModalContainer").remove();
            });

    };
	
	function ajaxSubmitForm(formCssSelector, formTitle, successCallback, isControllerActionReturnViewHtml, skipFormValidation)
	{
		return new Promise(function(resolve, reject) {

            var form = $(formCssSelector);

            if ($(form).length === 0) {
                return;
            }

            if(!skipFormValidation){
                refreshFormValidation(formCssSelector);
			
                if(form.valid() === false){
                    
                    animateToFirstValidationError();
                    return;
                }
            }

            showOverlay();

            var formData = new FormData(form[0]); //$(this).serialize()        

            $.ajax({    
                url: form[0].action,
                type: form[0].method,
                processData: false,
                contentType: false,
                cache : false,
                data: formData,
                success: function (result) {

                    hideOverlay();

                    $(form).unbind();

                    if(isControllerActionReturnViewHtml === true){
                        form.replaceWith(result);
                        if (isFunction(successCallback)) {
                            successCallback(result);
                        }
                        resolve(result);
                        return;
                    }
                    
                    if (result.success === true) {
           
                        if (result.successMessage) {
                            showAlert(formTitle,
                                result.successMessage,
                                function () {
                                    if (isFunction(successCallback)) {
                                        successCallback(result);
                                    }
                                });
                        } else {
                            if (isFunction(successCallback)) {
                                successCallback(result);
                            }
                        }
                        
                    } else if (result.success === false) {
                        if (result.errorMessage && result.errorMessage.length) {
                            result.errorMessage = stringReplaceAll(result.errorMessage, ",", "</br>");
                                //$(form).find('#CustomValidationSummary').html(result.errorMessage)
                            showAlert(formTitle, result.errorMessage);

                        } else {
                            //$(form).find('#CustomValidationSummary').html(result.errorMessage)
                            showAlert(formTitle, result.errorMessage);
                        }

                    } else {
                        showAlert(formTitle, result);

                    }
                    
                    resolve(result);
                },
				error: function (result) {
                    hideOverlay();
					showAlert(formTitle, getLocalString('ErrorOccured'));
				}
			});
		});		
	}

    function animateTo(cssSelector) {

        if (cssSelector && $(cssSelector).length) {
			$('html, body').animate({
				scrollTop: ($(cssSelector).offset().top - 300)
			}, 1000);
        }
    }

    function animateToFirstValidationError() {
        animateTo('.field-validation-error');
    }

    function redirectToUrl(url, appendReturnUrlParam) {
        if (!url) {
            return;
        }

        if ($.trim(url) === '') {
            return;
        }

        appendReturnUrlParam = appendReturnUrlParam === true;

        showOverlay();

        document.location.href =
            !appendReturnUrlParam ? url : updateQueryStringParameter('returnUrl', document.location.href, url);
    }


    function ajaxAppendPartial(containerSelector, url, successCallback) {

        if ($(containerSelector).length == 0) {
            return;
        }

        showOverlay();

        $(containerSelector).load(url,
            function (data) {
                
                hideOverlay();

                initMvcFormPage();

                var partialScripts = $.trim($(containerSelector).find("script[type='text/javascript']").last().html());
                if (partialScripts) {
                    var script = "<script type=\"text/javascript\"> " + partialScripts + " </script>";
                    $('body').append(script);//incorporates and executes inmediatelly
                }

                executePartialViewsScriptTags();

                if (isFunction(successCallback)) {
                    successCallback();
                }
            });
    };

    function initBtnBack() {

        var btnBack = $('#btnBack');
        var returnUrl = getQueryStringParameterByName('returnUrl');

        if (btnBack.length && btnBack.attr('href') === '#') {
            
            if (returnUrl && $.trim(returnUrl) !== '' ) {
                btnBack.click(function (e) {
                    e.preventDefault();
                    redirectToUrl(decodeURI(returnUrl));
                });
            } else {
                btnBack.text(getLocalString('Close'));
                btnBack.click(function (e) {
                    e.preventDefault();
                    window.close();
                });
            }

        }


    }
    

    function initBootstrapTabs(selector) {
        selector = selector ? selector : '.tabsContainer li a';
        $(selector).click(function (e) {
            e.preventDefault();
            $(this).tab('show');
        });
    }

    function activateBootstrapTab(tabId) {
        $('.nav-tabs a[href="#' + tabId + '"]').tab('show');
    };

    function initUploaderInfoTooltip() {
        $('.upload-terms-icn').on('mouseenter',
            function() {
                $(this).parent().find(".upload-terms").show();
            }).on('mouseleave',
            function() {
                $(this).parent().find(".upload-terms").hide();
            });
        //    $('.upload-terms-icn').off('hover').on('hover', function () {
        //        $(this).parent().find(".upload-terms").fadeToggle(100);
        //    });
    }

    function executePartialViewsScriptTags() {
        ////$("script[data-partial-view-script='true']").each(function (index) {
        ////    var partialViewScript = "<script type=\"text/javascript\"> " + $(this).html() + " </script>";
        ////    $('body').append(partialViewScript);//incorporates and executes inmediatelly
        ////});
    }

    // ------------------- Common Helper Functions ------------------------------------------------------------------

    function getRandomString() {
        var text = "";
        var possible = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        for (var i = 0; i < 5; i++)
            text += possible.charAt(Math.floor(Math.random() * possible.length));

        return text;
    }

    function swapElements(obj1, obj2) {
        // create marker element and insert it where obj1 is
        var temp = document.createElement("div");
        obj1.parentNode.insertBefore(temp, obj1);

        // move obj1 to right before obj2
        obj2.parentNode.insertBefore(obj1, obj2);

        // move obj2 to right before where obj1 used to be
        temp.parentNode.insertBefore(obj2, temp);

        // remove temporary marker node
        temp.parentNode.removeChild(temp);
    }


    function moveTableRow(tr, direction) {

        var selectedOption = $(tr);

        if (direction === 'down') {

            var nextOption = $(tr).next("tr");
            if ($(nextOption).text() !== "") {
                $(selectedOption).remove();
                $(nextOption).after($(selectedOption));
            }
        }

        if (direction === 'up') {

            var prevOption = $(tr).prev("tr");
            if ($(prevOption).text() !== "") {
                $(selectedOption).remove();
                $(prevOption).before($(selectedOption));
            }
        }

    }

    function stringReplaceAll(string, search, replacement) {
        return string.replace(new RegExp(search, 'g'), replacement);
    }

    function getQueryStringParameterByName(name) {
        name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
        var regex = new RegExp("[\\?&]" + name + "=([^&#]*)", 'gi');
         var  results = regex.exec(location.search);
        return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
    };

    function updateQueryStringParameter(key, value, url) {
        var uri = url || document.location.href;
        var re = new RegExp("([?&])" + key + "=.*?(&|$)", "i");
        var separator = uri.indexOf('?') !== -1 ? "&" : "?";
        if (uri.match(re)) {
            return uri.replace(re, '$1' + key + "=" + value + '$2');
        }
        else {
            return uri + separator + key + "=" + value;
        }
    }

    function htmlEncode(value) {
        //create a in-memory div, set it's inner text(which jQuery automatically encodes)
        //then grab the encoded contents back out.  The div never exists on the page.
        return $('<div/>').text(value).html();
    };

    function htmlDecode(value) {
        return $('<div/>').html(value).text();
    };

    function truncateString(string, count, suffix) {
        if (string === undefined || string === null)
            return "";
        if (string.length > count)
            return string.substring(0, string.substring(0, count).lastIndexOf(" ")) + suffix;
        else
            return string;
    };

    function isStringHasScriptTag(string) {
        var patt = new RegExp("<(.|\n)*?>");
        return patt.test(string);
    };

    function isFunction(functionToCheck) {
        var getType = {};
        return functionToCheck && getType.toString.call(functionToCheck) === '[object Function]';
    };

    function setCookie(name, value, days) {
        var expires;
        if (days) {
            var date = new Date();
            date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
            expires = "; expires=" + date.toGMTString();
        } else {
            expires = "";
        }
        document.cookie = name + "=" + value + expires + "; path=/";
    };

    function readCookie(name) {
        var nameEq = name + "=";
        var ca = document.cookie.split(';');
        for (var i = 0; i < ca.length; i++) {
            var c = ca[i];
            while (c.charAt(0) === ' ') c = c.substring(1, c.length);
            if (c.indexOf(nameEq) === 0) return c.substring(nameEq.length, c.length);
        }
        return null;
    };

    function eraseCookie(name) {
        setCookie(name, "", -1);
    }

    // ---------------------- Return Public Accessible Functions ---------------------------------------------------


    function initMvcFormPage() {
        $commons.applyMaxLengthAttributes();
        $commons.applyTextAreasCharCountDown();
        $commons.applyAutoPostBackAttribute();
        $commons.initLangSwitcher();
        $commons.initRequiredFieldsMark();
        $commons.initSubmitButtonOverlay();
        $commons.initUploaderInfoTooltip();
        $commons.initBootstrapTabs();
        $commons.executePartialViewsScriptTags();
        $commons.initBtnBack();
        $commons.refreshFormValidation();
    }

    var toReturn = {
        isArabic: isArabic,
        isRtl: isRtl,
        getTimeSuffix: getTimeSuffix,
        applyAutoPostBackAttribute: applyAutoPostBackAttribute,
        initLangSwitcher: initLangSwitcher,
        applyMaxLengthAttributes: applyMaxLengthAttributes,
        applyTextAreasCharCountDown: applyTextAreasCharCountDown,
        initSubmitButtonOverlay: initSubmitButtonOverlay,
        initRequiredFieldsMark: initRequiredFieldsMark,
        printUrlContents: printUrlContents,
        printElementContents: printElementContents,
        refreshFormValidation: refreshFormValidation,
        disableInputs: disableInputs,
        stringReplaceAll: stringReplaceAll,
        getQueryStringParameterByName: getQueryStringParameterByName,
        updateQueryStringParameter: updateQueryStringParameter,
        initSelect: initSelect,
        showAlert: showAlert,
        confirm: confirm,
        hideModal: hideModal,
        showModal: showModal,
        confirmWithAjaxPost: confirmWithAjaxPost,
        showModalWithPartial: showModalWithPartial,
        confirmYesNo: confirmYesNo,
        showOverlay: showOverlay,
        hideOverlay: hideOverlay,
        bindCheckBoxList: bindCheckBoxList,
        initCheckBoxList: initCheckBoxList,
        htmlEncode: htmlEncode,
        htmlDecode: htmlDecode,
        truncateString: truncateString,
        isStringHasScriptTag: isStringHasScriptTag,
        isFunction: isFunction,
        setCookie: setCookie,
        readCookie: readCookie,
        eraseCookie: eraseCookie,
        initBootstrapTabs: initBootstrapTabs,
        activateBootstrapTab: activateBootstrapTab,
        executePartialViewsScriptTags: executePartialViewsScriptTags,
        getCurrentLang: getCurrentLang,
        moveTableRow: moveTableRow,
        swapElements: swapElements,
        ajaxAppendPartial: ajaxAppendPartial,
        initUploaderInfoTooltip: initUploaderInfoTooltip,
        setCurrentCulture: setCurrentCulture,
        showSuccessInlineAlert: showSuccessInlineAlert,
        showErrorInlineAlert: showErrorInlineAlert,
        showInformationInlineAlert: showInformationInlineAlert,
        showWarningInlineAlert: showWarningInlineAlert,
        animateTo: animateTo,
        animateToFirstValidationError: animateToFirstValidationError,
        ajaxSubmitForm: ajaxSubmitForm,
        redirectToUrl: redirectToUrl,
        initBtnBack: initBtnBack,
        initMvcFormPage: initMvcFormPage,
        diffDates: diffDates,
        tryDiffDates: tryDiffDates,
        getRandomString: getRandomString
    };
    return toReturn;
})();


// ---------------------------------- Validation Attributes for custom validations ------------------------------------------- //



if (jQuery.validator && jQuery.validator.unobtrusive) {


    // CurrentEntryLanguageTextAttribute Attribute

    jQuery.validator.unobtrusive.adapters.add('iscurrententrylanguage',
        ['allowpattern'], function (options) {
            // simply pass the options.params here
            options.rules['iscurrententrylanguage'] = options.params;
            options.messages['iscurrententrylanguage'] = function () { return $(options.element).attr('data-val-iscurrententrylanguage'); };//options.message;
        });

    jQuery.validator.addMethod('iscurrententrylanguage', function (value, element, params) {

        var parts = $(element);

        if (!parts.length) return true;
        
        if (parts.val() === "") { return true; }

        var errors = [];

        //check file type
        if (params.allowpattern) {
            var regex = new RegExp(params.allowpattern);
            if ((regex.test(parts.val()))) {
                return true;
            }
        }
        
        return false;


    }, '');


    // enable validaitions on specific input hidden fields too!

    $.validator.setDefaults({ ignore: ':hidden:not(.do-not-ignore-validation)' });

    //must be true attribute client validation
    if (jQuery.validator && jQuery.validator.unobtrusive) {
        //must be true attribute
        jQuery.validator.unobtrusive.adapters.addBool("mustbetrue");

        jQuery.validator.addMethod('mustbetrue', function (value, element, params) {
            return element.checked;
        }, '');
    }

    // date validation to support ummalqura and also gregorian dates
    jQuery.validator.methods.date = function (value, element) {

        var ele = $(element);
       
        var widSettings = {
            dateFormat: "dd/mm/yyyy",
            lang: "ar",
            calendarType: ele.parents('.datePickerContainer').find('select').val() || 'gregorian'
        };        

        var calendar = $.calendars.instance(widSettings.calendarType, widSettings.lang);
        calendar.dateFormat = widSettings.dateFormat;

        try {
            calendar.parseDate(widSettings.dateFormat, ele.val());
            return true;
        } catch (e) {
            return false;
        }
    };


    // RequiredAttribute with AllowEmptyString=true in ASP.NET MVC unobtrusive validation

    jQuery.validator.methods.oldRequired = jQuery.validator.methods.required;

    jQuery.validator.addMethod("required", function (value, element, param) {
            if ($(element).attr('data-val-required-allowempty') === 'true') {
                return true;
            } else if ($.trim(value) === '') {
                return false;
            }

            return jQuery.validator.methods.oldRequired.call(this, value, element, param);
        },
        jQuery.validator.messages.required // use default message
    );


    // IsDateAfterAttribute Attribute

    jQuery.validator.unobtrusive.adapters.add('isdateafter',
        ['propertytested', 'allowequaldates'], function (options) {
            // simply pass the options.params here
            options.rules['isdateafter'] = options.params;
            options.messages['isdateafter'] = function () { return $(options.element).attr('data-val-isdateafter'); };//options.message;
        });

    jQuery.validator.addMethod('isdateafter', function (value, element, params) {

        var parts = element.name.split(".");
        var prefix = "";

        if (parts.length > 1)
            prefix = parts[0] + ".";

        var startDateElement =  $('input[name="' + prefix + params.propertytested + '"]');
        var endDateElement = $(element);

        var startDateWidgetSettings = JSON.parse(startDateElement.attr('data-widget-settings'));
        var endDateWidgetSettings = JSON.parse(endDateElement.attr('data-widget-settings'));

        var startdatevalue = startDateElement.val();
        if (!value || !startdatevalue)
            return true;
        
        var isValid = true;
        
        if(params.allowequaldates === 'True') {
            //isValid = Date.parse(startdatevalue) <= Date.parse(value);
           var dateDiffInDays = $commons.diffDates(startdatevalue, startDateWidgetSettings, value, endDateWidgetSettings);
           isValid = dateDiffInDays >= 0;

        }  else{
            //isValid = Date.parse(startdatevalue) < Date.parse(value);
            var dateDiffInDays = $commons.diffDates(startdatevalue, startDateWidgetSettings, value, endDateWidgetSettings);
            isValid = dateDiffInDays > 0;
        }

        return isValid;
            

    }, '');

// DateRestrictionAttribute

    jQuery.validator.unobtrusive.adapters.add('daterestriction',
        ['todaydate', 'daterestrictiontype'], function (options) {
            // simply pass the options.params here
            options.rules['daterestriction'] = options.params;
            options.messages['daterestriction'] = function () { return $(options.element).attr('data-val-daterestriction'); };//options.message;
        });

    jQuery.validator.addMethod('daterestriction', function (value, element, params) {

        var todayDate = params.todaydate;
        var selectedDate = value;
        
        if (!value || !todayDate)
            return true;

        var isValid = false;
        var calendarType = $(element).parents('.datePickerContainer').find('select').val() || 'gregorian';
        var dateDiffInDays = $commons.tryDiffDates(todayDate, 'gregorian', selectedDate, calendarType);

        switch (params.daterestrictiontype) {

            case "futureOnly":                
                isValid = dateDiffInDays > 0;
                break;

            case "futureIncludingToday":
                if (dateDiffInDays >= 0) {
                    isValid = true; 
                }
                break;

            case "pastOnly":
                isValid = dateDiffInDays < 0;
                break;

            case "pastIncludingToday":
                if (dateDiffInDays <= 0) {
                    isValid = true;
                }
                break;

            default:
                isValid = true;
                break;
        }
        
        return isValid;


    }, '');


// GreaterThanAttribute Attribute

jQuery.validator.unobtrusive.adapters.add('greaterthan',
['propertytested', 'allowequalvalues'], function (options) {
    // simply pass the options.params here
    options.rules['greaterthan'] = options.params;
    options.messages['greaterthan'] = function () { return $(options.element).attr('data-val-greaterthan'); };//options.message;
});

jQuery.validator.addMethod('greaterthan', function (value, element, params) {

    if (value.length === 0 && this.optional(element)) {
        return true;
    }

    if (isNaN(parseInt(value))) {
        amountNumberValidationError = $sharedResources.NumberOnlyErrorMessage;
        return false;
    }

    var parts = element.name.split(".");
    var prefix = "";

    if (parts.length > 1)
        prefix = parts[0] + ".";

    var minValElemnet =  $('input[name="' + prefix + params.propertytested + '"]');
    var maxValElement = $(element);

    var minValue = minValElemnet.val();

    if (!value || !minValue)
        return true;

    var isValid = true;

    if(params.allowequalvalues === 'True') {
    isValid = value >= minValue;

    }  else{
        isValid = value > minValue;
    }

    return isValid;
    

}, '');


}

$(function () {

    $commons.initMvcFormPage();

    if ($("#divOverlay").length) {
        $("#divOverlay").remove();
    }

    //hide empty notifications
    //workaround!
    if ($('.alert-danger ul li').length && $('.alert-danger ul li').html() === '') {
        $('.alert-danger').hide();
    }
});

window.parseBoolean = function (string) {
    var bool;
    bool = (function () {
        switch (false) {
            case string.toLowerCase() !== 'true':
                return true;
            case string.toLowerCase() !== 'false':
                return false;
        }
    })();
    if (typeof bool === "boolean") {
        return bool;
    }
    return void 0;
};