testrun .exe may be executed under MIT license terms until a license is choosen for the entire project.

all files within this folder are needed to run the application, other licenses may apply to these other
files, dunno.

abbr:
abbr	abbreviations
__WIP__ work in progress

________________________________________________________________________________________________________

2024, Aiding Elements User Interface readme.txt
_______________________________________________
currently implemented functions:
11 canvas screens with a total of 1.100.000.000 pixels screen space
easy accessible, every screen saves and loads its contents, each
screen can be modified via coreoptions element via changing of
'canvas' values.

each element can be shifted over the level system. each screen
holds a total number of 200 levels for the user, and one for the system,
elements can be made invisible, if level radiobox is selected in
levelshift element.

CoreOptions element has been improved, but is unfinished__WIP__

element control: __WIP__
	elements can be moved via left mouse button

	alt 	show or hide level system at screen top

	ctrl + left click
		FileLink, Image or Link or canvas_name(enter for save)
		 	reset element

	shift + left click on element
		select lement 

	hold shift + left mouse button pressed
		draw selection rectangle on canvas

	selected elements can be deleted with 'ctrl + shift + x'

	right click on element to delete that element

	selections can be manipulated via 'selection' menu item options

left click on surface: deselect all selected elements

right click on surface: instantiate a right click element, which allows
	you to quit the application or shutdown your computer
_______________________________________________
hints:
FileLink, Image and Link elements neet to be setup. From top to bottom
you'll need to first select a file, a folder or insert a webadress,
next line you can alter the linktext (and the size in case of Image)
and lastly you can finalize your work by hitting the "setup link" button.
to reset a Link, Image or FileLink element, hold CTRL and left click onto the element. 

once the ui was active for one time, a data folder will be created
in the same folder where the binary is located. within this folder
there is a core folder and within that are several ...data.xml files.
manipulate the values of the files to change AEUIs appearance, __WIP__
_______________________________________________
shortcuts:
Function Keys F1 ... F10 open main menu items

_______________________________________________
commands via Command element:
	current features:
>ELEMENTNAME create new element of type ELEMENTNAME on canvas
 currently implemented elements are:

	Copy, Paste, Move, Adjust, Request, 
	Command, Coordinates, CoreOptions, FileLink, Image, LevelShift, 
	Link, LocalDrives, Manual, MyNote, Random, RightClickChoice

<NUMBER select specific container 
<ELEMENTNAME select all elements of type ELEMENTNAME on canvas
<° select all containers

!.<° force delete all containers

_______________________________________________
Troubleshooting:
in case you delete the data folder and have a black aeui on startup, quit aeui
via windows functions, in folder data\core open ...Data.xml files and change
the color values. Background and Foreground need to be different, the color format
is ARGB, meaning Alpha, Red, Green Blue, each has two hexadecimal numbers.
To avoid transparency, make sure Alpha is set to FF or at least AA. To be able to
see anything, make sure Foreground and Background have some contrast.
