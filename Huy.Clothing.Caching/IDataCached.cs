using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Huy.Clothing.Caching
{
    public interface IDataCached
    {
      
        T Get<T>(string key, Func<T> accquire, int? cacheTime=null);
        T Get<T>(string key);
        object Get(string key);
        IList<string> GetKeys();
        IList<T> GetValues<T>(string pattern);
        void Set<T>(string key, T? value, int cacheTime);
        bool IsSet(string key);
        void Remove(string key);
        void Clear();
    }
}
