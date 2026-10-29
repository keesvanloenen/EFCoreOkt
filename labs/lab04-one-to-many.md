# Lab 04 - One to Many Relationship

## Scenario

Your task is to modify the models to establish a one-to-many relationship between `Department` and `DepartmentAdministrator` and display the departments with their associated `AdministratorId`.

Details:

- A `Department` may have zero or one `DepartmentAdministrator`.
- A `DepartmentAdministrator` may have zero or more `Departments`.

## Exercise 1: Modify Models to setup the Relationship

**If you already have an idea of how to build the relationship, feel free to start coding, otherwise read further.**

1. Update the `Department` model. Include a nullable `AdministratorId` property and an `Administrator` property of type `DepartmentAdministrator`.
2. Update the `DepartmentAdministrator` model. Include a collection of `Departments`.

## Exercise 2: Add Data

1. In the `Seed` method, keep the first line and replace the remainder with the content of `Reference/seed-lab04.txt`.
   The only new thing is that departments now have got an `AdministratorId`.

## Exercise 3: Display Departments

1. Update the logic in `DepartmentService`, method `AllDepartments`. Show the `AdministratorId` as well.
