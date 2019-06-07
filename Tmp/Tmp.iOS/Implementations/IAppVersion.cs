using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using Tmp.Interfaces;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(Tmp.iOS.Version_iOS))]

namespace Tmp.iOS
{
    public class Version_iOS : IAppVersion
    {
        public string GetVersion()
        {
            return NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleShortVersionString").ToString();
        }
        public int GetBuild()
        {
            return int.Parse(NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleVersion").ToString());
        }

        public void NavigateToUrl(string url)
        {
            NSUrl itunesLink = new NSUrl(url);
            UIApplication.SharedApplication.OpenUrl(itunesLink, new NSDictionary() { }, null);
        }
    }
}