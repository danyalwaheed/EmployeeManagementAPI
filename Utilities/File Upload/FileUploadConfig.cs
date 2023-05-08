using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementAPI.Utilities.File_Upload
{
	public class FileUploadConfig
	{
		public string RelativeUploadPath { get; set; }

		public int? MaximumFileCount { get; set; }

		public string[] AllowedExtensions { get; set; }

		public long MaxFileSizeBytes { get; set; }
	}
}
