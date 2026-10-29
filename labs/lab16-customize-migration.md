# Lab 16 - Customize Migration

In an earlier lab we worked with a stored procedure. You might have noticed that the migration we ran created a SchoolV3 database with the correct table schema but did not include any stored procedure. In this lab, you'll address this by manually creating a new migration and adding the SQL code to create the missing element.

## Exercise 1: Add an Empty Migration

1. Add an empty migration to your project class library. In PMC select `School.Library' as default project and run:

  ```shell
  Add-Migration SqlCustomization
  ```

## Exercise 2: Add DB Elements to the Migration

1. Find the <div class="flavor-sqlserver"> stored procedure </div><div class="flavor-pgsql"> function </div> `GetDepartmentsWithEnrollmentsAbove` in the `Seed` method of the `SchoolInitializer` class.
1. Modify the `Up` method of the new migration to execute the SQL statement to create the <div class="flavor-sqlserver">stored procedure</div><div class="flavor-pgsql">function</div>. Use the `migrationBuilder.Sql` method.

<div class="flavor-sqlserver">

  ```cs
  migrationBuilder.Sql(@"
      CREATE PROCEDURE [dbo].[YourStoredProcedure]
      AS
      BEGIN
          -- Your SQL code here
      END
  ");
  ```

</div>
<div class="flavor-pgsql">

  ```cs
  migrationBuilder.Sql(@"
      CREATE OR REPLACE FUNCTION school."YourFunction"()
      ...
      AS $$
      
          -- Your SQL code here
      $$;
  ");
  ```

</div>

## Exercise 4: Drop the Items in the Down Method

1. In the `Down` method of the new migration, add the `migrationBuilder.Sql` statements to drop the Store Procedure.

<div class="flavor-sqlserver">

  ```cs
  migrationBuilder.Sql("DROP PROCEDURE [dbo].[YourStoredProcedure]");
  ```

</div>
<div class="flavor-pgsql">

  ```cs
  migrationBuilder.Sql("DROP FUNCTION school.\"YourFunction\"");
  ```
  
</div>

## Exercise 5: Apply the Migration

- Execute the `Update-Database` command to apply the migration.
- Verify that the stored procedure has been added to the SchoolV3 database.
