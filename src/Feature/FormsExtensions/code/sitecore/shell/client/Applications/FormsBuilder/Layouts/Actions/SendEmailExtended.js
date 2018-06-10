(function (speak) {
    var parentApp = window.parent.Sitecore.Speak.app.findApplication('EditActionSubAppRenderer');
    var designBoardApp = window.parent.Sitecore.Speak.app.findComponent('FormDesignBoard');
    var messageParameterName = 'messageId';
    var typeParameterName = 'type';

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

                    this.MessagesDataSource.on("change:DynamicData", this.messagesChanged, this);

                    this.SettingsForm.Message.on("change:SelectedItem", this.validate, this);
                    this.SettingsForm.FieldEmailAddressId.on("change:SelectedItem", this.validate, this);
                    this.SettingsForm.Type.on("change:SelectedItem", this.changedType, this);
                    this.SettingsForm.FixedEmailAddress.on("change:Value", this.changedType, this);
                    this.changedType();

                    if (parentApp) {
                        parentApp.loadDone(this, this.HeaderTitle.Text, this.HeaderSubtitle.Text);
                    }
                },
                validate: function () {
                    parentApp.setSelectability(this, false);
                    if (this.SettingsForm.Message.SelectedValue != "") {
                        var type = this.SettingsForm.Type.SelectedValue;
                        if (type === 'fixedAddress') {
                            if (this.SettingsForm.FixedEmailAddress.Value != "") {
                                parentApp.setSelectability(this, true);
                            }
                        } else if (type === 'currentContact') {
                            parentApp.setSelectability(this, true);
                        } else if (type === 'fieldValue') {
                            if (this.SettingsForm.FieldEmailAddressId.SelectedValue != "") {
                                parentApp.setSelectability(this, true);
                            }
                        }

                    }
                },
                setDynamicData: function (listComponent, data, currentValue) {
                    var items = data.slice(0);
                    items.unshift({ Id: "", Name: "" });

                    if (currentValue && !_.findWhere(items, { Id: currentValue })) {
                        items.unshift({
                            Id: "",
                            Name: currentValue + " - value not in the selection list"
                        });

                        listComponent.DynamicData = items;
                        $(listComponent.el).find('option').eq(0).css("font-style", "italic");
                    } else {
                        listComponent.DynamicData = items;
                        listComponent.SelectedValue = currentValue;
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
                messagesChanged: function (items) {
                    this.setDynamicData(this.SettingsForm.Message, items, this.Parameters[messageParameterName]);
                    this.validate();
                },
                changedType: function () {
                    var typeField = this.SettingsForm.Type;
                    var type = typeField.SelectedValue;
                    this.SettingsForm.FixedAddressSection.IsVisible = false;
                    this.SettingsForm.FromFieldSection.IsVisible = false;
                    if (type === 'fixedAddress') {
                        this.SettingsForm.FixedAddressSection.IsVisible = true;
                    } else if (type === 'currentContact') {

                    } else if (type === 'fieldValue') {
                        this.SettingsForm.FromFieldSection.IsVisible = true;
                    }
                    this.validate();
                },
                loadDone: function (parameters) {
                    this.Parameters = parameters || {};
                    this.SettingsForm.setFormData(this.Parameters);
                    this.setEmailFieldData(this.SettingsForm.FieldEmailAddressId, getFields(), this.Parameters["fieldEmailAddressId"]);
                    this.validate();
                },
                getData: function () {
                    this.Parameters[messageParameterName] = this.SettingsForm.Message.SelectedValue;
                    this.Parameters[typeParameterName] = this.SettingsForm.Type.SelectedValue;
                    this.Parameters["fixedEmailAddress"] = this.SettingsForm.FixedEmailAddress.Value;
                    this.Parameters["fieldEmailAddressId"] = this.SettingsForm.FieldEmailAddressId.SelectedValue;
                    this.Parameters["updateCurrentContact"] = this.SettingsForm.UpdateCurrentContact.IsChecked;
                    return this.Parameters;
                }
            };
        });
})(Sitecore.Speak);
