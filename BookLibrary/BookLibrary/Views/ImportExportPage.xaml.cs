using BookLibrary.Helpers;
using BookLibrary.Interfaces;
using BookLibrary.Services;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.IO;
using Xamarin.Forms;
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

                Console.WriteLine("File name chosen: " + fileName);
                Console.WriteLine("File data: " + contents);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception choosing file: " + ex.ToString());
            }
        }

        private async void Export_Button_ClickedAsync(object sender, EventArgs e)
        {
            try
            {
                PermissionStatus status = PermissionStatus.Unknown;
                status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);

                //await DisplayAlert("Pre - Results", status.ToString(), "OK");

                if (status != PermissionStatus.Granted)
                {

                    status = await PermissionUtility.CheckPermissions(Permission.Storage);
                }

                if(status == PermissionStatus.Granted)
                {
                    string exportString = await _importExportService.GetExportLibraryContentAsync();

                    string downloadPath = DependencyService.Get<IFileHelper>().GetDownloadFolderPath("libraryBackup.json");

                    File.WriteAllText(downloadPath, exportString);

                    await DisplayAlert("Success", "Your file has been successfully downloaded", "Ok");
                }

                //await DisplayAlert("Results", status.ToString(), "OK");

            }
            catch (Exception ex)
            {

                await DisplayAlert("Error", ex.Message, "Ok");

                Console.WriteLine("Exception exporting file: " + ex.ToString());
            }

            //try
            //{
            //    string exportString = await _importExportService.GetExportLibraryContentAsync();

            //    string downloadPath = DependencyService.Get<IFileHelper>().GetDownloadFolderPath("libraryBackup.json");

            //    //File.WriteAllText(downloadPath, exportString);

            //    //await DisplayAlert("Success", "Your file has been successfully downloaded", "Ok");
            //}
            //catch (Exception ex)
            //{
            //    await DisplayAlert("Error", ex.Message, "Ok");

            //    Console.WriteLine("Exception exporting file: " + ex.ToString());
            //}
        }
    }
}