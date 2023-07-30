# HeroBase

ALL OF THE FOLLOWING IS SUBJECT TO RECONSIDERATION UPON DISCUSSION

Database with a front-end program to create and store TTRPG characters.
Lets get into it, though:

TTRPG Character Database will become more than a mere database.

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
If I'm mistaken, let me know.

    Database and Front-End Program:
        The project aims to create a database to store TTRPG characters along with a front-end program for user interaction.
        Users will create accounts to manage their characters, and the front-end should provide a stylized character sheet for character creation and editing.
        The front-end should sync with the database, updating character data as users input different values into the character sheet fields.

    Character Creation Options:
        The character creator should offer options like rolling 4d6 drop the lowest and other systems, as well as a point buy option.
        Additionally, users should have the ability to manually roll dice and input the values into the character's stat fields.

    Automated Calculation:
        The character sheet should automatically generate certain values that are the outcome of mathematical calculations based on the input data.

    Project Name and Program:
        The project is named "HeroBase," and the front-end program is called "HeroBase" and uses a subprogram called "CharForge" to generate and
        fill out character sheets.

    Technologies and Implementation:
        The project will be built using C# and ASP.NET.
        The back-end will connect to Microsoft SQL Server for data storage.
        The development will follow the Model-View-Controller (MVC) design pattern, where C# will handle the application's logic (Model), ASP.NET
          will handle the presentation (View), and the Controller will manage the flow and communication between the Model and View.
