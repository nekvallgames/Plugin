using Plugin.Installers;
using Plugin.Interfaces;
using Plugin.OpComponents;
using Plugin.Runtime.Services.ExecuteAction.Action;
using System;
using System.Collections.Generic;

namespace Plugin.Runtime.Services.ExecuteOp.Executors
{
    /// <summary>
    /// Выполнить действие игрока - выполнить активное действие игрока.
    /// Например, если он стрелок, значит он выстрелил во вражеского юнита
    /// </summary>
    public class ExecuteOpAction : IExecuteOp
    {
        private UnitsService _unitsService;
        private ExecuteActionService _executeActionService;

        // Данные, которые нужны для восзоздания действия игрока
        private int _unitId;
        private int _instanceId;
        private int _posW;
        private int _posH;
        private int _targetActorId;


        public ExecuteOpAction()
        {
            var gameInstaller = GameInstaller.GetInstance();

            _unitsService = gameInstaller.unitsService;
            _executeActionService = gameInstaller.executeActionService;
        }

        /// <summary>
        /// Может ли текущий класс выполнить действие игрока?
        /// </summary>
        public bool CanExecute(List<ISyncComponent> componentsGroup)
        {
            foreach (ISyncComponent component in componentsGroup){
                if (component.GetType() == typeof(ActionOpComponent)){
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Выполнить действие игрока
        /// </summary>
        public void Execute(int playerActorID, List<ISyncComponent> componentsGroup)
        {
            // 1. Вытаскиваем нужные нам компоненты из списка
            if (!ParceData(componentsGroup)){
                throw new ArgumentException($"ExecuteOpService :: ExecuteOpAction :: Execute() playerActorID = {playerActorID}. I can't parce data");
            }

            // 2. Найти юнита, который выполнил действие
            IUnit unit = _unitsService.GetUnit(playerActorID, _unitId, _instanceId);

            if (unit == null){
                throw new ArgumentException($"ExecuteOpService :: ExecuteOpAction :: Execute() playerActorID = {playerActorID}, unitID = {_unitId}, instanceID = {_instanceId}. I don't find this unit for execute actions");
            }

            // 3. Отбращаемся к классу, который выполняет действия юнитов, и просим 
            // его, выполнять для текущего юнита действие
            _executeActionService.ExecuteAction(unit, _targetActorId, _posW, _posH);
        }

        /// <summary>
        /// Распарсить входящие данные
        /// </summary>
        private bool ParceData(List<ISyncComponent> componentsGroup)
        {
            bool isParceAction = false;
            bool isParceUnitID = false;
            bool isParceTargetActorID = false;

            foreach (ISyncComponent component in componentsGroup)
            {
                if (component.GetType() == typeof(ActionOpComponent))
                {
                    _posW = ((ActionOpComponent)component).w;
                    _posH = ((ActionOpComponent)component).h;
                    isParceAction = true;
                }
                else
                if (component.GetType() == typeof(UnitIdOpComponent))
                {
                    _unitId = ((UnitIdOpComponent)component).uid;
                    _instanceId = ((UnitIdOpComponent)component).i;
                    isParceUnitID = true;
                }
                else
                if (component.GetType() == typeof(TargetActorIdOpComponent))
                {
                    _targetActorId = ((TargetActorIdOpComponent)component).aid;
                    isParceTargetActorID = true;
                }
            }

            if (isParceAction && isParceUnitID && isParceTargetActorID)
            {
                return true;
            }

            return false;
        }
    }
}
