Problems you may encounter on trying to compile the first time: 

Android: 

If your AppCompat.v7 Library is not up to date, you may see Visual Studio throw errors saying that it cannnot locate certain colors. Relax, this is nothing you did, you just need to go to NuGet and update your Platforms/Android project. AppCompat is a Xamarin library that targets Android API v27. Once you update AppCompat you should be able to compile without any issue. 

UWP: 

You may encounter issues with Windows Defender concerning unsigned app security. 

iOS: 

As of this writing, if you are compiling from a Windows workstation you will need to pair a Mac to use as a remote iOS build server. 

