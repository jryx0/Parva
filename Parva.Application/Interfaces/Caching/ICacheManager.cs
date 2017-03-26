namespace Parva.Application.Interfaces.Caching
{
    public interface ICacheManager
    {
        T Get<T>(string key);

        void Set(string key, object data);

        bool IsSet(string key);

        void Remove(string key);

        void Clear();
    }
}