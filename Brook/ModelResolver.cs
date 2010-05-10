using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Brook
{
    public class ModelResolver
    {
        private static IList<ResolutionStrategy> _resolutionStrategies;
        private Func<FrameworkElement, string, object> _locator;

        public ModelResolver(Func<FrameworkElement, string, object> locator, IList<ResolutionStrategy> resolutionStrategies)
        {
            _locator = locator;
            _resolutionStrategies = resolutionStrategies;
        }

        public object Resolve(FrameworkElement view, string viewName)
        {
            string viewModelName = null;
            var strategies = _resolutionStrategies.AsEnumerable().Reverse();
            foreach (var strategy in strategies)
            {
                if (strategy.Condition(viewName))
                    viewModelName = strategy.Mapping(viewName);
                
                if (viewModelName != null)
                    break;
            }

            if (viewModelName != null)
                return _locator(view, viewModelName);
            else
                return null;
        }

        internal static ModelResolver _resolver;
        public static void SetResolver(ModelResolver resolver)
        {
            _resolver = resolver;
        }
    }
}