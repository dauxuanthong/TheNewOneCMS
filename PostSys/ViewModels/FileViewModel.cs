using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PostSys.ViewModels
{
	public class FileViewModel
	{
		//folder
		public string FileName { get; set; }
		public string FilePath { get; set; }
		public bool IsSelected { get; set; }
		//Database
		public int PostId { get; set; }
		public string PostName { get; set; }
		public string StudentName { get; set; }
		public string CourseName { get; set; }
		public string ClassName { get; set; }
		public DateTime PostDate { get; set; }
		public string NameOfFile { get; set; }
		public byte[] File { get; set; }
		public string UrlFile { get; set; }

	}
}