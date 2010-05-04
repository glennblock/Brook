using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Markup;
using System.ComponentModel;

namespace Brook
{
    public class ViewElement : ContentControl, ISupportInitialize
    {
        public ViewElement()
        {
        }

        public string ViewName { get; set; }

        #region ISupportInitialize Members

        public void BeginInit()
        {
            
        }

        public void EndInit()
        {
            this.SetValue(Brook.View.NameProperty, ViewName);
        }

        #endregion
    }
}
