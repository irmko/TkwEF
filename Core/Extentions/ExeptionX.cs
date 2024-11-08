using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.ServiceModel;
using System.Reflection;
using System.Data.Entity.Validation;

namespace TkwEF.Core.Extentions
{
    [Serializable]
    public class Exception<T> : Exception, ISerializable
        where T : ExceptionArgs
    {
        private const String c_args = "Args "; // Для ( де ) сериализации
        private readonly T m_args;
        public T Args { get { return m_args; } }
        public Exception(String message = null, Exception innerException = null)
            : this(null, message, innerException) { }
        public Exception(T args, String message = null, Exception innerException = null)
            : base(message, innerException)
        {
            m_args = args;
        }

        // Конструктор для десериализации : так как класс запечатан, конструктор
        // закрыт . Для незапечатанного класса конструктор должен быть защищенным
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        protected Exception(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            m_args = (T)info.GetValue(c_args, typeof(T));
        }

        // Метод для сериализации : оноткрыт из-за интерфейса ISerializaЬle
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(c_args, m_args);
            base.GetObjectData(info, context);
        }

        public override string Message
        {
            get
            {
                string baseMsg = base.Message;
                return m_args == null ? baseMsg : baseMsg + " (" + m_args.Message + ")";
            }
        }

        public override bool Equals(object obj)
        {
            Exception<T> other = obj as Exception<T>;
            if (obj == null) return false;
            return Object.Equals(m_args, other.m_args) && base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
    [Serializable]
    public abstract class ExceptionArgs
    {
        public virtual string Message { get { return string.Empty; } }
    }

    public static class ExceptionEx
    {
        /// <summary>
        /// Собирает все сообщения
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static string GetAllMessages(this Exception ex)
        {
            if(ex is AggregateException)
            {
                return (ex as AggregateException).GetAllMessages();
            }
            var builder = new StringBuilder();
            while (ex != null)
            {
                builder.AppendFormat("{0}{1}", ex.Message, Environment.NewLine);
                ex = ex.InnerException;
            }
            return builder.ToString();
        }
        /// <summary>
        /// Собирает все сообщения
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static string GetAllMessages(this AggregateException ex)
        {
            var builder = new StringBuilder();
            foreach (var item in ex.InnerExceptions)
            {
                Exception exx = item;
                while (exx != null)
                {
                    builder.AppendFormat("{0}{1}", exx.Message, Environment.NewLine);
                    exx = item.InnerException;
                }
            }
            return builder.ToString();
        }
    }
    
    public static class FaultExceptionEx
    {
        public static string GetAllMessages<T>(this FaultException<T> ex)
        {
            var builder = new StringBuilder();
            PropertyInfo[] properties = ex.Detail.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (PropertyInfo property in properties)
            {
                builder.AppendFormat("{0}{1}", property.GetValue(ex.Detail, null), Environment.NewLine);
            }
            return builder.ToString();
        }
    }
    
    public static class DbEntityValidationExceptionEx
    {
        public static string GetAllMessages(this DbEntityValidationException ex)
        {
            string msg = string.Empty;
            foreach (DbEntityValidationResult validationError in ex.EntityValidationErrors)
            {
                msg = "Object: " + validationError.Entry.Entity.ToString();

                foreach (DbValidationError err in validationError.ValidationErrors)
                {
                    msg += err.ErrorMessage + ",";
                }
            }
            return msg;
        }
    }
}
