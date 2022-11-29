﻿// using Plugin.Interfaces;
// using Plugin.Schemes;
// using System.Collections.Generic;
// 
// namespace Plugin.Runtime.Services
// {
//     public class _SortStepSchemeOpService
//     {
//         /// <summary>
//         /// Перебераем всю кучу из компонентов, и возвращаем только те компоненты, 
//         /// которые подходят по syncStep и stepGroup
//         /// 
//         /// То есть, мы в метод закидываем StepScheme stepScheme (это то, что прислал нам клиент), 
//         /// и указав syncStep и stepGroup, вытаскиваем компоненты, что бы после выполнить 
//         /// действие игрока на стороне сервера
//         /// </summary>
//         public List<ISyncComponent> Sort(StepScheme stepScheme, int syncStep, uint stepGroup)
//         {
//             var groupComponents = new List<ISyncComponent>();
// 
//             if (stepScheme.syncActions != null)
//             {
//                 foreach (ISyncComponent component in stepScheme.syncActions)
//                 {
//                     if (IsCorrectComponent(component, syncStep, stepGroup))
//                     {
//                         groupComponents.Add(component);
//                     }
//                 }
//             }
// 
//             if (stepScheme.syncAdditional != null)
//             {
//                 foreach (ISyncComponent component in stepScheme.syncAdditional)
//                 {
//                     if (IsCorrectComponent(component, syncStep, stepGroup))
//                     {
//                         groupComponents.Add(component);
//                     }
//                 }
//             }
// 
//             if (stepScheme.syncPositionOnGrid != null)
//             {
//                 foreach (ISyncComponent component in stepScheme.syncPositionOnGrid)
//                 {
//                     if (IsCorrectComponent(component, syncStep, stepGroup))
//                     {
//                         groupComponents.Add(component);
//                     }
//                 }
//             }
// 
//             if (stepScheme.syncTargetActorID != null)
//             {
//                 foreach (ISyncComponent component in stepScheme.syncTargetActorID)
//                 {
//                     if (IsCorrectComponent(component, syncStep, stepGroup))
//                     {
//                         groupComponents.Add(component);
//                     }
//                 }
//             }
// 
//             if (stepScheme.syncUnitID != null)
//             {
//                 foreach (ISyncComponent component in stepScheme.syncUnitID)
//                 {
//                     if (IsCorrectComponent(component, syncStep, stepGroup))
//                     {
//                         groupComponents.Add(component);
//                     }
//                 }
//             }
// 
//             if (stepScheme.syncVip != null)
//             {
//                 foreach (ISyncComponent component in stepScheme.syncVip)
//                 {
//                     if (IsCorrectComponent(component, syncStep, stepGroup))
//                     {
//                         groupComponents.Add(component);
//                     }
//                 }
//             }
// 
//             return groupComponents;
//         }
// 
//         /// <summary>
//         /// Проверяем, текущий компонент подходит под выборку?
//         /// </summary>
//         private bool IsCorrectComponent(ISyncComponent component, int syncStep, uint stepGroup)
//         {
//             return (component.SyncStep == syncStep && component.GroupIndex == stepGroup);
//         }
//     }
// }
