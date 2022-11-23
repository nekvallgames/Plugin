using Plugin.Installers;
using Plugin.Interfaces;
using Plugin.Interfaces.Units;
using Plugin.Models.Private;
using Plugin.Schemes;
using Plugin.Signals;
using Plugin.Tools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Plugin.Runtime.Services
{
    /// <summary>
    /// Контроллер, который выполняет всякую логику, котора нужна для юнитов
    /// </summary>
    public class UnitsService
    {
        private UnitsPrivateModel<IUnit> _model;
        private OpStockService _opStockService;
        private ConvertService _convertService;

        public UnitsService(UnitsPrivateModel<IUnit> model)
        {
            _model = model;

            var gameInstaller = GameInstaller.GetInstance();
            _opStockService = gameInstaller.opStockService;
            _convertService = gameInstaller.convertService;

            gameInstaller.signalBus.Subscrible<OpStockPrivateModelSignal>( OpStockModelChange );
        }

        /// <summary>
        /// Модель із операціями акторів була оновлена
        /// </summary>
        private void OpStockModelChange(OpStockPrivateModelSignal signalData)
        {
            // Якщо це операція choosedUnitsForGame, це означає, що потрібно для гравця створити юнітів
            if (signalData.OpCode == OperationCode.choosedUnitsForGame)
            {
                // Забрати зі складу операцію, в якій знаходяться дані із юнітами, котрих обрав актор
                var opChoosedUnits = _opStockService.TakeOp(signalData.ActorId, signalData.OpCode);

                var choosedUnitsScheme = _convertService.DeserializeObject<ChoosedUnitsScheme>(opChoosedUnits.Data.ToString());

                foreach( int unitId in choosedUnitsScheme.unitsId )
                {

                }
            }
        }

        public bool IsAlive(IUnit unit)
        {
            // TODO добавити перевірку на броню. Юніт може не мати житів, але має тільки броню. Наприклад робот

            return ((IHealth)unit).Capacity > 0;
        }

        /// <summary>
        /// Получить юнитов, которые находятся в позиции posW, posH на игровой сетке
        /// unitOwnerID - указать владельца юнита, выстрел по которому будем проверять, попали мы или нет
        /// posW - позиция на игровой сетке по ширине
        /// posH - позиция на игровой сетке по ширине
        /// </summary>
        public List<IUnit> GetUnitsUnderThisPosition( int unitOwnerID, int posW, int posH )
        {
            List<IUnit> unitsUnderPos = new List<IUnit>();

            foreach (var unit in _model.Items)
            {
                // 1. Проверяем, юнит принадлежит игроку unitOwnerID
                if (unit.OwnerActorId != unitOwnerID){
                    continue;
                }

                // 2. Проверяем, попали мы по этому юниту
                if (IsPositionUnderUnitArea(unit, posW, posH)){
                    unitsUnderPos.Add(unit);
                }
            }

            return unitsUnderPos;
        }


        /// <summary>
        /// Восстановить все действия юнитов (перезарядить оружия, восстановить магию и т.д.)
        /// </summary>
        public void ReviveAmmunitionForAllUnits(int actorId)
        {
            List<IUnit> units = _model.Items.FindAll(x => x.OwnerActorId == actorId);

            foreach (IUnit unit in units){
                unit.ReviveAction();
            }
        }

        public IUnit GetUnit(int actorId, int unitId, int instanceId)
        {
            return _model.Items.Find(x => x.OwnerActorId == actorId && x.UnitId == unitId && x.InstanceId == instanceId);
        }

        public List<IUnit> GetUnits(int actorId)
        {
            return _model.Items.FindAll(x => x.OwnerActorId == actorId);
        }

        /// <summary>
        /// Восстановить действие юнита
        /// </summary>
        public void ReviveAction(IUnit unit)
        {
            unit.ReviveAction();
        }

        /// <summary>
        /// Нанести урон юниту
        /// </summary>
        public void SetDamage(IUnit unit, int damage)
        {
            int health = unit.Health;
            int armor = unit.Armor;

            // Если есть броня, то нанести урон по брони
            if (armor > 0)
            {
                armor -= damage;
                if (armor < 0)
                {
                    armor = 0;
                }

                unit.Armor = armor;
                return;
            }


            // Нанести урон по жизням
            health -= damage;
            if (health < 0)
            {
                health = 0;
            }

            unit.Health = health;
        }

        /// <summary>
        /// Вылечить текущего юнита
        /// </summary>
        public void Healing(IUnit unitTargets, int healthPower)
        {
            int health = unitTargets.Health;

            health += healthPower;

            if (health > unitTargets.MaxHealth)
            {
                health = unitTargets.MaxHealth;
            }
        }

        /// <summary>
        /// Ячейка posW, posH находится под Area юнита
        /// </summary>
        private bool IsPositionUnderUnitArea(IUnit unit, int posW, int posH)
        {
            // 1. Позиция юнита на игровой сетке.  
            int unitPosW = unit.PositionOnGridW;
            int unitPosH = unit.PositionOnGridH;

            // 2. Ширина и высота юнита, которую он занимает на игровой сетке
            int areaW = (unit.AreaWidth - 1);
            int areaH = (unit.AreaHeight - 1);

            if (areaW < 0) areaW = 0;
            if (areaH < 0) areaH = 0;

            if ((posW >= unitPosW) && (posW <= (unitPosW + areaW))       // проверяем по ширине
                && (posH >= unitPosH) && (posH <= (unitPosH + areaH)))   // проверяем по высоте
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Игрок имеет живых юнитов?
        /// </summary>
        public bool HasAliveUnit(int actorId)
        {
            return _model.Items.FindAll(x => x.OwnerActorId == actorId).Any(x => x.Health > 0);
        }

        /// <summary>
        /// Перебрать всех юнитов, и вернуть истину, если есть мертвые юниты
        /// </summary>
        public bool HasAnyDeadUnit(int actorId)
        {
            return _model.Items.FindAll(x => x.OwnerActorId == actorId).Any(x => x.Health <= 0);
        }
    }

}
