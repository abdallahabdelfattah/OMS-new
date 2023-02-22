//class constructor
DropDownWidget = function (settings) {

    var self = this;
    //private variables setters
    //initialize class logic here
    self.settings = settings;
    self.settings.disabled = (self.settings.disabled === true) ? self.settings.disabled : false;
    self.eleSelect = $('#' + self.settings.selectId);
};

//class members
DropDownWidget.prototype =
{
    //widget load entry point
    load: function () {

        var self = this;

        if (!self.eleSelect) return;

        //set select value only once for first init
        if (self.settings.selectedVal) {
            self.eleSelect.val(self.settings.selectedVal);
        }
        
        self.eleSelect.select2();

        self.eleSelect.change(function () {
          
            if (self.eleSelect.valid) {
                self.eleSelect.valid();
            }
        });
        

        //if selected value changed fire validation if validation library exist
        $(document.body).on("change", "#" + self.settings.selectId, function () {
            if (self.eleSelect.valid) {
                self.eleSelect.valid();
            }
        });

    }//load End.


}; //class end