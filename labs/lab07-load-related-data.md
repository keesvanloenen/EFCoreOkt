# Lab 07 - Load Related Data

## Scenario

You'll write queries to **eagerly load** related data from the database. Eager loading allows you to load related entities as part of the initial query, reducing the number of queries needed to retrieve the data.

Then you'll practice **explicit loading** of related data. This method allows you to load related entities on demand, which is useful for optimizing performance and controlling data retrieval.

## Exercise 1: Update AllDepartments

- First ensure that instead of `AdministratorId`, the full name of the `DepartmentAdministrator` is shown:

  ```txt
  Department name: Engineering, Budget: 350.000,00, Administrator: Kim Abercrombie
  Department name: English, Budget: 120.000,00, Administrator: Candace Kapoor
  ...
  ```

- Then try to also display the `Title` of all courses pertaining to the `DepartmentAdministrator`:

  ```txt
  Department name: Engineering, Budget: 350.000,00, Administrator: Kim Abercrombie
          - Chemistry
  Department name: English, Budget: 120.000,00, Administrator: Candace Kapoor
  ...
  ```

## Exercise 2: Update AverageGradePerStudent

- Show the student's full name instead of `StudentId`:

  ```txt
  Student                   Grade
  Gytis Barzdukas           3,75
  Peggy Justice             3,50
  ...
  ```

## Exercise 3: Update LowestGradePerCourse

- Show the course's `Title` instead of `CourseId`:

  ```txt
  Calculus: 1,50
  Chemistry: 3,50
  ```

## Exercise 4: Explicitly Load Related Data

Prompt the user to enter the `Id` of the `Instructor` they want to see more information about. Use the private static method `GetInstructorId`.

- Explicitly load the courses related to the selected `Instructor`. If the Instructor cannot be found show a message:

   ```txt
   No instructor found with id 66.
   ```

- Display the title of the courses that the `Instructor` is teaching. If there are no courses, display "No courses assigned":

   ```txt
   Enter instructor id: 31
   Instructor Kim Abercrombie
   Teaching:
           - Chemistry
   ```

   ```txt
   Enter instructor id: 34
   Instructor Candace Kapoor
   Teaching:
           - No courses assigned
   ```

Hint: Use the `Entry` method of the `DbContext` to access the `Collection` method for explicit loading.

