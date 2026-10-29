# Lab 06 - Querying Data

## Scenario

In the previous labs you've added tables and data to the database. Let's use LINQ to query data as a real data scientist.

## Exercise 1: Filter and Sort Students

- Update the query in `PersonService`, method `Students`. Sort the students by their enrollment date, from the earliest to the latest:

  ```txt
  Student                EnrollmentDate
  Meredith Browning      01/09/2000
  Alexandra Walker       01/09/2000
  Damien Jai             01/09/2001
  ...
  ```

## Exercise 2: Find the First Instructor Hired

- Add logic to `PersonService`, method `FirstInstructorHired`, to display the first instructor hired, based on hire date:

  ```txt
  Kim Abercrombie (11/03/1995)
  ```

## Exercise 3: Calculating Average Grades

- Add logic to `PersonService`, method `AverageGradePerStudent`, to display the average grade for each StudentId:

  ```txt
  StudentId  Grade
  6          3,75
  7          3,50
  ...
  ```

## Exercise 4: Finding the Lowest Grade per Course

- Add logic to `CourseService`, method `LowestGradePerCourse`, to find the lowest grade for each CourseId:

   ```txt
   CourseId  Lowest Grade
   1045      1,50
   1050      3,50
   ...
   ```
