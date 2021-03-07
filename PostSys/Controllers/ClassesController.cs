using PostSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using PostSys.ViewModels;
using Microsoft.AspNet.Identity;

namespace PostSys.Controllers
{
	public class ClassesController : Controller
	{
		private ApplicationDbContext _context;

		public ClassesController()
		{
			_context = new ApplicationDbContext();
		}


		// GET: Classes
		/*[Authorize(Roles = "Marketing Manager")]*/
		public ActionResult Index(string searchString)
		{
			var getAllClasses = _context.Classes.Include(m => m.Coordinator).Include(m => m.Faculty);
			
			if (!String.IsNullOrEmpty(searchString))
			{
				getAllClasses = getAllClasses.Where(s => s.Name
										     .Contains(searchString) || 
											 s.Faculty.Name.Contains(searchString));
			}

			return View(getAllClasses.ToList());
		}

		// GET: Classes/Create
		/*[Authorize(Roles = "Marketing Manager")]*/
		[HttpGet]
		public ActionResult Create()
		{

			//get all account in coordinator role
			var role = (from r in _context.Roles where r.Name.Contains("Marketing Coodinator") select r)
															 .FirstOrDefault();
			var showCoordinator = _context.Users.Where(x => x.Roles.Select(y => y.RoleId)
									  .Contains(role.Id))
									  .ToList();

			var showFaculty = _context.Faculties.ToList();

			var newFacultyViewModel = new CoordinatorFacultyViewModel
			{
				Faculties = showFaculty,
				Coordinators = showCoordinator
			};
			return View(newFacultyViewModel);
		}

		/*[Authorize(Roles = "Marketing Manager")]*/
		[HttpPost]
		public ActionResult Create(Class @class)
		{
			if (!ModelState.IsValid)
			{
				return View("~/Views/ErrorValidations/Null.cshtml");
			}

			var checkCoordinatorInFaculty = _context.Classes.Any(c => c.CoordinatorId == @class.CoordinatorId ||
																   c.FacultyId == @class.FacultyId);

			if (checkCoordinatorInFaculty == true)
			{
				return View("~/Views/ErrorValidations/Exist.cshtml");
			}

			var newClass = new Class
			{
				Name = @class.Name,
				FacultyId = @class.FacultyId,
				CoordinatorId = @class.CoordinatorId
			};

			_context.Classes.Add(newClass);
			_context.SaveChanges();
			return RedirectToAction("Index");
		}

		/*[Authorize(Roles = "Marketing Manager")]*/
		[HttpGet]
		public ActionResult Delete(int Id)
		{
			var classInDb = _context.Classes.SingleOrDefault(c => c.Id == Id);

			if (classInDb == null)
			{
				return HttpNotFound();
			}

			_context.Classes.Remove(classInDb);
			_context.SaveChanges();

			return RedirectToAction("Index");
		}

		/*[Authorize(Roles = "Marketing Coodinator")]*/
		[HttpGet]
		public ActionResult Mine()
		{
			var mineId = User.Identity.GetUserId();

			var mineFaculty = _context.Classes.Where(m => m.CoordinatorId == mineId).Include(m => m.Faculty).Include(m => m.Coordinator).ToList();

			return View(mineFaculty);
		}
	}
}
