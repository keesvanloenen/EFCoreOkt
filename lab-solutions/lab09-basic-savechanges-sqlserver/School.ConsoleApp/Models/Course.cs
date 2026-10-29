namespace School.ConsoleApp.Models;

public class Course
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public int Credits { get; set; }

    public List<Instructor> Instructors { get; } = [];
    public List<Enrollment> Enrollments { get; } = [];
    public List<Student> Students { get; } = [];
}
