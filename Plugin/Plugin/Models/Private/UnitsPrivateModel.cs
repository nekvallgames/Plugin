using Plugin.Interfaces;
using Plugin.Interfaces.Units;
using Plugin.Templates;

namespace Plugin.Models.Private
{
    public class UnitsPrivateModel<T> : BaseModel<T>, IPrivateModel where T : IUnit
    {
        
    }
}
