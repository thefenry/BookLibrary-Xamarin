namespace BookLibrary.Interfaces
{
    public interface IFileHelper
    {
        string GetLocalFilePath(string filename);

        string GetDownloadFolderPath(string filename);
    }
}