# Lab 01 - Code-First Setup

## Scenario

Throughout the labs you'll work on a console app called 'School System'. The UI of this app has been built by another team already. It consists of menus and submenus. Also, a couple of services have been created, but they don't do anything yet, apart from showing a message in the console.

You will be responsible for applying _all_ logic. From building and filling a new `School` database, up to adding logic to query and modify data in all kinds of ways. Good luck!

## Exercise 1: DbContext

1. Open the provided solution `School` and run the project `School.ConsoleApp`. Test some menus.
1. Stop the app and investigate the menu logic and services.
1. Add the NuGet package <div class="flavor-sqlserver"> `Microsoft.EntityFrameworkCore.SqlServer`.</div><div class="flavor-pgsql"> `Npgsql.EntityFrameworkCore.PostgreSQL`.</div>
1. Add a 'DAL' folder (_DAL_ means _Data Access Layer_) to the project. Inside add a `SchoolContext` class that inherits from `DbContext`.
1. Add a constructor that accepts a parameter `options` of type `DbContextOptions<SchoolContext>`, pass `options` to the base. Keep the body of the constructor empty.
1. Configure the options in the `Main` method of the `Program` class, just before the services are instantiated.  
   The name of the database should be **`SchoolV2`**:

   <div class="flavor-sqlserver">

   ```cs
      var options = new DbContextOptionsBuilder<SchoolContext>()
         .UseSqlServer(@"Connection string here")
         .Options;
   ```

   </div>
   <div class="flavor-pgsql">

   ```cs
      var options = new DbContextOptionsBuilder<SchoolContext>()
         .UseNpgsql(@"Connection string here")
         .Options;
   ```

   </div>

1. Ensure that the database is created afterwards:

   ```cs
   using var context = new SchoolContext(options);
   // Add line here to create the database
   ```

1. Compile and run the application. Check if a database **`SchoolV2`** has been created.

## Exercise 2: Model

1. Add a 'Models' folder. Inside add a new `Person` class. Add the properties `Id`, `FirstName` and `LastName`. Example:

   ```cs
   public string FirstName { get; set; } = null!;
   ```

1. Add a property to the `DbContext` and map the `Person` class to the `People` table.
1. Compile and run the application. The database should have been recreated. Check if the table `People` is available.

## Exercise 3: Add data

1. In the 'DAL' folder add a new `SchoolInitializer` class with one static `Seed` method. This method should expect an `options` parameter of type `DbContextOptions<SchoolContext>`.
1. As done before, open a new instance of `SchoolContext` and pass `options`. Don't forget the `using` keyword.
1. Copy the code from `Reference/seed-lab01.txt` and paste it into the `Seed` method.

   Later you will learn more about seeding a database.

1. The database already contains an empty table. To ensure the old database is deleted _before_ a new database is created, add a line of code in the `Program` class.
1. Call the static `Seed` method after creating the database.
1. Compile and run the application. There should be no errors. Show all people in the next exercise.

## Exercise 4: Read & display data

1. Displaying people will happen in the `PersonService`. Add logic to this service so that the `DbContextOptions<SchoolContext>` can be injected:

   ```cs
   private readonly DbContextOptions<SchoolContext> _options;

   public PersonService(DbContextOptions<SchoolContext> options)
   {
       _options = options;
   }
   ```

1. When instantiating the `PersonService`, pass `options`:

   ```cs
   var personService = new PersonService(options);
   ```

1. Add logic to the `DisplayAllPeople` method so that all people are displayed this way:

   ```txt
   All People
    1 - Fadi Fakhouri
    2 - Stacy Serrano
    3 - Jasmine Stewart
    ...
   ```

1. Test.

1. Repeat step 1 and 2 for the other services as well to make them database-ready.

## Exercise 5: Use configuration file

(if time permits)

1. Move the connection string to a separate `appsettings.json` file and verify the application still works.
