using System;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel.Composition;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using Path = System.IO.Path;
using System.ComponentModel.Composition.Primitives;

namespace Brook.Mef
{
    public class MefLocator : Locator
    {
        public static void SetCompositionContainer(CompositionContainer container)
        {
            _container = container;
        }

        private static CompositionContainer _container;

        public MefLocator()
        {
            
        }

        protected override object GetRuntimeViewModel(FrameworkElement view, string name)
        {
            return _container.GetExportedValue<object>(name);
        }
    }
}
