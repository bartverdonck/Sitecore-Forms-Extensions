(function (speak) {
    var propertyGridApp = window.parent.Sitecore.Speak.app.findApplication('PropertyGrid'),
        selectDatasourceSubApp = window.parent.Sitecore.Speak.app.findApplication("SelectDatasourceSubAppRenderer"),
        designBoardApp = window.parent.Sitecore.Speak.app.findComponent('FormDesignBoard');

    speak.pageCode(["/-/speak/v1/formsbuilder/assets/formeditorutils.js", "/-/speak/v1/formsbuilder/assets/formservices.js"], function (formEditorUtils, formServices) {
        var defaultOptions = {
            skipFlattenKeys: ["items", "values", "validationDataModels", "submitActions"],
            immutableFieldProperties: ["itemId", "propertyDesigner"]
        };

        return {

            initialize: function () {
                this.defineProperty("ContextItem", null);
                this.defineProperty("AllowedValidations", []);
				this.PrefillTokensDataSource.on("change:DynamicData", this.tokensChanged, this);
				this.PrefillTokensDataSource.on("error", this.tokensChanged, this);
                this.on({
                    "loaded": this.loadDone,
                    "change:ContextItem": this.initProperties,
                    "properties:ApplyChanges": this.applyChanges,
                    "dynamicdatasource:Select": this.selectDynamicDatasource
                }, this);
				
            },

            initialized: function () {
                this.datasourceControl = formEditorUtils.getFormControl(this.PropertiesForm, "isDynamic");
                propertyGridApp.loadDone(this, !!this.datasourceControl);
				
				if(this.PropertiesForm.PrefillValue){
					this.PropertiesForm.PrefillValue.on("change:SelectedItem", this.changedPrefillToken, this);
				}				
            },
			
			tokensChanged: function(items) {
				if(this.PropertiesForm.PrefillValue){
					this.setDynamicData(this.PropertiesForm.PrefillValue, items, this.PropertiesForm.BindingTarget.prefillToken);
				}
			},
			
			changedPrefillToken:function(){
				this.PropertiesForm.FormData.prefillToken = this.PropertiesForm.PrefillValue.SelectedValue;
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

            loadDone: function () {
                this.PropertiesForm.children.forEach(function (child) {
                    if (child.deps && (child.deps.indexOf("bclSelection") !== -1 || child.deps.indexOf("bclMultiSelection") !== -1)) {
                        child.IsSelectionRequired = false;
                    }
                });

                var submitActionsControl = formEditorUtils.getFormControl(this.PropertiesForm, "submitActions");
                if (submitActionsControl) {
                    submitActionsControl.EditActionsDialog = window.parent.Sitecore.Speak.app.findApplication("EditActionSubAppRenderer");
                }

                this.initialFormData = formEditorUtils.getFormData(this.PropertiesForm);
                this.options = $.extend(true, {}, defaultOptions);
            },

            initProperties: function () {
                var hasContextItem = speak.utils.is.a.object(this.ContextItem) && speak.utils.is.a.object(this.ContextItem.model);
                var hasDatasource = !!this.datasourceControl;

                this.MainBorder.IsVisible = hasContextItem;

                selectDatasourceSubApp[hasDatasource && hasContextItem ? "on" : "off"]("selectdatasource:ItemSelected", this.updatedDatasourceGuid, this);
                if (!hasContextItem) {
                    return;
                }

                var valueControl = formEditorUtils.getFormControl(this.PropertiesForm, "value");
                if (valueControl && valueControl.key === "DatesManager") {
                    valueControl.reset();
                }

                this.options = $.extend(true, {}, defaultOptions);

                var bindingTarget = formEditorUtils.getFormBindingTarget(this.initialFormData, this.ContextItem.model, this.options.skipFlattenKeys);
                this.initValidations(bindingTarget);
                this.initDatasource(bindingTarget);

                this.PropertiesForm.BindingTarget = bindingTarget;
                this.PropertiesForm.setErrors({});
            },

            initValidations: function (bindingTarget) {
                var fieldValidation = formEditorUtils.getFormControl(this.PropertiesForm, "validationDataModels");
                if (!fieldValidation) return;

                $(fieldValidation.el).closest(".sc-formcomponent").parent().css("display", this.AllowedValidations.length ? "" : "none");

                bindingTarget.allowedValidations = this.AllowedValidations;
                var valueFieldName = fieldValidation.ValueFieldName;

                var selectableValidations = _.filter(bindingTarget.validationDataModels, function (item) {
                    return _.find(this.AllowedValidations, function (allowedItem) {
                        return item[valueFieldName] === allowedItem[valueFieldName];
                    }, this);
                }, this);

                bindingTarget.validationDataModels = selectableValidations;
            },
            
            initDatasource: function (bindingTarget) {
                if (!this.datasourceControl) return;

                this.datasourceControl.IsDynamic = bindingTarget.isDynamic;
                if (bindingTarget.isDynamic) {
                    this.reloadDatasource({
                        datasource: bindingTarget.dataSource || ""
                    });
                }
                else {
                    this.updateDatasourceHandlers();
                    this.updateDatasourceHandlers(true);
                }
            },

            selectDynamicDatasource: function () {
                selectDatasourceSubApp.show(this.datasourceControl.DynamicDatasource.DataSourceGuid);
            },

            updatedDatasourceGuid: function (datasourceGuid) {
                setTimeout(function () {
                    this.PropertiesForm.DatasourceManager.CompDatasourceGuid = datasourceGuid;
                }.bind(this), 0);
            },

            updateDatasourceHandlers: function (attach, attachFields) {
                this.datasourceControl.DynamicDatasource[attach ? "on" : "off"]("change:DataSourceGuid", this.datasourceRootChanged, this);
                this.datasourceControl.DynamicDatasource[attach && attachFields ? "on" : "off"]("change:CurrentDisplayFieldName change:CurrentValueFieldName", this.datasourceFieldChanged, this);
            },

            datasourceSettingChanged: function (reloadDatasource, reloadListItems) {
                if (!this.datasourceControl || !this.datasourceControl.IsDynamic) return;

                if (!this.datasourceControl.DynamicDatasource.DataSourceGuid) {
                    this.reloadDatasourceCompleted({});
                    this.reloadListItemsCompleted([]);
                    return;
                }

                if (reloadDatasource) {
                    this.reloadDatasource({
                        datasource: this.datasourceControl.DynamicDatasource.DataSourceGuid
                    });
                }

                if (reloadListItems) {
                    this.reloadListItems({
                        datasource: this.datasourceControl.DynamicDatasource.DataSourceGuid,
                        displayFieldName: this.datasourceControl.DynamicDatasource.CurrentDisplayFieldName || "",
                        valueFieldName: this.datasourceControl.DynamicDatasource.CurrentValueFieldName || ""
                    });
                }
            },

            datasourceRootChanged: function () {
                this.datasourceSettingChanged(true, true);
            },

            datasourceFieldChanged: function () {
                this.datasourceSettingChanged(false, true);
            },

            reloadDatasource: function (dataOptions) {
                formServices.reloadDatasource(dataOptions, designBoardApp ? designBoardApp.CurrentLanguage : speak.Context.Current().language)
                    .then(this.reloadDatasourceCompleted.bind(this))
                    .fail(this.reloadDatasourceError.bind(this));
            },

            reloadDatasourceError: function () {
                this.reloadDatasourceCompleted({});
            },

            reloadDatasourceCompleted: function (data) {
                if (!this.datasourceControl)
                    return;

                if (typeof data === "string") {
                    data = JSON.parse(data);
                }

                this.updateDatasourceHandlers();
                this.datasourceControl.DynamicDatasource.Path = data.path || "";
                this.datasourceControl.DynamicDatasource.Fields = data.fields || [];
                this.updateDatasourceHandlers(true, data.fields && data.fields.length);
            },

            reloadListItems: function (dataOptions) {
                formServices.reloadListItems(dataOptions, designBoardApp ? designBoardApp.CurrentLanguage : speak.Context.Current().language)
                    .then(this.reloadListItemsCompleted.bind(this))
                    .fail(this.reloadListItemsError.bind(this));
            },

            reloadListItemsError: function () {
                this.reloadListItemsCompleted([]);
            },

            reloadListItemsCompleted: function (data) {
                if (!this.datasourceControl)
                    return;

                if (typeof data === "string") {
                    data = JSON.parse(data);
                }

                var selection = this.datasourceControl.DynamicDatasource.Selection.splice(0);
                this.datasourceControl.DynamicDatasource.Items = data || [];
                this.datasourceControl.DynamicDatasource.Selection = selection;
            },

            hasChanged: function () {
                if (!this.ContextItem)
                    return false;

                var flatFormData = formEditorUtils.getFormData(this.PropertiesForm);
                return formEditorUtils.hasChanged(this.ContextItem.model, flatFormData, this.options.immutableFieldProperties);
            },

            applyChanges: function () {
                var flatFormData = formEditorUtils.getFormData(this.PropertiesForm);

                var errors = formEditorUtils.getFormErrors(this.PropertiesForm, flatFormData, this.RequiredFieldErrorText.Text);
                if (errors) {
                    this.PropertiesForm.setErrors(errors);
                    return;
                }

                formEditorUtils.updateProperties(this.ContextItem.model, flatFormData, this.options.immutableFieldProperties);

                propertyGridApp.trigger("properties:ApplyChangesCompleted", this.ContextItem);
            }
        };
    });
})(Sitecore.Speak);
