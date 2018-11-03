using System;
using System.Collections.Generic;
using System.Text;

namespace TodoListProject.Core.Caching {
    public interface ICacheManager {
        T Get<T>(string key);
        void Add(string key, object data, int cacheTime);
        void Remove(string key);
    }
}
