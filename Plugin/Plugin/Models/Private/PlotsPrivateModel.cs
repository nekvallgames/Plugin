using Plugin.Interfaces;
using Plugin.Templates;

namespace Plugin.Models.Private
{
    /// <summary>
    /// Приватна модель даних, котра зберігає в схеми, в котрих будемо зберігати данні ігрового сюжету
    /// </summary>
    public class PlotsPrivateModel<T> : BaseModel<T>, IPrivateModel where T : IPlotModelScheme
    {
        
    }
}
