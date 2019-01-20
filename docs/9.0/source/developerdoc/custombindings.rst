===============
Custom Bindings
===============

Extend the :ref:`FormBindings` with  your own custom xDB facets or even with an entirely different source like an ERP system.

Add a custom binding source
===========================

Create BindingHandler
---------------------

To add a custom binding source, you should write a IBindingHandler interface.

.. code-block:: c#

  namespace Feature.FormsExtensions.Business.FieldBindings
  {
    public interface IBindingHandler
    {
      IBindingHandlerResult GetBindingValue();
      void StoreBindingValue(object newValue);
    }
  }

The GetBindingValue should return a IBindingHandlerResult.

An example implementation could be like:

.. code-block:: c#

  namespace Feature.FormsExtensions.Business.FieldBindings
  {
    public class DemoBindingHandler : IBindingHandler
    {
        public IBindingHandlerResult GetBindingValue()
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

Register BindingHandler
-----------------------

To register the bindinghandler(s) you have created, you must create a processor.

.. code-block:: c#

  public class DemoBindingHandlerLoader : MvcPipelineProcessor<LoadFieldBindingHandlersArgs>
  {
    public override void Process(LoadFieldBindingHandlersArgs args)
    {
      var tokenKey = new FieldBindingTokenKey("My Custom Handlers","x.y.z.customhandler","Custom Handler");
      args.FieldBindingHandlers.Add(tokenKey,new DemoBindingHandler());
    }
  }

The tokenkey consists of 3 parameters:
  - Category: this will group the handlers in the sitecore forms user interface
  - Id: a unique id for your handler (this can be anything)
  - Name: the name of the handler that will be shown to the user

Once forms are created with your custom handler, you should not change the id anymore. 
The category and name can be safely changed as they are not stored on the form components.


Add the BindingHandlerLoader to the loader pipeline
---------------------------------------------------

Create a config file to add your loader to the forms.loadFieldBindingHandlers pipeline.

.. code-block:: xml

  <configuration>
    <sitecore>
      <pipelines>
        <forms.loadFieldBindingHandlers>
          <processor type="mypackage.DemoBindingHandlerLoader , mydll" resolve="true" />
        </forms.loadFieldBindingHandlers>
      </pipelines>
    </sitecore>
  </configuration>


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
