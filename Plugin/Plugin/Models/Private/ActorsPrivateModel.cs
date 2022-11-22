using Plugin.Interfaces;
using Plugin.Schemes;
using Plugin.Templates;

namespace Plugin.Models.Private
{
    public class ActorsPrivateModel<T> : BaseModel<T>, IPrivateModel where T : ActorScheme
    {

    }
}
