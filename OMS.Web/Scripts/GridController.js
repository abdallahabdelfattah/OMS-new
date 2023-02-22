function GridController(url, sc, showCheckboxes) {
    var self = this;

    // Public Properties
    self.selectedItem = ko.observable();
    self.CheckedBoxesItems = ko.observableArray([]);
    self.ShowCheckboxes = Boolean(showCheckboxes);
    self.ShowFooter = ko.observable(false);
    self.GridErrors = ko.observableArray([]);
    self.SearchUrl = url;
    self.SC = sc;
    self.Data = ko.viewmodel.fromModel([]);
    self.TotalCount = ko.viewmodel.fromModel(0);
    self.RowCount = 10;
    sc.RowCount(self.RowCount);
    self.PageIndex = ko.viewmodel.fromModel(1);
    self.OrignialSC = ko.viewmodel.fromModel(ko.toJS(sc));
    self.TotalPagesCount = ko.viewmodel.fromModel(0);
    self.TotalOrdersValue = ko.observable();
    self.SubTotalOrdersValue = ko.observable();
    self.GridColumns = ko.viewmodel.fromModel([]);
    self.AddColumn = function (displayname, property, type) {
        self.GridColumns.push(ko.viewmodel.fromModel({ Prop: property, Display: displayname, Type: type }));
    }
    self.OnRowSelectedCallBack = null;
    self.RowSelected = function (row) {
        self.selectedItem(row);
        if (self.OnRowSelectedCallBack !== null) {
            self.OnRowSelectedCallBack(row);
        }
    }
    self.Sort = function (col) {
        if (col.Prop() === self.SC.SortProp())
            self.SC.SortDir(self.SC.SortDir() === "asc" ? "desc" : "asc");
        else
            self.SC.SortDir("asc");

        self.SC.SortProp(col.Prop());
        self.Search('Sorting...');
    }
    self.Search = function (loadingtext) {
        $("#errorDiv").hide();

        ko.viewmodel.updateFromModel(self.OrignialSC, ko.toJS(self.SC));

        self.OrignialSC.PageIndex(1);
        if (loadingtext !== undefined)
            waitingDialog.show(loadingtext);
        $.ajax({
            type: "POST",
            url: url,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: ko.toJSON(self.OrignialSC),
            success: function (result) {
                if (result.Exceptions != undefined && result.Exceptions.length > 0) {
                    self.GridErrors(result.Exceptions);
                } else {
                    self.Data(result.Data);
                    self.TotalCount(result.TotalCount);
                    self.CalculatePagers();
                    //if (self.CheckedBoxesItems().length > 0)
                    //{
                    //    for (let i = 0; i < self.CheckedBoxesItems().length; i++) {
                    //        self.CheckedBoxesItems()[i].Selected = ko.observable(true);
                    //    }
                    //}

                    if (result.TotalOrdersValue !== undefined) {
                        self.TotalOrdersValue(result.TotalOrdersValue);
                    }

                    if (result.SubTotalOrdersValue !== undefined) {
                        self.SubTotalOrdersValue(result.SubTotalOrdersValue);
                    }

                }
                if (loadingtext !== undefined)
                    waitingDialog.hide();
            },
            failure: function () {
                //   alert('Fail');
            }
        });
    }



    self.PagersRanges = {};
    self.PagerRangeIndex = 1

    for (var i = 1; i < 200; i += 5) {
        self.PagersRanges[self.PagerRangeIndex] = Number.range(i, i + 4).every(1);
        self.PagerRangeIndex++;

    }
    self.PagersComputed = ko.computed(function () {
        var pagers = [];
        var arr = [];
        for (var prop in self.PagersRanges) {
            for (var i = 0; i < self.PagersRanges[prop].length; i++) {
                if (self.PagersRanges[prop][i] === self.PageIndex()) {
                    arr = self.PagersRanges[prop];
                    break;
                }
            }
            if (arr.length > 0)
                break;
        }

        for (var j = 0; j < arr.length; j++) {
            if (arr[j] <= self.TotalPagesCount()) {
                pagers.push({ PageNo: arr[j], IsCurrent: arr[j] === self.PageIndex(), GoToPage: self.GoToPage });
            }
        }
        return pagers;
    });

    self.GoToPage = function (pageNo, showloading) {
        $("#errorDiv").hide();
        if (showloading !== false)
            waitingDialog.show('Loading...');
        self.OrignialSC.PageIndex(pageNo);
        self.PageIndex(pageNo);
        $.ajax({
            type: "POST",
            url: url,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: ko.toJSON(self.OrignialSC),
            success: function (result) {

                self.Data(result.Data);
                self.TotalCount(result.TotalCount);
                if (showloading !== false)
                    waitingDialog.hide();
                self.CalculatePagers();
                if (result.TotalCount > 0 && result.Data.length === 0)// Delete last item in the last page
                {
                    if (pageNo > 1) {
                        pageNo = pageNo - 1;

                        self.GoToPage(pageNo, false);// Then go to the previous page
                    }
                }

            },
            failure: function () {
                //   alert('Fail');
            }
        });
    }
    self.CalculatePagers = function () {
        self.TotalPagesCount(Math.ceil(self.TotalCount() / self.RowCount));
        for (var i = 1; i < self.TotalPagesCount(); i += 5) {
            self.PagersRanges[self.PagerRangeIndex] = Number.range(i, i + 4).every(1);
            self.PagerRangeIndex++;

        }

    }

    self.ClearGrid = function () {
        self.Data([]);
    }

    self.GetCheckedIDs = function () {
        if (self.CheckedBoxesItems().length > 0) {
            return self.CheckedBoxesItems();
        }

    }

    self.ClearCheckBoxes = function () {
        self.CheckedBoxesItems([]);
    }

   
    self.SelectAll = ko.pureComputed({
        read: function () {
            
            return self.CheckedBoxesItems().length == self.Data().length;
           
        },
        write: function (value) {
            var ids = [];
            for (var i = 0; i < self.Data().length; i++) {
                ids.push(self.Data()[i].ID);
            }
            self.CheckedBoxesItems(value ? ids.slice(0):[]);
        },
        owner: self
        
    });


}