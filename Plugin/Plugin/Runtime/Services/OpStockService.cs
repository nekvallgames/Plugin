using Plugin.Models.Private;
using Plugin.Schemes;

namespace Plugin.Runtime.Services
{
    public class OpStockService
    {
        private OpStockPrivateModel<OpScheme> _model;

        public OpStockService(OpStockPrivateModel<OpScheme> model)
        {
            _model = model;
        }

        public void Add(OpScheme opScheme)
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
    }
}
