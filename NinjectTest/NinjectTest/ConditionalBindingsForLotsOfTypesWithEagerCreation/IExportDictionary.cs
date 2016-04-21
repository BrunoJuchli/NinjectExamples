using System.Collections.Generic;

namespace NinjectTest.ConditionalBindingsForLotsOfTypesWithEagerCreation
{
    internal interface IExportDictionary
    {
        IExport Get(string key);
    }

    internal class ExportDictionary : IExportDictionary
    {
        private readonly Dictionary<string, IExport> dictionary;

        public ExportDictionary(IEnumerable<IExport> exports)
        {
            dictionary = new Dictionary<string, IExport>();
            foreach (IExport export in exports)
            {
                dictionary.Add(export.GetType().FullName, export);
            }
        }

        public IExport Get(string key)
        {
            return dictionary[key];
        }
    }
}