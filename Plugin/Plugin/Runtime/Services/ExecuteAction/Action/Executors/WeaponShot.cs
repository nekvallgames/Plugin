using Plugin.Installers;
using Plugin.Interfaces;
using Plugin.Interfaces.Actions;
using Plugin.Interfaces.Units;
using Plugin.Runtime.Services.Sync;
using Plugin.Runtime.Services.Sync.Groups;
using Plugin.Tools;
using System;
using System.Collections.Generic;

namespace Plugin.Runtime.Services.ExecuteAction.Action.Executors
{
    /// <summary>
    /// Выполнить действие юнита
    /// 
    /// Текущий класс выполняет один выстрел с огнестрельного оружия
    /// 
    /// То есть, что мы делаем:
    /// 1. проверяем, есть ли у юнита патрон для выстрела
    /// 2. целимся, находим цель в точке выстрела
    /// 3. стреляем, снимаем урон
    /// 4. забираем у игрока 1 патрон
    /// 
    /// </summary>
    public class WeaponShot : IExecuteAction
    {
        private SyncService _syncService;
        private UnitsService _unitsService;
        private SortTargetOnGridService _sortTargetOnGridService;

        public WeaponShot()
        {
            var gameInstaller = GameInstaller.GetInstance();

            _syncService = gameInstaller.syncService;
            _unitsService = gameInstaller.unitsService;
            _sortTargetOnGridService = gameInstaller.sortTargetOnGridService;
        }

        /// <summary>
        /// Может ли текущий класс выполнить действие для юнита?
        /// </summary>
        public bool CanExecute(IUnit unit){
            if (unit is IDamageAction){
                return true;
            }

            return false;
        }

        /// <summary>
        /// Выполнить действие
        /// </summary>
        public void Execute( IUnit unit, int targetActorID, int posW, int posH )
        {
            // Проверяем, может ли юнит вытсрелить?
            var unitWithWeapon = (IDamageAction)unit;

            if (!unitWithWeapon.CanExecute()){
                throw new ArgumentException($"ExecuteActionService :: WeaponShot :: Execute() ownerID = {unit.OwnerActorId}, unitID = {unit.UnitId}, instanceID = {unit.InstanceId}, targetActorID = {targetActorID}, posW = {posW}, posH = {posH}, I can't shot, maybe I don't have ammunition.");
            }

            unitWithWeapon.Execute();     // делаем выстрел. Юнит тратит 1 патрон


            // Синхронизировать выполненное действие юнита на игровой сетке
            ISyncGroupComponent syncOnGrid = new SyncActionGroup(unit,
                                                                 targetActorID,
                                                                 posW,
                                                                 posH);
            _syncService.Add(unit.OwnerActorId, syncOnGrid);



            Int2[] actionArea = unitWithWeapon.GetArea();

            foreach (Int2 area in actionArea)
            {
                int targetW = posW + area.x;
                int targetH = posH + area.y;

                // Находим всех противников, в которых мы выстрелили
                List<IUnit> enemyTargets = _sortTargetOnGridService.SortTargets(_unitsService.GetUnitsUnderThisPosition(targetActorID, targetW, targetH));

                if (enemyTargets.Count <= 0){
                    return;                     // игрок выстрелил мимо!
                }

                int damage = unitWithWeapon.Power;   // получить урон, который игрок нанес выстрелом

                _unitsService.SetDamage(enemyTargets[0], damage);
            }
        }
    }
}
