# Xamarin-Firebase-Cross-Architecture

Xamarin.Forms Firebase Integration of CrossGeeks-FirebasePushNotificationPlugin


<img width="423" alt="Screenshot 2021-04-24 at 11 14 03 AM" src="https://user-images.githubusercontent.com/4394119/115948848-41eaa300-a4ee-11eb-9403-3748643d9051.png">

Libraries App:

1. CrossGeeks/FirebasePushNotificationPlugin
https://github.com/CrossGeeks/FirebasePushNotificationPlugin

2. Material Library
https://github.com/Baseflow/XF-Material-Library

3. FirebasePushNotificationPlugin
https://github.com/CrossGeeks/FirebasePushNotificationPlugin

4. ACR User Dialogs
https://github.com/aritchie/userdialogs



Libraries API:
1. LINQKit.Core
https://www.nuget.org/packages/LINQKit.Core/

2. Microsoft.EntityFrameworkCore
https://www.nuget.org/packages/Microsoft.EntityFrameworkCore


Google Firebase Cloud Messaging setup:
Follow Steps from https://github.com/CrossGeeks/FirebasePushNotificationPlugin.
Download google-services.json to obtain your Firebase Android config, add it to Xamarin-Android Project and set it's BuildAction to GoogleServiceJson.

Server Side Tables:
CREATE TABLE [UsersFcmTokens](
[ID] [bigint] IDENTITY(1,1) NOT NULL,
[UserID] [bigint] NULL,
[DeviceID] [varchar](100) NULL,
[Token] [varchar](max) NULL);



