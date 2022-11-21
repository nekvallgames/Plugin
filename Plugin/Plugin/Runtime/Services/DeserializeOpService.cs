using Newtonsoft.Json;
using Plugin.Interfaces;
using Plugin.Schemes;

namespace Plugin.Runtime.Services
{
    public class DeserializeOpService
    {
        /// <summary>
        /// Десериализировать операцию
        /// </summary>
        public T DeserializeOp<T>(OpScheme opData) where T : IOpScheme
        {
            return JsonConvert.DeserializeObject<T>( opData.Data.ToString() );
        }
    }
}
