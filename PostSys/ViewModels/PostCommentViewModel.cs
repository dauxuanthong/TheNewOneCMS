using PostSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PostSys.ViewModels
{
	public class PostCommentViewModel
	{
		public Comment Comment { get; set; }
		public Post Post { get; set; }
	}
}