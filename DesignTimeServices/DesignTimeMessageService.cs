using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using BrookSample;
using Brook.Mef;
using System.ComponentModel.Composition;
using Brook;

namespace DesignTimeServices
{
    [DesignTimeViewModel(Name = "ViewModelSample.MainPageViewModel")]
    public class MainPageViewModelDesignTime : MainPageViewModel
    {
        public MainPageViewModelDesignTime()
            :base(new DesignTimeMessageService())
        {
        }
    }

    public class DesignTimeMessageService : IMessageService
    {
        public DesignTimeMessageService()
        {
            Message = "DesignTime";
        }

        public string Message { get; set; }
    }
}
