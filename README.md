# Valkyrie2.0

Valkyrie is v2 of an open-source 2d sidescrolling game engine. It is written in C#, using the Xamarin cross-platform mobile framework
and can target Universal Windows, Android and iOS operating systems. The goal of this engine is to create a useful tool that requires
little more than custom images and the ability to write some XML data files to create a unique game that can hit a broad audience. 
The app has been designed following the MVVM design pattern, with XAML content page markups, C# code behind and a C# ViewModel with
supporting classes.

# Support Valkyrie on Kickstarter

There is a Kickstarter campaign to support this project live right now. 
https://www.kickstarter.com/projects/valkyrieengine/valkyrie-an-open-source-2d-mobile-game-engine/

## What is covered by the open-source license: 

The Valkyrie.GL game logic .NET standard library: this contains game logic coordinates, collision detection, combat engine mechanics
and other non-graphical elements required to create a dynamic game. 

The Valkyrie.Graphics .NET standard library: this contains utility classes and the coordinate translation necessary to display game
elements on-screen, and relies heavily on the SkiaSharp graphics library. Most graphics work is handled with an instance of the 
SKGLView class, which utilizes native platform hardware acceleration. 

Valkyrie.Test: is an NUNIT Test project used to debug logic in the Valkyrie.GL library.

Valkyrie.App: This .NET standard library, formerly referred to in Xamarin as the PCL provides the .XAML markup and C# code-behind which
is ported at compile-time into the platform-specific OS builds. A stock splash screen page, a menu page, an options page and a game page
are included in this build to demonstrate execution, resource extraction, gameplay and control flow. Currently, this engine is specialized
for melee-combat like an old-school Street Fighter game with some jump-puzzle elements, however in the future I hope to develop a set of 
templates to cover more geanres. In this early alpha build we are also not yet multi-threaded, and inputs to the virtual game controller 
buttons tends to impact framerate. 

## What is not covered by the open-source license:

the embedded resources in the /Model directory are not open-source. These sample resources are provided to help developers become 
familiar with the way art is used in a Valkyrie project, and may be experimented with, altered and modded for any non-commercial 
use free of charge or royalty. For commercial application, or if you are interested in retaining my services to create content, 
send inquiries to adam.coville@gmail.com. 

Build artifacts are not covered by the open source license. Anyone may use this source code to create and monetize a propietary 2D game. 
These include debug or production executables, assemblies and .dlls. Any custom created content should be included in the 
Valkyrie.App.Model directory with the build action: Embedded Resource. This ensures that the images, sounds or other proprietary 
content are part of the compiled assembly and the file contents are not directly accessible in the host OS. 

## How to Create a game using Valkyrie

Step 1: simply fork or clone this repo. I developed this using Visual Studio 2019, which has the Xamarin mobile work package included
by default. Developers for Android may need to update your AppCompat library in NuGet. All projects in the /Platforms directory will 
need Xamarin.Essentials and SkiaSharp libraries. To debug on Android, you can either set up an Android virtual machine or connect an
Android phone with developer mode enabled. To debug on iOS, you will need a Mac to use as a remote build server (unfortunately, you cannot
directly compile to this platform from Windows), although Windows 10 users can debug the .UWP project on your local host. 

Step 2: I suggest starting with the sample map and the map manifest in Valkyrie.App/Model/Maps. Inside the <level> root node you will find 
several child nodes (their order inside the document are not important, the Level Loader will search the maps folder for filenames
matching the manifest and if it can successfully open the file, it parse the whole document to find them). The level loader loads an 
XmlDocument object into memory and passes it to the GameLogic layer, starting with the Level class constructor. From there, individual 
nodes of the doucment are passed off to instantiate the obstacles, monsters, events, and other elements needed to play the game. The 
advantage of doing things this way is that if you want to change the way for example a monster is serialized or deserialized you only 
need to change the map and the class constructor. The loading mechanisms never look at the data. 

Step 3: Custom artwork, maps. Any custom content should be included in the /Model folder. Once there, right click and access the properties
menu. Ensure that build action is set to: Embedded Resource. This ensures your proprietary content remains proprietary, since the file 
will be compiled into part of an assembly it will not be directly accessible to the host operating system.

More thorough documentation will be forthcoming once I have achieved a stable alpha build
