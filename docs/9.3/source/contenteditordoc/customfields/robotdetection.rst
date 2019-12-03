===============
Robot Detection
===============

Sitecore Forms 9.3 has build-in robot detection. This works pretty well, but sometimes people are marked as a robot by mistake. (e.g. When they use tracking blockers)

When this happens, the user will not notice that has session is not marked as genuine, and the form will just continue to work. However, the submit actions (marked as don't execute for robots) are not executed.
In effect, the data doesn't get saved, emails are not send out, etc..

The Robot Detection component of Sitecore Forms Extensions, will block the form submission if the session is marked as a robot and send a validation message to the user.

The message can be translated and adapted by editing the item "/sitecore/system/Dictionary/robotdetection-robotdetected".