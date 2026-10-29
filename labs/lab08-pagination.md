# Lab 08 - Pagination

⭐ **This lab is optional** ⭐

## Scenario

You're going to display a list of people from the database. The application will allow users to navigate through the list in pages of 5 people each, using **keyset pagination** for efficient data retrieval.

## Exercise 1: Pagination Logic

- Comment out the method `AllPeople`. Add a new `AllPeople` above it, along with a `Previous` and `Next` method:

  ```cs
  public List<Person> Previous(int firstPersonId, int pageSize)
  {
       // TODO
  }

  public List<Person> Next(int lastPersonId, int pageSize)
  {
      // TODO
  }
  ```

- In `AllPeople` initialize pagination variables (`pageSize`, `firstPersonId`, `lastPersonId`).
- Fetch the first page of people using the `Next` method.

## Exercise 2: User Interaction

- Display the list of people in the current page.
- Prompt the user to navigate:

  ```txt
  1 - Fadi Fakhouri
  2 - Stacy Serrano
  3 - Jasmine Stewart
  4 - Kristen Xu
  5 - Roger Van Houten
  (P)revious  (N)ext  (Q)uit
  ```

## Exercise 3: Implement `Previous`

- Retrieve people whose `Id` values are less than the `Id` of the first person in the current page.
- Make sure the results are ordered in ascending order by `Id` so the page displays correctly.
- Hint: You may need to order by descending `Id`, take the desired number of results, and then re-order ascending before returning.

## Exercise 4: Implement `Next`

- Retrieve people whose `Id` values are greater than the `Id` of the last person in the current page.
- Return the next set of people, ordered by ascending `Id`.
