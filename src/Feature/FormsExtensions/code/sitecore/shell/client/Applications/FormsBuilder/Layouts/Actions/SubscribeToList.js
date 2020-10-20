(function (speak) {
    var parentApp = window.parent.Sitecore.Speak.app.findApplication('EditActionSubAppRenderer');

    var getLists = function (data) {
        return _.reduce(data,
            function (memo, item) {
                     memo.push({
                        itemId: item.Id,
                        name: item.Name
                    });
                return memo;
            },
            [
                {
                    itemId: '',
                    name: ''
                }
            ],
            this);
    };

    speak.pageCode(["underscore"],
        function (_) {
            return {
                initialized: function () {
                    this.on({ "loaded": this.loadDone }, this);
                    this.SelectListForm.ListId.on("change:SelectedItem", this.validate, this);                    
                    if (parentApp) {
                        parentApp.loadDone(this, this.HeaderTitle.Text, this.HeaderSubTitle.Text);
                    }
                },
                validate: function () {
                    parentApp.setSelectability(this, false);
                    if (this.SelectListForm.ListId.SelectedValue != "") {
                        parentApp.setSelectability(this, true);
                    }
                },
                setListIdData: function (listComponent, data, currentValue) {
                    var items = data.slice(0);
                    if (currentValue && !_.findWhere(items, { itemId: currentValue })) {
                        var currentField = {
                            itemId: currentValue,
                            name: currentValue + " - value not in the selection list"
                        };
                        items.splice(1, 0, currentField);
                        listComponent.DynamicData = items;
                        $(listComponent.el).find('option').eq(1).css("font-style", "italic");
                    } else {
                        listComponent.DynamicData = items;
                        listComponent.SelectedValue = currentValue;
                    }
                },
                loadDone: function (parameters) {
                    this.Parameters = parameters || {};
                    var org = this;
                    $.getJSON( "/sitecore/api/ssc/ListManagement/ContactList", function( data ) {
                        org.setListIdData(org.SelectListForm.ListId, getLists(data), org.Parameters["listId"]);
                    });                    
                    this.validate();
                },
                getData: function () {
                    this.Parameters["listId"] = this.SelectListForm.ListId.SelectedValue;
                    return this.Parameters;
                }
            };
        });
})(Sitecore.Speak);
