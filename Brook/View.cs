using System.Windows;
using System.ComponentModel;

namespace Brook
{
    public static class View
    {
        public static object GetName(DependencyObject obj)
        {
            return (object)obj.GetValue(NameProperty);
        }

        public static void SetName(DependencyObject obj, object value)
        {
            obj.SetValue(NameProperty, value);
        }

        // Using a DependencyProperty as the backing store for ViewModel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NameProperty =
            DependencyProperty.RegisterAttached("Name", typeof(string), typeof(View), new PropertyMetadata((s, e) =>
                {
                    var element = (FrameworkElement)s;
                    //element.SetValue(DesignTimeData);
                    string view = null;
                    if (DesignerProperties.IsInDesignTool)
                    {
                        view = (string)e.NewValue;
                        var locator = new Locator();
                        var initializer = new ViewModelInitializer(locator.GetViewModel);
                    }
                    else
                        view = s.GetType().FullName;

                    WireViewModel(element, view);
                    
                }
                                                                                          ));
        
        private static void WireViewModel(FrameworkElement element, string view)
        {
            element.DataContext = ModelResolver._resolver.Resolve(element, view);
        }
    
    
    }


}