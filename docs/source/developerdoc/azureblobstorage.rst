.. _azureblobstorage:

============================
Configure Azure Blob Storage
============================
     
Setup Storage Account in Azure
==============================

First, in the case you don't have an Azure Blob Storage in your solution already, you will need to create one.

To do so, go into azure portal. Click New and choose Storage Account.

.. image:: storage-account-create-1.png

Next, create a storage container.

Choose public access level blob if you want easy access to the uploaded files by link. (All files will be renamed with a random GUID for security.)

*Note: This is not required for the module to work*

.. image:: storage-account-create-2.png


Add config file to your solution
================================

Next, we will tell the module how to connect to the storage account.

Create a file *Feature.FormsExtentions.AzureStorageProvider.config* and add it in your website folder under App_Config/Environment

.. code-block:: xml

  <?xml version="1.0"?>
  <configuration xmlns:patch="http://www.sitecore.net/xmlconfig/"  xmlns:role="http://www.sitecore.net/xmlconfig/role/">
    <sitecore>
      <formExtensions>      
        <fileUploadStorageProvider type="Feature.FormsExtensions.Business.FileUpload.AzureBlobStorageFileUploadStorageProvider, Feature.FormsExtensions">
          <connectionString></connectionString>
          <blobContainer></blobContainer>
          <folder>formextentions/{formName}/{fieldName}/{language}</folder>
        </fileUploadStorageProvider>
      </formExtensions>
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