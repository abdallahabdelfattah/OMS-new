ko.bindingHandlers.numeric = {
    init: function (element) {
        $(element).on("keydown", function (event) {
            // Allow: backspace, delete, tab, escape, and enter
            if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 || event.keyCode == 13 ||
                // Allow: Ctrl+A
                (event.keyCode == 65 && event.ctrlKey === true) ||
                // Allow: . ,
                (event.keyCode == 188 || event.keyCode == 110) ||//|| event.keyCode == 190 
                // Allow: home, end, left, right
                (event.keyCode >= 35 && event.keyCode <= 39)) {
                // let it happen, don't do anything
                return;
            }
            else {
                // Ensure that it is a number and stop the keypress
                if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105)) {
                    event.preventDefault();
                }
            }
        });
    }
};

ko.bindingHandlers.datepicker = {
    init: function (element, valueAccessor, allBindingsAccessor) {
        var $el = $(element);
        //$el.width('89%');
        var startDate = new Date();

        if (allBindingsAccessor.has('startDate')){
            startDate = Date.create(allBindingsAccessor.get('startDate'), 'fr');
        }

        //initialize datepicker with some optional options
        var options = {
            showOn: "button",
            changeMonth: true, changeYear: true, buttonImageOnly: true,
            dateFormat: 'yy-mm-dd', /*minDate: startDate,*/ buttonImage: '../images/calendar.png'
        };
        $el.datepicker(options);

        //handle the field changing
        ko.utils.registerEventHandler(element, "change", function () {
            var observable = valueAccessor();
            var value = $el.datepicker("getDate");
            if (value != null)
                observable(Date.create(value, 'fr').format('{yyyy}-{MM}-{dd}'));
            else
                observable(null);
        });

        //handle disposal (if KO removes by the template binding)
        ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
            $el.datepicker("destroy");
        });

    },
    update: function (element, valueAccessor) {
        var value = ko.utils.unwrapObservable(valueAccessor()),
            $el = $(element),
            current = $el.datepicker("getDate");

        if (value - current !== 0) {
            if (value != null)
                $el.datepicker("setDate", Date.create(value, 'fr').format('{yyyy}-{MM}-{dd}'));
            else
                $el.datepicker("setDate", null);
        }
    }
};

ko.bindingHandlers.fileUpload = {
    init: function (element, valueAccessor) {
        var prgid = "prg_"+Date.create().format("{yyyy}_{MM}_{dd}_{hh}_{mm}_{ss}");
        $(element).data("progressID", prgid)
        //$(element).after('<div id='+prgid+' class="progress"><div class="bar"></div><div class="percent">0%</div></div><div class="progressError"></div>');
    },
    update: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        var options = ko.utils.unwrapObservable(valueAccessor()),
            property = ko.utils.unwrapObservable(options.property),
            url = ko.utils.unwrapObservable(options.url),
            fileTypes = ko.utils.unwrapObservable(options.fileTypes),
            callback = ko.utils.unwrapObservable(options.callback);
        fileTypes = ".jpg,.png,.gif,.jpeg,.jfif,.exif,.tiff,.bmp,.svg,.ico";
            var fileTypeArr = fileTypes.split(',');
            var count = 0;
            if (property && url) {
            $(element).change(function () {
                count = 0;

                if (element.files.length > 0) {
                    for (var i = 0; i < element.files.length; i++) {
                        fileName = element.files[i].name;
                        //check file type
                        for (var j = 0; j < fileTypeArr.length; j++) {
                            if (fileName.toLowerCase().endsWith(fileTypeArr[j])) {
                                count ++;
                            } 
                        }
                    }

                }

                if (count == element.files.length) {
                    // this uses jquery.form.js plugin
                    $(element.form).ajaxSubmit({
                        url: url,
                        type: "POST",
                        dataType: "text",
                        headers: { "Content-Disposition": "attachment; filename=" + fileName },
                        beforeSubmit: function () {
                            $(".progress").show();
                            $(".progressError").hide();
                            $(".bar").width("0%")
                            $(".percent").html("0%");
                        },
                        uploadProgress: function (event, position, total, percentComplete) {
                            var percentVal = percentComplete + "%";
                            $(".bar").width(percentVal)
                            $(".percent").html(percentVal);
                        },
                        success: function (data) {
                            $(".progress").hide();
                            $(".progressError").hide();
                            // set viewModel property to filename
                            //var arr = data.substring(1, data.length - 1).split('_');
                            //data = data.replace(new RegExp(/\r?\n|\r/, 'g'), "");
                            //data = data.trim();
                            var arr = JSON.parse(data.trim());
                            var length = arr.length;
                            var testObj = arr[0];
                            //if(arr[0]=="OK")
                            //callback({ID:parseInt(arr[1]),Name:arr[2]});

                            if (arr.length == 1)
                                callback(arr[0]);

                            if (arr.length > 1)
                                callback(arr)
                        },
                        error: function (jqXHR, textStatus, errorThrown) {
                            $(".progress").hide();
                            $("div.progressError").html(jqXHR.responseText);
                        }
                    });
                }

                else {
                    alert("Sorry, this file type is not allowed.");
                    $("input:file").val("");
                }



            });
        }
    }
}

ko.bindingHandlers.RichEditor = {
    init: function (element, valueAccessor, allBindingsAccessor) {
        var observable = valueAccessor();
        $(element).summernote({
            height: 300, callbacks: {
                onChange: function (contents, $editable) {
                    //console.log('onChange:', contents, $editable);
                    observable(contents);
                }
            }
        });

        //$(element).wysiwyg().on('change', function () {
        //    observable($(element).summernote());
        //});
    },
    update: function (element, valueAccessor) {
        var value = ko.utils.unwrapObservable(valueAccessor());
        if (value != $(element).summernote('code'))
            $(element).summernote('code', value);
    }
};

//ko.bindingHandlers.datetimepicker = {
//    init: function (element, valueAccessor, allBindingsAccessor) {
//        var $el = $(element);

//        var startDate = new Date();

//        if (allBindingsAccessor.has('startDate')) {
//            startDate = Date.create(allBindingsAccessor.get('startDate'), 'fr');
//        }

//        //initialize datepicker with some optional options
//        var options = {
//            showOn: "button",
//            changeMonth: true, changeYear: true, buttonImageOnly: true, dateFormat: 'dd/mm/yy',
//            /*minDate: startDate,*/ buttonImage: '../images/calendar-clock.png', buttonImageOnly: true, controlType: 'select', stepMinute: 1
//        };
//        $el.datetimepicker(options);

//        //handle the field changing
//        ko.utils.registerEventHandler(element, "change", function () {
//            var observable = valueAccessor();
//            observable(Date.create($el.datetimepicker("getDate"), 'fr').format('{dd}/{MM}/{yyyy} {HH}:{mm}'));
//        });

//        //handle disposal (if KO removes by the template binding)
//        ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
//            $el.datetimepicker("destroy");
//        });

//    },
//    update: function (element, valueAccessor) {
//        var value = ko.utils.unwrapObservable(valueAccessor()),
//            $el = $(element),
//            current = $el.datetimepicker("getDate");

//        if (value - current !== 0) {
//            $el.datetimepicker("setDate", Date.create(value, 'fr'));
//        }
//    }
//};

ko.bindingHandlers.daterangepicker = {
    init: function (element, valueAccessor, allBindingsAccessor) {
        var $el = $(element);

        //initialize datepicker with some optional options
        var options = {
            locale: {
                format: 'DD/MM/YYYY'
            },
            function(start, end) {
                var observable = valueAccessor();
                observable(start.format('DD/MM/YYYY') + ' - ' + end.format('DD/MM/YYYY'));
                console.log(observable());
            }
        };
        $el.daterangepicker(options);

        //handle the field changing
        ko.utils.registerEventHandler(element, "change", function (event) {
            console.log(event);
            console.log(element);

            var value = valueAccessor();
            if (ko.observable(value))
                value(event.currentTarget.value);
        });

        //handle disposal (if KO removes by the template binding)
        ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
            $el.daterangepicker("destroy");
        });

    },
    update: function (element, valueAccessor) {

        var data = $(element).data('daterangepicker');
        //console.log(moment(data.startDate._d).format('DD/MM/YYYY'));
        //console.log(moment(data.endDate._d).format('DD/MM/YYYY'));
    }
};

ko.bindingHandlers.slideVisible = {
    update: function (element, valueAccessor, allBindings) {
        // First get the latest data that we're bound to
        var value = valueAccessor();

        // Next, whether or not the supplied model property is observable, get its current value
        var valueUnwrapped = ko.unwrap(value);

        // Grab some more data from another binding property
        var duration = allBindings.get('slideDuration') || 400; // 400ms is default duration unless otherwise specified

        // Now manipulate the DOM element
        if (valueUnwrapped == true)
            $(element).fadeIn(duration); // Make the element visible
        else
            $(element).fadeOut(duration);   // Make the element invisible
    }
};


ko.bindingHandlers.dateTimePicker = {
    init: function (element, valueAccessor, allBindingsAccessor) {
        //initialize datepicker with some optional options

        format = "DD/MM/YYYY";//"DD/MM/YYYY hh:mm";

        if (allBindingsAccessor.has('format')) {
            format = allBindingsAccessor.get('format');
        }

        var options = allBindingsAccessor().dateTimePickerOptions || {};
        options.format = format;
        options.showClear = true;
        $(element).datepicker(options);//datetimepicker(options);

        //when a user changes the date, update the view model
        ko.utils.registerEventHandler(element, "dp.change", function (event) {

            if (event.date !== false) {
                var value = valueAccessor();

                //if (value() !== "") {
                //    var dateObj = moment(event.date, "DD/MM/YYYY hh:mm");
                //    value(dateObj.toDate());
                //}
                //else {
                //    value(moment().format("DD/MM/YYYY hh:mm"));
                //}

                if (ko.isObservable(value)) {
                    if (value() !== "" && event.date != null && !(event.date instanceof Date)) {
                        value(event.date.toDate());
                        valueAccessor()(event.date.toDate());
                    } else if (value() === "") {
                        var dateObj = moment().format(format);//format("DD/MM/YYYY hh:mm");
                        value(dateObj.toDate);
                    } else {
                        value(event.date);
                    }
                }
            }
            else {
                valueAccessor()(null);
            }

        });

        ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
            var picker = $(element).data("DateTimePicker");
            if (picker) {
                picker.destroy();
            }
        });
    },
    update: function (element, valueAccessor, allBindings, viewModel, bindingContext) {

        var picker = $(element).data("DateTimePicker");
        //when the view model is updated, update the widget
        if (picker) {
            var koDatec = ko.utils.unwrapObservable(valueAccessor());
            var koDate = moment(koDatec);

            //in case return from server datetime i am get in this form for example /Date(93989393)/ then fomat this
            koDate = (typeof (koDate) !== 'object') ? new Date(parseFloat(koDate.replace(/[^0-9]/g, ''))) : koDate;

            picker.date(koDate);
        }
    }
};