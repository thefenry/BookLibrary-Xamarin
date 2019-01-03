using BookLibrary.Interfaces;
using BookLibrary.Services;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;

namespace BookLibrary.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        private ImportExportService _importExportService;

        public SettingsPage()
        {
            InitializeComponent();
            _importExportService = new ImportExportService();
        }

        private async void Import_Button_ClickedAsync(object sender, EventArgs e)
        {
            try
            {
                string[] acceptedFileTypes = new[] { "application/json" };
                FileData fileData = await CrossFilePicker.Current.PickFile(acceptedFileTypes);
                if (fileData == null)
                    return; // user canceled file picking

                string fileName = fileData.FileName;
                string contents = System.Text.Encoding.UTF8.GetString(fileData.DataArray);

                System.Console.WriteLine("File name chosen: " + fileName);
                System.Console.WriteLine("File data: " + contents);
            }
            catch (Exception ex)
            {
                //TODO Implement SecurityException
                System.Console.WriteLine("Exception choosing file: " + ex.ToString());
            }
        }

        private async void Export_Button_ClickedAsync(object sender, EventArgs e)
        {
            try
            {
                string exportString = await _importExportService.GetExportLibraryContentAsync();

                //string directory = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, 
                //    Android.OS.Environment.DirectoryDownloads);

                //string Mp3 = Path.Combine(directory, "14 Hey MrDJ.mp3");
                //Toast.MakeText(this, "directory: " + directory, ToastLength.Long).Show();
                //if (File.Exists(Mp3))
                //{  // Do the Funky Dance Here...  
                //}  else  {  Toast.MakeText(this, "File Not Found...", ToastLength.Long).Show();  } 

                //var backingFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "libraryBackup.json");
                //using (var writer = File.CreateText(backingFile))
                //{
                //    await writer.WriteLineAsync(exportString.ToString());
                //}
                
                var downloadPath = DependencyService.Get<IFileHelper>().GetDownloadFolderPath();

                var combinedPath = Path.Combine(downloadPath, "libraryBackup.json");

                File.WriteAllText(combinedPath, exportString);

                //FileData fileData = new FileData();
                ////fileData.DataArray = Encoding.ASCII.GetBytes(exportString);

                //await CrossFilePicker.Current.SaveFile(fileData);
            }
            catch (Exception ex)
            {
                //TODO Implement SecurityException
                Console.WriteLine("Exception exporting file: " + ex.ToString());
            }
        }
    }
}