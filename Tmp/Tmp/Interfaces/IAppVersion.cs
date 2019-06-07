using System;
using System.Collections.Generic;
using System.Text;

namespace Tmp.Interfaces
{
    public interface IAppVersion
    {

        string GetVersion();
        int GetBuild();
        void NavigateToUrl(string url);

    }
}
