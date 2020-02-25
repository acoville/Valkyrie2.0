# Valkyrie 2.0

## Setting up your Development Environment


##1. Visual Studio 2019 

    If you do not already have Visual Studio installed, head to https://visualstudio.microsoft.com/vs/ 
    and select the appropriate version. 2019 should by default include the Xamarin mobile work flow and 
    all tools needed to start and compile a Xamarin project. I suggest creating a new Mobile project 
    and ensuring it compiles and can run before starting on a Valkyrie project. I actually had a hard 
    time getting a passing grade because my assessor's development environment simply would not compile
    my project.  

### Can't I just use Visual Studio Code / Powershell or bash from Linux to do this? 

    You can of course do your editing with whatever tools you want, but as of this writing .NET Core does 
    not have any CLI tools to support Xamarin. It would be nice, I prefer using VS Code and managing git 
    from the console. But in the meantime to get any kind of workflow requires Visual Studio 2019.

##2. Android Studio, Android SDK 
see: Microsoft's Android Documentation
https://docs.microsoft.com/en-us/xamarin/android/

    If you are developing for the Android platform, you will need to install Android 
    Studio https://developer.android.com/studio. This will make sure that the Android 
    SDK and most recent version of Java are installed and you can configure an Android 
    virtual machine for debugging. Visual Studio will then be able to launch that 
    VM and debug using it. I found the VM useful for making sure the menus work 
    correctly, however even with hardware acceleration enabled (either HyperV or Intel 
    HAXM) my gameplay framerate was only about 16 fps. 

    For debugging gameplay I suggest pairing an actual Android phone over USB. Developer mode
    needs to be enabled on your phone to do this. Here is a guide for enabling developer mode: 
    https://www.howtogeek.com/129728/how-to-access-the-developer-options-menu-and-enable-usb-debugging-on-android-4.2/

### Compiling Android project for first time: 

    Quick note: when trying to compile / launch the Android project the first time you may 
    see some errors from the IDE about missing colors. This is not a problem with your code or mine.
    This is just a deprecated color set from an older Android SDK. AppCompat.v7 is a library
    Xamarin uses to help compile Android projects. To make this go away, update your Android 
    Project's AppCompat version in NuGet. More information on AppCompat.v7 and Xamarin.Essentials
    can be found here: 
    
    https://montemagno.com/resolving-android-support-library-nuget-installation-issues/

##3. Paired mac, iOS
see: Microsoft's iOS Documentation
https://docs.microsoft.com/en-us/xamarin/ios/

    I have yet to compile a project to iOS with Xamarin, but if your development machine is running Windows,
    in Visual Studio you have to "pair to mac" and use this mac as a remote buildserver to create an iOS 
    Assembly. This is due to the closed-source licensing terms of Apple's iOS platform. Similarly, to launch 
    an iPhone simulator requires a paired mac. We can hope that Apple changes and embraces open architecture but 
    I won't hold my breath. If you are running this on a Mac, please document your efforts and email me with what
    you find, or make a pull request to update this documentation yourself.

##4. UWP
see: Microsoft's UWP Documentation
https://docs.microsoft.com/en-us/xamarin/xamarin-forms/platform/windows/

    If you are developing in a Windows environment you should be able to debug on your local host by selecting 
    the UWP Project as your Startup Project. You may have to mess with Windows Defender to allow side-loading of
    apps, as your App in the early stages will be unsigned. 

##5. Other Tools (Optional)

    For developing your A/V resources whatever tools make you comfortable. I develop my 2D sprites, tiles and
    other art with AutoDesk SketchBook Pro, which has a lot of the same features as Photoshop and Illustrator 
    like layers, alpha channels, supporting Wacom tablets, etc.. but importantly is free. Download it here: 

    https://sketchbook.com/

    For version control I prefer Git and you will find a .gitignore and .git in the root directory of this 
    project. If you'd prefer to use another system like Subversion that's your business, but just know that I
    maintain this project on Github and this is going to be the best way to pull in updates as I push them. 