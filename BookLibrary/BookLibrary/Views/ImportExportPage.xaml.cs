using System;
using System.Collections.Generic;
using System.IO;
using BookLibrary.Helpers;
using BookLibrary.Interfaces;
using BookLibrary.Services;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PermissionStatus = Plugin.Permissions.Abstractions.PermissionStatus;

namespace BookLibrary.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        private readonly ImportExportService _importExportService;

        private readonly List<string> acceptedExtensions = new List<string> { "json" };

        public SettingsPage()
        {
            InitializeComponent();
            _importExportService = new ImportExportService();
        }

        private async void Import_Button_ClickedAsync(object sender, EventArgs e)
        {
            try
            {
                // TODO: Impletement the pick options
                //string[] acceptedFileTypes = new[] { "application/json" };
                //PickOptions options = new PickOptions
                //{
                //    FileTypes = new FilePickerFileType(
                //        new Dictionary<DevicePlatform, IEnumerable<string>>
                //        {
                //            { DevicePlatform.Android, new string[] { ".json" } },
                //            { DevicePlatform.iOS, new string[] { ".json" } },
                //            { DevicePlatform.UWP, new string[] { ".json" } },
                //        }),
                //        PickerTitle = "Select a file to import"
                //};

                FileResult fileData = await FilePicker.PickAsync();

                if (fileData != null)
                {
                    string[] fileNameArray = fileData.FileName.Split('.');

                    if (IsValidFormat(fileNameArray))
                    {
                        Stream stream = await fileData.OpenReadAsync();
                        using (StreamReader streamReader = new StreamReader(stream))
                        {
                            string content = await streamReader.ReadToEndAsync();

                            int bookCount = await _importExportService.ImportBooksAsync(content);

                            await DisplayAlert("Horray!", $"You just added {bookCount} books", "Great");
                        }                      
                    }
                    else
                    {
                        await DisplayAlert("Panic!", $"I'm sorry but we currently only support {string.Join(", ", acceptedExtensions)} " +
                            $"files. Please try again with a different file", "Ok");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception choosing file: " + ex.ToString());
                await DisplayAlert("Error", "I'm sorry something has gone wrong. Please try again with a different file", "Ok");
            }
        }


        private async void Export_Button_ClickedAsync(object sender, EventArgs e)
        {
            try
            {
                PermissionStatus status = await CrossPermissions.Current.CheckPermissionStatusAsync<StoragePermission>();
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Storage))
                    {
                        await DisplayAlert("Need Storage Access", "We need to get storage permission in order to backup your files", "OK");
                    }

                    status = await CrossPermissions.Current.RequestPermissionAsync<LocationPermission>();
                }

                if (status != PermissionStatus.Granted)
                {

                    status = await PermissionUtility.CheckPermissions(Permission.Storage);
                }

                if (status == PermissionStatus.Granted)
                {
                    string exportString = await _importExportService.GetExportLibraryContentAsync();

                    string downloadPath = DependencyService.Get<IFileHelper>().GetDownloadFolderPath("libraryBackup.json");

                    File.WriteAllText(downloadPath, exportString);

                    await DisplayAlert("Success", "Your file has been successfully downloaded. You can find your file in your download folder, named 'libraryBackup.json'", "Ok");
                }
            }
            catch (Exception ex)
            {

                await DisplayAlert("Error", ex.Message, "Ok");

                Console.WriteLine("Exception exporting file: " + ex.ToString());
            }
        }

        /// <summary>
        /// Checks the file format
        /// </summary>
        /// <param name="fileNameArray"></param>
        /// <returns></returns>
        private bool IsValidFormat(string[] fileNameArray)
        {
            return acceptedExtensions.Contains(fileNameArray[fileNameArray.Length - 1]);
        }
    }
}