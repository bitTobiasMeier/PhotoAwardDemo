using System.IO;

namespace PhotoAward.ReliableServices.Core
{
    public static class DirectoryInfoExtension
    {
        public static void Copy(this DirectoryInfo sourceDirectory, string targetDiretory)
        {
            if (!sourceDirectory.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist: "
                    + sourceDirectory.Name);
            }

            DirectoryInfo[] dirs = sourceDirectory.GetDirectories();
            if (!Directory.Exists(targetDiretory))
            {
                Directory.CreateDirectory(targetDiretory);
            }
            
            var files = sourceDirectory.GetFiles();
            foreach (var file in files)
            {
                string temppath = Path.Combine(targetDiretory, file.Name);
                file.CopyTo(temppath, false);
            }


            foreach (var subdir in dirs)
            {
                string temppath = Path.Combine(targetDiretory, subdir.Name);
                Copy(new DirectoryInfo(subdir.FullName), temppath);
            }

        }
    }
}