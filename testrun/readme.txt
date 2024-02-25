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
easy accessible, every screen saves and loads its contents in a separate
folder, each screen can be modified via coreoptions element via changing of
'canvas' values. a zoom feature and scroll bars assist you navigating each screen.

each element can be shifted over the level system. each screen holds a total
number of 200 levels for the user, and one for the system, elements can be
made invisible, if level radiobox is selected in LevelShift element.

main menu

[control -> Screens]
	will open Screens overview element, click on screen button to jump
	to the associated screen, screen button background will display
	canvas background, not level background
[control -> Command]
	will create a Command element to issue commands via textbox. look further
	down this document to learn more
[control-> System]
	will open SYSTEM screen
[control -> LevelSystem]
	will display level bar on top of screen,
	level 0 displays an overview over every other level of the active screen,
	use level buttons to navigate to the designated level, level name and
	level description can be changed via the level overview, as well as via
	the level bar(using Ctrl + LeftClick on name or description label),
	the level bar can also alter the level background
[control -> CoreOptions]
	will create an CoreOptions element on the screen, to change appearance of
	elements, select what you want to change, make the changes and hit [save changes]
	button, this will apply to all elements within aeui.
	if you want to change appearance only on certain elements, make a selection or
	make your changes in CoreOptions, while either button, container, label or
	textbox is selected in combobox, then hit [apply to selection] button.
	appearance of the selected elements will change , but specific changes on buttons,
	labels or textboxes within an element container will not be saved and loaded atm.
	fonts and some mainwindow features are currently not affected by changes.

CoreOptions element has been improved, but is unfinished__WIP__
BrushSetup element has been improved, but is unfinished __WIP__

element control: __WIP__
	elements can be moved via left mouse button

	alt 	show or hide level system at screen top

	ctrl + left click
		FileLink, Image or Link or canvas_name(enter for save)
		 	reset element

	shift + left click on element
		select lement 

	__BROKEN__ hold shift + left mouse button pressed
	__BROKEN__	draw selection rectangle on canvas
	
	selected elements can be deleted with 'ctrl + shift + x'

	right click on element to delete that element

	selections can be manipulated via 'selection' menu item options


left click on surface: deselect all selected elements

left mouse button pressed and mouse move: move sceen area
left double click outside of zoomed screen area will reset zoom to 0
mousewheel: zoom screen area, screen area will be slightly colored with transparent white

right click on surface: create a right click element, which allows
	you to quit the application or shutdown your computer
_______________________________________________
hints:
FileLink, Image and Link elements need to be set up. From top to bottom
you'll need to first select a file, a folder or insert a webadress,
next line you can alter the linktext (and the size in case of Image)
and lastly you can finalize your work by hitting the "setup link" button.
to reset a Link, Image or FileLink element, hold CTRL and left click onto the element. 

once the ui was active for one time, a data folder will be created
in the same folder where the binary is located. within this folder
there is a core folder and within that are several ...data.xml files.
manipulate the values of the files to change AEUIs appearance or for debug, __WIP__
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
