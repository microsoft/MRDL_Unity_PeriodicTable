# Mixed Reality Design Labs
This repo is where Microsoft's Windows Mixed Reality Design team publishes sample apps and experiments. Find out details on https://developer.microsoft.com/en-us/windows/mixed-reality/periodic_table_of_the_elements

# Periodic Table of the Elements
Periodic Table of the Elements is a open-source sample app from Microsoft's Mixed Reality Design Labs. With this project, you can learn how to layout an array of objects in 3D space with various surface types using [Object Collection](https://github.com/Microsoft/MixedRealityToolkit-Unity/blob/master/Assets/HoloToolkit-Examples/UX/Readme/README_ObjectCollection.md). Also learn how to create objects that respond to the standard inputs from HoloLens and Immersive headset's motion controllers, using  [Interactable Object](https://github.com/Microsoft/MixedRealityToolkit-Unity/blob/master/Assets/HoloToolkit-Examples/UX/Readme/README_InteractableObjectExample.md). You can use this project's components to create your own mixed reality app experiences. 

<img src="https://github.com/Microsoft/MRDesignLabs_Unity_PeriodicTable/blob/master/External/ReadMeImages/PeriodicTable_Hero.jpg" alt="Periodic Table of the Elements">



# Supported Unity version: 2017.2.0P1-MRTP4
The current supported version of Unity is [**Unity 2017.2.0p1-MRTP4**](http://beta.unity3d.com/download/b1565bfe4a0c/UnityDownloadAssistant.exe).  If you are looking to have support for previous versions of Unity please check under **[Releases](https://github.com/Microsoft/MRDesignLabs_Unity/releases)**.


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
## Common controls and examples ##
https://github.com/Microsoft/MRDesignLabs_Unity

<img src="https://github.com/Microsoft/MRDesignLabs_Unity/blob/master/External/ReadMeImages/InteractibleObject_Hero.jpg">
<img src="https://github.com/Microsoft/MRDesignLabs_Unity/blob/master/External/ReadMeImages/HolobarAndBoundingBox_Hero.jpg">

## Sample app - Lunar Module ##
<img src="https://github.com/Microsoft/MRDesignLabs_Unity_LunarModule/blob/master/External/ReadMeImages/LM_hero.jpg" alt="Lunar Module sample app">
https://github.com/Microsoft/MRDesignLabs_Unity_LunarModule

Lunar Module is a open-source sample app from Microsoft's Mixed Reality Design Labs, it is a spiritual sequel to the 1979 Atari classic, *Lunar Lander*. This sample app will demonstrate how to extend Hololens' base gestures with two hand tracking and xbox controller input, reactive objects to surface mapping and plane finding, and simple menu systems. You can use this project's components to create your own mixed reality app experience. 




# Contributing

This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/). For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.
