using Plugin.Models.Private;
using Plugin.Schemes;
using Plugin.Signals;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Plugin.Runtime.Services
{
    public class ActorsService
    {
        private ActorsPrivateModel<ActorScheme> _model;
        private SignalBus _signalBus;

        public ActorsService(ActorsPrivateModel<ActorScheme> model, SignalBus signalBus)
        {
            _signalBus = signalBus;

            _model = model;
        }

        /// <summary>
        /// Створити нового актора
        /// </summary>
        public void CreateActor(string userId, int actorId)
        {
            if (Has(actorId)){
                Debug.Fail($"ActorsService :: CreateActor() I can't create actorId = {actorId}, because this actor already was created.");
                return;
            }

            _model.Add(new ActorScheme(userId, actorId));
        }

        public bool Has(int actorId)
        {
            return _model.Items.Any(x => x.ActorId == actorId);
        }

        public void EnableConnected(int actorId, bool enable)
        {
            if (!Has(actorId)){
                throw new ArgumentNullException($"ActorsService :: EnableConnected() actorId = {actorId} is null");
            }

            _model.Items.Find(x => x.ActorId == actorId).IsConnected = enable;

            _signalBus.Fire(new ActorsPrivateModelSignal(actorId, ModelChangeSignal.StatusType.change));
        }

        /// <summary>
        /// Отримати всіх акторів
        /// </summary>
        public List<ActorScheme> Actors => _model.Items;
        
        /// <summary>
        /// Отримати всіх акторів, котрі приконекчені до ігрової кімнати
        /// </summary>
        public List<ActorScheme> GetConnectedActors()
        {
            return _model.Items.FindAll(x => x.IsConnected);
        }

        /// <summary>
        /// Видалити із моделі акторів, котрі були дісконекнуті
        /// </summary>
        public void RemoveDisconnectedActors()
        {
            List<ActorScheme> actors = _model.Items.FindAll(x => !x.IsConnected);
            if (actors.Any()){
                foreach (ActorScheme actor in actors){
                    _model.Remove(actor);
                }
            }
        }
    }
}
