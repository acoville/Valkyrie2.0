# Valkyrie 2.0
## Documentation 

## Props

Props are non-interactive 2d sprites which are placed throughout a level to add
mood, aesthetic and depth to a scene. They are added in a map's <Props> element.

### Adding a Prop to your Project

Inside your map file, locate the <Props> tag and add a <Prop> child node to 
this node. The attributes are: 

the X and Y game-logic block coordinates.

the desired layer you would like this prop rendered on 

an Image attribute showing the engine where in the /Images/Props 
directory the images corresponding to this prop are. There can be more
than 1 .png file associated with this prop, the Prop class constructor
will parse all the images in this directory and pick one at random to use
when a new object of this type is created.