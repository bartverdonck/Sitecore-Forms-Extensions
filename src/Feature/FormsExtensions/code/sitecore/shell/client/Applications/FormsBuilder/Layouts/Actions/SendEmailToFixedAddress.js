(function(speak) {
    var parentApp = window.parent.Sitecore.Speak.app.findApplication('EditActionSubAppRenderer');
    var messageParameterName = "messageId";
    var toParameterName = "to";

    speak.pageCode(["underscore"],
        function(_) {
            return {
                initialized: function() {
                    this.on({
                            "loaded": this.loadDone
                        },
                        this);

                    var componentName = this.Form.bindingConfigObject[messageParameterName].split(".")[0];
                    this.MessagesList = this.Form[componentName];
                    this.MessagesList.on("change:SelectedItem", this.changedSelectedItemId, this);

                    this.MessagesDataSource.on("change:DynamicData", this.messagesChanged, this);
                    this.MessagesDataSource.on("error", this.messagesError, this);

                    var toComponentName = this.Form.bindingConfigObject[toParameterName].split(".")[0];
                    this.To = this.Form[toComponentName];

                    if (parentApp) {
                        parentApp.loadDone(this, this.HeaderTitle.Text, this.HeaderSubtitle.Text);
                    }
                },

                changedSelectedItemId: function() {
                    var isSelectable = this.MessagesList.SelectedValue && this.MessagesList.SelectedValue.length;
                    parentApp.setSelectability(this, isSelectable);
                },

                setDynamicData: function(listComponent, data, currentValue) {
                    var items = data.slice(0);
                    items.unshift({ Id: "", Name: "" });

                    if (currentValue && !_.findWhere(items, { Id: currentValue })) {
                        items.unshift({
                            Id: "",
                            Name: currentValue +
                                " - " +
                                (this.ValueNotInListText.Text || "value not in the selection list")
                        });

                        listComponent.DynamicData = items;
                        $(listComponent.el).find('option').eq(0).css("font-style", "italic");
                    } else {
                        listComponent.DynamicData = items;
                        listComponent.SelectedValue = currentValue;
                    }
                },

                messagesError: function(result) {
                    var errorMessage = {
                        Type: "error",
                        Text: result.response.status === 401 || result.response.status === 403
                            ? this.UnauthorizedErrorMessage.Text
                            : this.GenericErrorMessage.Text,
                        IsClosable: true,
                        IsTemporary: false
                    };
                    this.MessageBar.add(errorMessage);
                    this.setDynamicData(this.MessagesList, [], this.Parameters[messageParameterName]);
                },

                messagesChanged: function(items) {
                    this.setDynamicData(this.MessagesList, items, this.Parameters[messageParameterName]);
                },

                loadDone: function(parameters) {
                    this.Parameters = parameters || {};
                    this.Form.setFormData(this.Parameters);
                },

                getData: function() {
                  this.Parameters[messageParameterName] = this.MessagesList.SelectedValue;
                  this.Parameters[toParameterName] = this.To.Value;
                  return this.Parameters;
                }
            };
        });
})(Sitecore.Speak);
