=========================
Value Provider Store Back
=========================

From Sitecore 9.1, value providers where added. With these value providers you can prefill your form fields. 

With Value Provider Store Back, the value provider has the additional ability to store the value back into the value provider after the form was submitted.
In essence, the value provider has read and write capabilities.


Creating a traditional readonly Sitecore Value Provider
=======================================================

Please find more info on setting up a traditional readonly value provider in the Sitecore documentation.

https://doc.sitecore.com/developers/91/sitecore-experience-management/en/walkthrough--setting-up-a-value-provider-for-prefilling-forms.html


Creating Value Provider with store back functionality
=====================================================

To add the store back functionality, just create your Value Provider as documented by Sitecore, but inherrit from the IFieldValueBinder class of the Sitecore Forms Extensions.

.. code-block:: c#
  
  namespace Feature.FormsExtensions.ValueProviders
  {
    public interface IFieldValueBinder : Sitecore.ExperienceForms.ValueProviders.IFieldValueProvider
    {
      void StoreValue(object newValue);
    }
  }


An example implementation could be like:

.. code-block:: c#

  namespace Feature.FormsExtensions.ValueProviders
  {
    public class DemoBindingHandler : IFieldValueBinder
    {
        public FieldValueProviderContext ValueProviderContext { get; set; }

        public object GetValue(string parameters)
        {
            var fullName = Sitecore.Context.User.Profile.FullName;
            if (string.IsNullOrEmpty(fullName))
            {
                return string.Empty;
            }
            return fullName;
        }

        public void StoreValue(object newValue)
        {
            if (newValue is string fullName)
            {
                Sitecore.Context.User.Profile.FullName = fullName;
            }
        }
    }
  }

The storevalue method is only called when the newValue is not null.

Configure preferred email, address and phonenumber
==================================================

By default, Sitecore only ships without build-in Value Providers.

The module comes with a set of value providers to support xDB. 

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
