using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BookLibrary.Helpers
{
    public static class PermissionUtility
    {
        public static async Task<PermissionStatus> CheckPermissions(Permission permission)
        {
            PermissionStatus permissionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(permission);
            bool request = false;
            if (permissionStatus == PermissionStatus.Denied)
            {
                if (Device.RuntimePlatform == Device.iOS)
                {

                    string title = $"{permission} Permission";
                    string question = $"To use this plugin the {permission} permission is required. Please go into Settings and turn on {permission} for the app.";
                    string positive = "Settings";
                    string negative = "Maybe Later";
                    Task<bool> task = Application.Current?.MainPage?.DisplayAlert(title, question, positive, negative);
                    if (task == null)
                        return permissionStatus;

                    bool result = await task;
                    if (result)
                    {
                        CrossPermissions.Current.OpenAppSettings();
                    }

                    return permissionStatus;
                }

                request = true;

            }

            if (request || permissionStatus != PermissionStatus.Granted)
            {
                Dictionary<Permission, PermissionStatus> newStatus = await CrossPermissions.Current.RequestPermissionsAsync(permission);

                if (!newStatus.ContainsKey(permission))
                {
                    return permissionStatus;
                }

                permissionStatus = newStatus[permission];

                if (newStatus[permission] != PermissionStatus.Granted)
                {
                    permissionStatus = newStatus[permission];
                    string title = $"{permission} Permission";
                    string question = $"To use the plugin the {permission} permission is required.";
                    string positive = "Settings";
                    string negative = "Maybe Later";
                    Task<bool> task = Application.Current?.MainPage?.DisplayAlert(title, question, positive, negative);
                    if (task == null)
                        return permissionStatus;

                    bool result = await task;
                    if (result)
                    {
                        CrossPermissions.Current.OpenAppSettings();
                    }
                    return permissionStatus;
                }
            }

            return permissionStatus;
        }
    }
}
