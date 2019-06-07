
using Android.Content.PM;
using Tmp.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(Tmp.Droid.Version_Android))]
namespace Tmp.Droid
{
    public class Version_Android : IAppVersion
    {
        public string GetVersion()
        {
            var context = global::Android.App.Application.Context;

            PackageManager manager = context.PackageManager;
            PackageInfo info = manager.GetPackageInfo(context.PackageName, 0);

            return info.VersionName;
        }

        public int GetBuild()
        {
            var context = global::Android.App.Application.Context;
            PackageManager manager = context.PackageManager;
            PackageInfo info = manager.GetPackageInfo(context.PackageName, 0);

            return info.VersionCode;
        }

        public void NavigateToUrl(string url)
        {
            // throw new System.NotImplementedException();
        }
    }
}