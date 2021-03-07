using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PostSys.Models
{
	public class FileModel
	{
		public string FileName { get; set; }
		public string FilePath { get; set; }
		public bool IsSelected { get; set; }

		public string UserNameInFile { get; set; }

	}
}