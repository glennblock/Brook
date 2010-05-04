using System;
using System.Collections.Generic;
using System.Linq;

namespace Brook
{
    public class ModelResolver
    {
        private IList<ResolutionStrategy> _resolutionStrategies;
        private Func<string, object> _locator;

        public ModelResolver(Func<string, object> locator, IList<ResolutionStrategy> resolutionStrategies)
        {
            _locator = locator;
            _resolutionStrategies = resolutionStrategies;
        }

        public object Resolve(string viewName)
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
                return _locator(viewModelName);
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