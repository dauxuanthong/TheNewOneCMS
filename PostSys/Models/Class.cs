using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PostSys.Models
{
	public class Class
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public string CoordinatorId {get; set;}
		public ApplicationUser Coordinator { get; set; }

		public int FacultyId { get; set; }
		public Faculty Faculty { get; set; }
	}
}