testrun .exe may be executed under MIT license terms
until a license is choosen for the entire project.

all files within this folder are needed to run the
application, other licenses may apply to these other
files, dunno.


currently implemented functions:

elements can be moved via left mouse button

right click on surface: instantiate a right click element, which allows
	you to quit the application or shutdown your computer

F1: instantiate Manual element (english language only)
	
F2: instantiate MyNote element, an application for the creation of notes
	to capture ideas, save and load of note contents now works
	properly, the element itself is still work in progress(__WIP__)

F3: instantiate FlatShareCostCalculator element, still __WIP__


began implementing property saving and loading,
once the ui was active for one time, a data folder will be created
in the same folder where the binary is located. within this folder
there is a core folder and within that is a coredata.xml file.
manipulate the values of that file to change parts of the appearance.
still wip, other data types will follow next.