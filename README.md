# Mixed Reality Design Labs
This repo is where Microsoft's Windows Mixed Reality Design team publishes examples and explorations. The goal is to inspire creators and help them to build Mixed Reality experiences. We share sample app projects here that demonstrate how to use various types of common controls and patterns in Mixed Reality. Find out details about common controls and sample apps on https://developer.microsoft.com/en-us/windows/mixed-reality/design

# Important: Adding submodule MRDesignLab
As soon as you clone the repo, init and update submodule with git command:
### cd MRDesignLabs_Unity
### "git submodule init"
### "git submodule update"
This will add [HUX and related tools](https://github.com/Microsoft/MRDesignLabs_Unity_tools) under Assets/MRDesignLab/ folder

# Periodic Table of the Elements
<img src="https://github.com/Microsoft/MRDesignLabs_Unity_PeriodicTable/blob/master/External/ReadMeImages/PeriodicTable_Hero.jpg" alt="Periodic Table of the Elements">

Periodic Table of the Elements is a open-source sample app from Microsoft's Mixed Reality Design Lab. With this project, you can learn how to layout an array of objects in 3D space with various surface types using Object collection. Also learn how to create interactable objects that respond to standard inputs from HoloLens. You can use this project's components to create your own mixed reality app experiences. 

# Technical Details

Open **MRDL_PeriodicTable\Scenes\Main.unity** to view the main scene. You will see a deactivated prefab called **ElementContainer** - activate it and navigate to 'Element' in the hierarchy. You will see three components: **Element, ElementButton**, and **PresentToPlayer**.
 
<img src="https://github.com/Microsoft/MRDesignLabs_Unity_PeriodicTable/blob/master/External/ReadMeImages/PeriodicTable_Technical1.jpg" alt="Periodic Table of the Elements">

**ElementButton** extends the abstract Button class - its job is to listen for state change events. It passes these events along to Element, which then handles displaying and animating information about the element. It relies in part on **PresentToPlayer**, which orients and moves the box toward the player.
 
Deeper in the prefab you'll find an **MoleculeObject** with the **Atom** component attached. This script uses Unity's instanced rendering API to draw hundreds of mesh particles with very few draw calls. This can be a useful alternative to Shuriken particle systems, which aren't always performant enough for Hololens devices without a great deal of careful tweaking.

<img src="https://github.com/Microsoft/MRDesignLabs_Unity_PeriodicTable/blob/master/External/ReadMeImages/PeriodicTable_Technical2.jpg" alt="Periodic Table of the Elements">

Box elements use the **AcrylicReflective** and **AcrylicReflectiveTransparent shaders** These can be used in other projects for anything that needs a shiny, stylized look without tanking the frame rate.
 
The **ObjectCollectionMode** script inherits from one of HUX's most powerful tools, the **InteractionReceiver** class. With it you can rapidly set up an object to listen for input from HUX buttons. Just use the inspector to drag HUX buttons to the Interactables array, override a base function like OnTapped, and you're ready to go.

<img src="https://github.com/Microsoft/MRDesignLabs_Unity_PeriodicTable/blob/master/External/ReadMeImages/PeriodicTable_Technical3.jpg" alt="Periodic Table of the Elements" width="450px">

If you're rapidly prototyping, using the button's name to differentiate input is a quick way to get things done. You could also create a custom component that information relevant to a button's function. This only scratches the surface of the **InteractionReceiver class** - we’ll be exploring it in full elsewhere.

# More from Mixed Reality Design Labs #
**Common Controls Examples**
https://github.com/Microsoft/MRDesignLabs_Unity




# Contributing

This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/). For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.
