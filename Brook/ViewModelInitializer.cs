﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Markup;

namespace Brook
{
    [ContentProperty("DesignTimeAssemblies")]
    public abstract class ViewModelInitializer
    {
        private List<ResolutionStrategy> _strategies;

        public ViewModelInitializer(Func<string,object> locator, params ResolutionStrategy[] strategies)
        {
            if (!strategies.Any())
            {
                _strategies = new List<ResolutionStrategy>();
                var strategy = new ResolutionStrategy(
                    v => true, //match on anything
                    v =>
                        {
                            var viewModelName = v + "ViewModel";
                            return viewModelName;
                        }
                    );

                _strategies.Add(strategy);
                strategy = new ResolutionStrategy(
                    v => v.EndsWith("View"), //match on view name ending with view,
                    v =>
                        {
                            var viewModelName = v + "Model";
                            return viewModelName;
                        }
                    );
                _strategies.Add(strategy);
            }
            else
                _strategies = strategies.ToList();

            var resolver = new ModelResolver(locator, _strategies);
            ModelResolver.SetResolver(resolver);
            DesignTimeAssemblies = new List<DesignTimeAssembly>();
        }

        public List<DesignTimeAssembly> DesignTimeAssemblies { get; set; }

        internal IEnumerable<string> GetDesignTimeAssemblyPaths()
        {
            return DesignTimeAssemblies.Select(dc => dc.Name);
        }
    }
}