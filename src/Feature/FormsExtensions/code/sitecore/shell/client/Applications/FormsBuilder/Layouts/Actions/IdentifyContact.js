(function (speak) {
    var parentApp = window.parent.Sitecore.Speak.app.findApplication('EditActionSubAppRenderer');
    var designBoardApp = window.parent.Sitecore.Speak.app.findComponent('FormDesignBoard');

    var getFields = function () {
        var fields = designBoardApp.getFieldsData();
        return _.reduce(fields,
            function (memo, item) {
                if (item && item.model && item.model.hasOwnProperty('value')) {
                    memo.push({
                        itemId: item.itemId,
                        name: item.model.name
                    });
                }
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
                    this.Fields = getFields();
                    this.SettingsForm.FieldIdentifyContactId.on("change:SelectedItem", this.validate, this);

                    if (parentApp) {
                        parentApp.loadDone(this, this.HeaderTitle.Text, this.HeaderSubtitle.Text);
                    }
                },
                validate: function () {
                    parentApp.setSelectability(this, false);
                    if (this.SettingsForm.FieldIdentifyContactId.SelectedValue != "") {
                        parentApp.setSelectability(this, true);
                    }
                },
                setEmailFieldData: function (listComponent, data, currentValue) {
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
                    this.SettingsForm.setFormData(this.Parameters);
                    this.setEmailFieldData(this.SettingsForm.FieldIdentifyContactId, getFields(), this.Parameters["fieldIdentifyContactId"]);
                    this.validate();
                },
                getData: function () {
                    this.Parameters["fieldIdentifyContactId"] = this.SettingsForm.FieldIdentifyContactId.SelectedValue;
                    return this.Parameters;
                }
            };
        });
})(Sitecore.Speak);
