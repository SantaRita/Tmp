using System;
using System.Collections.Generic;
using System.Text;
using Tmp.ViewModels;

namespace Tmp.Infrastructure
{
    public class InstanceLocator
    {
        public MainViewModel Main { get; set; }

        public InstanceLocator()
        {
            Main = new MainViewModel();
        }

    }
}
