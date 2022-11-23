using Plugin.Interfaces;
using Plugin.Runtime.Services;
using Plugin.Schemes;
using Plugin.Templates;

namespace Plugin.Models.Public
{
    /// <summary>
    /// Публічна модель із даними, котра буде зберігати в собі дані із локаціями
    /// Дані локації потрібні для створення ігрової сітки
    /// </summary>
    public class LocationsPublicModel<T> : BaseModel<T>, IPublicModel where T : LocationScheme
    {
        private ConvertService _convertService;

        public LocationsPublicModel(ConvertService convertService)
        {
            _convertService = convertService;
        }

        public void Parse()
        {
            // TODO в майбутньому отрамати дані ігрової локацій із адмінки
            string locationData1 = "{\"Name\":\"locationName\",\"SizeGrid\":{ \"x\":22,\"y\":16},\"GridMask\":[16,16,16,16,16,16,16,16,16,16,16,16,16,16,16,16,16,16,16,16,16,16,16,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,16,16,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,16,16,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,16,16,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,16,16,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,16,16,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,16,16,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,16,16,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,16,16,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,16,16,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,16,16,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,16,16,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,16,16,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,16,16,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,16,16,16,16,16,16,16,16,16,16,16,16,16,16,16,16,16,16,16,16,16,16,16]}";            

            T locationScheme = _convertService.DeserializeObject<T>(locationData1);

            Add(locationScheme);
        }
    }
}
