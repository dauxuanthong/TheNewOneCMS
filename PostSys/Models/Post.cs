using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PostSys.Models
{
	public class Post
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public byte[] File { get; set; }
		public string UrlFile { get; set; }
		public string NameOfFile { get; set; }
		public DateTime PostDate { get; set; }
		public bool Ischecked { get; set; }

		public int CourseId {get; set;}
		public Course Course { get; set; }
	}
}