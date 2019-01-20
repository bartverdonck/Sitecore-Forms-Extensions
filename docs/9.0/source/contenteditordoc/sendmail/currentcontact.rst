=======================
Send to current contact
=======================

Pick mailto “current xDB contact’s email address".

Just like the sitecore build in ‘send email campaign message’ submit action, 
this will send the exm mail to the current identified contact. 

However, using the send email action from the Sitecore Forms Extensions module will make the forms field values 
available as tokens for the composed email.

Note that, when you use the action, you must make sure that the current visitor is identified and has an email address on his contact data.
In order to this, you could use the :ref:`FormBindings` feature on an email field in the form.

.. image:: form-12-dialog-currentcontact.png