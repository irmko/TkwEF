using System.ComponentModel;
using System.Threading;
using System.Web;
using System.Web.Caching;
using System.Collections.Generic;
using System.Collections;
using System;

namespace TkwEF.Core.BLL
{
    #region MyCache
    //public sealed class Cache : IEnumerable
    //{

    //    #region Fields

    //    // Сводка:
    //    //     Используется в параметре absoluteExpiration при вызове метода System.Web.Caching.Cache.Insert(System.String,System.Object),
    //    //     чтобы указать, что срок действия этого элемента никогда не истечет.Это поле
    //    //     доступно только для чтения.
    //    //public static readonly DateTime NoAbsoluteExpiration;
    //    //
    //    // Сводка:
    //    //     Используется в качестве параметра slidingExpiration в вызове метода System.Web.Caching.Cache.Insert(System.String,System.Object)
    //    //     или System.Web.Caching.Cache.Add(System.String,System.Object,System.Web.Caching.CacheDependency,System.DateTime,System.TimeSpan,System.Web.Caching.CacheItemPriority,System.Web.Caching.CacheItemRemovedCallback)
    //    //     для отключения скользящих сроков действия.Это поле доступно только для чтения.
    //    //public static readonly TimeSpan NoSlidingExpiration;

    //    private Dictionary<string, object> _cache;

    //    #endregion

    //    // Сводка:
    //    //     Инициализирует новый экземпляр класса System.Web.Caching.Cache.
    //    [TargetedPatchingOptOut("Performance critical to inline this Type of method across NGen image boundaries")]
    //    public Cache()
    //    {
    //        _cache = new Dictionary<string, object>();
    //    }

    //    // Сводка:
    //    //     Получение числа элементов, сохраненных в кэше.
    //    //
    //    // Возвращает:
    //    //     Число элементов, сохраненных в кэше.
    //    public int Count { get { return _cache.Count; } }

    //    // Сводка:
    //    //     Возвращает или задает элемент кэша при указанном ключе.
    //    //
    //    // Параметры:
    //    //   key:
    //    //     Объект System.String, который представляет ключ для элемента кэша.
    //    //
    //    // Возвращает:
    //    //     Указанный элемент кэша.
    //    public object this[string key]
    //    {
    //        get
    //        {
    //            if (!_cache.ContainsKey(key))
    //                this.Insert(key, null);
    //            return _cache[key];
    //        }
    //        set
    //        {
    //            if (_cache.ContainsKey(key))
    //                this.Remove(key);
    //            this.Insert(key, value);
    //        }
    //    }

    //    // Сводка:
    //    //     Добавление указанного элемента в объект System.Web.Caching.Cache с зависимостями,
    //    //     политиками сроков действия и приоритетов, а также с делегатом, которого можно
    //    //     использовать для уведомления приложения при удалении вставленного элемента
    //    //     из Cache.
    //    //
    //    // Параметры:
    //    //   key:
    //    //     Ключ кэша, используемый для ссылки на элемент.
    //    //
    //    //   value:
    //    //     Элемент для добавления в кэш.
    //    //
    //    //   dependencies:
    //    //     Зависимости файла или ключа кэша для элемента.Если какая-либо зависимость
    //    //     меняется, объект становится недопустимым и удаляется из кэша.Если зависимости
    //    //     отсутствуют, данный параметр имеет значение null.
    //    //
    //    //   absoluteExpiration:
    //    //     Время истечения срока действия добавленного объекта и его удаления из кэша.Если
    //    //     используется скользящий срок действия, параметр absoluteExpiration должен
    //    //     быть System.Web.Caching.Cache.NoAbsoluteExpiration.
    //    //
    //    //   slidingExpiration:
    //    //     Интервал между временем последнего обращения к добавленному объекту и временем
    //    //     истечения срока действия этого объекта.Если это значение равно 20 минутам,
    //    //     срок действия объекта истечет, и он будет удален из кэша через 20 минут после
    //    //     последнего обращения к этому объекту.Если используется абсолютный срок действия,
    //    //     параметр slidingExpiration должен быть System.Web.Caching.Cache.NoSlidingExpiration.
    //    //
    //    //   priority:
    //    //     Относительная цена объекта, выраженная перечислением System.Web.Caching.CacheItemPriority.Это
    //    //     значение используется в кэше при исключении объектов. Объекты с более низкой
    //    //     ценой удаляются из кэша раньше, чем объекты с более высокой ценой.
    //    //
    //    //   onRemoveCallback:
    //    //     При наличии делегата он вызывается в случае удаления объекта из кэша.Его
    //    //     можно использовать для уведомления приложений при удалении объектов из кэша.
    //    //
    //    // Возвращает:
    //    //     Объект, представляющий добавленный элемент, если элемент был сохранен в кэше
    //    //     ранее; в противном случае — значение null.
    //    //
    //    // Исключения:
    //    //   System.ArgumentNullException:
    //    //     Для параметра key или value указано значение null.
    //    //
    //    //   System.ArgumentOutOfRangeException:
    //    //     Для параметра slidingExpiration указано значение меньше TimeSpan.Zero или
    //    //     больше одного года.
    //    //
    //    //   System.ArgumentException:
    //    //     Оба параметра — absoluteExpiration и slidingExpiration — заданы для элемента,
    //    //     выбранного для добавления в Cache.
    //    //public object Add(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemPriority priority, CacheItemRemovedCallback onRemoveCallback);
    //    //
    //    // Сводка:
    //    //     Получение указанного элемента из объекта System.Web.Caching.Cache.
    //    //
    //    // Параметры:
    //    //   key:
    //    //     Идентификатор для элемента кэша, который необходимо извлечь.
    //    //
    //    // Возвращает:
    //    //     Полученный элемент кэша или null, если ключ не найден.
    //    public object Get(string key)
    //    {
    //        return this[key];
    //    }
    //    //
    //    // Сводка:
    //    //     Получение перечислителя словаря, используемого для итерации в ключевых параметрах
    //    //     и их значениях, содержащихся в кэше.
    //    //
    //    // Возвращает:
    //    //     Перечислитель для итерации в объекте System.Web.Caching.Cache.
    //    public IDictionaryEnumerator GetEnumerator()
    //    {
    //        return _cache.GetEnumerator();
    //    }
    //    //
    //    // Сводка:
    //    //     Вставка элемента в объект System.Web.Caching.Cache с ключом кэша для ссылки
    //    //     на его расположение с помощью значений по умолчанию, предоставленных перечислением
    //    //     System.Web.Caching.CacheItemPriority.
    //    //
    //    // Параметры:
    //    //   key:
    //    //     Ключ кэша, используемый для ссылки на элемент.
    //    //
    //    //   value:
    //    //     Объект для вставки в кэш.
    //    //
    //    // Исключения:
    //    //   System.ArgumentNullException:
    //    //     Параметр key или value имеет значение null.
    //    public void Insert(string key, object value)
    //    {
    //        if (_cache.ContainsKey(key))
    //            this.Remove(key);
    //        _cache.Add(key, value);
    //    }
    //    //
    //    // Сводка:
    //    //     Вставка объекта в System.Web.Caching.Cache, имеющий зависимости файла или
    //    //     ключа.
    //    //
    //    // Параметры:
    //    //   key:
    //    //     Ключ кэша, используемый для определения элемента.
    //    //
    //    //   value:
    //    //     Объект для вставки в кэш.
    //    //
    //    //   dependencies:
    //    //     Зависимости файла или ключа кэша для вставленного объекта.Если какая-либо
    //    //     зависимость меняется, объект становится недопустимым и удаляется из кэша.Если
    //    //     зависимости отсутствуют, данный параметр имеет значение null.
    //    //
    //    // Исключения:
    //    //   System.ArgumentNullException:
    //    //     Параметр key или value имеет значение null.
    //    //public void Insert(string key, object value, CacheDependency dependencies);
    //    //
    //    // Сводка:
    //    //     Вставка объекта в System.Web.Caching.Cache с зависимостями и политиками сроков
    //    //     действия.
    //    //
    //    // Параметры:
    //    //   key:
    //    //     Ключ кэша, используемый для ссылки на объект.
    //    //
    //    //   value:
    //    //     Объект для вставки в кэш.
    //    //
    //    //   dependencies:
    //    //     Зависимости файла или ключа кэша для вставленного объекта.Если какая-либо
    //    //     зависимость меняется, объект становится недопустимым и удаляется из кэша.Если
    //    //     зависимости отсутствуют, данный параметр имеет значение null.
    //    //
    //    //   absoluteExpiration:
    //    //     Время истечения срока действия вставленного объекта и его удаления из кэша.Чтобы
    //    //     избежать возможных проблем с местным временем, например перехода от стандартного
    //    //     времени к летнему, используйте для этого параметра значение System.DateTime.UtcNow,
    //    //     а не System.DateTime.Now.Если используется абсолютный срок действия, параметр
    //    //     slidingExpiration должен иметь значение System.Web.Caching.Cache.NoSlidingExpiration.
    //    //
    //    //   slidingExpiration:
    //    //     Интервал между временем последнего обращения к вставленному объекту и временем
    //    //     истечения срока действия этого объекта.Если это значение равно 20 минутам,
    //    //     срок действия объекта истечет, и он будет удален из кэша через 20 минут после
    //    //     последнего обращения к этому объекту.Если используется скользящий срок действия,
    //    //     параметр absoluteExpiration должен быть System.Web.Caching.Cache.NoAbsoluteExpiration.
    //    //
    //    // Исключения:
    //    //   System.ArgumentNullException:
    //    //     Параметр key или value имеет значение null.
    //    //
    //    //   System.ArgumentOutOfRangeException:
    //    //     Для параметра slidingExpiration указывается значение меньше TimeSpan.Zero
    //    //     или больше одного года.
    //    //
    //    //   System.ArgumentException:
    //    //     Оба параметра — absoluteExpiration и slidingExpiration — заданы для элемента,
    //    //     выбранного для добавления в Cache.
    //    //public void Insert(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration);
    //    //
    //    // Сводка:
    //    //     Вставляет в объект System.Web.Caching.Cache объект с зависимостями, политиками
    //    //     сроков действия и делегатом, который можно использовать для уведомления приложения
    //    //     перед удалением элемента из кэша.
    //    //
    //    // Параметры:
    //    //   key:
    //    //     Ключ кэша, используемый для ссылки на объект.
    //    //
    //    //   value:
    //    //     Объект, вставляемый в кэш.
    //    //
    //    //   dependencies:
    //    //     Зависимости файла или ключа кэша для элемента.Если какая-либо зависимость
    //    //     меняется, объект становится недопустимым и удаляется из кэша.Если зависимости
    //    //     отсутствуют, данный параметр имеет значение null.
    //    //
    //    //   absoluteExpiration:
    //    //     Время истечения срока действия вставленного объекта и его удаления из кэша.Чтобы
    //    //     избежать возможных проблем с местным временем, например перехода от стандартного
    //    //     времени к летнему, используйте для этого параметра значение System.DateTime.UtcNow
    //    //     вместо System.DateTime.Now.Если используется абсолютный срок действия, параметру
    //    //     slidingExpiration следует присвоить значение System.Web.Caching.Cache.NoSlidingExpiration.
    //    //
    //    //   slidingExpiration:
    //    //     Интервал между временем последнего обращения к кэшируемому объекту и временем
    //    //     истечения срока действия этого объекта.Если это значение равно 20 минутам,
    //    //     срок действия объекта истечет, и он будет удален из кэша через 20 минут после
    //    //     последнего обращения к этому объекту.Если используется скользящий срок действия,
    //    //     параметру absoluteExpiration следует присвоить значение System.Web.Caching.Cache.NoAbsoluteExpiration.
    //    //
    //    //   onUpdateCallback:
    //    //     Делегат, который будет вызываться перед удалением объекта из кэша.Можно использовать
    //    //     этот вызов для обновления кэшируемого элемента, с тем чтобы он не был удален
    //    //     из кэша.
    //    //
    //    // Исключения:
    //    //   System.ArgumentNullException:
    //    //     Значение параметра key, value или onUpdateCallback равно null.
    //    //
    //    //   System.ArgumentOutOfRangeException:
    //    //     Значение параметра slidingExpiration меньше TimeSpan.Zero или эквивалентно
    //    //     периоду более одного года.
    //    //
    //    //   System.ArgumentException:
    //    //     Оба параметра — absoluteExpiration и slidingExpiration — заданы для элемента,
    //    //     который вы пытаетесь добавить в Cache.–либо–Параметр dependencies имеет значение
    //    //     null, параметру absoluteExpiration присвоено значение System.Web.Caching.Cache.NoAbsoluteExpiration,
    //    //     а параметру slidingExpiration — System.Web.Caching.Cache.NoSlidingExpiration.
    //    //public void Insert(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemUpdateCallback onUpdateCallback);
    //    //
    //    // Сводка:
    //    //     Вставка объекта в объект System.Web.Caching.Cache с зависимостями, политиками
    //    //     сроков действия и приоритетов, а также с делегатом, которого можно использовать
    //    //     для уведомления приложения при удалении вставленного элемента из Cache.
    //    //
    //    // Параметры:
    //    //   key:
    //    //     Ключ кэша, используемый для ссылки на объект.
    //    //
    //    //   value:
    //    //     Объект для вставки в кэш.
    //    //
    //    //   dependencies:
    //    //     Зависимости файла или ключа кэша для элемента.Если какая-либо зависимость
    //    //     меняется, объект становится недопустимым и удаляется из кэша.Если зависимости
    //    //     отсутствуют, данный параметр имеет значение null.
    //    //
    //    //   absoluteExpiration:
    //    //     Время истечения срока действия вставленного объекта и его удаления из кэша.Чтобы
    //    //     избежать возможных проблем с местным временем, например перехода от стандартного
    //    //     времени к летнему, используйте для этого параметра значение System.DateTime.UtcNow,
    //    //     а не System.DateTime.Now.Если используется абсолютный срок действия, параметр
    //    //     slidingExpiration должен иметь значение System.Web.Caching.Cache.NoSlidingExpiration.
    //    //
    //    //   slidingExpiration:
    //    //     Интервал между временем последнего обращения к вставленному объекту и временем
    //    //     истечения срока действия этого объекта.Если это значение равно 20 минутам,
    //    //     срок действия объекта истечет, и он будет удален из кэша через 20 минут после
    //    //     последнего обращения к этому объекту.Если используется скользящий срок действия,
    //    //     параметр absoluteExpiration должен быть System.Web.Caching.Cache.NoAbsoluteExpiration.
    //    //
    //    //   priority:
    //    //     Цена объекта относительно других элементов, сохраненных в кэше, выраженная
    //    //     перечислением System.Web.Caching.CacheItemPriority.Это значение используется
    //    //     в кэше при исключении объектов. Объекты с более низкой ценой удаляются из
    //    //     кэша раньше, чем объекты с более высокой ценой.
    //    //
    //    //   onRemoveCallback:
    //    //     Делегат, который, будучи предоставленным, будет вызываться при удалении объекта
    //    //     из кэша.Его можно использовать для уведомления приложений при удалении объектов
    //    //     из кэша.
    //    //
    //    // Исключения:
    //    //   System.ArgumentNullException:
    //    //     Параметр key или value имеет значение null.
    //    //
    //    //   System.ArgumentOutOfRangeException:
    //    //     Для параметра slidingExpiration указывается значение меньше TimeSpan.Zero
    //    //     или больше одного года.
    //    //
    //    //   System.ArgumentException:
    //    //     Оба параметра — absoluteExpiration и slidingExpiration — заданы для элемента,
    //    //     выбранного для добавления в Cache.
    //    //public void Insert(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemPriority priority, CacheItemRemovedCallback onRemoveCallback);
    //    //
    //    // Сводка:
    //    //     Удаление указанного элемента из объекта System.Web.Caching.Cache приложения.
    //    //
    //    // Параметры:
    //    //   key:
    //    //     Идентификатор System.String для элемента кэша, который будет удален.
    //    //
    //    // Возвращает:
    //    //     Элемент, удаленный из Cache.Если значение в ключевом параметре не найдено,
    //    //     возвращает null.
    //    public object Remove(string key)
    //    {
    //        object result = null;
    //        if (_cache.ContainsKey(key))
    //            result = _cache[key];
    //        _cache.Remove(key);
    //        return result;
    //    }

    //    IEnumerator IEnumerable.GetEnumerator()
    //    {
    //        return _cache.GetEnumerator();
    //    }
    //} 
    #endregion

    public abstract partial class BIZ_Base
    {
        private static HttpRuntime _httpRuntime;

        //20170527 нужна статика (((
        //protected virtual string cachePrefix { get { return this.GetType().ToString(); } }

        /// <summary>
        /// Разрешено ли использовать кэш
        /// </summary>
        public static bool AllowCache
        {
            get { return false; }
        }
        /// <summary>
        /// Возвращает время хранения данных в кеше в секундах
        /// </summary>
        public static TimeSpan SlidingExpirationDefault
        {
            get { return TimeSpan.MinValue; }
        }

        protected static void EnsureHttpRuntime()
        {
            if (null == _httpRuntime)
            {
                try
                {
                    Monitor.Enter(_lockStatic);
                    if (null == _httpRuntime)
                    {
                        // Create an Http Content to give us access to the cache.
                        _httpRuntime = new HttpRuntime();
                    }
                }
                finally
                {
                    Monitor.Exit(_lockStatic);
                }
            }
        }
        
        #region Cache

        public static Cache Cache
        {
            get
            {
                EnsureHttpRuntime();
                return HttpRuntime.Cache;
            }
        }
        /// <summary>
        /// Очистка кеша с указанным префиксом
        /// </summary>
        /// <param name="prefix"></param>
        protected static void PurgeCacheItems(string prefix)
        {
            prefix = prefix.ToLower();
            List<string> ItemsToRemove = new List<string>();

            IDictionaryEnumerator enumerator = Cache.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Key.ToString().ToLower().StartsWith(prefix))
                    ItemsToRemove.Add(enumerator.Key.ToString());
            }
            foreach (string itemToRemove in ItemsToRemove)
                Cache.Remove(itemToRemove);
        }
        ///// <summary>
        ///// Вставка объекта в System.Web.Caching.Cache с зависимостями и политиками сроков действия
        ///// </summary>
        ///// <param name="cache"></param>
        ///// <param name="key">Ключ кэша, используемый для ссылки на объект.</param>
        ///// <param name="value">Объект для вставки в кэш.</param>
        ///// <param name="slidingExpiration">Интервал между временем последнего обращения к вставленному объекту и временем
        /////     истечения срока действия этого объекта.Если это значение равно 20 минутам,
        /////     срок действия объекта истечет, и он будет удален из кэша через 20 минут после
        /////     последнего обращения к этому объекту.Если используется скользящий срок действия,
        /////     параметр absoluteExpiration должен быть System.Web.Caching.Cache.NoAbsoluteExpiration.</param>
        //protected static void Insert(this Cache cache, string key, object value, TimeSpan slidingExpiration)
        //{
        //    cache.Insert(key, value, null, System.Web.Caching.Cache.NoAbsoluteExpiration, slidingExpiration);
        //}

        /// <summary>
        /// Cache the input data, if caching is enabled
        /// </summary>
        public static void CacheData(string key, object data, TimeSpan slidingExpiration)
        {
            if (AllowCache && data != null)
            {
                //Cache.Insert(key, data, null, DateTime.Now.AddSeconds(Globals.. CacheDuration), TimeSpan.Zero);
                Cache.Insert(key, data, null, Cache.NoAbsoluteExpiration, slidingExpiration);
            }
        }

        #endregion
    }
}
