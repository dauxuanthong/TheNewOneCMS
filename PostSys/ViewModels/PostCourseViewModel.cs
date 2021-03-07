using PostSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PostSys.ViewModels
{
	public class PostCourseViewModel
	{
		public Course Course { get; set; }
		public Post Post { get; set; }

		
		//manage course
		public int postId { get; set; }
		public string courseName { get; set; }

		//manage post
		public string postName { get; set; }

		//set deadline
		public int Status { get; set; }
	}
}