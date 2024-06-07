namespace DemoAPI01.Repositories.Abstracts;

public interface IFileService
{
    public Tuple<int, string> SaveImage(IFormFile imageFile);
    public bool DeleteImage(string imageFileName);
}