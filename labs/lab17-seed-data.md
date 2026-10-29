# Lab 17 - Seed Data

Your new SchoolV3 database is currently empty. It contains tables, keys, indexes and a stored procedure, but it lacks data. Your task is to write code to insert data into these tables, ensuring that the data gets inserted during a migration.

## Exercise 1: Insert Data Using the HasData Method

In the entity configuration classes use the `HasData` method to add the seed data.

1. Add the content of `Reference/seed-lab-17-Department` to your `DepartmentConfiguration`.
1. Add the content of `Reference/seed-lab-17-Enrollment` to your `EnrollmentConfiguration`.
1. Add all configuration files from `Reference/Configurations` to `DAL/EntityConfigurations` _and_
   Apply them:

   ```cs
   modelBuilder.ApplyConfiguration(new DepartmentAdministratorConfiguration());
   modelBuilder.ApplyConfiguration(new InstructorConfiguration());
   modelBuilder.ApplyConfiguration(new OnlineCourseConfiguration());
   modelBuilder.ApplyConfiguration(new OnsiteCourseConfiguration());
   modelBuilder.ApplyConfiguration(new StudentConfiguration());
   ```

## Exercise 2: Add the Migration

1. In the PMC select the Class Library project as default project.
1. Add a migration to generate the seed data by running the following command:

   ```shell
   Add-Migration SeedData
   ```

## Exercise 3: Customize the `Up` Method

Since we don't have a class to map the table **`CoursesInstructors`**, we can't use the `HasData` method for that. This means that you will have to customize the new migration to add the rows manually.

1. Add the following items in the `Up` method:

   ```cs
   migrationBuilder.InsertData(
       table: "CoursesInstructors",
       columns: new[] { "CourseId", "InstructorId" },
       values: new object[,]
       {
           {2030, 1},
           {2021, 2},
           {1061, 3},
           {4041, 4},
           {4061, 5},
           {1050, 31},
           {1045, 32},
           {4022, 33},
           {2042, 34}
       });
   ```

## Exercise 4: Customize the `Down` Method

1. Ensure the `Down` method of the new migration class deletes the `CoursesInstructors` items _before_ the courses and instructors are deleted. This is important to avoid foreign key constraint violations when rolling back the migration.

   ```cs
   migrationBuilder.DeleteData(
       table: "CoursesInstructors",
       keyColumns: new[] { "CourseId", "InstructorId" },
       keyValues: new object[,]
       {
           { 2030, 1 },
           { 2021, 2 },
           { 1061, 3 },
           { 4041, 4 },
           { 4061, 5 },
           { 1050, 31 },
           { 1045, 32 },
           { 4022, 33 },
           { 2042, 34 }
       });
   ```

## Exercise 5: Apply the Migration

1. Execute the `Update-Database` command to apply the migration.
1. Build and run the app. All should work fine again!
