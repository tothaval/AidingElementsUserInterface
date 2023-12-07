development started 3 days ago, it doesn't do much atm, and i haven't choosen a license yet,

testrun .exe may be executed under MIT license terms until a license is choosen for the entire project.

all files within this folder are needed to run the application, other licenses may apply to these other files, dunno.



currently implemented functions:

elements can be moved via left mouse button

right click on surface: instantiate a right click element, which allows
	you to quit the application or shutdown your computer

F1: instantiate Manual element

	
F2: instantiate MyNote element, an application for the creation of notes
	to capture ideas, save and load of note contents now works
	properly

F3: instantiate FlatShareCostCalculator element, still work in progress,
	it currently produces some sort of stack overflow when saving or
	loading data from xml. couldn't figure it out today, but narrowed
	it down. somehow the load function is called a multitude of times,
	gonna fix this asap.