using System.Windows;

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
                    var view = (string) e.NewValue;
                    if (view == "")
                    {
                        if (s is ViewElement)
                        {
                            var parentType = element.Parent.GetType();
                            view = parentType.FullName;
                        }
                        else
                            view = s.GetType().FullName;
                    }
                    element.DataContext = ModelResolver._resolver.Resolve(view);
                }
                                                                                          ));
        
    }
}