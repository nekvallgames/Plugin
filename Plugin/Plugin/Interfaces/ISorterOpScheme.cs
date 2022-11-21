using Plugin.Schemes;
using System.Collections.Generic;

namespace Plugin.Interfaces
{
    public interface ISorterOpScheme
    {
        /// <summary>
        /// Может ли сортировщик сортировать текущею схему?
        /// </summary>
        bool CanSort(OpScheme scheme);

        /// <summary>
        /// Сортировать текущею схему
        /// </summary>
        List<ISyncComponent> Sort(OpScheme scheme, int stepHistory, uint stepGroup);
    }
}
