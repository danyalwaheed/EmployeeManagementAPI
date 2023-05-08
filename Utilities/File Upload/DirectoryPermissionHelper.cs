using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementAPI.Utilities.File_Upload
{
	public class DirectoryPermissionHelper
	{
		private const string TempFileName = "file.txt";

		private static async Task<bool> IsAccessibleAsync(string directoryPath)
		{
			if (!Directory.Exists(directoryPath))
			{
				throw new ArgumentException($"{nameof(directoryPath)} must be a directory");
			}

			try
			{
				var fullPathName = Path.Combine(directoryPath, TempFileName);
				using (var stream = new FileStream(fullPathName, FileMode.Create))
				{
					await stream.WriteAsync(Encoding.UTF8.GetBytes(TempFileName));
				}


				// delete file if present
				if (!File.Exists(fullPathName)) return false;
				File.Delete(fullPathName);
				return true;
			}
			catch (UnauthorizedAccessException)
			{
				return false;
			}
		}

		public static async Task VerifyUploadDirectoriesAsync(IServiceProvider serviceProvider)
		{
			var configuration = serviceProvider.GetRequiredService<IConfiguration>();
			var configOptionsAccessor = serviceProvider.GetRequiredService<IOptionsSnapshot<FileUploadConfig>>();

			var baseUploadPath = configuration.GetValue<string>("FileUpload:RootPath");

			var uploadPaths = Enum
				.GetValues(typeof(FileUploadConfigTypeEnum))
				.Cast<FileUploadConfigTypeEnum>()
				.Select(c => configOptionsAccessor.Get(c.ToString()))
				.Where(c => c != null && !string.IsNullOrEmpty(c.RelativeUploadPath))
				.Select(c => c.RelativeUploadPath);

			foreach (var path in uploadPaths)
			{
				var fullPath = Path.Combine(baseUploadPath, path);
				if (!Directory.Exists(fullPath))
				{
					throw new FileNotFoundException(
						$"'{path}' file upload directory not created. Please create the folder at {fullPath}");
				}

				if (!await IsAccessibleAsync(fullPath))
				{
					throw new UnauthorizedAccessException(
						$"An error occured while trying to access the directory at '{fullPath}'." +
						" Please ensure the correct permissions are enabled for system account running the application.");
				}
			}
		}
	}
}
