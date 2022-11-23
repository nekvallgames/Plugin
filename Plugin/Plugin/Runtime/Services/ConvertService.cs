using Newtonsoft.Json;

namespace Plugin.Runtime.Services
{
    /// <summary>
    /// Сервіс, котрий буде допомогати серіалізувати/десереалізувати обьекти
    /// </summary>
    public class ConvertService
    {
        public ConvertService()
        {
            
        }

        public T DeserializeObject<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }
    }
}
