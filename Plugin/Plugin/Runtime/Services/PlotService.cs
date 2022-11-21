using Plugin.Interfaces;
using Plugin.Models.Private;
using Plugin.Templates;

namespace Plugin.Runtime.Services
{
    /// <summary>
    /// Сервіс, за допомогою котрого будемо переключати стейти ігрового сценарія
    /// </summary>
    public class PlotService : BaseStateMachine<IState, PlotStatesPrivateModel>
    {
        public PlotService(PlotStatesPrivateModel plotStatesPrivateModel) : base(plotStatesPrivateModel)
        {

        }

        public void ChangeState(string nextStateName)
        {
            base.ChangeState(nextStateName);
        }
    }
}
