# Unity Build Badge Service

## How to Use

### Setup Cloud Build
1. In Unity's Build Cloud, view the project you want to get the status for
2. Take note of the project id GUID
3. View the Notifications options.
4. Click on Add New to add a Webhook.
5. For the URL enter http://unitybuildbadge.azurewebsites.net/api/update
6. Leave the Content Type as 'application/json'
7. Leave all the Events ticked
8. Switch SSL Verify to Off
9. Click Next: Save

### Add Badge
In the readme mark down add an image with the url http://unitybuildbadge.azurewebsites.net/api/status/<projectid>
where <projectid> is the GUID for the project

Example: `![Build Status](http://unitybuildbadge.azurewebsites.net/api/status/7edd192b-eb38-410a-9caa-54ed4192ae87)`

## How it works
The Unity Cloud Build posts build status information to the Unity Build Badge Service which is stored in a NoSQL database.

When the build badge with your Id is fetched the latest build status information is used to compose a (sheild.io)[http://shield.io] SVG format badge.