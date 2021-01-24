using System;
using System.Collections.Generic;
using System.Linq;
using MvvmCross.Binding.Bindings.SourceSteps;
using MvvmCross.Binding.Combiners;

namespace MvvmCrossAlerts.iOS
{
    public class StringValueCombiner : MvxValueCombiner
    {
        public override Type SourceType(IEnumerable<IMvxSourceStep> steps)
        {
            return typeof(string);
        }

        public override bool TryGetValue(IEnumerable<IMvxSourceStep> steps, out object value)
        {
            var sourceSteps = steps as IMvxSourceStep[] ?? steps.ToArray();

            var strings = sourceSteps
                .Select(item => item.GetValue())
                .Where(item => item != null)
                .OfType<string>()
                .ToArray();

            value = string.Join(Environment.NewLine, strings);

            return true;
        }
    }
}