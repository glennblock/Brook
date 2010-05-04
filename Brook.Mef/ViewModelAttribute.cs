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

namespace Brook.Mef
{
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class)]
    public class ViewModelAttribute : ExportAttribute
    {
        public ViewModelAttribute()
            :base("ViewModel", typeof(object))
        {
        }

        public string Name { get; set; }
    }
}
