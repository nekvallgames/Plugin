using System;

namespace Plugin.Interfaces.Actions
{
    /// <summary>
    /// Выполнить действие - бросить гранату
    /// Интерфейс с гранатой разширяет интерфейс с огнестрельным оружием
    /// </summary>
    public interface IGrenadeWeaponsAction : IWeaponsAction
    {
        /// <summary>
        /// Получить урон от гранаты на 1-й волне
        /// </summary>
        Int16[] GetWaveDamage();
    }
}
