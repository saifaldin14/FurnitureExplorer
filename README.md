# FurnitureExplorer
 
 Furniture Explorer is an interactive and easy-to-use interio design system that allows the user to easily design and visualize a room from a set of pre-made furniture objects. 

# How to run the project
Simply clone this repository and open the project using Unity Hub. All assets and scripts are already in the repository so no set up is required.

# Features:
* Add object to scene by clicking on the button from the list of furniture to choose
* Move the object in the scene and place it where you want
   * The object will have a temporary green colour to indicate that this location is available
   * If there is another object at the location it will have a colour of red and the user will not be able to place it there
* Rotate the object position prior to placing it
   * Press the R key to rotate the object 90 degrees clockwise
   * Press the T key to rotate the object 90 degrees counterclockwise
* Select and deselect an already placed object
   * A dialog with different buttons will appear, the buttons are Change, Clone, Move and Delete
   * All placed objects are automatically selected by default 
   * To deselect, left click the mouse button
* Move an existing object
   * Press the Move button in the select dialog
* Delete an existing object
   * Press the Delete button in the select dialog
* Clone an object
   * Press the Clone button in the select dialog and a new instance of that selected object will appear in the scene, the user can then place it wherever they like in the scene
* Change the state of an object
   * Press the Change button in the select dialog
   * For this challenge the Change button will switch to a different model of that furniture but it can be used to show on/off functionality simply by updating the 3D models
* Save the scene
   * Press the S key and a JSON file will be generated locally in a Saves folder
* Load an existing scene
   * Press the L key and the system will check to see if there is a saves file, if there is the scene will be loaded and existing unsaved objects will be destroyed 
