# Valkrie 2.0
# Runtime Execution Flow

So when we get to the main menu and hit either the Newgame button or load a saved game,
what exactly is happening under the hood? I'll say chances are strong that if you have
a loading problem it won't be here, but it's still good to know what is going on under
the hood. 

For a developer's purposes, the entry point to your application will be in App.xaml.cs.
Your constructor runs InitializeComponent(), which is a Xamarin function that 
parses .XAML markup files into partial C# class definitions. I have chosen to institute a 
splash screen crediting my one-man studio by setting MainPage equal to a new SplashScreen
and navigating to it. 4 seconds later, The app's OnStart() method creates an instance 
of MainMenu and navigates to it. Feel free to delete or alter this to suit your project.

## Main Menu 

The MenuPage instance owns complex objects which control gameplay options and load level 
content at runtime, starting with the MenuPage's own View Model. This View Model (hence 
referred to as MPVM) creates a LevelLoader when created. 

## LevelLoader

The LevelLoader's constructor in turn, reads the LevelManifest.xml embedded resource in 
Valkyrie.App.Model.Maps. LevelLoader will parse this document for the number and names 
of map files and stores that list for later. Control returns to the MainMenu constructor, 
and then awaits user input

## Starting a New Game

Clicking the New Game button instructs the MPVM's LevelLoader to load the first level
contained in LevelManifest.xml. I will guess that most errors will happen here, either 
because of a typo in your .xml files or because one of the map files don't have the 
Build Action set to Embedded Resource. In these cases, a null reference exception will
be thrown at runtime when attempting to read the file complaining the variable is not 
set to an instance of object. All this means is that the MPVM's LevelLoader can't find 
the file. If it does find the file it will read the XML contents into memory in the 
form of a complete XmlDocument (from the System.Xml library) and pass this as a parameter
to a Level constructor from the Valkyrie.GL library. 

## Valkyrie.GL.Level

The Level class has a constructor accepting a complete XmlDocument as a parameter. It will 
navigate the node tree of this document, passing each node off to various other Valkyrie.GL 
class constructors. This is a trivially modifiable system. The only nodes that the Level
class parses for iteslf is the starting position and the background image. Every other piece,
from an obstacle to monsters to powerups - these nodes are parsed by their own class constructors.

This is advantageous to the developer. If you choose to change the way one of these classes is
serialized or extend its functionality, or create derived classes of it you only need to make changes
in 2 places: the XML document and the class constructor. The Level in this case is just a loading 
mechanism and never looks at that data. 



