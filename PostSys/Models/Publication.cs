using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PostSys.Models
{
	public class Publication
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public int PostId { get; set; }
		public Post Post { get; set; }
	}
}