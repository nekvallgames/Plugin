using Plugin.Installers;
using Plugin.Models.Private;
using Plugin.Schemes;
using Plugin.Signals;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Plugin.Runtime.Services
{
    public class ActorsService
    {
        private ActorsPrivateModel<ActorScheme> _model;
        private SignalBus _signalBus;

        public ActorsService(ActorsPrivateModel<ActorScheme> model)
        {
            _signalBus = GameInstaller.GetInstance().signalBus;

            _model = model;
        }

        public void CreateActor(string userId, int actorId)
        {
            if (Has(actorId)){
                throw new ArgumentException($"ActorsService :: CreateActor() I can't create actorId = {actorId}, because this actor already was created.");
            }

            _model.Add(new ActorScheme(userId, actorId));
            CreateSignal(actorId);
        }

        public bool Has(int actorId)
        {
            return _model.Items.Any(x => x.ActorId == actorId);
        }

        public void EnableConnected(int actorId, bool enable)
        {
            if (Has(actorId)){
                throw new ArgumentNullException($"ActorsService :: EnableConnected() actorId = {actorId} is null");
            }

            _model.Items.Find(x => x.ActorId == actorId).IsConnected = enable;
            CreateSignal(actorId);
        }

        /// <summary>
        /// Отримати всіх акторів, котрі приконекчені до ігрової кімнати
        /// </summary>
        public List<ActorScheme> GetConnectedActors()
        {
            return _model.Items.FindAll(x => x.IsConnected);
        }

        /// <summary>
        /// Створити сигнал, що модель даних ActorsPrivateModel була змінена
        /// </summary>
        private void CreateSignal(int actorId)
        {
            _signalBus.Fire(new ActorsPrivateModelSignal(actorId));
        }

        
    }
}
