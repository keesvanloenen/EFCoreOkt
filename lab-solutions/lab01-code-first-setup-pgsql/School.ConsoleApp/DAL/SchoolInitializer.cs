using Microsoft.EntityFrameworkCore;
using School.ConsoleApp.Models;

namespace School.ConsoleApp.DAL;

public class SchoolInitializer
{
    public static void Seed(DbContextOptions<SchoolContext> options)
    {
        using var context  = new SchoolContext(options);

        List<Person> people =
        [
            new Person { LastName = "Fakhouri", FirstName = "Fadi" },
            new Person { LastName = "Stewart", FirstName = "Jasmine" },
            new Person { LastName = "Xu", FirstName = "Kristen" },
            new Person { LastName = "Van Houten", FirstName = "Roger" },
            new Person { LastName = "Barzdukas", FirstName = "Gytis" },
            new Person { LastName = "Justice", FirstName = "Peggy" },
            new Person { LastName = "Li", FirstName = "Yan" },
            new Person { LastName = "Norman", FirstName = "Laura" },
            new Person { LastName = "Olivotto", FirstName = "Nino" },
            new Person { LastName = "Tang", FirstName = "Wayne" },
            new Person { LastName = "Alonso", FirstName = "Meredith" },
            new Person { LastName = "Lopez", FirstName = "Sophia" },
            new Person { LastName = "Browning", FirstName = "Meredith" },
            new Person { LastName = "Anand", FirstName = "Arturo" },
            new Person { LastName = "Walker", FirstName = "Alexandra" },
            new Person { LastName = "Powell", FirstName = "Carson" },
            new Person { LastName = "Jai", FirstName = "Damien" },
            new Person { LastName = "Carlson", FirstName = "Robyn" },
            new Person { LastName = "Bryant", FirstName = "Carson" },
            new Person { LastName = "Suarez", FirstName = "Robyn" },
            new Person { LastName = "Holt", FirstName = "Roger" },
            new Person { LastName = "Alexander", FirstName = "Carson" },
            new Person { LastName = "Morgan", FirstName = "Isaiah" },
            new Person { LastName = "Martin", FirstName = "Randall" },
            new Person { LastName = "Rogers", FirstName = "Cody" },
            new Person { LastName = "White", FirstName = "Anthony" },
            new Person { LastName = "Griffin", FirstName = "Rachel" },
            new Person { LastName = "Shan", FirstName = "Alicia" },
            new Person { LastName = "Gao", FirstName = "Erica" },
            new Person { LastName = "Abercrombie", FirstName = "Kim" },
            new Person { LastName = "Harui", FirstName = "Roger" },
            new Person { LastName = "Zheng", FirstName = "Roger" },
            new Person { LastName = "Kapoor", FirstName = "Candace" },
        ];
        context.AddRange(people);
        context.SaveChanges();
    }
}
