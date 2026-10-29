# Lab 05 - Many To Many Relationship

## Scenario

You will extend your application by configuring many-to-many relationships between `Course` and `Instructor`, and `Course` and `Student`. This lab focuses on setting up the correct relationships in your models and displaying the join entities.

## Task 1 - Course-Instructor  (implicit join entity)

- One `Course` can be taught by multiple `Instructors`.
- One `Instructor` can teach multiple `Courses`.

1. Create a join table with minimum effort. Just add navigation properties.  
   Compile and run the program. Inspect the generated table.
1. Add some fluent api in the course configuration file, to ensure that the names are different:

   - **CoursesInstructors**
     - **`CourseId`**, `int`, not nullable
     - **`InstructorId`**, `int`, not nullable  

   Compile and run the program. Inspect the generated table.

## Task 02 - Course-Student (explicit join entity and payload)

- One `Course` may be followed by multiple `Students`.
- One `Student` may be enrolled in multiple `Courses`.
- Each student `Enrollment` also stores the `Grade` of type nullable `decimal(3,2)`.

The database should get an `Enrollments` table with the following columns:

- `CourseId`  INT NOT NULL
- `StudentId` INT NOT NULL
- `Grade`     DECIMAL(3,2) NULL

`CourseId` and `StudentId` together form the primary key. `CourseId` is a foreign key related to `Course.Id`, and `StudentId` is a foreign key related to `Student.Id`.

1. Create an `Enrollment` entity and use fluent-api to configure the entity.
1. Add a property to the `DbContext`. Table name should be `Enrollments`.
1. Modify the two related entities. Compile and run the application. Inspect the new table.
1. In the `Seed` method, keep the first line and replace the remainder with the content of `Reference/seed-lab05.txt`.
1. Display the `StudentId` and `CourseId` of all enrollments in `EnrollmentService`, method `DisplayAllEnrollments`:

   ```txt
   StudentId CourseId
   6         2021
   6         2030
   7         2021
   ```
