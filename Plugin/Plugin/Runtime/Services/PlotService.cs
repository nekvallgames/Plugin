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
        private PlotStatesPrivateModel _model;

        public PlotService(PlotStatesPrivateModel model) : base(model)
        {
            _model = model;
        }

        public void ChangeState(string nextStateName)
        {
            base.ChangeState(nextStateName);
        }

        public void Add(IState[] states)
        {
            foreach (var state in states)
                Add(state);
        }

        public void Add(IState state)
        {
            _model.Add(state);
        }
    }
}
