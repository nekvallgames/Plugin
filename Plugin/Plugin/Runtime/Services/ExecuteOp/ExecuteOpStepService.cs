﻿using Plugin.Interfaces;
using Plugin.Schemes;
using System.Collections.Generic;

namespace Plugin.Runtime.Services.ExecuteOp
{
    /// <summary>
    /// Сервіс, котрий виконає всі дії, котрі актор прислав в операції ActorStep
    /// </summary>
    public class ExecuteOpStepService
    {
        private SortOpStepService _sortOpStepService;
        private ExecuteOpGroupService _executeOpGroupService;

        public ExecuteOpStepService( SortOpStepService sortOpStepService, ExecuteOpGroupService executeOpGroupService )
        {
            _sortOpStepService = sortOpStepService;
            _executeOpGroupService = executeOpGroupService;
        }

        public void Execute(int actorId, int syncStep, StepScheme stepScheme)
        {
            uint componentsGroup = 0;

            while (true)
            {
                // 1. Вытаскиваем из кучи компонентов только ту группу, которая нам нужна, а именно: stepHistory и componentsGroup
                List<ISyncComponent> componentGroup = _sortOpStepService.Sort(stepScheme, syncStep, componentsGroup);

                if (componentGroup == null || componentGroup.Count <= 0)
                {
                    // если список пуст, значит сортировщик не нашел групп из компонентов
                    // возможно, мы перебрали все компоненты в схеме
                    break;
                }

                // 2. Отправить группу из компонентов действий игрока на выполнение
                _executeOpGroupService.Execute(actorId, componentGroup);

                // 3. Увеличиваем шаг и обращаемся к следующей группе
                componentsGroup++;
            }
        }
    }
}