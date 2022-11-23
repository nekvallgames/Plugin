using Plugin.Installers;
using Plugin.Interfaces;
using Plugin.Interfaces.UnitComponents;
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

        public bool IsDead(IUnit unit)
        {
            return unit.IsDead;
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
        public void ReviveAction( int actorId )
        {
            List<IUnit> units = _model.Items.FindAll(x => x is IActionComponent);

            foreach (IUnit unit in units){
                ((IActionComponent)unit).ReviveAction();
            }
        }

        public IUnit GetUnit(int actorId, int unitId, int instanceId){
            return _model.Items.Find(x => x.OwnerActorId == actorId && x.UnitId == unitId && x.InstanceId == instanceId);
        }

        public List<IUnit> GetUnits(int actorId)
        {
            return _model.Items.FindAll(x => x.OwnerActorId == actorId);
        }

        public List<IUnit> GetUnits(int actorId, int unitId)
        {
            return _model.Items.FindAll(x => x.OwnerActorId == actorId && x.UnitId == unitId);
        }

        /// <summary>
        /// Нанести урон юниту
        /// </summary>
        public void SetDamage( IUnit unit, int damage )
        {
            bool hasHealth = HasComponent<IHealthComponent>(unit);
            bool hasArmor = HasComponent<IArmorComponent>(unit);

            if ((hasArmor && hasHealth) || hasArmor){
                SetDamageByArmor(unit, damage);
            }
            else
                if (hasHealth){
                    SetDamageByHealth(unit, damage);
                }
        }

        /// <summary>
        /// Вылечить текущего юнита
        /// </summary>
        public void Healing(IUnit unit, int healthPower)
        {
            if (!HasComponent<IHealthComponent>(unit)){
                throw new ArgumentException($"UnitService :: Healing() I can't healing unit, because this unit don't have IHealthComponent. ActorId = {unit.OwnerActorId}, unitId = {unit.UnitId}, instanceId = {unit.InstanceId}");
            }

            var healthComponent = (IHealthComponent)unit;

            int health = healthComponent.Capacity + healthPower;

            if (health > healthComponent.CapacityMax){
                health = healthComponent.CapacityMax;
            }

            ((IHealthComponent)unit).Capacity = health;
        }

        /// <summary>
        /// Ячейка posW, posH находится под Area юнита
        /// </summary>
        private bool IsPositionUnderUnitArea(IUnit unit, int posW, int posH)
        {
            // Позиция юнита на игровой сетке.  
            Int2 unitPos = unit.Position;

            // Ширина и высота юнита, которую он занимает на игровой сетке
            var bodySize = new Int2(unit.BodySize.x - 1 < 0 ? 0 : unit.BodySize.x - 1,
                                    unit.BodySize.y - 1 < 0 ? 0 : unit.BodySize.y - 1);

            if ((posW >= unitPos.x) && (posW <= (unitPos.x + bodySize.x))       // проверяем по ширине
                && (posH >= unitPos.y) && (posH <= (unitPos.y + bodySize.y)))   // проверяем по высоте
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
            return _model.Items.Any(x => x.OwnerActorId == actorId && !x.IsDead);
        }

        /// <summary>
        /// Перебрать всех юнитов, и вернуть истину, если есть мертвые юниты
        /// </summary>
        public bool HasAnyDeadUnit(int actorId)
        {
            return _model.Items.Any(x => x.OwnerActorId == actorId && x.IsDead);
        }

        /// <summary>
        /// В поточного юніта є вказаний компонент?
        /// </summary>
        public bool HasComponent<T>(IUnit unit)
        {
            return typeof(T).IsAssignableFrom(unit.GetType());
        }

        /// <summary>
        /// Нанести урон по жизням
        /// </summary>
        private void SetDamageByHealth(IUnit unit, int damage)
        {
            int curr = ((IHealthComponent)unit).Capacity - damage;
            if (curr < 0) curr = 0;
            
            ((IHealthComponent)unit).Capacity = curr;
        }

        /// <summary>
        /// Нанести урон по броні
        /// </summary>
        private void SetDamageByArmor(IUnit unit, int damage)
        {
            int curr = ((IArmorComponent)unit).Capacity - damage;
            if (curr < 0) curr = 0;

            ((IArmorComponent)unit).Capacity = curr;
        }
    }

}
