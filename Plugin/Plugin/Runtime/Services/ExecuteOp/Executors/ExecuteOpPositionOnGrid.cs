using Plugin.Installers;
using Plugin.Interfaces;
using Plugin.OpComponents;
using Plugin.Runtime.Services.ExecuteAction;
using System;
using System.Collections.Generic;

namespace Plugin.Runtime.Services.ExecuteOp.Executors
{
    /// <summary>
    /// Виконати операцію клієнта, котру він прислав на Game Server
    /// Выполнить действие игрока - переместить игрового юнита на игровой сетке
    /// </summary>
    public class ExecuteOpPositionOnGrid : IExecuteOp
    {
        private UnitsService _unitsService;
        private ExecuteMoveService _executeMoveService;

        // Данные, которые нужны для восзоздания действия игрока
        private int _unitID;
        private int _instanceID;
        private int _posW;
        private int _posH;

        public ExecuteOpPositionOnGrid()
        {
            var gameInstaller = GameInstaller.GetInstance();

            _unitsService = gameInstaller.unitsService;
            _executeMoveService = gameInstaller.executeMoveService;
        }

        /// <summary>
        /// Может ли текущий класс выполнить действие игрока?
        /// </summary>
        public bool CanExecute( List<ISyncComponent> componentsGroup )
        {
            foreach (ISyncComponent component in componentsGroup){
                if (component.GetType() == typeof(PositionOnGridOpComponent)){
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Выполнить действие игрока. А именно позиционировать юнита на игровой сетке
        /// </summary>
        public void Execute(int playerActorId, List<ISyncComponent> componentsGroup)
        {
            // Вытаскиваем нужные нам компоненты из списка
            if (!ParceData(componentsGroup)){
                throw new ArgumentException($"ExecuteOpService :: ExecuteOpPositionOnGrid() playerActorId = {playerActorId}, I can't parce data");
            }

            // Вытащить юнита, к которому будем применять перемещение
            IUnit unit = _unitsService.GetUnit(playerActorId, _unitID, _instanceID);

            if (unit == null){
                throw new ArgumentException($"ExecuteOpService :: ExecuteOpPositionOnGrid() playerActorID = {playerActorId}, unitID = {_unitID}, instanceID = {_instanceID}, I don't find this unit for execute actions");
            }

            // Переместить юнита в указаную позицию
            _executeMoveService.PositionOnGrid(unit, _posW, _posH);
        }

        /// <summary>
        /// Распарсить входящие данные
        /// </summary>
        private bool ParceData(List<ISyncComponent> componentsGroup)
        {
            bool isParcePosition = false;
            bool isParceUnitId = false;

            foreach (ISyncComponent component in componentsGroup)
            {
                if (component.GetType() == typeof(PositionOnGridOpComponent))
                {
                    _posW = ((PositionOnGridOpComponent)component).w;
                    _posH = ((PositionOnGridOpComponent)component).h;
                    isParcePosition = true;
                }
                else
                if (component.GetType() == typeof(UnitIdOpComponent))
                {
                    _unitID = ((UnitIdOpComponent)component).uid;
                    _instanceID = ((UnitIdOpComponent)component).i;
                    isParceUnitId = true;
                }
            }

            if (isParcePosition && isParceUnitId){
                return true;
            }

            return false;
        }
    }
}
