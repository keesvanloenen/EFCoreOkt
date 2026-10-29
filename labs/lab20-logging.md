# Lab 20 - Logging

⭐ **This lab is optional** ⭐

## Scenario

You are going to enable simple logging on your existing application.

## Exercise 1: ChangeTracking and LogLevel.Information

1. Configure your Console Application to log the `Microsoft.EntityFrameworkCore.ChangeTracking` category on the console.
1. Filter the log level to `LogLevel.Information`.
1. Run the application and execute `AddUpdateDeleteDepartment`.
1. Inspect the console and notice no logs.

## Exercise 2: ChangeTracking and LogLevel.Debug

1. Filter the log level to `LogLevel.Debug`.
1. Run the application and execute `AddUpdateDeleteDepartment`.
1. Inspect the console and notice the logs, missing the Id of the tracked entity.

## Exercise 3: Sensitive Data Logging

1. Enable Sensitive Data Logging.
1. Run the application and execute `AddUpdateDeleteDepartment`.
1. Inspect the console and notice the logs, containing the Id of the tracked entity.
