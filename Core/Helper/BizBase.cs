using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.ComponentModel;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace TkwEF.Core.BLL
{
    [Serializable]
    [DataContract]
    public abstract partial class BIZ_Base : INotifyPropertyChanged, IDataErrorInfo, ICloneable, IBiz
    {
        [DllImport("kernel32.dll")]
        protected static extern bool SetProcessWorkingSetSize(IntPtr handle,
            int minimumWorkingSetSize, int maximumWorkingSetSize);

        #region Public static

        public static bool SetProcessWorkingSetSizeMinimum()
        {
            return SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
        }

        #endregion

        #region Static Protected

        /// <summary>
        /// Максимальное количество строк для веб
        /// </summary>
        public const int MAXROWS = int.MaxValue;
        /// <summary>
        /// Минимальная дата для MS SQL
        /// </summary>
        public static DateTime MINSQLDATE
        {
            get { return DateTime.MinValue; }
        }

        /// <summary>
        /// Возвращает дату пригодную для MS SQL
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        protected static DateTime ConvertDateToDateSql(DateTime date)
        {
            if (date < MINSQLDATE)
                return MINSQLDATE;
            else
                return date;
        }
        /// <summary>
        /// Возвращает пустую строку, если переданное значение равно Null
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected static string ConvertNullToEmptyString(string input)
        {
            return (input == null ? "" : input);
        }
        /// <summary>
        /// Возвращает индекс страницы
        /// </summary>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        protected static int GetPageIndex(int startRowIndex, int maximumRows)
        {
            if (maximumRows <= 0)
                return 0;
            else
                return (int)Math.Floor((double)startRowIndex / (double)maximumRows);
        }
        protected volatile static object _lockStatic = new object();

        #endregion

        #region Fields

        [DataMember]
        public int Id { get; set; }
        /// <summary>
        /// Возвращает образец регулярного выражения для почты строго один адрес
        /// </summary>
        public const string EmailPatternStrong = @"^[a-zA-Z0-9_\.-]+@[a-zA-Z0-9\.-]+\.[a-zA-Z]{2,4}$";
        /// <summary>
        /// Возвращает образец регулярного выражения для почты
        /// </summary>
        public const string EmailPattern = "\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";

        #endregion

        #region ctor

        public BIZ_Base() { }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Событие вызываемое при изменении значения в поле
        /// </summary>
        /// <param name="propName"></param>
        protected void FirePropertyChangedNotification(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        #endregion

        #region IDataErrorInfo

        protected string _error = string.Empty;
        protected Hashtable _propErrors = new Hashtable();

        string IDataErrorInfo.Error
        {
            get { return _error; }
        }

        string IDataErrorInfo.this[string columnName]
        {
            get { return (string)_propErrors?[columnName]; }
        }

        #endregion

        /// <summary>
        /// Исключение выданное на уровне BLL
        /// </summary>
        [Serializable]
        public class RdbsBllException : ApplicationException
        {
            #region ctor

            public RdbsBllException() { }
            public RdbsBllException(string message)
                : base(message) { }
            public RdbsBllException(string message, Exception innerException)
                : base(message, innerException) { }

            #endregion

            /// <summary>
            /// Возвращает все сообщения исключения в виде строки
            /// </summary>
            /// <param name="ex"></param>
            /// <returns></returns>
            public static string GetMessageAll(Exception ex)
            {
                lock (_lockStatic)
                {
                    string res = string.Empty;
                    Exception e = ex;
                    while (e != null)
                    {
                        res = string.Format(" Сообщение: {0};", e.Message);
                        e = e.InnerException;
                    }
                    return res.Trim();
                }
            }
        }

        #region Public function

        public object Clone()
        {
            if (this is ISerializable)
                return _cloneSerializable();
            return _cloneReflection();
        }
        /// <summary>
        /// Клонирование сериализуемого объекта
        /// </summary>
        /// <returns></returns>
        protected virtual object _cloneSerializable()
        {
            // Создание временного потока в памяти
            using (MemoryStream stream = new MemoryStream())
            {
                // Создание модуля форматирования для сериализации
                BinaryFormatter formatter = new BinaryFormatter();

                formatter.Context = new StreamingContext(StreamingContextStates.Clone);

                // Сериализация графа объекта в поток в памяти
                formatter.Serialize(stream, this);

                // Возвращение к началу потока в памяти перед десиреализацией
                stream.Position = 0;

                // Десиреализация графа в новый набор объектов и возвращение корня графа (детальной копии) вызывающему методу
                return formatter.Deserialize(stream);
            }
        }
        /// <summary>
        /// Клонирование через отражение
        /// </summary>
        /// <returns></returns>
        protected virtual object _cloneReflection()
        {
            object newObject = Activator.CreateInstance(GetType());
            //PropertyInfo[] propertyInfos = GetType().GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            FieldInfo[] fieldInfos = GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
            _cloneFillFields(newObject, fieldInfos);

            PropertyInfo[] propertyInfos = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            _cloneFillProperty(newObject, propertyInfos);

            return newObject;
        }

        private void _cloneFillFields(object newObject, FieldInfo[] fieldInfos)
        {
            foreach (FieldInfo fieldInfo in fieldInfos)
            {
                if (fieldInfo.FieldType.IsGenericType)
                {
                    if (fieldInfo.FieldType.GetInterface("IList", true) != null)
                    {
                        IList oldList = fieldInfo.GetValue(this) as IList;
                        if (oldList != null && oldList.Count > 0 &&
                    oldList[0].GetType().GetInterface("ICloneable", true) != null)
                        {
                            IList newList = (IList)fieldInfo.GetValue(newObject);
                            foreach (object obj in oldList)
                            {
                                ICloneable clone = (ICloneable)obj;
                                newList.Add(clone.Clone());
                            }
                        }
                        else
                        {
                            fieldInfo.SetValue(newObject, oldList);
                        }
                    }
                    else if (fieldInfo.FieldType.GetInterface("IDictionary", true) != null)
                    {
                        IDictionary oldDic = fieldInfo.GetValue(this) as IDictionary;
                        if (oldDic != null && oldDic.Count > 0 && oldDic[0].GetType().GetInterface("ICloneable", true) != null)
                        {
                            IDictionary newDic = (IDictionary)fieldInfo.GetValue(newObject);
                            foreach (DictionaryEntry entry in oldDic)
                            {
                                ICloneable clone = (ICloneable)entry.Value;
                                newDic[entry.Key] = clone.Clone();
                            }
                        }
                        else
                        {
                            fieldInfo.SetValue(newObject, oldDic);
                        }
                    }
                    //Clone IClonable object
                    else if (fieldInfo.GetType().GetInterface("ICloneable", true) != null)
                    {
                        ICloneable clone = (ICloneable)fieldInfo.GetValue(this);
                        fieldInfo.SetValue(newObject, clone.Clone());
                    }
                    else
                    {
                        fieldInfo.SetValue(newObject, fieldInfo.GetValue(this));
                    }
                }
                else
                {
                    //Clone IClonable object
                    if (fieldInfo.GetType().GetInterface("ICloneable", true) != null)
                    {
                        ICloneable clone = (ICloneable)fieldInfo.GetValue(this);
                        fieldInfo.SetValue(newObject, clone.Clone());
                    }
                    else
                    {
                        fieldInfo.SetValue(newObject, fieldInfo.GetValue(this));
                    }
                }
            }
        }
        private void _cloneFillProperty(object newObject, PropertyInfo[] propertyInfos)
        {
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                if (propertyInfo.PropertyType.IsGenericType)
                {
                    if (propertyInfo.PropertyType.GetInterface("IList", true) != null)
                    {
                        IList oldList = propertyInfo.GetValue(this, null) as IList;
                        if (oldList != null && oldList.Count > 0 &&
                    oldList[0].GetType().GetInterface("ICloneable", true) != null)
                        {
                            IList newList = (IList)propertyInfo.GetValue(newObject, null);
                            foreach (object obj in oldList)
                            {
                                ICloneable clone = (ICloneable)obj;
                                newList.Add(clone.Clone());
                            }
                        }
                        else
                        {
                            propertyInfo.SetValue(newObject, oldList, null);
                        }
                    }
                    else if (propertyInfo.PropertyType.GetInterface("IDictionary", true) != null)
                    {
                        IDictionary oldDic = propertyInfo.GetValue(this, null) as IDictionary;
                        if (oldDic != null && oldDic.Count > 0 && oldDic[0].GetType().GetInterface("ICloneable", true) != null)
                        {
                            IDictionary newDic = (IDictionary)propertyInfo.GetValue(newObject, null);
                            foreach (DictionaryEntry entry in oldDic)
                            {
                                ICloneable clone = (ICloneable)entry.Value;
                                newDic[entry.Key] = clone.Clone();
                            }
                        }
                        else
                        {
                            propertyInfo.SetValue(newObject, oldDic, null);
                        }
                    }
                    //Clone IClonable object
                    else if (propertyInfo.GetType().GetInterface("ICloneable", true) != null)
                    {
                        ICloneable clone = (ICloneable)propertyInfo.GetValue(this, null);
                        propertyInfo.SetValue(newObject, clone.Clone(), null);
                    }
                    else
                    {
                        if (propertyInfo.CanWrite)
                            propertyInfo.SetValue(newObject, propertyInfo.GetValue(this, null), null);
                    }
                }
                else
                {
                    //Clone IClonable object
                    if (propertyInfo.GetType().GetInterface("ICloneable", true) != null)
                    {
                        ICloneable clone = (ICloneable)propertyInfo.GetValue(this, null);
                        propertyInfo.SetValue(newObject, clone.Clone(), null);
                    }
                    else
                    {
                        if (propertyInfo.CanWrite)
                            propertyInfo.SetValue(newObject, propertyInfo.GetValue(this, null), null);
                    }
                }
            }
        }

        #endregion

        #region Override

        public static bool operator !=(BIZ_Base bz1, BIZ_Base bz2)
        {
            //return (object)bz1 == null || (object)bz2 == null || !(bz1.Id == bz2.Id);
            if ((object)bz1 != null && (object)bz2 == null) return true;
            if ((object)bz1 == null && (object)bz2 != null) return true;
            if ((object)bz1 == null && (object)bz2 == null) return false;
            if (bz1.Id == bz2.Id)
                return false;
            else
                return true;
        }

        public static bool operator ==(BIZ_Base bz1, BIZ_Base bz2)
        {
            //return !(bz1 != bz2);
            if ((object)bz1 == null && (object)bz2 == null) return true;
            if ((object)bz1 != null && (object)bz2 == null) return false;
            if ((object)bz1 == null && (object)bz2 != null) return false;

            if (bz1.Id == bz2.Id)
                return true;
            else
                return false;
        }

        public override string ToString()
        {
            return string.Format("Id={0};", Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj == null || this.GetType() != obj.GetType()) return false;
            return Id.Equals(((BIZ_Base)obj).Id);
        }

        #endregion

        //#region IEditableObject

        //private BIZ backupCopy;
        //private bool inEdit;

        //public virtual void BeginEdit()
        //{
        //    if (inEdit) return;
        //    inEdit = true;
        //    backupCopy = MemberwiseClone() as BIZ;
        //}

        //public virtual void CancelEdit()
        //{
        //    if (!inEdit) return;
        //    inEdit = false;
        //    backupCopy = null;
        //}

        //public virtual void EndEdit()
        //{
        //    if (!inEdit) return;
        //    inEdit = false;

        //    var thisFields = GetType().GetProperties();
        //    var thatFields = backupCopy.GetType().GetProperties();
        //    foreach (PropertyInfo thisField in thisFields)
        //    {
        //        foreach (PropertyInfo thatField in thatFields)
        //        {
        //            if (thisField.Name != thatField.Name)
        //                continue;
        //            // 20140604 Ошибка: Id BIZ имеет тип int, а у DocOperType Id имеет тип string
        //            // Сделал у DocOperType переопределение
        //            if (thisField.CanWrite && thisField.GetType() == thatField.GetType())
        //                thisField.SetValue(this, thatField.GetValue(backupCopy, null), null);
        //            break;
        //        }
        //    }
        //}

        //#endregion
    }

    public abstract class BizComparer : IEqualityComparer<BIZ_Base>
    {
        public virtual bool Equals(BIZ_Base x, BIZ_Base y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null) || ReferenceEquals(y, null)) return false;
            return x.Id == y.Id;
        }

        public virtual int GetHashCode(BIZ_Base obj)
        {
            return obj.Id.GetHashCode() ^ obj.Id.GetHashCode();
        }
    }
    public class ReportCap
    {
        public string Zagolovok { get; set; }
        public DateTime? DateBegin { get; set; }
        public DateTime? DateEnd { get; set; }
        public string ObjName { get; set; }
        public string SkladName { get; set; }
        public DateTime ByDate { get; set; }

    }
}
