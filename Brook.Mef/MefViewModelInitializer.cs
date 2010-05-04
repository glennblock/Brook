using System.Collections.Generic;
using System;
using System.Linq;
using System.ComponentModel;
using System.Windows;
using System.Windows.Markup;
namespace Brook.Mef
{
    public class MefViewModelInitializer : ViewModelInitializer
    {
        public MefViewModelInitializer()
            :base(new MefLocator().GetViewModel)
        {
        }
    }



}