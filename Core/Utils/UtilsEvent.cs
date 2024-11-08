using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TkwEF.Core
{
    public static partial class UtilityCore
    {
        public class UtilsEvent
        {
            public class EventArgs<T> : EventArgs //класс информации о событии, предназначенный для транспортировки одного значения
            {
                public EventArgs(T value)
                {
                    this.value = value;
                }

                private T value;

                public T Value
                {
                    get { return value; }
                }
            }
        }
    }
}
