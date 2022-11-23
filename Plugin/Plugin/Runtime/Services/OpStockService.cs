using Plugin.Interfaces;
using Plugin.Models.Private;

namespace Plugin.Runtime.Services
{
    public class OpStockService
    {
        private OpStockPrivateModel<IOpStockItem> _model;

        public OpStockService(OpStockPrivateModel<IOpStockItem> model)
        {
            _model = model;
        }

        public void Add(IOpStockItem opScheme)
        {
            _model.Add(opScheme);
        }

        /// <summary>
        /// Получить общее количество указаной операции на складе всех игроков
        /// </summary>
        public int GetOpCount(byte operationCode)
        {
            return _model.Items.FindAll(x => x.OpCode == operationCode).Count;
        }

        /// <summary>
        /// Отримати і видалити операцію зі складу 
        /// </summary>
        public IOpStockItem TakeOp(int actorId, byte opCode)
        {
            var item = _model.Items.Find(x => x.ActorId == actorId && x.OpCode == opCode);
            _model.Items.Remove(item);

            return item;
        }
    }
}
