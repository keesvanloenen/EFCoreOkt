# Lab 15 - Basic Migrations

## Scenario

You'll split up the current project into two projects: `School.ConsoleApp` and `School.Library`. This will make the solution a bit more 'real-world'. The DbContext must be updated to support migrations. Finally you will use EF Core Migrations to generate a brand new database with the same schema that we've been using so far. Don't worry about filling the database with data, yet.

## Exercise 1: Add Library project

1. Update all installed NuGet packages to the latest version.
2. Add a Class Library project `School.Library` to the solution.
3. Add a project reference in the ConsoleApp to the Library.
4. Move the folders `DAL`, ` Models`, `ViewModels` and `Services` to the Library. Keep the folder `Menu` and `appsetttings.json` in the ConsoleApp project.
5. Remove the package `Microsoft.EntityFrameworkCore.SqlServer` from the ConsoleApp project, and add it to the Library project.
6. Update namespaces accordingly.
7. **Test if the ConsoleApp still runs.**

## Exercise 2: Comment out creation and fill logic

1. Comment out the 'quick prototyping' logic which is responsible for dropping and creating the database with each run:

   ```cs
   context.Database.EnsureDeleted();
   context.Database.EnsureCreated();
   ```

1. Also comment out the call to the `SchoolInitializer.Seed` method.
1. **Test if the ConsoleApp still runs.**

## Exercise 3: Update Connection String

1. Depending on your setup, you might have a connection string in the:

- `Program.cs`, or
- `appSettings.json`
  Modify the connection string to point to a new database: **SchoolV3**.

## Exercise 4: Update DbContext

1. Add a parameterless constructor to the `SchoolContext`.
1. Override the `OnConfiguring method`. Details in the code:

    <div class="flavor-sqlserver">

    ```cs
    // Constructor via which SchoolApp provide the connection details
    public SchoolContext(DbContextOptions<SchoolContext> contextOptions): base(contextOptions)
    {
    }

    // 👇 Add parameterless constructor to support migrations
    public SchoolContext()
    {
    }

    // 👇 Override OnConfiguring to support migrations
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // 👇 The program wasn't started by the console app apparently
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(/* connection string here */);
        }
    }
    ```

    </div>
    <div class="flavor-pgsql">

    ```cs
    // Constructor via which SchoolApp provide the connection details
    public SchoolContext(DbContextOptions<SchoolContext> contextOptions): base(contextOptions)
    {
    }

    // 👇 Add parameterless constructor to support migrations
    public SchoolContext()
    {
    }

    // 👇 Override OnConfiguring to support migrations
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // 👇 The program wasn't started by the console app apparently
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql(/* connection string here */);
        }
    }
    ```

    </div>

1. Copy the correct connection string into the `OnConfiguring` method. We don't like hard-coded connection strings, but we keep it as is for now.

## Exercise 5: Install EF Tools and Design

1. As `School.ConsoleApp` is the startup project, add the package `Microsoft.EntityFrameworkCore.Design` to the `School.ConsoleApp` project.
1. As `School.Library` contains the DbContext, add the package `Microsoft.EntityFrameworkCore.Tools` to the `School.Library` project.

## Exercise 6: Add Migration

1. Open the Package Manager Console.
1. Select **School.Library** as default project and run:

   ```shell
   Add-Migration InitialCreate
   ```

- Inspect the generated Migrations folder.

## Exercise 7: Build Database

1. Build the new database with all tables, fields and relations:

   ```shell
   Update-Database
   ```
    <div class="flavor-pgsql">

    - You might see an error about non-existing database SchoolV3. Don't worry, the migration will create it

    </div>

1. **Check if the database SchoolV3 has been created and the ConsoleApp runs (without any data yet).**
