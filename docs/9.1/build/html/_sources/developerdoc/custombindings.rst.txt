===============
Custom Bindings
===============

Extend the :ref:`FormBindings` with  your own custom xDB facets or even with an entirely different source like an ERP system.

Note that in Sitecore 9.1 you can create readonly bindings out of the box. The custom bindings off the module add support to store the value back.

Please find more info on setting up a value provider in the Sitecore documentation.

https://doc.sitecore.com/developers/91/sitecore-experience-management/en/walkthrough--setting-up-a-value-provider-for-prefilling-forms.html

Add a custom binding source
===========================

To add a custom binding source, you should write a IBindingHandler interface.

.. code-block:: c#
  
  namespace Feature.FormsExtensions.Business.FieldBindings
  {
    public interface IBindingHandler : Sitecore.ExperienceForms.ValueProviders.IFieldValueProvider
    {
      void StoreBindingValue(object newValue);
    }
  }


An example implementation could be like:

.. code-block:: c#

  namespace Feature.FormsExtensions.Business.FieldBindings
  {
    public class DemoBindingHandler : IBindingHandler
    {
        public FieldValueProviderContext ValueProviderContext { get; set; }

        public object GetValue(string parameters)
        {
            var fullName = Sitecore.Context.User.Profile.FullName;
            if (string.IsNullOrEmpty(fullName))
            {
                return new NoBindingValueFoundResult();
            }
            return new BindingValueFoundResult(fullName);
        }

        public void StoreBindingValue(object newValue)
        {
            if (newValue is string fullName)
            {
                Sitecore.Context.User.Profile.FullName = fullName;
            }
        }
    }
  }

The storebindingvalue is only called when the newValue is not null.

Configure preferred email, address and phonenumber
==================================================

The module comes with a set of databinding handlers to support xDB. 
The email, address and phonenumber facet on the contact profile contain a lists. 
There is always one preferred entry in the list.

The build in bindings always store and load from the preferred email, address or phonenumber.

If the facet does not yet exist, it has to create the facet and set the preferred email, address or phonenumber. 
The key that is used for this is stored in a sitecore setting. 
These settings can be overridden to fit your projects needs.

.. code-block:: xml

  <?xml version="1.0"?>
  <configuration xmlns:patch="http://www.sitecore.net/xmlconfig/"  xmlns:role="http://www.sitecore.net/xmlconfig/role/">
    <sitecore>
      <settings>
        <setting name="XDbPreferredAddress" value="address" />
        <setting name="XDbPreferredPhoneNumber" value="phone" />
        <setting name="XDbPreferredEmailAddress" value="email" />
      </settings>
    </sitecore>
  </configuration>
