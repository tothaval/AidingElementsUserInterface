testrun .exe may be executed under MIT license terms
until a license is choosen for the entire project.

all files within this folder are needed to run the
application, other licenses may apply to these other
files, dunno.

currently implemented functions:

element control: __WIP__
	elements can be moved via left mouse button

	ctrl + left click
		Link or FileLink element 
		 	reset element

	shift + left click on element
		select lement 

	hold shift + left mouse button pressed
		draw selection rectangle on canvas

	selected elements can be deleted with 'ctrl + shift + x'

	right click on element to delete that element

left click on surface: deselect all selected elements

right click on surface: instantiate a right click element, which allows
	you to quit the application or shutdown your computer

Link and FileLink elements neet to be setup. From top to bottom
you'll need to first select a file, a folder or insert a webadress,
next line you can alter the linktext and lastly you can finalize your
work by hitting the "setup link" button. to reset a Link or FileLink
element, hold CTRL and left click onto the element. 

F1: instantiate Manual element (english language only), __WIP__
	
F2: instantiate MyNote element, an application for the creation of notes
	to capture ideas, save and load of note contents now works
	properly, __WIP__

F3: instantiate FlatShareCostCalculator element, __WIP__

Command element currently features:
>ELEMENTNAME create new element of type ELEMENTNAME on canvas
<ELEMENTNAME select all elements of type ELEMENTNAME on canvas

once the ui was active for one time, a data folder will be created
in the same folder where the binary is located. within this folder
there is a core folder and within that are several ...data.xml files.
manipulate the values of the files to change AEUIs appearance, __WIP__

abbr:
abbr	abbreviations
__WIP__ work in progress