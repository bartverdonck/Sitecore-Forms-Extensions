.. _azureblobstorage:

==================================================
Configure Azure Blob Storage File Storage Provider
==================================================

From Sitecore 9.3, a file upload component is available out of the box.

By default the files are stored into the forms SQL database. (Even when the Sitecore Forms Extensions module is installed)

Sitecore Forms Extensions offers the ability to use Azure Blob Storage instead of SQL.

Enable Azure Blob Storage
=========================

To swap SQL storage with Azure Blob Storage, rename the file "App_Config/Include/Feature/FormsExtensions/Feature.FormsExtensions.AzureBlobFileStorageProviders.config.disabled" to "Feature.FormsExtensions.AzureBlobFileStorageProviders.config".

Open the file *Feature.FormsExtensions.AzureBlobFileStorageProviders.config* and fill in the "ConnectionString", "BlobContainer", and "Folder" setting.

.. code-block:: xml

  <?xml version="1.0"?>
  <configuration xmlns:patch="http://www.sitecore.net/xmlconfig/"  xmlns:role="http://www.sitecore.net/xmlconfig/role/">
    <sitecore>
      <services>
        <register serviceType="Sitecore.ExperienceForms.Data.IFileStorageProvider, Sitecore.ExperienceForms"
                implementationType="Feature.FormsExtensions.FileStorageProviders.AzureBlobStorageFileStorageProvider, Feature.FormsExtensions"
                lifetime="Transient"
                patch:instead="*[@serviceType='Sitecore.ExperienceForms.Data.IFileStorageProvider, Sitecore.ExperienceForms']" />
      </services>
      <settings>
        <setting name="AzureBlobStorageFileStorageProvider.ConnectionString" value="" />
        <setting name="AzureBlobStorageFileStorageProvider.BlobContainer" value="" />
        <setting name="AzureBlobStorageFileStorageProvider.Folder" value="" />
      </settings>
    </sitecore>
  </configuration>

Go to your storage account on Azure, browse to Access Keys, and copy either one of the connectionstrings.
Put this connectionstring in the config.

  .. image: azurestoragekeys.jpg

Enter the name you have chosen for your container in the blobcontainer part of the configuration.

Finally, you will notice the folder attribute in the configuration. 
You can leave this empty or put in any desired folder structure that should be followed to store your files in.

All forms will follow the same config for upload of the files. You cannot create individual storages for individual forms.
This is a design choice, so that content editors do not have to wory about where to store files.

However, the folder structure does support 3 variables that can be used:

- {formName} This handle will get replaced by the name of the form.
- {fieldName} Each form-upload field can be named in the forms-editor. This handle will be replaced by that name.
- {language} If you want to seperate the content by language, use this handle.

Note that none of these handles are required. They are all optional, including the order.