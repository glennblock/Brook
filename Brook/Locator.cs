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
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.IO;
using System.ComponentModel;

namespace Brook
{
    public abstract class Locator
    {
        private IDictionary<string,Type> _viewModelTypeMapping;

        public Locator()
        {
            
        }

        private IDictionary<string,Type> GetViewModelTypeMapping(IEnumerable<string> designTimeAssemblies)
        {
            dynamic appDomain = AppDomain.CurrentDomain;
            var assemblies = (Assembly[])appDomain.GetAssemblies();
            var mapping = new Dictionary<string, Type>();
            
            foreach (var assemblyName in designTimeAssemblies)
            {
                var assembly = assemblies.Where(a => a.FullName.StartsWith(assemblyName)).OrderBy(a => File.GetLastWriteTime(a.Location)).FirstOrDefault();
                if (assembly != null)
                {
                    var types = assembly.GetTypes();
                    foreach(var type in types)
                    {
                        var viewModelAttribute = type.GetCustomAttributes(typeof(DesignTimeViewModelAttribute), false).Cast<DesignTimeViewModelAttribute>().SingleOrDefault();
                        if (viewModelAttribute != null)
                            mapping[viewModelAttribute.Name] = type;
                    }
                    
                }
            }
            return mapping;
        }

        protected virtual object GetDesignTimeViewModel(string name)
        {
            if (_viewModelTypeMapping == null)
            {
                var init = (ViewModelInitializer)Application.Current.Resources["init"];
                _viewModelTypeMapping = GetViewModelTypeMapping(init.GetDesignTimeAssemblyPaths());
            }

            Type viewModelType = null;
            bool found = _viewModelTypeMapping.TryGetValue(name, out viewModelType);
            if (found)
                return Activator.CreateInstance(viewModelType);
            else
                return null;
        }

        protected virtual object GetRuntimeViewModel(string name)
        {
            return null;
        }

        public object GetViewModel(string name)
        {
            if (!DesignerProperties.IsInDesignTool)
                return GetRuntimeViewModel(name);
            else
                return GetDesignTimeViewModel(name);
        }
    }
}
