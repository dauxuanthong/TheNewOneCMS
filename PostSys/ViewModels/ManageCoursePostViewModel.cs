using PostSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PostSys.ViewModels
{
	public class ManageCoursePostViewModel
	{
		public IEnumerable<Course> Courses { get; set; }
		public IEnumerable<Post> Posts { get; set; }
		public Course Course { get; set; }
		public Post Post { get; set; }
	}
}