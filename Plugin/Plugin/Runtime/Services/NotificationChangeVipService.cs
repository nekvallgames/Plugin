using Photon.Hive.Plugin;
using Plugin.Runtime.Providers;
using Plugin.Schemes;
using Plugin.Signals;
using Plugin.Tools;
using System.Collections.Generic;

namespace Plugin.Runtime.Services
{
    /// <summary>
    /// Сервис, который занимается тем, что в момент, когда игрок изменит своего VIP,
    /// то об этом нужно уведомить противника.
    ///
    /// В игровом состоянии локальный игрок может изменять
    /// своего VIP безконечное количество раз, сколько хочет.
    /// Но отправить уведомление об изменении VIP, может только 1 раз. 
    /// А работа сервера, это сообщение перенаправить противнику.
    /// </summary>
    public class NotificationChangeVipService
    {
        private SignalBus _signalBus;
        private BroadcastProvider _broadcastService;
        private OpStockService _opStockService;
        private ActorsService _actorsService;

        public NotificationChangeVipService(OpStockService opStockService, ActorsService actorsService, SignalBus signalBus, BroadcastProvider broadcastProvider)
        {
            _opStockService = opStockService;
            _actorsService = actorsService;
            _signalBus = signalBus;
            _broadcastService = broadcastProvider;
            
            _signalBus.Subscrible<OpStockPrivateModelSignal>(OnOpStockModelChange);
        }

        private void OnOpStockModelChange(OpStockPrivateModelSignal signalData)
        {
            if (signalData.OpCode == OperationCode.changeVip 
                && signalData.Status == OpStockPrivateModelSignal.StatusType.add)
            {
                // На склад операцій упала операція OperationCode.changeVip

                _opStockService.TakeOp(signalData.ActorId, signalData.OpCode);  // видалити операцію зі складу

                // Створити массив із акторів, кому ми відправимо цей івент
                List<ActorScheme> actors = _actorsService.GetActors().FindAll(x => x.ActorId != signalData.ActorId);
                var actorsId = new List<int>();
                foreach (ActorScheme actor in actors){
                    actorsId.Add(actor.ActorId);
                }

                // Відправити всім участникам цей івент
                _broadcastService.Send(actorsId, 
                                       signalData.ActorId,
                                       OperationCode.changeVip,
                                       null,
                                       CacheOperations.DoNotCache); // не кэшировать сообщение
            }
        }
    }
}
