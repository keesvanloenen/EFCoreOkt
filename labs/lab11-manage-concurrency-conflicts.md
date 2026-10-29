# Lab 11 - Manage Concurrency Conflicts

## Scenario

You will extend the application to manage concurrency. The goal is to ensure that updates to the `Departments` table handle concurrency appropriately.

## Exercise 1: Entity Configuration

1. The **Departments Table** has a `Name` column of type `varchar(50)`, which is not nullable. Configure the `Department` entity to use the `Name` column as a concurrency token during updates.

## Exercise 2: Implement Update Operation

1. In `DepartmentService` implement the method `UpdateDepartment`. Start with the happy path (no concurrency conflicts).  
   As a starting point copy/paste next logic (don't worry about the errors):

   ```cs
   Console.Write("Enter Department Id: ");
   var departmentId = int.Parse(Console.ReadLine()!);

   Console.WriteLine($"Id = {department.Id}, Name = {department.Name}, Budget = {department.Budget}, Startdate = {department.StartDate}, AdministratorId = {department.AdministratorId}");

   Console.Write("Enter the new name: ");
   var newName = Console.ReadLine()!;

   Console.Write("Enter the new budget: ");
   var newBudget = decimal.Parse(Console.ReadLine()!);

   Console.Write("Enter the new start date: ");
   var newStartDate = DateOnly.Parse(Console.ReadLine()!);
   ```

1. Implement the remainder of the logic. Update a department with the given user input, save to the database and show a confirmation message afterwards.

   ```txt
   Enter Department Id: 1
   Id = 1, Name = Engineering, Budget = 350000,00, Startdate = 01/09/2007, AdministratorId = 31
   Enter the new name: Software Development
   Enter the new budget: 999999
   Enter the new start date: 25/5/2026
   Department updated:
   Id = 1, Name = Software Development, Budget = 999999, Startdate = 25/05/2026, AdministratorId = 31
   ```

## Exercise 3: Add Concurrent Action

1. Before the `SaveChanges()` add a line of code to mimick another user touching the same data.

   <div class="flavor-sqlserver">

   ```cs
      // Simulate a change in the database by another user
      context.Database.ExecuteSql($"Update Departments SET Name = 'Something different' WHERE Id = {departmentId}");
   ```

   </div>
   <div class="flavor-pgsql">

   ```cs
      // Simulate a change in the database by another user
      context.Database.ExecuteSql($"Update school.\"Departments\" SET \"Name\" = 'Something different' WHERE \"Id\" = {departmentId}");
   ```

   </div>

1. Test. An exception will be thrown:

   ```txt
   Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException: 'The database operation was expected to affect 1 row(s), but actually affected 0 row(s); data may have been modified or deleted since entities were loaded.
   ```

## Exercise 4: Exception Handling

1. Handle this exception to manage conflicts and prompt the user for resolution.
1. Implement and test the **🛢️ Database Wins** strategy.
1. Implement and test the **🧑‍💻 Client Wins** strategy.
