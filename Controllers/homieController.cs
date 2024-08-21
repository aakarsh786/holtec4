using holtec4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using X.PagedList.Extensions;
using holtec4.ViewModels;
using Microsoft.Extensions.Logging;
using holtec4.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace holtec4.Controllers
{
    public class homieController : Controller
    {
        
        holtec4Context db = new holtec4Context();
        private readonly ILogger<homieController> _logger;

        public homieController(ILogger<homieController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Signup()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Signup(Signup sup)
        {
            try
            {
                
                db.Signups.Add(sup);
                db.SaveChanges();
                //ViewBag.uid = sup.Userid;
                //ViewBag.Keep();
                TempData["UserId"] = sup.Userid;
                TempData["UserId1"] = sup.Userid;
                return RedirectToAction("Userinfo");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during signup.");
                ModelState.AddModelError("", "An error occurred while processing your request.");
                return View();
            }
        }

        //Homepage
        public IActionResult Homepage()
        {
            ViewBag.userrole = TempData.Peek("userrole").ToString();
            return View();
        }

        //About Us
        public IActionResult AboutUs()
        {
            ViewBag.userrole = TempData.Peek("userrole").ToString();
            return View();
        }

        public IActionResult Userinfo()
        {

            return View();
        }

        //Register Wizard--> Userinfo
        [HttpPost]
        public IActionResult Userinfo(Alluserdatum audi, IFormFile Resume, IFormFile Profilepic)
        {
            try
            {
                //audi.Alluserid = 8;
                audi.Userrole = "user";
                audi.Userid = (int)TempData["UserId"];
                audi.Alluserid = (int)TempData["UserId"];
                if (Resume != null && Resume.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        Resume.CopyTo(memoryStream);
                        audi.Resume = memoryStream.ToArray();
                    }
                }

                if (Profilepic != null && Profilepic.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        Profilepic.CopyTo(memoryStream);
                        audi.Profilepic = memoryStream.ToArray();
                    }
                }
                db.Alluserdata.Add(audi);
                db.SaveChanges();
                var user = db.Signups.FirstOrDefault(d => d.Userid == audi.Userid);
                TempData["naam"] = user.Username;
                TempData["email"] = user.Email;
                TempData["fullname"] = $"{audi.Firstname} {audi.Lastname}";
                return RedirectToAction("Homepage");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during user info submission.");
                ModelState.AddModelError("", "An error occurred while processing your request.");
                return View();
            }
        }

        [HttpPost]
        public IActionResult Login(string Username, string Password)
        {
            try
            {
                // Look for a user with the provided username and password
                var user = db.Signups.FirstOrDefault(d => d.Username == Username && d.Password == Password);

                if (user != null)
                {
                    var userData = db.Alluserdata.FirstOrDefault(d => d.Userid == user.Userid);

                    if (userData != null)
                    {
                        TempData["fullname"] = $"{userData.Firstname} {userData.Lastname}";
                        TempData["userrole"]=userData.Userrole;
                        TempData["userid"]=userData.Userid;
                    }

                    TempData["naam"] = user.Username;
                    TempData["email"] = user.Email;

                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim("UserId", user.Userid.ToString())
                };


                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);


                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = false,
                    };


                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties).GetAwaiter().GetResult();

                    //return RedirectToAction("Userprofile");
                    return RedirectToAction("Homepage");
                }
                else
                {

                    ModelState.AddModelError(string.Empty, "Invalid username or password.");
                    return View();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during login.");
                ModelState.AddModelError("", "An error occurred while processing your request.");
                return View();
            }
        }


        public IActionResult Userprofile()
        {
            try
            {
                ViewBag.userrole = TempData.Peek("userrole").ToString();
                var userIdClaim = User.FindFirst("UserId")?.Value;
                if (string.IsNullOrEmpty(userIdClaim))
                {
                    return Unauthorized(); // The user is not authenticated
                }

                int userId = int.Parse(userIdClaim);


                int jobsAppliedCount = db.Applicants.Count(a => a.Userid == userId);


                ViewBag.JobsAppliedCount = jobsAppliedCount;

                var recentJobApplied = (from applicant in db.Applicants
                                        join job in db.Jobs on applicant.JobId equals job.JobId
                                        where applicant.Userid == userId
                                        orderby applicant.ApplicantId descending
                                        select job).FirstOrDefault();

                if (recentJobApplied != null)
                {
                    ViewBag.JobTitle = recentJobApplied.JobTitle;

                    ViewBag.JobLocation = recentJobApplied.Location;
                    ViewBag.JobSalary = recentJobApplied.Salary;
                    ViewBag.JobExperience = recentJobApplied.Experience;
                    ViewBag.JobType = recentJobApplied.JobType;
                }



                ViewBag.Fullname = TempData.Peek("fullname")?.ToString();
                ViewBag.naam = TempData.Peek("naam")?.ToString();
                ViewBag.email = TempData.Peek("email")?.ToString();
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving user profile.");
                return View("Error");
            }
        }

        //User will see all details
        public IActionResult Userdata()
        {
            try
            {
                ViewBag.userrole = TempData.Peek("userrole").ToString();
                var userIdClaim = User.FindFirst("UserId")?.Value;
                if (string.IsNullOrEmpty(userIdClaim))
                {
                    return Unauthorized();
                }

                int userId = int.Parse(userIdClaim);
                var userData = db.Alluserdata.FirstOrDefault(u => u.Userid == userId);

                if (userData == null || userData.Dateofbirth == null)
                {
                    return NotFound();
                }

                // Calculate age
                DateTime dob = userData.Dateofbirth ?? DateTime.MinValue; // Use a default date if null
                int age = DateTime.Now.Year - dob.Year;
                if (DateTime.Now.DayOfYear < dob.DayOfYear)
                {
                    age--;
                }



                ViewBag.Age = age;
                ViewBag.Fullname = TempData.Peek("fullname")?.ToString();
                ViewBag.Email = TempData.Peek("email")?.ToString();
                return View(userData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving user data.");
                return View("Error");
            }
        }

        //action for all users
        //public IActionResult Usermanagement()
        //{
        //    return View(db.Alluserdata.ToList());
        //}

        public IActionResult Usermanagement(string searchString)
        {
            try
            {
                var users = from u in db.Alluserdata
                            select u;

                // If searchString is not empty, filter the results
                if (!String.IsNullOrEmpty(searchString))
                {
                    users = users.Where(s => s.Firstname.Contains(searchString) || s.Lastname.Contains(searchString));
                }

                ViewBag.SearchString = searchString; // Maintain the search string in the view
                return View(users.ToList()); // Return all users if searchString is empty or null
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during user management.");
                return View("Error");
            }
        }


        //Post job ka controller

        public IActionResult postjob()
        {
            ViewBag.userrole = TempData.Peek("userrole").ToString();
            return View();
        }

        [HttpPost]
        public IActionResult postjob(Job j1)
        {
            try
            {
                db.Jobs.Add(j1);
                db.SaveChanges();

                return RedirectToAction("Alljobs");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while posting job.");
                ModelState.AddModelError("", "An error occurred while processing your request.");
                return View();
            }
        }

        public IActionResult Alljobs(string jobType, string location, string jobCategory, int? page, string email)
        {
            try
            {
                ViewBag.userrole = TempData.Peek("userrole").ToString();
                ViewBag.usrrole= TempData.Peek("userrole").ToString();
                int pageNumber = (page ?? 1);
                int pageSize = 3;

                // Query the jobs and apply filters
                var jobsQuery = db.Jobs.AsQueryable();

                if (!string.IsNullOrEmpty(jobType))
                {
                    jobsQuery = jobsQuery.Where(j => j.JobType == jobType);
                }
                if (!string.IsNullOrEmpty(jobCategory))
                {
                    jobsQuery = jobsQuery.Where(j => j.JobTitle == jobCategory);
                }
                if (!string.IsNullOrEmpty(location))
                {
                    jobsQuery = jobsQuery.Where(j => j.Location == location);
                }

                var pagedJobs = jobsQuery.ToPagedList(pageNumber, pageSize);

                return View(pagedJobs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving jobs.");
                return View("Error");
            }

        }

        public IActionResult JobDetails(int id)
        {
            try
            {
                ViewBag.userrole = TempData.Peek("userrole").ToString();
                var job = db.Jobs.FirstOrDefault(u => u.JobId == id);
                return View(job);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving job details.");
                return View("Error");
            }
        }
        [HttpPost]
        public JsonResult SendApplication(int JobId)
        {
            try
            {
                var job = db.Jobs.FirstOrDefault(j => j.JobId == JobId);
                if (job == null)
                {
                    return Json(new { success = false, message = "The job you are trying to apply for does not exist." });
                }

                var userIdClaim = User.FindFirst("UserId")?.Value;
                if (string.IsNullOrEmpty(userIdClaim))
                {
                    return Json(new { success = false, message = "Unauthorized" });
                }

                int userId = int.Parse(userIdClaim);
                bool hasApplied = db.Applicants.Any(a => a.JobId == JobId && a.Userid == userId);

                if (hasApplied)
                {
                    return Json(new { success = false, message = "You have already applied for this job." });
                }

                Applicant newApplicant = new Applicant
                {
                    JobId = job.JobId,
                    Userid = userId
                };

                db.Applicants.Add(newApplicant);
                db.SaveChanges();

                return Json(new { success = true, message = "Application sent successfully!" });
            }
            catch (Exception ex)
            {
                // Log the exception
                return Json(new { success = false, message = "An error occurred while processing your application." });
            }
        }

        [HttpGet]
        public IActionResult EditUser()
        {
            int userId = Convert.ToInt32(TempData.Peek("userid"));
            var userDetails = db.Alluserdata.SingleOrDefault(u => u.Userid == userId);
            if (userDetails == null)
            {
                return NotFound();
            }
            return View("EditUser",userDetails);
        }

        [HttpPost]
        public IActionResult EditUser(Alluserdatum model, IFormFile Resume, IFormFile Profilepic)
        {
            if (ModelState.IsValid)
            {
                // Find the existing user record in the database by UserID
                var existingUser = db.Alluserdata.SingleOrDefault(u => u.Userid == model.Userid);
                if (existingUser != null)
                {
                    // Update the existing user details with the values from the model
                    existingUser.Firstname = model.Firstname;
                    existingUser.Lastname = model.Lastname;
                    existingUser.Dateofbirth = model.Dateofbirth;
                    existingUser.PhoneNumber = model.PhoneNumber;
                    existingUser.Userrole = model.Userrole;
                    existingUser.ClassX = model.ClassX;
                    existingUser.XMarks = model.XMarks;
                    existingUser.XYear = model.XYear;
                    existingUser.ClassXii = model.ClassXii;
                    existingUser.XiiMarks = model.XiiMarks;
                    existingUser.XiiYear = model.XiiYear;
                    existingUser.Bachelors = model.Bachelors;
                    existingUser.BachelorsMarks = model.BachelorsMarks;
                    existingUser.BachelorsYear = model.BachelorsYear;
                    existingUser.Masters = model.Masters;
                    existingUser.MastersMarks = model.MastersMarks;
                    existingUser.MastersYear = model.MastersYear;
                    existingUser.Diploma = model.Diploma;
                    existingUser.DiplomaMarks = model.DiplomaMarks;
                    existingUser.DiplomaYear = model.DiplomaYear;
                    existingUser.Company1 = model.Company1;
                    existingUser.Role1 = model.Role1;
                    existingUser.Years1 = model.Years1;
                    existingUser.Company2 = model.Company2;
                    existingUser.Role2 = model.Role2;
                    existingUser.Years2 = model.Years2;
                    existingUser.Company3 = model.Company3;
                    existingUser.Role3 = model.Role3;
                    existingUser.Years3 = model.Years3;
                    existingUser.Company4 = model.Company4;
                    existingUser.Role4 = model.Role4;
                    existingUser.Years4 = model.Years4;
                    existingUser.Company5 = model.Company5;
                    existingUser.Role5 = model.Role5;
                    existingUser.Years5 = model.Years5;
                    existingUser.Cplusplus = model.Cplusplus;
                    existingUser.Javascript = model.Javascript;
                    existingUser.C = model.C;
                    existingUser.Python = model.Python;
                    existingUser.Java = model.Java;
                    existingUser.Html = model.Html;
                    existingUser.Flutter = model.Flutter;
                    existingUser.Aiml = model.Aiml;
                    existingUser.React = model.React;
                    existingUser.Angular = model.Angular;
                    existingUser.Autocad = model.Autocad;
                    existingUser.ComputerGraphics = model.ComputerGraphics;
                    //existingUser.Resume = model.Resume;
                    //existingUser.Profilepic = model.Profilepic;
                    if (Resume != null && Resume.Length > 0)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            Resume.CopyTo(memoryStream);
                            model.Resume = memoryStream.ToArray();
                            existingUser.Resume = model.Resume;
                        }
                    }

                    if (Profilepic != null && Profilepic.Length > 0)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            Profilepic.CopyTo(memoryStream);
                            model.Profilepic = memoryStream.ToArray();
                            existingUser.Profilepic = model.Profilepic;
                        }
                    }

                    // Save the changes to the database
                    db.SaveChanges();

                    // Redirect to the Userdata action after successful update
                    return RedirectToAction("Userdata");
                }
            }

            // If the model is invalid, return the view with validation errors
            return Content("Error");
        }



        public IActionResult ApplicantView(int jobId)
        {
            try
            {
                ViewBag.userrole = TempData.Peek("userrole").ToString();
                var applicantsWithJobs = from applicant in db.Applicants
                                         join job in db.Jobs on applicant.JobId equals job.JobId
                                         join user in db.Alluserdata on applicant.Userid equals user.Userid
                                         where job.JobId == jobId
                                         select new ApplicantJobViewModel
                                         {
                                             Applicant = applicant,
                                             FullName = user.Firstname + " " + user.Lastname,
                                             Location = job.Location,
                                             DepartmentId = job.DepartmentId
                                         };

                return View(applicantsWithJobs.ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving applicants.");
                return View("Error");
            }
        }

        public IActionResult HRCandidateView(int userId)
        {
            try
            {
                ViewBag.userrole = TempData.Peek("userrole").ToString();
                var userData = db.Alluserdata.FirstOrDefault(u => u.Userid == userId);
                var signupInfo = db.Signups.FirstOrDefault(s => s.Userid == userId);

                ViewBag.Fullname = $"{userData.Firstname} {userData.Lastname}";
                ViewBag.Email = signupInfo.Email;
                ViewBag.ClassX = userData.ClassX;
                ViewBag.XMarks = userData.XMarks;
                ViewBag.XYear = userData.XYear;
                ViewBag.ClassXii = userData.ClassXii;
                ViewBag.XiiMarks = userData.XiiMarks;
                ViewBag.XiiYear = userData.XiiYear;
                ViewBag.Bachelors = userData.Bachelors;
                ViewBag.BachelorsMarks = userData.BachelorsMarks;
                ViewBag.BachelorsYear = userData.BachelorsYear;
                ViewBag.Masters = userData.Masters;
                ViewBag.MastersMarks = userData.MastersMarks;
                ViewBag.MastersYear = userData.MastersYear;
                ViewBag.Diploma = userData.Diploma;
                ViewBag.DiplomaMarks = userData.DiplomaMarks;
                ViewBag.DiplomaYear = userData.DiplomaYear;
                ViewBag.Company_1 = userData.Company1;
                ViewBag.Role_1 = userData.Role1;
                ViewBag.Years_1 = userData.Years1;
                ViewBag.Company_2 = userData.Company2;
                ViewBag.Role_2 = userData.Role2;
                ViewBag.Years_2 = userData.Years2;
                ViewBag.Company_3 = userData.Company3;
                ViewBag.Role_3 = userData.Role3;
                ViewBag.Years_3 = userData.Years3;
                ViewBag.Company_4 = userData.Company4;
                ViewBag.Role_4 = userData.Role4;
                ViewBag.Years_4 = userData.Years4;
                ViewBag.Company_5 = userData.Company5;
                ViewBag.Role_5 = userData.Role5;
                ViewBag.Years_5 = userData.Years5;


                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving HR candidate view.");
                return View("Error");
            }
        }
        public IActionResult Delete(int id)
        {
            var job = db.Jobs.FirstOrDefault(u => u.JobId == id);
            if (job == null)
            {
                return NotFound();
            }

            var applicants = db.Applicants.Where(a => a.JobId == id).ToList();
            if (applicants.Any())
            {
                db.Applicants.RemoveRange(applicants);
            }
            db.Jobs.Remove(job);
            db.SaveChanges();
            return RedirectToAction("Alljobs");
        }

        public IActionResult Edit(int id)
        {
            var job = db.Jobs.FirstOrDefault(u => u.JobId == id);
            if (job == null)
            {
                return NotFound();
            }
            return View(job);
        }


        [HttpPost]
        public IActionResult Edit(Job job)
        {
            if (ModelState.IsValid)
            {
                var existingJob = db.Jobs.FirstOrDefault(u => u.JobId == job.JobId);
                if (existingJob != null)
                {
                    existingJob.JobTitle = job.JobTitle;
                    existingJob.JobDescription = job.JobDescription;
                    existingJob.JobRequirements = job.JobRequirements;
                    existingJob.Location = job.Location;
                    existingJob.Salary = job.Salary;
                    existingJob.Experience = job.Experience;
                    existingJob.JobType = job.JobType;
                    existingJob.Email = job.Email;
                    existingJob.Deadline = job.Deadline;

                    db.SaveChanges();
                    return RedirectToAction("Alljobs");
                }
                else
                {
                    return NotFound();
                }
            }
            return View(job);
        }

        public IActionResult AppliedJobs() 
        {
            
            var userId = (int)TempData.Peek("userid");
            var appliedJobs = from applicant in db.Applicants
                              join job in db.Jobs on applicant.JobId equals job.JobId
                              where applicant.Userid == userId
                              select new AppliedJobViewModel
                              {
                                  JobTitle = job.JobTitle,
                                  Location = job.Location,
                                  Salary = job.Salary,
                                  //Experience = (int)job.Experience,
                                  //ApplicationDate = applicant.ApplicationDate // Assuming you have this field
                              };

            return View(appliedJobs.ToList());


        }


        public IActionResult Logout()
        {
            try
            {
                HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).GetAwaiter().GetResult();
                return RedirectToAction("Signup");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during logout.");
                return View("Error");
            }
        }




    }
}

