using Plugin.Tools;
using System.Numerics;

namespace Plugin.Interfaces.Actions
{
    /// <summary>
    /// Выполнить действие - выстрелы с огнестрельного оружия
    /// </summary>
    public interface IWeaponsAction
    {
        /// <summary>
        /// Инитиализировать оружие
        /// </summary>
        void InitializeGun();

        /// <summary>
        /// Перезарядить оружие
        /// </summary>
        void GunReload();

        /// <summary>
        /// Может ли юнит выстрелить?
        /// Есть ли патроны для выстрела?
        /// </summary>
        bool CanShot();

        /// <summary>
        /// Юнит выстрелил. Использовать аммуницию
        /// </summary>
        void Shot();

        /// <summary>
        /// Сила урона от текущего оружия. Но, текущее значение может изменятся,
        /// так как сущность может получить как баф, так и дебаф
        /// </summary>
        int CurrDamage { get; set; }

        /// <summary>
        /// Базовая сила урона от текущего оружия
        /// Текущий параметр изменять нельзя!!!
        /// </summary>
        int OriginalDamage { get; }

        /// <summary>
        /// Получить рисунок экшена
        /// </summary>
        Vector2Int[] GetActionArea();

        /// <summary>
        /// Текущее количество баффа для урона
        /// Число указывать в %. Это число может быть как отрицательным, так и положительным
        /// </summary>
        int DamageBuff { get; set; }
    }
}
