namespace School.Library.Models;

public class Instructor: Person
{
    public DateOnly HireDate { get; set; }

    public List<Course> Courses { get; } = [];
}
