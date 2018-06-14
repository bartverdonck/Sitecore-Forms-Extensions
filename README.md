# Sitecore-Forms-Extensions

> Check my blogposts on http://onelittlespark.bartverdonck.be/category/sitecore-forms-extensions/ for detailed information on usage.

## Why
The new Sitecore Forms module that came with Sitecore 9 is a very promissing tool that allows content editors to add custom forms to their website.

This module aims to add some functionality to this forms creator.

## What
### 1.3
- The libraries integration with xDB had a refactoring. The update contact custom save action was removed. The forms extensions only focusses on the email address.
- Introduction of the IXDbContactFactory. <register serviceType="Feature.FormsExtensions.XDb.IXDbContactFactory, Feature.FormsExtensions" implementationType="Feature.FormsExtensions.XDb.FormsExtensionsXDbContactFactory, Feature.FormsExtensions" lifetime="Singleton" /> You can provide your own implementation here to use your own IdentifierSource and IdentifierValue to fetch contacts from xDB. The module comes with a default implementation that uses "email" as identifiersource and uses the email as its identiefiervalue.


### 1.2
- *Send Email*, Custom Submit Action: Replacement for Send Email to Fixed Address. This new action support sending mails to a fixed backoffice email address, the email of the current identified contact or to a value of the form. The values from the form are passed to EXM as custom tokens and can be used in the email.

### 1.1
- *Google ReCaptcha v2*, Custom Form Element: Add this field on your form to secure your form by adding a Google Recaptcha v2 control into your form.
- *File Upload*, Custom Form Element: Adds a file upload control to your toolbox. You can add a custom class to store the file anywhere you want. Out of the box this controls comes with a storage provider for local disk and for Azure Blog Storage Accounts. 

### 1.0
- *Send Email to Fixed Address*, Custom Submit Action: With this submit action you can send a mail to an email address defined on the submit action. (So not to the contact filling in the form as the build-in mailing action does) The values from the form are passed to EXM as custom tokens and can be used in the email.
- *Update Contact*, Custom Submit Action: This is an implementation of the [custom submit action walkthrough](https://doc.sitecore.net/sitecore_experience_platform/digital_marketing/sitecore_forms/setting_up_and_configuring/walkthrough_creating_a_custom_submit_action_that_updates_contact_details) in the Sitecore documentation. It allows you to update the contacts firstname, lastname and email based on the content of the form.

## Compatibility
The module is tested and compatible with Sitecore 9.0 - Update 1. Older versions are not supported.

## Installation
Download the module under "downloads" and install as sitecore package.
2 config files should be patched:
- *Feature.FormsExtensions.Settings.config*
  - GoogleCaptchaPublicKey
  - GoogleCaptchaPrivateKey
- *Feature.FormsExtensions.FileUploadStorageProviders.config* Enable one of the FileUploadStorageProviders and fill in the attributes.
  - FileSystemFileUploadStorageProvider: Requires a path to store the files and an URL format to download the uploaded file afterwards.
  - AzureBlobStorageFileUploadStorageProvider: Requires BlobStorage ConnectionString and name of the blobcontainer. The connection string can be found in Azure on the Storage Account resource under "Access Keys"


## Usage
> For detailed info, check my blogposts on http://onelittlespark.bartverdonck.be/category/sitecore-forms-extensions/

### Send Email To Fixed Address
- Create an automated message in EXM, you can use the token $formFields$ to render the entire form results or use $form_**fieldName**$ to add the fields individually
- Create a sitecore form, add the send email to fixed address action to your submit button. Enter a fixed email adress to send the form to, and choose your email campaign to be send out.

### Google Captcha Control
Form Editors can just put this control on to their form. No additional configuration required.

### File Upload Control
Form Editors can just put this control on to their form. No additional configuration required. Files are stored according to the installed fileuploadstorageprovider. (see installation)

### Content Type Validator
This validator is linked to the file upload control and allows to enter a list of allowed content types. Files with other content types will be refused.

### File Size Validtor
This validator is linked to the file upload control and checks the file size. If the file is larger then the entered value, it will be refused.
