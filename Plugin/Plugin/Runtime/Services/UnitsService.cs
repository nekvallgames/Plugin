using Plugin.Interfaces;
using Plugin.Models.Private;
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

        public UnitsService(UnitsPrivateModel<IUnit> model)
        {
            _model = model;
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
                if (unit.OwnerActorID != unitOwnerID){
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
            List<IUnit> units = _model.Items.FindAll(x => x.OwnerActorID == actorId);

            foreach (IUnit unit in units){
                unit.ReviveAction();
            }
        }

        public IUnit GetUnit(int playerActorId, int unitId, int instanceId)
        {
            return _model.Items.Find(x => x.OwnerActorID == playerActorId && x.UnitID == unitId && x.InstanceID == instanceId);
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

            if (health > unitTargets.GetMaxHealth)
            {
                health = unitTargets.GetMaxHealth;
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
            return _model.Items.FindAll(x => x.OwnerActorID == actorId).Any(x => x.Health > 0);
        }

        /// <summary>
        /// Перебрать всех юнитов, и вернуть истину, если есть мертвые юниты
        /// </summary>
        public bool HasAnyDeadUnit(int actorId)
        {
            return _model.Items.FindAll(x => x.OwnerActorID == actorId).Any(x => x.Health <= 0);
        }
    }

}
