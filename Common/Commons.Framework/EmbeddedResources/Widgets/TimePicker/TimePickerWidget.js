//class constructor
TimePickerWidget = function (pickerId, format, forceRoundTime, step, compareToName, compareOperator, viewMode, minTime, maxTime) {
    var self = this;
    //private variables setters
    //initialize class logic here
    self.pickerId = pickerId;
    self.forceRoundTime = forceRoundTime;
    self.format = format;
    self.elePicker = $('#' + pickerId);
    self.eleValid = $("#" + pickerId + "_IsValid");
    self.eleHidden = $("#" + pickerId + "_setter");
    self.step = step;
    self.minTime = minTime;
    self.maxTime = maxTime;
    self.compareOperator = compareOperator;
    if (compareToName !== '' && compareToName !== undefined && compareToName !== null) {
        var selectedObjId = $("input[type='hidden'][name='" + compareToName + "']").prop('id');
        self.eleCompareTo = $("#" + selectedObjId.substr(0, selectedObjId.lastIndexOf('_')));
    } else {
        self.eleCompareTo = null;
    }
    //if (viewMode) {
    //    self.elePicker.val('');
    //}

};

//class members
TimePickerWidget.prototype =
    {
        //widget load entry point
        load: function () {
            var self = this;

            if (!self.elePicker) return;
            //self.elePicker refer to $('#' + pickerId)
            //format 0=> 14:30:00 or 02:30 PM
            if (self.format === 0) {
                self.elePicker.timepicker({ 'timeFormat': 'H:i:s', 'forceRoundTime': self.forceRoundTime, 'step': self.step, 'minTime': self.minTime, 'maxTime': self.maxTime });
            } else {
                self.elePicker.timepicker({ 'timeFormat': 'h:i A', 'forceRoundTime': self.forceRoundTime, 'step': self.step, 'minTime': self.minTime, 'maxTime': self.maxTime });
            }


            self.elePicker.keyup(function (e) {
                validation(self.elePicker) ? self.eleValid.hide() : self.eleValid.show();
            });
            var convertedTime;
            self.elePicker.change(function (e) {

                var isvalid = validation(self.elePicker);
                isvalid ? self.eleValid.hide() : self.eleValid.show();
                var durationIndex = 0;
                if (isvalid && self.eleCompareTo !== null) {
                    var duration = [];
                    switch (self.compareOperator) {
                        case 1://GreaterThan
                            var t = new Date('2000-1-1 ' + convertedTime);
                            var m = t.getMinutes() - 1;
                            var h = m > -1 ? t.getHours() : t.getHours() - 1;
                            m = m > -1 ? m : 59;
                            m = m.toString().length === 1 ? '0' + m.toString() : m;
                            h = h.toString().length === 1 ? '0' + h.toString() : m;
                            duration = [h + ':' + m + ':00', '23:59:59'];
                            durationIndex = 0;
                            break;
                        case 2://SmallerThan
                            duration = ['00:00:00', convertedTime.substr(0, convertedTime.lastIndexOf('0')) + '1'];
                            durationIndex = 1;
                            break;
                        case 3://GreaerThanOrEqual
                            duration = [convertedTime, '23:59:59'];
                            durationIndex = 0;
                            break;
                        case 4://SmallerThanOrEqual
                            duration = ['00:00:00', convertedTime];
                            durationIndex = 1;
                            break;
                        default:
                    }

                    self.eleCompareTo.timepicker('option', {
                        'timeFormat': 'h:i A',
                        'forceRoundTime': self.forceRoundTime,
                        'step': 30,
                        'disableTimeRanges': [
                            duration
                        ]
                    });
                    self.eleCompareTo.val('');
                }


            });
            convertedTime = self.elePicker.val();

            function validation(elem) {
                if (!checkTime(elem.val())) {
                    elem.val("");
                    elem.focus();
                    return false;
                } else {

                    if (self.format === 1) {
                        convertedTime = timeConvertor(elem.val());
                        self.eleHidden.val(convertedTime);
                    }
                    return true;
                }
            }

            console.log('Time Picker Loaded');

            function timeConvertor(time) {
                var PM = time.match('PM') ? true : false;

                time = time.split(':');
                var min = time[1].substr(0, 2);

                if (PM) {
                    if (time[0] == 12) {
                        var hour = time[0];
                    } else {
                        var hour = 12 + parseInt(time[0], 10);
                    }
                } else {
                    var hour = time[0];
                }
                return (hour + ':' + min + ':00');

            }

            function checkTime(field) {
                var errorMsg = "";

                // regular expression to match required time format
                re = /^(\d{1,2}):(\d{2})(:00)?( [AP]M)?$/;

                if (field != '') {
                    if (regs = field.match(re)) {
                        if (regs[4]) {
                            // 12-hour time format with am/pm
                            if (regs[1] < 1 || regs[1] > 12) {
                                errorMsg = $commons.getLocalString('InvalidHours') + regs[1];
                            }
                        } else {
                            // 24-hour time format
                            if (regs[1] > 23) {
                                errorMsg = $commons.getLocalString('InvalidHours') + regs[1];
                            }
                        }
                        if (!errorMsg && regs[2] > 59) {
                            errorMsg = $commons.getLocalString('InvalidMinutes') + regs[2];
                        }
                    } else {
                        errorMsg = $commons.getLocalString('InvalidTime') + field;
                    }
                }

                if (errorMsg != "") {
                    //alert(errorMsg);
                    self.eleValid.text(errorMsg);
                    self.eleValid.show();
                    return false;
                }

                return true;
            }

            function compareTime(time) {
                var currentElementTime = new Date("1980-01-01 " + convertedTime);

                var compareToElementTime = new Date("1980-01-01 " + time);

                if (compareToElementTime >= currentElementTime) {
                    alert("Newer time is newer");

                } else {
                    alert("The time is not newer");

                }

            }

            var elemValidation = $("span[data-valmsg-for='" + self.eleHidden.prop('name') + "']");
            if (elemValidation !== null && elemValidation !== undefined && elemValidation !== '') {
                $(elemValidation).attr('data-valmsg-for', self.eleHidden.prop('id').replace('_setter', ''));
            }
        }//load End.


    }//class end

