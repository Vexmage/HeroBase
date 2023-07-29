# HeroBase
Database with a front-end program to create and store TTRPG characters.
Lets get into it, though:

TTRPG Character Database will be more than a mere database.

The database functionality should be advanced
However, attention must be given to the front end and
what that will look like.


Users will make an account, on this account, they can create
characters.  There should be a stylized character sheet that syncs
with a database.  When different values are entered in the different
fields, the database gets updated. There could be a character creator
option in which you're given a prompt between rolling 4d6 drop the lowest
and other systems, or point buy.  Additionally, there should be an option
 to just roll dice without using the automated generator and input the 
values yourself into the stat fields. The character sheet should automatically
 add certain fields to generate numbers for fields that are the
 outcome of a mathematical calculation.


For a name I'm thinking it could be called HeroBase
and it utilizes a built in program called CharacterForge or CharForge
to build the characters that get stored in the database.

The idea is we'll be building this in C# and ASP, connecting to microsoft SQL server.
If I understand things right, we'll begin by designing the C# code before we begin to gradually
implement the SQL.  We'll be using the Model-View-Controller (MVC) design pattern and C#. 
