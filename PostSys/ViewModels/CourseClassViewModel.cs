using PostSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PostSys.ViewModels
{
	public class CourseClassViewModel
	{
		public Class Class { get; set; }
		public IEnumerable<Class> Classes { get; set; }
		public Course Course { get; set; }

		public IEnumerable<Course> Courses { get; set; }

	}
}