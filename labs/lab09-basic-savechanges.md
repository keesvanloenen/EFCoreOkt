# Lab 09 - Basic SaveChanges

## Scenario

You will focus on the `SaveChanges` method to perform basic data operations such as inserting, updating, and deleting a single `Department` entity.

In `DepartmentService` lives the method `AddUpdateDeleteDepartment`. This method calls 3 private methods which you'll implement in the next exercises.

## Exercise 1: Add a New Department

- Prompt the user to enter the name and budget for a new department. Copy/paste next code in `AddNewDepartment`:

  ```cs
  Console.Write("Department Name: ");
  var name = Console.ReadLine();
  if (name == null)
  {
      Console.WriteLine($"Not a valid name: {name}");
      return;
  }

  Console.Write("Department Budget: ");
  var input = Console.ReadLine();
  if (!decimal.TryParse(input, out var budget))
  {
      Console.WriteLine($"Not a valid budget: {input}");
      return;
  }
  ```

- Set the start date to the current date.
- Insert the new department into the database and save the changes.
- Show a confirmation message afterwards:

  ```txt
  Department Name: Test
  Department Budget: 100000
  Department 'Test' added with ID 5
  ```

## Exercise 2: Update the Budget of the Most Recently Added Department

- Identify the most recently added department.
- Prompt the user to enter a new budget:

  ```txt
  New budget for department 'Test': 130000
  ```

- Update the budget of the identified department and save the changes.
- Show a confirmation message:

  ```txt
  Department 'Test' has got a new budget: 130000
  ```

## Exercise 3: Delete the Most Recently Added Department

- Identify the most recently added department.
- Delete the identified department from the database and save the changes.
- Show a confirmation message:

  ```txt
  Department 'Test' has been deleted
  ```

## Exercise 4: Show State Entity

1. Review the method `AddUpdateDeleteDepartment` (`DepartmentService`). Look at the code you added earlier.

1. In the method `AddNewDepartment` add a statements before and after `SaveChanges`:

   ```cs
   Console.WriteLine($"***** State: {context.Entry(department).State} BEFORE SaveChanges *****");
   context.SaveChanges();
   Console.WriteLine($"***** State: {context.Entry(department).State} AFTER SaveChanges *****");
   ```

1. Try to predict what will be shown and run the logic.

1. Repeat above steps for the `UpdateBudgetLastAddedDepartment`

1. Repeat above steps for the `DeleteLastAddedDepartment`

## Exercise 5: Detect Changes and use ChangeTracker.DebugView

1. Remove the lines from previous exercise.

1. Add a helper in `DepartmentService`:

   ```cs
   private static void DisplayChangeTrackerDebugView(SchoolContext context, bool detectChanges = true)
   {
       var defaultColor = Console.ForegroundColor;

       if (detectChanges)
       {
           context.ChangeTracker.DetectChanges();
       }
       Console.ForegroundColor = ConsoleColor.Green;
       Console.WriteLine(context.ChangeTracker.DebugView.LongView);
       Console.ForegroundColor = defaultColor;
   }
   ```

1. Call this helper just before the `SaveChanges` in `UpdateBudgetLastAddedDepartment`:

   ```cs
   DisplayChangeTrackerDebugView(context);
   ```

   Test.

1. Also call this helper just after the `SaveChanges`.
   Test.

1. Update the first call to not detect changes:

   ```cs
   DisplayChangeTrackerDebugView(context, false);
   ```

   Test.
   Remove the second argument (`false`).

## Exercise 6: PropertyEntry

1. Add another helper:

   ```cs
   private void DisplayPropertyEntries(IEnumerable<PropertyEntry> propertyEntries)
   {
       var defaultColor = Console.ForegroundColor;
       foreach (PropertyEntry property in propertyEntries)
       {
           Console.ForegroundColor = ConsoleColor.Magenta;

           var propertyName = property.Metadata.Name;
           var currentValue = property.CurrentValue;
           var originalValue = property.OriginalValue;
           Console.WriteLine($"{propertyName}: CurrentValue={currentValue}, OriginalValue={originalValue}");

           Console.ForegroundColor = defaultColor;
       }
   }
   ```

1. Call it in `UpdateBudgetLastAddedDepartment`:

   ```cs
   DisplayChangeTrackerDebugView(context);
   DisplayPropertyEntries(context.Entry(department).Properties);   // 👈

   context.SaveChanges();

   DisplayChangeTrackerDebugView(context);
   DisplayPropertyEntries(context.Entry(department).Properties);   // 👈
   ```