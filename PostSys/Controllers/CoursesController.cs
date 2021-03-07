using PostSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using PostSys.ViewModels;
using Microsoft.AspNet.Identity;
using System.IO;

namespace PostSys.Controllers
{
	public class CoursesController : Controller
	{
		private ApplicationDbContext _context;

		public CoursesController()
		{
			_context = new ApplicationDbContext();
		}

		// GET: Courses
		/*[Authorize(Roles = "Marketing Manager, Marketing Coodinator")]*/
		public ActionResult Index()
		{
			var getAllCourse = _context.Courses.Include(m => m.Class).Include(m => m.Student).ToList();

			return View(getAllCourse);
		}

		[HttpGet]
		/*[Authorize(Roles = "Marketing Coodinator")]*/
		public ActionResult AssignStudentToCourse()
		{
			var currentUserId = User.Identity.GetUserId();
			/*var showMineClass = _context.Classes.Where(m => m.CoordinatorId == currentUserId).Include(m => m.Coordinator).ToList();*/


			//Get account in role student
			var role = (from r in _context.Roles where r.Name.Contains("Student") select r)
															 .FirstOrDefault();
			var showAllStudentInRole = _context.Users.Where(x => x.Roles.Select(y => y.RoleId)
									  .Contains(role.Id))
									  .ToList();


			var newStudentInCourseViewModel = new StudentToCourseViewModel
			{
				Students = showAllStudentInRole,
				/*Classes = showMineClass*/
			};

			return View(newStudentInCourseViewModel);
		}

		[HttpPost]
		/*[Authorize(Roles = "Marketing Coodinator")]*/
		public ActionResult AssignStudentToCourse(Course course)
		{
			if (!ModelState.IsValid)
			{
				return View("~/Views/ErrorValidations/Null.cshtml");
			}

			/*var checkStudentInCourse = _context.Courses.Any(c => c.ClassId == course.ClassId ||
																   c.StudentId == course.StudentId);
			if (checkStudentInCourse == true)
			{
				return View("~/Views/ErrorValidations/Exist.cshtml");
			}*/

			if (course.StartDate >= course.EndDate)
			{
				return View("~/Views/ErrorValidations/Null.cshtml");
			}


			var currentUserId = User.Identity.GetUserId();
			var obj = (from cl in _context.Classes where cl.CoordinatorId.Contains(currentUserId) select cl.Id).ToList();
			var classId = obj[0];

			var newStudentInCourse = new Course
			{
				Name = course.Name,
				ClassId = classId,
				StartDate = course.StartDate,
				EndDate = course.EndDate,
				StudentId = course.StudentId
			};

			_context.Courses.Add(newStudentInCourse);
			_context.SaveChanges();

			return RedirectToAction("AssignStudentToCourse");

		}

		[HttpGet]
		/*[Authorize(Roles = "Marketing Coodinator, Marketing Manager")]*/
		public ActionResult Delete(int Id)
		{
			var courseInDb = _context.Courses.SingleOrDefault(c => c.Id == Id);

			_context.Courses.Remove(courseInDb);
			_context.SaveChanges();

			return RedirectToAction("Index");
		}

		public ActionResult DeleteManageMineCourse(int Id)
		{
			var courseInDb = _context.Courses.SingleOrDefault(c => c.Id == Id);

			_context.Courses.Remove(courseInDb);
			_context.SaveChanges();

			return RedirectToAction("ManageCourse");
		}

		[HttpGet]
		/*[Authorize(Roles = "Student")]*/
		public ActionResult MineCourse()
		{
			var mineId = User.Identity.GetUserId();
			var mineCourse = _context.Courses.Where(m => m.StudentId == mineId).Include(m => m.Class).Include(m => m.Student).ToList();

			return View(mineCourse);
		}

		/*[HttpGet]
		public ActionResult ManageCourse()
		{
			var currentCoordinatorId = User.Identity.GetUserId();	

			var obj = (from cl in _context.Classes
					   where cl.CoordinatorId.Contains(currentCoordinatorId)
					   join c in _context.Courses
					   on cl.Id equals c.ClassId
					   select new
					   {
						   courseId = c.Id,
						   courseName = c.Name,
						   className = cl.Name,
						   studentName = (from st in _context.Users where st.Id.Contains(c.StudentId) select st.UserName),
						   startDate = c.StartDate,
						   endDate = c.EndDate,
					   }
					   ).ToList().Select(p => new PostCourseViewModel()
					   {
						   courseId = p.courseId,
						   courseName = p.courseName,
						   className = p.className,
						   studentName = string.Join(",", p.studentName),
						   startDate = p.startDate,
						   endDate = p.endDate
					   }
					   );
			return View(obj);
		}*/

		/////////////////////////////
		[HttpGet]
		public ActionResult ManageMineCourse()
		{
			var currentCoordinatorUserName = User.Identity.GetUserName();
			var getClass = _context.Classes.Include(m => m.Coordinator).ToList();

			var getCourseOfCoordinator = _context.Courses.Where(m => m.Class.Coordinator.UserName == currentCoordinatorUserName).Include(m => m.Class);

			return View(getCourseOfCoordinator.ToList());
		}
		/////////////////////////////////////////
		[HttpGet]
		public ActionResult PostTopic(int id)
		{
			var courseInDb = _context.Courses.SingleOrDefault(c => c.Id == id);
			//check deadline


			int status;
			if (DateTime.Now <= courseInDb.EndDate) //can submit
			{
				status = 1;
			}
			else //can't submit
			{
				status = 0;
			}

			var newPostCourseViewModel = new PostCourseViewModel
			{
				Course = courseInDb,
				// 1 = can Submit
				// 0 = can't Submit
				Status = status

			};

			return View(newPostCourseViewModel);
		}

		private bool ValidateExtension(string extension)
		{
			extension = extension.ToLower();
			switch (extension)
			{
				case ".jpg":
					return true;
				case ".jpeg":
					return true;
				case ".png":
					return true;
				case ".doc":
					return true;
				case ".docx":
					return true;				
				default:
					return false;
			}
		}

		[HttpPost]
		public ActionResult PostTopic([Bind(Include = "Name, Description, Status, File, UrlFile, PostDate")] Post post, Course course, HttpPostedFileBase file, int id)
		{
			string extension = Path.GetExtension(file.FileName);

			if (!ValidateExtension(extension))
			{
				return View("~/Views/ErrorValidations/Exist.cshtml");
			}

			if (file != null && file.ContentLength > 0)
			{
				var userName = User.Identity.GetUserName();
				var coursePost = (from c in _context.Courses where c.Id == id select c.Name).ToList();
				//Get courseName 
				var courseName = coursePost[0];
				//Get class Name
				var courseClass = (from c in _context.Courses
								   where c.Id == id
								   join cl in _context.Classes
								   on c.ClassId equals cl.Id
								   select cl.Name).ToList();
				var className = courseClass[0];

				string prepend = userName + "_" + courseName + "_" + className;

				post.File = new byte[file.ContentLength]; // image stored in binary formate
				file.InputStream.Read(post.File, 0, file.ContentLength);
				string fileName = prepend + "_" +  System.IO.Path.GetFileName(file.FileName);
				string urlImage = Server.MapPath("~/Files/" + fileName);

				
				file.SaveAs(urlImage);

				post.UrlFile = "Files/" + fileName;
			}

			var courseInDb = _context.Courses.SingleOrDefault(c => c.Id == course.Id);
			
			var newPost = new Post
			{
				CourseId = courseInDb.Id,
				Name = post.Name,
				Description = post.Description,
				PostDate = DateTime.Now,
				File = post.File,
				UrlFile = post.UrlFile
			};

			_context.Posts.Add(newPost);
			_context.SaveChanges();

			return View("~/Views/Home/Index.cshtml");
		}
	}
}