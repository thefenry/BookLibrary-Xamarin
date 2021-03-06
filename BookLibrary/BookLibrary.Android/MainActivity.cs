﻿
using Android.App;
using Android.Content.PM;
using Android.OS;
using Plugin.Permissions;
//using Xfx;

namespace BookLibrary.Droid
{
    [Activity(Label = "Book Library", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            //XfxControls.Init();
            global::Xamarin.Forms.Forms.Init(this, bundle);

            // This line is leveraging the android-specific implementation
            ZXing.Mobile.MobileBarcodeScanner.Initialize(Application);

            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
