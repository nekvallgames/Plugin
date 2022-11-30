using Photon.Hive.Plugin;
using Plugin.Builders;
using Plugin.Runtime.Providers;
using Plugin.Schemes;
using Plugin.Tools;
using System.Collections.Generic;

namespace Plugin.Runtime.Services
{
    /// <summary>
    /// Сервіс, котрий буде синхронізувати виконані дії гравців між собою
    /// Беремо дані синхронізації із моделі SyncPrivateModel, запихуємо всі дані в StepScheme
    /// і відправляємо ці дані акторам
    /// </summary>
    public class SyncStepService
    {
        private BroadcastProvider _broadcastProvider;
        private ActorsService _actorsService;
        private StepSchemeBuilder _stepSchemeBuilder;
        private ConvertService _convertService;

        public SyncStepService(BroadcastProvider broadcastProvider, 
                               ActorsService actorsService, 
                               StepSchemeBuilder stepSchemeBuilder,
                               ConvertService convertService)
        {
            _broadcastProvider = broadcastProvider;
            _actorsService = actorsService;
            _stepSchemeBuilder = stepSchemeBuilder;
            _convertService = convertService;
        }

        public void Sync(int[] syncSteps)
        {
            // Создать коллекцию, которая будет хранить в себе данные, которые нужно синхронизировать 
            // между клиентами
            // key   - это ActorID
            // value - это StepScheme, которая имеет кучу из компонентов
            var pushData = new Dictionary<byte, object> { };

            // Зібрати синхронізацію дій акторів і відправити результат їхній дій всім акторам в кімнаті
            foreach (ActorScheme actor in _actorsService.Actors)
            {
                StepScheme scheme = _stepSchemeBuilder.Create(actor.ActorId, syncSteps);
                string jsonString = _convertService.SerializeObject(scheme);
                pushData.Add((byte)actor.ActorId, jsonString);
            }

            _broadcastProvider.Send(ReciverGroup.All,                   // отправить сообщение всем
                                    0,                                  // номер актера, если нужно отправить уникальное сообщение
                                    0,
                                    OperationCode.stepResult,
                                    pushData,
                                    CacheOperations.DoNotCache);        // не кэшировать сообщение
        }
    }
}
