using Plugin.Interfaces;
using System.Collections.Generic;

namespace Plugin.Templates
{
    public abstract class BaseModel<T> : IModel<T>
    {
        private List<T> _list = new List<T>();

        /// <summary>
        /// Добавити елемент до списку
        /// </summary>
        public void Add(T item)
        {
            if (!CanAddHook(item))
                return;

            _list.Add(item);

            AfterAddHook(item);
        }

        /// <summary>
        /// Заоверайдити метот, що би реалізувати перевірку, 
        /// чи можно/не можно добавити поточний єлемент до списку
        /// </summary>
        protected virtual bool CanAddHook(T item) { return true; }
        /// <summary>
        /// Виконається після добавлення поточного єлементу до списку
        /// </summary>
        protected virtual void AfterAddHook(T item) { }
        public List<T> Items => _list;
        
    }
}
