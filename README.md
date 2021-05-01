# Employment System Test
a web base software for employment. this software in asp .net core 3.0 razor page. 
this software has multi level that each level is a section of employment process like level 1 is primary information section, level 2 is change password section and ... .
manager can specify sequence of levels by modifing PageSequence table in database.

# Add New Level
for adding a new level to software you should do below steps:
1.create a new field in PageSequence table that its name is english number in letters like Ten, Eleven, Twelve and ... .
2.make new PageSequence field value null in previous rows.
3.scaffold database in visual studio.
4.in root Index.cs file in the "OnGet() > if(id==null)" section introduce new level.
5.in Utilities > utility.cs introduce new level in two section of file.
6.create new table for new level in database and scaffold it.
7.create new folder named "LevelN" that N is last number + 1 and should contain Index.cs file.
8.change partial view of steps for new level.
9.in Level folder (that has no number end of its name) you should enter last number + 1 for partial view model.


