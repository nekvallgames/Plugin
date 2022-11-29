using Plugin.Installers;
using Plugin.Interfaces;
using Plugin.OpComponents;
using Plugin.Runtime.Services.ExecuteAction;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Plugin.Runtime.Services.ExecuteOp.Executors
{
    /// <summary>
    /// Выполнить действие игрока - активировать/деактивировать VIP для юнита
    /// </summary>
    public class ExecuteOpGroupVip : IExecuteOpGroup
    {
        private UnitsService _unitsService;
        private VipService _vipService;

        // Данные, которые нужны для восзоздания действия игрока
        private int _unitId;
        private int _instanceId;
        private bool _enable;

        public ExecuteOpGroupVip(UnitsService unitsService, VipService vipService)
        {
            _unitsService = unitsService;
            _vipService = vipService;
        }

        /// <summary>
        /// Может ли текущий класс выполнить действие игрока?
        /// </summary>
        public bool CanExecute(List<ISyncComponent> componentsGroup)
        {
            foreach (ISyncComponent component in componentsGroup){
                if (component.GetType() == typeof(VipOpComponent)){
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Выполнить действие игрока. Активировать/деактивировать VIP для юнита
        /// </summary>
        public void Execute(int actorId, List<ISyncComponent> componentsGroup)
        {
            // Вытаскиваем нужные нам компоненты из списка
            if (!ParceData(componentsGroup)){
                Debug.Fail($"ExecuteOpService :: ExecuteVip() actorID = {actorId}, I can't parce data");
                return;
            }

            // 2. Найти юнита, который выполнил действие
            IUnit unit = _unitsService.GetUnit(actorId, _unitId, _instanceId);

            if (unit == null){
                Debug.Fail($"ExecuteOpService :: ExecuteVip() actorID = {actorId}, unitId = {_unitId}, instanceId = {_instanceId}. I don't find this unit for execute vip");
                return;
            }

            // Обращаемся к классу, который активировать/деактивировать VIP для юнита, и просим 
            // его, выполнять для текущего юнита действие
            _vipService.ChangeVip(unit, _enable);
        }

        /// <summary>
        /// Распарсить входящие данные
        /// </summary>
        private bool ParceData(List<ISyncComponent> componentsGroup)
        {
            bool isParceVip = false;
            bool isParceUnitID = false;

            foreach (ISyncComponent component in componentsGroup)
            {
                if (component.GetType() == typeof(VipOpComponent))
                {
                    _enable = ((VipOpComponent)component).e;
                    isParceVip = true;
                }
                else
                    if (component.GetType() == typeof(UnitIdOpComponent))
                    {
                        _unitId = ((UnitIdOpComponent)component).uid;
                        _instanceId = ((UnitIdOpComponent)component).i;
                        isParceUnitID = true;
                    }
            }

            if (isParceVip && isParceUnitID)
            {
                return true;
            }

            return false;
        }
    }
}
