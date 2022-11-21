﻿namespace Plugin.Interfaces
{
    public interface ISyncComponent
    {
        /// <summary>
        /// Получить шаг истории, к которой принадлежит компонент
        /// </summary>
        int HistoryStep { get; set; }

        /// <summary>
        /// Получить группу, в которой текущий компонент принадлежит
        /// </summary>
        int GroupIndex { get; set; }
    }
}
