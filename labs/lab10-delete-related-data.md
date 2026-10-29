# Lab 10 - Delete Related Data

## Scenario

You'll extend the database schema with a one-to-many relationhip between `Department` and `Course`. In the configuration ensure that departments with related courses cannot be deleted.

Then you'll add logic to:

- delete a Department Administrator and keep associated departments;
- delete a Department Administrator with associated departments (_unless_ the departments have associated courses, then an exception will be thrown which needs to be handled).

## Exercise 1: Create one-to-many between Department and Course

1. Update both models for this relationship:
   - a department can have zero or more courses
   - a course _must_ have a `DepartmentId`

1. Add fluent api for `Course` to ensure that when the user tries to delete a `Department` having related courses, the database raises an error.
1. In the `Seed` method, keep the first line and replace the remainder with the content of `Reference/seed-lab10.txt`. Both onlineCourses and onsiteCourses now have got a `DepartmentId`.
1. Build and run the application. No errors should occur.

## Exercise 2: Update the relationship Department - Administrator

1. This relationship is already optional as the `AdministratorId` foreign key in the `Department` entity is `nullable`.
1. Add fluent api to ensure that when an `Administrator` is deleted, the `AdministratorId` of all related departments is set to `null` _directly_ in the database.

## Exercise 3: Implement `DeleteDepartmentAdministratorKeepDepartments`

1. Allow the user to delete an administrator without deleting their departments. Start the method with:

   ```cs
   Console.Write("Enter administrator id: ");
   var administratorId = int.Parse(Console.ReadLine()!);
   ```

   Note. Kim Abercrombie (Id 31) is the administrator of department 'Engineering'. After deleting Kim, check Departments, Display All Departments. 'Engineering' should exist _without_ administrator.

1. After deletion show a confirmation:

   ```txt
   Enter administrator id: 34
   Department Administrator Candace Kapoor deleted and 1 related department(s) were kept
   ```

## Exercise 4: Implement `DeleteDepartmentAdministratorWithDepartments`

(if time permits)

1. In the `Seed` method, temporarily update the `DepartmentId` of the courses "Trigonometry" and "Calculus" from 4 to **1**.

1. Allow the user to delete an administrator along with their departments. Ensure the application catches database errors when trying to delete a department that has related courses.

1. Alway inform the user appropriately:

   ```txt
   Enter administrator id: 32
   Department Administrator Roger Harui and 1 related department(s) deleted
   ```

   ```txt
   Enter administrator id: 34
   Error deleting the Department Administrator. They may have courses associated with it.
   The DELETE statement conflicted with the REFERENCE constraint "FK_Courses_Departments_DepartmentId". The conflict occurred in database "SchoolV2", table "dbo.Courses", column 'DepartmentId'.
   ```

1. Undo the change in the `Seed` method.
