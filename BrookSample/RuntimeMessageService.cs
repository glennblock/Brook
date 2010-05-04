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
using System.ComponentModel.Composition;

namespace BrookSample
{
    [Export(typeof(IMessageService))]
    public class RuntimeMessageService : IMessageService
    {
        public RuntimeMessageService()
        {
            Message = "Runtime";
        }

        public string Message { get; set; }
    }
}

