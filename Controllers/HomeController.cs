using System.Data.Entity;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UserCourseEnrollApp.DbContext;
using UserCourseEnrollApp.Models;

namespace UserCourseEnrollApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly MyDbContext _dbContext;

        public HomeController(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Add<User>(user);
                _dbContext.SaveChanges();
                ViewBag.message = "The user " + user.firstName + " is saved successfully";
            }
            
            return View("Index");
        }

        [HttpPost]
        public IActionResult Course(Course course)
        {
            if (ModelState.IsValid)
            {
                var Data = new Course { Name = course.Name, Description = course.Description };
                _dbContext.Add<Course>(Data);
                _dbContext.SaveChanges();
                ViewBag.message = "The course " + course.Name + " is saved successfully";
            }

            return View("Course");
        }

        public IActionResult Course()
        {           
            return View();
        }

        public IActionResult EnrollMent()
        {
            //var courses = _dbContext.course.Select(x => new Course { Id=x.Id, Name=x.Name });
            //ViewBag.Courses = new MultiSelectList(courses, "Id", "Name");
            UserCourseEnrollApp.Models.EnrollMent enroll = new EnrollMent();
            enroll.Courses= _dbContext.course.Select(x => new Course { CourseId = x.CourseId, Name = x.Name }).ToList();
            enroll.Users = _dbContext.user.Select(x => new User { Id = x.Id, firstName = x.firstName }).ToList();
            return View(enroll);
        }

        [HttpPost]
        public IActionResult EnrollMent(EnrollMent enrollment)
        {
            if (ModelState.IsValid)
            {
                enrollment.CourseIds.ToList().ForEach(x =>
                {
                    var Data= new EnrollMent() { CourseId = x, StartDate=enrollment.StartDate, EndDate=enrollment.EndDate, UserId=enrollment.UserId };
                    _dbContext.Add<EnrollMent>(Data);
                });
                _dbContext.SaveChanges();
                ViewBag.message = "The course  is saved successfully";
            }
            return RedirectToAction("EnrollMentData");
        }

      
        public IActionResult EnrollMentData()
        {
            List<EnrollMent> enrollMent=_dbContext.enrollMent.ToList();
            List<User> user = _dbContext.user.ToList();
            List<Course> course = _dbContext.course.ToList();

            var Result = (from enroll in enrollMent
                          join userd in user on enroll.UserId equals userd.Id
                          join coursed in course on enroll.CourseId equals coursed.CourseId
                          select new EnrollData 
                          {
                              firstName = userd.firstName,
                              lastName = userd.lastName,
                              CourseName = coursed.Name,
                              CourseDesc = coursed.Description,
                              StartDate = enroll.StartDate,
                              EndDate = enroll.EndDate
                          }).ToList();
            
            return View(Result);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}