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
    public class Locator
    {
        private IDictionary<string, Type> _viewModelTypeMapping;
        public Locator()
        {

        }

        private IDictionary<string, Type> GetViewModelTypeMapping()
        {
            dynamic appDomain = AppDomain.CurrentDomain;
            var assemblies = (Assembly[])appDomain.GetAssemblies();
            var mapping = new Dictionary<string, Type>();
            

            var designTimeAssemblies =
                assemblies.Where(a => a.FullName.Contains("Design,")).OrderBy(a => a.FullName).OrderBy(
                    a => File.GetLastWriteTime(a.Location)).Reverse();

            string lastAssembly = null;
            foreach (var assembly in designTimeAssemblies)
            {
                if (assembly.FullName != lastAssembly)
                {
                    var types = assembly.GetTypes().Where(t=>t.Name.EndsWith("Design"));
                    foreach (var type in types)
                    {
                        var viewModelName = type.Name.Substring(0, type.Name.Length - "Design".Length);
                        mapping[viewModelName] = type;
                    }
                }
                lastAssembly = assembly.FullName;
            }
            return mapping;
        }

        protected virtual object GetDesignTimeViewModel(string name)
        {
            if (_viewModelTypeMapping == null)
            {
                _viewModelTypeMapping = GetViewModelTypeMapping();
                var resolver = new ModelResolver(this.GetViewModel, null);
                ModelResolver.SetResolver(resolver);
            }

            Type viewModelType = null;
            bool found = _viewModelTypeMapping.TryGetValue(name, out viewModelType);
            if (found)
                return Activator.CreateInstance(viewModelType);
            else
                return null;
        }

        protected virtual object GetRuntimeViewModel(FrameworkElement view, string name)
        {
            var viewModelType = view.GetType().Assembly.GetType(name);
            return Activator.CreateInstance(viewModelType);
        }

        public object GetViewModel(FrameworkElement view, string name)
        {
            if (DesignerProperties.IsInDesignTool)
                return GetDesignTimeViewModel(name);
            else
                return GetRuntimeViewModel(view, name);
        }
    }
}
