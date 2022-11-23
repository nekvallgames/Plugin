using Plugin.Installers;
using Plugin.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Plugin.Runtime.Services
{
    /// <summary>
    /// Сервіс, котрий створює інстанси для юнітів
    /// </summary>
    public class UnitInstanceService
    {
        private UnitsService _unitsService;

        public UnitInstanceService()
        {
            var gameInstaller = GameInstaller.GetInstance();

            _unitsService = gameInstaller.unitsService;
        }

        /// <summary>
        /// Отримати для вказаного юніта інстанс
        /// actorId - власник юніта
        /// unitId - ид юніта, котрого буде створено
        /// </summary>
        public int GetInstance(int actorId, int unitId)
        {
            List<IUnit> list = _unitsService.GetUnits(actorId, unitId);
            if (!list.Any())
                return 0;

            int maxInstance = list.Max(x => x.InstanceId);
            maxInstance++;

            return maxInstance;
        }
    }
}
