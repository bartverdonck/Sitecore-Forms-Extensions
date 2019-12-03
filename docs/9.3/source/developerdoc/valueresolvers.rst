===============
Value Resolvers
===============

Value resolvers are used in combination with the Send Email action.

When tokens are used in the Send Email, the values of the fields are added into the tokens, so that they can be used in the mail message.

As not all fields have a string value, a conversion into a human readable format is required. (E.g. multiselect fields, file uploads, dates, checkboxes,...)

With the Value Resolvers from Sitecore Forms Extensions, the conversion is done in the **formsextensions.getStringValueFromViewModel** pipeline.

By default, the framework provides conversion for all of the fields in Sitecore Forms.

However, as a developer you can adapt the conversions by replacing or adding your own processors into the pipeline.

.. code-block:: xml

  <formsextensions.getStringValueFromViewModel>
    <processor type="Feature.FormsExtensions.Fields.ValueResolvers.StringValueFromStringInputViewModelReader, Feature.FormsExtensions" />
    <processor type="Feature.FormsExtensions.Fields.ValueResolvers.StringValueFromStringListInputViewModelReader, Feature.FormsExtensions" />
    <processor type="Feature.FormsExtensions.Fields.ValueResolvers.StringValueFromDateTimeInputViewModelReader, Feature.FormsExtensions" />
    <processor type="Feature.FormsExtensions.Fields.ValueResolvers.StringValueFromBooleanInputViewModelReader, Feature.FormsExtensions" />
    <processor type="Feature.FormsExtensions.Fields.ValueResolvers.StringValueFromStoredFileInputViewModelReader, Feature.FormsExtensions" />
    <processor type="Feature.FormsExtensions.Fields.ValueResolvers.StringValueFromDoubleInputViewModelReader, Feature.FormsExtensions" />
  </formsextensions.getStringValueFromViewModel>


Custom processor
================

To create your custom processor just create a new class and add it into the pipeline.

Set the value in the args.Value field. Abort the pipeline if no further processing is required.

.. code-block:: c#

  namespace Feature.FormsExtensions.Fields.ValueResolvers
  {
    public class StringValueFromBooleanInputViewModelReader : MvcPipelineProcessor<GetStringValueFromViewModelArgs>
    {
        public override void Process(GetStringValueFromViewModelArgs args)
        {
            if (args.FieldViewModel is InputViewModel<bool> booleanInputViewModel)
            {
                args.Value = booleanInputViewModel.Value ? "Checked" : "Not Checked";
                args.AbortPipeline();
            }

        }
    }
  }
