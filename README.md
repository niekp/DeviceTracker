# DeviceTracker

Monitor the on/off time of a device and send notifications according to a ruleset.

- Request access to a device.
- View the time the device is on or off.
- Set rules to send a notification after a device has been off or on for a certain amount of time.

Can be used to monitor servers, computers, services.

![devices](https://user-images.githubusercontent.com/19265518/102508252-7bfdf100-4085-11eb-8811-7ae634278804.png)

Create a cronjob to send a poll to `/ping/ping/?device=Identifier&Info=OptionalInfo` every minute a device is running. 
