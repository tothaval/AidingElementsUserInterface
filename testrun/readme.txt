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


content and container save and load currently broken

recommendation: 
menu -> control -> System to access system canvas
in System canvas, LevelShift can be deactivated with click on LevelShift in 
menu -> tools -> LevelShift

use Command to call CoreOptions, element has been improved, but is unfinished__WIP__

element control: __WIP__
	elements can be moved via left mouse button

	ctrl + left click
		FileLink, Image or Link
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
F1: instantiate Manual element (english language only), __WIP__
	
F2: instantiate MyNote element, an application for the creation of notes
	to capture ideas, save and load of note contents now works
	properly, __WIP__

F3: instantiate FlatShareCostCalculator element, __WIP__ currently disabled

_______________________________________________
commands via Command element:
	current features:
>ELEMENTNAME create new element of type ELEMENTNAME on canvas
 currently implemented elements are:
	Command, Coordinates, CoreOptions, FileLink, Image, LevelShifter, 
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
