=========================
Value Provider Conditions
=========================

To support :ref:`ValueProviderConditions` in your own value providers, you must add a small line of code.

Please find more info on setting up a value provider in the Sitecore documentation.

https://doc.sitecore.com/developers/91/sitecore-experience-management/en/walkthrough--setting-up-a-value-provider-for-prefilling-forms.html

Add a support for Value Provider Conditions
===========================================

In your implementation of the IFieldValueProvider class add a check on FieldBindings.ValueProviderContext.ValueProviderConditionsMet.

.. code-block:: c#
  
        public virtual object GetValue(string parameters)
        {
            if (!FieldBindings.ValueProviderContext.ValueProviderConditionsMet)
                return string.Empty;
            return "custom prefilled value";
        }
