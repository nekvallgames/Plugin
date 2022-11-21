using Plugin.Installers;
using Plugin.Interfaces;
using Plugin.OpComponents;
using Plugin.Runtime.Services.ExecuteAction.Additional;
using System;
using System.Collections.Generic;

namespace Plugin.Runtime.Services.ExecuteOp.Executors
{
    /// <summary>
    /// Выполнить дополнительное действие юнита - выполнить дополнительное (пассивное) действие юнита.
    /// Например, если юнит хиллер, то вылечить юнита
    /// </summary>
    public class ExecuteOpAdditional : IExecuteOp
    {
        private UnitsService _unitsService;
        private ExecuteAdditionalService _executeAdditionalService;

        // Данные, которые нужны для восзоздания дополнительное (пассивное) действия юнита
        private int _unitID;
        private int _instanceID;
        private int _posW;
        private int _posH;
        private int _targetActorID;

        public ExecuteOpAdditional()
        {
            var gameInstaller = GameInstaller.GetInstance();

            _unitsService = gameInstaller.unitsService;
            _executeAdditionalService = gameInstaller.executeAdditionalService;
        }

        /// <summary>
        /// Может ли текущий класс выполнить действие игрока?
        /// </summary>
        public bool CanExecute(List<ISyncComponent> componentsGroup)
        {
            foreach (ISyncComponent component in componentsGroup)
            {
                if (component.GetType() == typeof(AdditionalOpComponent)){
                    return true;
                }
            }

            return false;
        }


        /// <summary>
        /// Выполнить дополнительное (пассивное) действия юнита
        /// </summary>
        public void Execute(int playerActorID, List<ISyncComponent> componentsGroup)
        {
            // Вытаскиваем нужные нам компоненты из списка
            if (!ParceData(componentsGroup)){
                throw new ArgumentException($"ExecuteOpService :: ExecuteOpAdditional :: Execute() playerActorId = {playerActorID}, I can't parce data");
            }

            // Найти юнита, который выполнил действие
            IUnit unit = _unitsService.GetUnit(playerActorID, _unitID, _instanceID);

            if (unit == null){
                throw new ArgumentException($"ExecuteOpService :: ExecuteOpAdditional :: Execute() playerActorID = {playerActorID}, unitID = {_unitID}, instanceID = {_instanceID}. I don't find this unit for execute actions");
            }

            // Отбращаемся к классу, который выполняет действия юнитов, и просим 
            // его, выполнять для текущего юнита действие
            _executeAdditionalService.ExecuteAdditional(unit, _targetActorID, _posW, _posH);
        }


        /// <summary>
        /// Распарсить входящие данные
        /// </summary>
        private bool ParceData(List<ISyncComponent> componentsGroup)
        {
            bool isParceAdditional = false;
            bool isParceUnitID = false;
            bool isParceTargetActorID = false;

            foreach (ISyncComponent component in componentsGroup)
            {
                if (component.GetType() == typeof(AdditionalOpComponent))
                {
                    _posW = ((AdditionalOpComponent)component).w;
                    _posH = ((AdditionalOpComponent)component).h;
                    isParceAdditional = true;
                }
                else
                if (component.GetType() == typeof(UnitIdOpComponent))
                {
                    _unitID = ((UnitIdOpComponent)component).uid;
                    _instanceID = ((UnitIdOpComponent)component).i;
                    isParceUnitID = true;
                }
                else
                if (component.GetType() == typeof(TargetActorIdOpComponent))
                {
                    _targetActorID = ((TargetActorIdOpComponent)component).aid;
                    isParceTargetActorID = true;
                }
            }

            if (isParceAdditional && isParceUnitID && isParceTargetActorID)
            {
                return true;
            }

            return false;
        }
    }
}
