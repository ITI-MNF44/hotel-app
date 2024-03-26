using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperClass
{
    public static class FileHelperClass
    {
        public static string GetProjectFolderPath()
        {
            // Get the directory where the executable is located
            string executablePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string executableDirectory = Path.GetDirectoryName(executablePath);

            // Traverse up the directory tree until the project folder is found
            DirectoryInfo directory = new DirectoryInfo(executableDirectory);
            while (directory != null)
            {
                FileInfo[] projectFiles = directory.GetFiles("*.csproj");
                if (projectFiles.Length > 0)
                {
                    return directory.FullName;
                }

                directory = directory.Parent;
            }

            return null;
        }
    
    }
}
