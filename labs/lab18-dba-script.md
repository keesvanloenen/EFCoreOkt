# Lab 18 - DBA Script

⭐ **This lab is optional** ⭐

You'll have to deploy your database to another machine using EF Core's migration tools. Let's build a SQL script, which is useful for DBAs to review before execution.

## Prerequisites

Ensure that you have completed the previous labs and have the following setup:

- Multiple projects: Console Application and Class Library.
- Existing migrations set up and ready to go.

## Exercise 1: Generate SQL Scripts

- In the PMC select the Class Library project as default project.
- Generate the SQL scripts for all the migrations by executing:

  ```shell
  Script-Migration -Output "sql-scripts\script.sql"
  ```

- This command will create a SQL script that includes all the necessary commands to create and seed the database.

## Exercise 2: Review the SQL Scripts

- Open the generated SQL script in a text editor.
- Review the script to ensure it contains the expected commands for creating tables, constraints, indexes, and seeding data.
