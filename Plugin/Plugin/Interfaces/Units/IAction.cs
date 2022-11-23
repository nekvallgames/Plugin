using Plugin.Tools;

namespace Plugin.Interfaces.Units
{
    public interface IAction
    {
        /// <summary>
        /// Восстановить экшены юнита
        /// </summary>
        void ReviveAction();

        /// <summary>
        /// Виконати экшены юніта
        /// </summary>
        void ExecuteAction(Int2 position);
    }
}
