using System.Collections.Generic;

namespace NinjectExamples
{
    public interface IComparePatternAndData
    {
        int PatternId { get; }
    }

    public interface IPatternDictionary
    {
        bool TryGetValue(int key, out IComparePatternAndData value);
    }

    public class PatternDictionary : Dictionary<int, IComparePatternAndData>, IPatternDictionary
    {
        public PatternDictionary(IComparePatternAndData[] patterns)
        {
            foreach (IComparePatternAndData pattern in patterns)
            {
                this[pattern.PatternId] = pattern;
            }
        }
    }
}