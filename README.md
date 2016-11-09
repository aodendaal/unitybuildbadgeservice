# Unity Build Badge Service

## How to Use

### Setup Cloud Build
In Unity's Build Cloud, view the project you want to get the status for
Take note of the project id GUID
View the Notifications options.
Click on Add New to add a Webhook.
For the URL enter http://unitybuildbadge.azurewebsites.net/api/update
Leave the Content Type as 'application/json'
Leave all the Events ticked
Switch SSL Verify to Off
Click Next: Save
### Add Badge
In the readme mark down add an image with the url http://unitybuildbadge.azurewebsites.net/api/status/<projectid>
where <projectid> is the GUID for the project