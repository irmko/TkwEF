using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace TkwEF.Core.BLL
{
    /// <summary>
    /// Представляет слабую ссылку, которая ссылается на объект, все еще ​​позволяя
    /// что объект будет утилизирован в процессе сборки мусора.
    /// </summary>
    /// <typeparam name="T">тип объекта, на который ссылается.</typeparam>
    [Serializable]
    public class WeakReferenceBase<T> : WeakReference where T : class
    {
        /// <summary>
        /// Инициализация нового экземпляра WeakReference {T} класса, ссылающийся
        /// указанного объекта
        /// </summary>
        /// <param name="target">The объект ссылки.</param>
        public WeakReferenceBase(T target)
            : base(target)
        { }
        /// <summary>
        /// Инициализация нового экземпляра WeakReference {T} класса, ссылающийся
        /// указанного объекта и, используя указанный отслеживания воскресение.
        /// </summary>
        /// <param name="target">объект для отслеживания.</param>
        /// <param name="trackResurrection">Указывает на то, чтобы остановить отслеживание объекта. 
        /// Если это правда, объект отслеживается после завершения,
        /// если ложно, объект отслеживается только до завершения</param>
        public WeakReferenceBase(T target, bool trackResurrection)
            : base(target, trackResurrection)
        { }
        protected WeakReferenceBase(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
        /// <summary>
        /// Получает или задает объект (мишень), на который ссылается
        /// текущая WeakReference {T} объекта.
        /// </summary>
        public new T Target
        {
            get
            {
                return (T)base.Target;
            }
            set
            {
                base.Target = value;
            }
        }
    }
}
