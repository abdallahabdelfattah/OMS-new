//class constructor
DatePickerWidget = function (settings) {

    var self = this;
    //private variables setters
    //initialize class logic here
    self.containerId = settings.containerId;
    self.settings = settings;

    self.settings.lang = (self.settings.lang) ? self.settings.lang : "ar";
    self.settings.dateFormat = (self.settings.dateFormat) ? self.settings.dateFormat.toLowerCase() : "dd/mm/yyyy";
    self.settings.calendarType = (self.settings.calendarType) ? self.settings.calendarType : "ummalqura";
    self.settings.pickerYearRange = (self.settings.pickerYearRange) ? self.settings.pickerYearRange : "c-100:c+50";
    self.settings.isReadOnly = (self.settings.isReadOnly === true) ? self.settings.isReadOnly : false;

    self.picker = null;
    self.currentCalendar = null;

    self.eleContainer = $("#" + self.containerId);
    self.txtPicker = $("#" + self.containerId + " .date-picker-input");
    self.eleSelectCalType = $("#" + self.containerId + " .date-picker-select");

    //set select value only once for first init
    //var selectedCalQueryStringVal = self.getQueryStringParameterByName('calendarType');
    //if (selectedCalQueryStringVal) {
    //    selectedCalQueryStringVal = selectedCalQueryStringVal.split(',')[0];
    //}

    //self.eleSelectCalType.val(selectedCalQueryStringVal ? selectedCalQueryStringVal : self.settings.calendarType);
    self.eleSelectCalType.val(self.settings.calendarType);
    //change event for select element trigger reload for picker with different calendar type
    self.eleSelectCalType.change(function () {

        var isReadonly = self.txtPicker.attr('readonly') === 'readonly';
        
        self.reloadPicker(self.settings.lang, self.eleSelectCalType.val());
        //if selected value changed fire validation if validation library exist

        if (isReadonly) {
            self.txtPicker.attr('readonly', 'readonly').off();
        }

        if (self.txtPicker.valid) {
            self.txtPicker.valid();
        }
    });

    //this.load();

};

//class members
DatePickerWidget.prototype =
{
    //widget load entry point
    load: function () {

        var self = this;

        if (!self.eleContainer) return;

        //readonly
        if (self.settings.isReadOnly === true) {
            self.txtPicker.attr("readonly", "readonly");
        } else {
            self.txtPicker.removeAttr("readonly");
        }
        
        //init calendar obj with selected caltype and lang for localization

       // self.settings.calendarType = 'gregorian';

      //  alert(self.settings.lang + ' ' + self.settings.calendarType);

        self.currentCalendar = $.calendars.instance(self.settings.calendarType, self.settings.lang);
        self.currentCalendar.dateFormat = self.settings.dateFormat;

        $.calendarsPicker.setDefaults(self.settings.lang === 'ar'
            ? $.calendarsPicker.regionalOptions.ar
            : $.calendarsPicker.regionalOptions.en);

        //year range logic
        //var rangeString = self.settings.pickerYearRange.replace(/c/g, self.settings.currentCalendar.today()._year);
       // var yearRangeToApply = eval(rangeString.split(":")[0]) + ":" + eval(rangeString.split(":")[1]);

        var rangeString = self.settings.pickerYearRange;
        var minYearFromExpresion = '0';
        var maxYearFromExpresion = '0';


        if (rangeString.indexOf(':') > 0) {

            rangeString = rangeString.split(':');

            minYearFromExpresion = rangeString[0].indexOf('-') > 0
                ? rangeString[0].split('-')[1]
                : '0';

            maxYearFromExpresion = rangeString[1].indexOf('+') > 0
                ? rangeString[1].split('+')[1]
                : '0';
        }

        var computedMaxDate = "";
        var computedMinDate = "";

        switch (self.settings.rangeType) {
            case "futureOnly":
                if (maxYearFromExpresion !== '0')
                    computedMaxDate = self.currentCalendar.formatDate(self.settings.dateFormat, self.currentCalendar.today().add(parseInt(maxYearFromExpresion), 'y'));
                computedMinDate = self.currentCalendar.formatDate(self.settings.dateFormat, self.currentCalendar.today().add(1, 'd'));
                break;
            case "futureIncludingToday":
                if (maxYearFromExpresion !== '0')
                    computedMaxDate = self.currentCalendar.formatDate(self.settings.dateFormat, self.currentCalendar.today().add(parseInt(maxYearFromExpresion), 'y'));
                computedMinDate = self.currentCalendar.formatDate(self.settings.dateFormat, self.currentCalendar.today());
                break;
            case "pastOnly":
                if (minYearFromExpresion !== '0')
                    computedMinDate = self.currentCalendar.formatDate(self.settings.dateFormat, self.currentCalendar.today().add((-1*parseInt(minYearFromExpresion)), 'y'));
                computedMaxDate = self.currentCalendar.formatDate(self.settings.dateFormat, self.currentCalendar.today().add(-1, 'd'));
                break;
            case "pastIncludingToday":
                if (minYearFromExpresion !== '0')
                    computedMinDate = self.currentCalendar.formatDate(self.settings.dateFormat, self.currentCalendar.today().add((-1 * parseInt(minYearFromExpresion)), 'y'));
                computedMaxDate = self.currentCalendar.formatDate(self.settings.dateFormat, self.currentCalendar.today());
                break;
            case "specificDates":
                if (self.settings.maxDate && self.settings.minDate) {
                    computedMaxDate = self.settings.maxDate;
                    computedMinDate = self.settings.minDate;
                }
                break;
        }
        
        //init picker jquery plugin
        self.txtPicker.calendarsPicker({
            minDate: computedMinDate,
            maxDate: computedMaxDate,
            //yearRange: yearRangeToApply, //'c-03:c+03',
            calendar: self.currentCalendar,
            dateFormat: self.settings.dateFormat,
            firstDay: 6,
            monthsToShow: 1,
            showOtherMonths: false,
            monthsToStep: 1,
            useMouseWheel: true,
            showOnFocus: !self.settings.isReadOnly, //in case of readonly we can not open picker in case of element focus
            onSelect: function(date) {
                //if selected value changed fire validation if validation library exist
                if (self.txtPicker.valid) {
                    self.txtPicker.valid();
                }
            }
        });
        
    }, //load End.

    reloadPicker: function (lang, calType) {
        var self = this;

        var newDate = null;

        if (lang) {
            self.settings.lang = lang;
        }

        if (calType && self.settings.calendarType !== calType) {

            var fromCalName = self.settings.calendarType;
            var toCalName = calType;

            //a convert between dates will be needed
            var selectedDate = self.getSelectedDate();
            if (selectedDate) {
                newDate = self.convertDates(selectedDate, fromCalName, toCalName);
            }

            self.settings.calendarType = calType;
        }

        //cleanup
        self.txtPicker.calendarsPicker("destroy");
        self.txtPicker.removeClass("hasCalendarsPicker");
        self.txtPicker.val("");

        if (newDate)
            self.txtPicker.val(newDate.formatDate(self.settings.dateFormat));

        self.load(newDate);
    },

    getSelectedDate: function () {
        var self = this;
        if (self.txtPicker.length > 0 && self.txtPicker.val() !== "")
            return self.currentCalendar.parseDate(self.settings.dateFormat, self.txtPicker.val());
        else
            return null;
    },

    getSelectedDateGregString: function (targetFormat) {
        
        var self = this;

        targetFormat = targetFormat ? targetFormat : self.settings.dateFormat;//'mm/dd/yyyy';

        if (self.txtPicker.length > 0 && self.txtPicker.val() !== "") {
            var dateObj = self.currentCalendar.parseDate(self.settings.dateFormat, self.txtPicker.val());
            return dateObj.formatDate(targetFormat);
            
        } else {
            return null;

        }
    },

    convertDates: function (dateObjToParse, fromCalName, toCalName) {
        //var jd = $.calendars.instance(fromCalName).newDate(year, month, day).toJD();
        var jd = dateObjToParse.toJD();
        return $.calendars.instance(toCalName).fromJD(jd);
    },

    getQueryStringParameterByName: function (name) {
        name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
        var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
            results = regex.exec(location.search);
        return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
    }


}; //class end