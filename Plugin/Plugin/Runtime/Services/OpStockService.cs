using Plugin.Interfaces;
using Plugin.Models.Private;

namespace Plugin.Runtime.Services
{
    /// <summary>
    /// Сервіс, для маніпуляції операцій, котрі присилають актори
    /// </summary>
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
        /// Отримати операцію зі складу 
        /// </summary>
        public IOpStockItem GetOp(int actorId, byte opCode)
        {
            return _model.Items.Find(x => x.ActorId == actorId && x.OpCode == opCode);
        }

        /// <summary>
        /// Отримати і видалити операцію зі складу 
        /// </summary>
        public IOpStockItem TakeOp(int actorId, byte opCode)
        {
            var item = GetOp(actorId, opCode);
            _model.Remove(item);

            return item;
        }
    }
}
