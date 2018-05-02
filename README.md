# Sitecore-Forms-Extentions

## Why
The new Sitecore Forms module that came with Sitecore 9 is a very promissing tool that allows content editors to add custom forms to their website.

This module aims to add some functionality to this forms creator.

## What
### 1.0
- *Send Email to Fixed Contact*, Custom Submit Action: With this submit action you can send a mail to an email address defined on the submit action. (So not to the contact filling in the form as the build-in mailing action does) The values from the form are passed to EXM as custom tokens and can be used in the email.
- *Update Contact*, Custom Submit Action: This is an implementation of the [custom submit action walkthrough](https://doc.sitecore.net/sitecore_experience_platform/digital_marketing/sitecore_forms/setting_up_and_configuring/walkthrough_creating_a_custom_submit_action_that_updates_contact_details) in the Sitecore documentation. It allows you to update the contacts firstname, lastname and email based on the content of the form.

## Compatibility
The module is tested and compatible with Sitecore 9.0 - Update 1. Older versions are not supported.

## Instalation
Download the module under "downloads" and install as sitecore package. No configuration is required.

