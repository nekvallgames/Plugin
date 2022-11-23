using Plugin.Interfaces;
using Plugin.Runtime.Services.ExecuteAction.Action.Executors;

namespace Plugin.Runtime.Services.ExecuteAction.Action
{
    /// <summary>
    /// Сервіс, который будет выполнять основное действия юнита
    /// </summary>
    public class ActionService
    {
        /// <summary>
        /// Исполнители действий
        /// </summary>
        private IExecuteAction[] _executorsActions;

        public ActionService()
        {
            _executorsActions = new IExecuteAction[]
            {
                new WaveDamageAction(),    // выполнить бросок гранаты и взорвать ее
                new DamageAction()      // выстрелить 1 раз с огнестрельного оружия
            };
        }

        /// <summary>
        /// Выполнить для текущего юнита его действие
        /// Например: если юнит стрелок, то юнит должен выстрелить
        /// </summary>
        /// <param name="unit"> Указать юнит, который будет выполнять действие </param>
        /// <param name="targetActorID"> Указать ID игрока, на сетке которого нужно выполнить действие </param>
        /// <param name="posW"> Позиция на игровой сетке </param>
        /// <param name="posH"> Позиция на игровой сетке </param>
        public void ExecuteAction(IUnit unit, int targetActorID, int posW, int posH)
        {
            foreach ( IExecuteAction executer in _executorsActions )
            {
                // Перебираем всех исполнителей действий, и проверяем, может 
                // ли какой то исполнитель выполнить действие для текущего юнита
                if (!executer.CanExecute(unit)){
                    continue;
                }

                // Текущим исполнителем выполнить действие для текущего юнита
                executer.Execute(unit, targetActorID, posW, posH);
                break;
            }
        }
    }
}
