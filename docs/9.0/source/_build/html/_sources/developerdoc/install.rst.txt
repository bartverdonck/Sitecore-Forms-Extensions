======================
Installation and Setup
======================
     
Download
========

The latest version can be found in the download section on `Github <https://github.com/bartverdonck/Sitecore-Forms-Extensions/tree/master/downloads>`_.

There are 3 distribution packages available:

Sitecore Forms Extensions-<version>.zip
  Sitecore Package to install via Sitecore's installation wizard. 
  This is the most common option.

Sitecore Forms Extensions-<version>.scwpd.zip
  Use this package for initial install on Azure PaaS via ARM templates.
  
Sitecore Forms Extensions-<version>-nodb.scwpd.zip
  Use this package for redeployment via ARM on Azure PaaS, this package will only install DLL's and file, sitecore items are excluded from this package.


Install
=======

Just install the package in Sitecore via installation wizard.

.. image:: installpackage.jpg


Configuration
=============

Most functionalities of the libray work out of the box. However, the fileupload and captcha features require some additional configuration.

File Upload
-----------

The file upload requires to set a location to store the uploaded files. A file upload storage provider must be added in configuration.

There are currently 2 store locations available:

.. toctree::
   :maxdepth: 1
   
   Azure Blob Storage <azureblobstorage>
   Local Disk Storage <localdiskstorage>

Google Captcha
--------------

For Google Captcha to work, we need to set the public and private key in the configuration.

.. toctree::
   :maxdepth: 1
   
   googlecaptcha