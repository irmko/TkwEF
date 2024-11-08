using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TkwEF.BLL
{
    public abstract class BIZ : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Событие вызываемое при изменении значения в поле
        /// </summary>
        /// <param name="propName"></param>
        protected void FirePropertyChangedNotification(string propName)
        {
            //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
            //Вызывает ошибку при выхове из вне формы (например из формы f_SpravUserClub форму f_SpravFizLitsa)
            //if (PropertyChanged != null)
            //    PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        #endregion

        #region Fields

        [Key]
        public int Id { get; set; }

        #endregion
    }
}
