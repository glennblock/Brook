using System;

namespace Brook
{
    public class ResolutionStrategy
    {
        public ResolutionStrategy(Predicate<string> condition, Func<string,string> mapping)
        {
            Condition = condition;
            Mapping = mapping;
        }

        public Predicate<string> Condition { get; private set; }
        public Func<string, string> Mapping { get; private set; }
    }
}