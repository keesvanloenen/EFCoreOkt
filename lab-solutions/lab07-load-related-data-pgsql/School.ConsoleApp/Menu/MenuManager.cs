namespace School.ConsoleApp.Menu;

using System.Collections.Generic;
using School.ConsoleApp.Services;

public class MenuManager
{
    private readonly PersonService _personService;
    private readonly CourseService _courseService;
    private readonly DepartmentService _departmentService;
    private readonly EnrollmentService _enrollmentService;
    
    public MenuManager(PersonService personService, CourseService courseService, DepartmentService departmentService, EnrollmentService enrollmentService)
    {
        _personService = personService;
        _courseService = courseService;
        _departmentService = departmentService;
        _enrollmentService = enrollmentService;
    }

    public void ShowMainMenu()
    {
        MenuSystem.Show(
            "Main Menu",
            new Dictionary<string, MenuItem>
            {
                ["1"] = new ("People", ShowPeopleMenu),
                ["2"] = new ("Courses", ShowCoursesMenu),
                ["3"] = new ("Departments", ShowDepartmentsMenu),
                ["4"] = new ("Enrollments", ShowEnrollmentsMenu),
            },
            isMainMenu: true
        );
    }

    public void ShowPeopleMenu()
    {
        MenuSystem.Show(
            "People",
            new Dictionary<string, MenuItem>
            {
                ["1"] = new ("All People", _personService.AllPeople),
                ["2"] = new("Students", _personService.Students),
                ["3"] = new("Instructors", _personService.Instructors),
                ["4"] = new("Department Administrators", _personService.DepartmentAdministrators),
                ["5"] = new("First Instructor Hired", _personService.FirstInstructorHired),
                ["6"] = new("Average Grade Per Student", _personService.AverageGradePerStudent),
                ["7"] = new("Instructor Details", _personService.InstructorDetails),
                ["8"] = new("Delete Department Administrator Keep Departments", _personService.DeleteDepartmentAdministratorKeepDepartments),
                ["9"] = new("Delete Department Administrator With Departments", _personService.DeleteDepartmentAdministratorWithDepartments),                
                ["10"] = new("Best 5 Students", _personService.Best5Students),                
            }
        );
    }

    public void ShowCoursesMenu()
    {
        MenuSystem.Show(
            "Courses",
            new Dictionary<string, MenuItem>
            {
                ["1"] = new("All Courses", _courseService.AllCourses),
                ["2"] = new("Online Courses", _courseService.OnlineCourses),
                ["3"] = new("Onsite Courses", _courseService.OnsiteCourses),
                ["4"] = new("Lowest Grade Per Course", _courseService.LowestGradePerCourse),
                ["5"] = new("Increase Credits Of Courses In Selected Department", _courseService.IncreaseCreditsOfCoursesInSelectedDepartment),
                ["6"] = new("Course Grade Standard Deviations", _courseService.CourseGradeStandardDeviations),
            }
        );
    }

    public void ShowDepartmentsMenu()
    {
        MenuSystem.Show(
            "Departments",
            new Dictionary<string, MenuItem>
            {
                ["1"] = new("All Departments", _departmentService.AllDepartments),
                ["2"] = new("Add Update Delete Department", _departmentService.AddUpdateDeleteDepartment),
                ["3"] = new("Update Department", _departmentService.UpdateDepartment),
                ["4"] = new("Get Departments With Enrollments Above", _departmentService.GetDepartmentsWithEnrollmentsAbove),
            }
        );
    }

    public void ShowEnrollmentsMenu()
    {
        MenuSystem.Show(
            "Enrollments",
            new Dictionary<string, MenuItem>
            {
                ["1"] = new("All Enrollments", _enrollmentService.AllEnrollments),
                ["2"] = new("Students And Courses", _enrollmentService.StudentsAndCourses),
            }
        );
    }
}