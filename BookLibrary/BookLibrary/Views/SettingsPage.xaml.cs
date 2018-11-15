using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BookLibrary.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingsPage : ContentPage
	{
		public SettingsPage ()
		{
			InitializeComponent ();
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
    }
}