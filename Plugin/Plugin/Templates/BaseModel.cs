using Plugin.Interfaces;
using System.Collections.Generic;

namespace Plugin.Templates
{
    public abstract class BaseModel<T> : IModel<T>
    {
        private List<T> _list = new List<T>();

        public void Add(T item)
        {
            if (!CanAddHook(item))
                return;

            _list.Add(item);

            AfterAddHook(item);
        }

        protected virtual bool CanAddHook(T item) { return true; }
        protected virtual void AfterAddHook(T item) { }
        public List<T> Items => _list;
        
    }
}
