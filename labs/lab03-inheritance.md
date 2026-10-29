# Lab 03 - Inheritance

## Scenario

In this lab you'll add models to represent _inherited entities_ and configuring them to map to new database tables.

## Task 1 - Students, Instructors and DepartmentAdministrators

- Watch this diagram:

:::mermaid
classDiagram
class Person {
Id: int
FirstName: string
LastName: string
}
class Student {
EnrollmentDate: Date
}
class Instructor {
HireDate: Date
}
class DepartmentAdministrator {
}
Person <|-- Student
Person <|-- Instructor
Instructor <|-- DepartmentAdministrator
:::

- A Student is a Person and has a property `EnrollmentDate` of type `Date`.
- An Instructor is a Person and has a property `HireDate` of type `Date`.
- A DepartmentAdministrator is an Instructor.

1. Add the new models to map this hierarchy to your application. There is no need to add model configurations yet. Update the `SchoolContext` accordingly. (Keep the `DbSet<Person>` property.)

1. All data should reside in one table: `People`.  
   **Before running the app, which database changes do you expect?**  
   Compile and run the app. Check the columns in the `People` table.

1. In the `Seed` method keep the first line and replace the remainder of the logic with the content of `Reference/seed-lab03.txt`. (Part of the code is commented out, this is on purpose.)

1. Build and run, no errors should occur.

1. Add logic to display the entities in the `PersonService`, methods:
   - `Students`
   - `Instructors`
   - `DepartmentAdministrators`

Note: Since an `Administrator` is an `Instructor`, when you show the `Instructors`, you may also see the `Administrators`. We will learn about queries and filters in a later lesson, so don't worry about that.

## Task 2 - OnlineCourses and OnsiteCourses

- Watch this diagram:

:::mermaid
classDiagram
class Course {
Id: int
Title: string
Credits: int
}
class OnlineCourse {
Url: string
}
class OnsiteCourse {
Location: string
Time: Time
}
Course <|-- OnlineCourse
Course <|-- OnsiteCourse
:::

- An `OnlineCourse` is a `Course` and has a `Url` of type `string`.
- An `OnsiteCourse` is a `Course` and has a `Location` of type `string` and a `Time` of type `Time`.

1. Like before, add the models to map this hierarchy to your application.

1. Update the `SchoolContext`.

1. This time, the database should get two additional tables: `OnlineCourses` and `OnsiteCourses`.  
   Both of these tables will have their own `Id` of type `int`, not nullable, set as `Primary Key`. Their `Id` column is also set as `Foreign Key` to the table `Courses`.

   Add one line of code to enable this in `CourseConfiguration`.

1. In the `Seed` method remove the logic to add courses:

   ```cs
   List<Course> courses = [...];
   context.AddRange(courses);
   context.SaveChanges();
   ```

   Uncomment the logic to add onlineCourses and onsiteCourses.

1. Add logic to display the entities in the `CourseService`, methods:
   - `OnlineCourses`
   - `OnsiteCourses`
