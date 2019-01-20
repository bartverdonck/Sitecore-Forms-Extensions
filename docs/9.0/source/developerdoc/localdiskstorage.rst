============================
Local Blob Storage
============================
     
Please note that when using this configuration on a loadbalanced CD, you must make sure, the local filepath is shared
between the instances.


Add config file to your solution
================================

Create a file *Feature.FormsExtentions.LocalStorageProvider.config* and add it in your website folder under App_Config/Environment

.. code-block:: xml

  <?xml version="1.0"?>
  <configuration xmlns:patch="http://www.sitecore.net/xmlconfig/"  xmlns:role="http://www.sitecore.net/xmlconfig/role/">
    <sitecore>
      <formExtensions>      
        <fileUploadStorageProvider type="Feature.FormsExtensions.Business.FileUpload.FileSystemFileUploadStorageProvider, Feature.FormsExtensions">
          <rootStoragePath>c:\temp\</rootStoragePath>
          <fileDownloadUrlBase>https://myfile.com/{0}</fileDownloadUrlBase>
		  <folder>formextentions/{formName}/{fieldName}/{language}</folder>
        </fileUploadStorageProvider>
      </formExtensions>
    </sitecore>
  </configuration>

The fileDownloadUrlBase is use to build a url that will host the uploaded images. Note that the module won't serve the images.
It is up to you to provide a download service for the images. If you don't want to create this, consider using *Azure Blob Storage Provider*.
It is not mandatory that a download service is provided for the module to work.

Make sure the application has enough rights to create files in the rootStoragePath you configured.

All forms will follow the same config for upload of the files. You cannot create individual storages for individual forms.
This is a design choice, so that content editors do not have to wory about where to store files.

However, the folder structure does support 3 variables that can be used:

- {formName} This handle will get replaced by the name of the form.
- {fieldName} Each form-upload field can be named in the forms-editor. This handle will be replaced by that name.
- {language} If you want to seperate the content by language, use this handle.

Note that none of these handles are required. They are all optional, including the order.