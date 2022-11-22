using Plugin.Interfaces;
using Plugin.Templates;

namespace Plugin.Models.Private
{
    /// <summary>
    /// Приватна модель, котра зберігає в собі стейти для роботи стейт машини PlotService
    /// </summary>
    public class PlotStatesPrivateModel : BaseStatesModel<IState>, IPrivateModel
    {

    }
}
