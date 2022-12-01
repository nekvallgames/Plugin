using Plugin.Interfaces;
using Plugin.Models.Private;

namespace Plugin.Runtime.Services
{
    /// <summary>
    /// Сервіс, за допомогою котрого будуть виконуватись маніпуляції із PlotsPrivateModel
    /// </summary>
    public class PlotsModelService
    {
        private PlotsPrivateModel<IPlotModelScheme> _model;

        public PlotsModelService(PlotsPrivateModel<IPlotModelScheme> model)
        {
            _model = model;
        }

        public void Add(IPlotModelScheme model)
        {
            _model.Add(model);
        }

        public IPlotModelScheme Get(int actorId)
        {
            return _model.Items.Find(x => x.OwnerActorId == actorId);
        }
    }
}
