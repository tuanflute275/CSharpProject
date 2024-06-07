using DemoAPI01.Repositories.Abstracts;

namespace DemoAPI01.Repositories.Implementations;

public class FileService : IFileService
{
    private IWebHostEnvironment _environment;

    public FileService(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    public Tuple<int, string> SaveImage(IFormFile imageFile)
    {
        try
        {
            var contentPath = _environment.ContentRootPath;

            var path = Path.Combine(contentPath, "Uploads");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            // Check allowed file extensions
            var ext = Path.GetExtension(imageFile.FileName);
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };
            if (!allowedExtensions.Contains(ext))
            {
                string msg = string.Format("Only {0} extensions are allowed", string.Join(",", allowedExtensions));
                return new Tuple<int, string>(0, msg);
            }

            string uniqueString = DateTime.Now.ToString("MM_dd_yyyy_hh_mm_ss");
            var fileName = Path.GetFileNameWithoutExtension(imageFile.FileName);
            var newFileName = uniqueString + fileName + ext;
            var fileWithPath = Path.Combine(path, newFileName);
            using (var file = new FileStream(fileWithPath, FileMode.Create))
            {
                imageFile.CopyTo(file);
            }

            return new Tuple<int, string>(1, newFileName);
        }
        catch (Exception e)
        {
            return new Tuple<int, string>(0, "Error has occured");
        }
    }

    public bool DeleteImage(string imageFileName)
    {
        try
        {
            var wwwPath = _environment.WebRootPath;
            var path = Path.Combine(wwwPath, "Uploads", imageFileName);
            System.IO.File.Delete(path);
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}