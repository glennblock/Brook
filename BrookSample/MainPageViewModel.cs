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
    [Export]
    public class MainPageViewModel
    {
        public MainPageViewModel() : this(new RuntimeMessageService())
        {
            
        }
        
        [ImportingConstructor]
        public MainPageViewModel(IMessageService messageService)
        {
            Message = messageService.Message;
        }

        public string Message { get; set; } 
    }
}
