# Sitecore-Forms-Extensions

![Sitecore Forms Extensions Logo](https://raw.githubusercontent.com/bartverdonck/Sitecore-Forms-Extensions/master/docs/Sitecore-Forms-Extensions-Logo.png)

> Extensive documentation for developers and content editors can be found on http://onelittlespark.bartverdonck.be/sitecoreformsextentions/

> Check my blogposts on http://onelittlespark.bartverdonck.be/category/sitecore-forms-extensions/ for inspiration.

## Download

> [Downloads can be found here](https://github.com/bartverdonck/Sitecore-Forms-Extensions/tree/master/downloads)

## Container Support

If you use [containered development for Sitecore](https://containers.doc.sitecore.com/docs/intro), an assets image is available on Docker. https://hub.docker.com/r/bverdonck/sitecore-forms-extensions-assets

You can reference the image with bverdonck/sitecore-forms-extensions-assets

More info can be found on my blog. http://onelittlespark.bartverdonck.be/container-support-for-sitecore-forms-extensions/

## Why
The new Sitecore Forms module that came with Sitecore 9 is a very promissing tool that allows content editors to add custom forms to their website.

This module aims to add some functionality to this forms creator.

For Sitecore 9.3+ (include SC10) use version 3.0
For Sitecore 9.1.x till 9.2.x use version 2.3.1
For Sitecore 9.0.x use version 1.8.3

## Changelog
### 3.0 (for Sitecore 9.3+) (This version is compatible with Sitecore 10)
- *FileUpload Component*: Component removed in favour of build-in component
- *AzureBlobStorageFileUploadStorageProvider*: Store the file uploads in Azure Blob Storage instead of SQL. (Disabled by default)
- *RobotDetection*: NEW component to give the user feedback when his session was marked as a robot.
- *Send Email*:
  - Now uses user-defined tokens like the build-in sendmail from Sitecore.
  - File uploads of new component can be added as attachment
  - Possibility to generate a catch-all token "AllFormFields" containing all fields and values. (Token name can be changed in settings.)
  - Reviewed token generation and conversion to a string value. Conversion now relies on pipelines to increase flexibility and extensibility.
- *Recaptcha*: Fix for issue #66
- Compatibility updates for 9.3
- Revised package and namespace structure
- Cleaned up obsolete code

### 2.3
- Fix for Safari issue with jquery.unobstrusive-ajax.js #50
- Config setting fix #46
#### 2.3
- Bugfix: Google ReCaptcha cshtml threw error

### 2.2 (for Sitecore 9.1.x - 9.2.x)
- *Hidden Field*: Support for conditions, custom value (credits to @rolek)
- *Recaptcha: Multiple recaptcha's on one page
- *FileSystemFileUploadStorageProvider*: Support for datafolder variable
- *Send Email*: fix for value from dropdownlist/checkboxlist
- Tested with Sitecore 9.1.1
- Fixed build framework to 4.7.1
- Fixed package install options (also for 1.8.1)
#### 2.2.1
- Bugfix: Google Captcha did not work in the editor (Bug #40)

### 2.1 (for Sitecore 9.1)
- *Value Provider Preconditions*: Ability to define preconditions with the rules editor. Only when the defined preconditions are met, the forms will be prefilled.
- *Google Recaptcha*: Fixed bug #23 where users could bypass recaptcha by excluding it entirely from the post action.
- *Hidden Field*: Fixed inheritance for Sitecore 9.1 (Bug #26)

### 1.8 (for Sitecore 9.0.x)
- *Value Provider Preconditions*: Ability to define preconditions with the rules editor. Only when the defined preconditions are met, the forms will be prefilled.
- *Google Recaptcha*: Fixed bug #23 where users could bypass recaptcha by excluding it entirely from the post action.

### 2.0
- *Sitecore 9.1 Compatibility*: A new package for Sitecore 9.1 was created. In this package the prefill/binding feature has been update to leverage the new build in prefilling features from Sitecore. The existing xDB binders are now available as value providers. The module still adds the possibility to store the values as well. (For sitecore 9.0.x keep using version 1.7)

### 1.7
- *Make Contact Known*: When updating the current contact with a fieldbinded value, the contact is know marked as known. This behaviour can be disabled in sitecore config.
- *Send Email to fieldvalue*: The "update current contact" action has been updated, not to identify the user anymore on email. It only sets the preferred email address on the current contact and uses the current contact to send the mail instead of a separate service contact.

### 1.6
- *Attachments*: The send email submit action has been extended to allow file uploads to be added to the email as a file attachment.
- *Folder*: The file upload control's storageproviders have been updated to support (dynamic) subfolders. Example: <folder>formuploads/{formName}/{fieldName}/{language}</folder>
- The download package is now also available as scwp for Azure PaaS ARM.
#### 1.6.1
- Bugfix: when upgrading from an older version, send email action might throw a nullpointer, this is now fixed
#### 1.6.2
- Bugfix Issue 19: javascript errors in IE11 + edge, update script to be compliant
#### 1.6.3
- Bugfix: hidden values not submitted (incl. dropdowns)
#### 1.6.4
- Bugfix: Issue 20: nullpointer on tracker.current when using store binding on multiform

### 1.5
- *ShowFormPage*, custom submit action: With this submit action you set the page in your form that needs to be displayed after a succesfull submit. This is usefull when you don't want to redirect to a seperate thank you page but replace the form with a thank you message after submit. [More info can be found here](http://onelittlespark.bartverdonck.be/inline-thank-you-message-on-sitecore-forms/)
- *RawHTML*, custom field: The content entered in the Raw HTML field is rendered on the page as pure html without escaping. This can be usefull to add small inline javascript snippets or other custom html. In combination with the ShowFormPage, this can be used to trigger a datalayer event to track that the form was submitted.
- *HiddenField*, custom field: Use this field to add an input type hidden on the form. This can be usefull when you want to send additional info to your analytics datalayer. This field also supports the databinding functionality and is thus able to send some xDB profile information to the client.
- *Date Timespan Validator*, custom validator: With this validator you can compare the entered date with the current datetime. The package contains implementations to check wether a date is in the present or the past. It also contains a validator to check someones age entered through the datepicker. [More info can be found here](http://onelittlespark.bartverdonck.be/date-timespan-validator/)
- Bugfix on the Identify Contact Submit Action
- Bugfix on fileupload control in combination with checkbox list: validation is now working correctly.
#### 1.5.1
- Bugfix: there was an error in the provided download package containing a wrong config file. This caused errors when trying to send mail through EXM.

### 1.4
- *Binding Fields* aka Prefill, Added functionality to the forms module to prefill the fields with data from another source. Build-in the library supports prefilling from the xDB profile of the current user filling in the form. But it can be easily extended to add your own binding to external databases, crm's, userprofile, etc... Not only can you prefill the fields, but the module also allows to store the values back into the binded field. (e.g. You can prefill a first name field with the first name from the xDb profile. You can choose to save the value filled in onto the xDB profile after submittion.) [More info can be found here](http://onelittlespark.bartverdonck.be/prefill-fields-in-sitecore-forms/) [Find out how you can add your own databinder](http://onelittlespark.bartverdonck.be/configure-and-extend-field-binding-for-sitecore-forms/)
- *Identify Contact*, Custom Submit Action. Allows to choose a field from the form, who's entered value will be used as the identifiervalue of the xdb profile. (In other words, it will make the visitor a known contact.)
#### 1.4.3
- Bugfix for sql session state provider in combo with multipage form
- Only save file once in multipage forms

#### 1.4.2
- Bugfix for multipage forms

#### 1.4.1
- Added FieldBindingMapApiController to allowedControllers in sitecore config.

### 1.3
- The libraries integration with xDB had a refactoring. The update contact custom save action was removed. The forms extensions only focusses on the email address.
- Introduction of the IXDbContactFactory. You can provide your own implementation here to use your own IdentifierSource and IdentifierValue to fetch contacts from xDB. The module comes with a default implementation that uses "email" as identifiersource and uses the email as its identiefiervalue.
```xml
<register serviceType="Feature.FormsExtensions.XDb.IXDbContactFactory, Feature.FormsExtensions" implementationType="Feature.FormsExtensions.XDb.FormsExtensionsXDbContactFactory, Feature.FormsExtensions" lifetime="Singleton" /> 
```

### 1.2
- *Send Email*, Custom Submit Action: Replacement for Send Email to Fixed Address. This new action support sending mails to a fixed backoffice email address, the email of the current identified contact or to a value of the form. The values from the form are passed to EXM as custom tokens and can be used in the email.

### 1.1
- *Google ReCaptcha v2*, Custom Form Element: Add this field on your form to secure your form by adding a Google Recaptcha v2 control into your form.
- *File Upload*, Custom Form Element: Adds a file upload control to your toolbox. You can add a custom class to store the file anywhere you want. Out of the box this controls comes with a storage provider for local disk and for Azure Blog Storage Accounts. 

### 1.0
- *Send Email to Fixed Address*, Custom Submit Action: With this submit action you can send a mail to an email address defined on the submit action. (So not to the contact filling in the form as the build-in mailing action does) The values from the form are passed to EXM as custom tokens and can be used in the email.
- *Update Contact*, Custom Submit Action: This is an implementation of the [custom submit action walkthrough](https://doc.sitecore.net/sitecore_experience_platform/digital_marketing/sitecore_forms/setting_up_and_configuring/walkthrough_creating_a_custom_submit_action_that_updates_contact_details) in the Sitecore documentation. It allows you to update the contacts firstname, lastname and email based on the content of the form.

## Compatibility
The module was tested and found compatible with on Sitecore 9.0-u1, 9.0-u2, 9.1, 9.1.1, 9.2 and 9.3
Older versions are not supported.

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
