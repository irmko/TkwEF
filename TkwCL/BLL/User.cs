using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using TkwEF.Core.Extentions;

namespace TkwEF.BLL
{
    public class User : BIZ
    {
        #region Fields

        [MaxLength(150)]
        [Required]
        public string Fam { get; set; }
        [MaxLength(150)]
        [Required]
        public string Name { get; set; }
        [MaxLength(150)]
        public string Otch { get; set; }
        public DateTime? Birghtday { get; set; }
        [MaxLength(15)]
        public string Telefon { get; set; }
        [MaxLength(1000)]
        public string Email { get; set; }
        /// <summary>
        /// Пояса
        /// </summary>
        //[InverseProperty("IdUserPoyas")]

        public virtual ICollection<UserPoyas> UserPoyases { get; set; }
        public virtual ICollection<UserClub> UserClubs { get; set; }

        #region ByGrid

        public string FIO
        {
            get
            {
                string val = (Fam ?? string.Empty) == string.Empty ? "" : Fam;
                val += (val.Length > 0 && !(Name ?? string.Empty).Equals(string.Empty) ? " " : "") + (!(Name ?? string.Empty).Equals(string.Empty) ? Name : "");
                val += (val.Length > 0 && !(Otch ?? string.Empty).Equals(string.Empty) ? " " : "") + (!(Otch ?? string.Empty).Equals(string.Empty) ? Otch : "");
                return val;
            }
        }

        public string FamIO
        {
            get
            {
                string val = (Fam ?? string.Empty) == string.Empty ? "" : Fam;
                if (val.Length == 0)
                    return FIO;
                val += (val.Length > 0 && !(Name ?? string.Empty).Equals(string.Empty) ? " " : "") + (!(Name ?? string.Empty).Equals(string.Empty) ? Name.Substring(0, 1).ToUpper() + "." : "");
                val += (val.Length > 0 && !(Otch ?? string.Empty).Equals(string.Empty) ? " " : "") + (!(Otch ?? string.Empty).Equals(string.Empty) ? Otch.Substring(0, 1).ToUpper() + "." : "");
                return val;
            }
        }
        /// <summary>
        /// Вазвращает возраст
        /// </summary>
        //public NotifyTaskCompletion<int?> Age
        //{
        //    get
        //    {
        //        if (Birghtday.HasValue)
        //        {
        //            var val = new NotifyTaskCompletion<int>(Birghtday.Value.GetAgeAsync());
        //        }
        //        return null;
        //    }
        //}
        //public int? Age { get { return Birghtday.HasValue ? (int?)new NotifyTaskCompletion<int>(Birghtday.Value.GetAgeAsync()).Result : null; } }
        public int? Age
        {
            get
            {
                //var val = Task.Factory.StartNew(async () => { await Task.Delay(TimeSpan.FromSeconds(3)); return 1; }).Result;
                int? res = Birghtday.HasValue ? (int?)Birghtday.Value.GetAge() : null;
                FirePropertyChangedNotification(nameof(Age));
                return res;
            }
        }

        private Poyas _curPoyas = null;
        public Poyas CurrentPoyas
        {
            get
            {
                if (_curPoyas == null)
                    _curPoyas = (UserPoyases?.Count ?? 0) == 0 ? null : UserPoyases?.Where(w => w.Poyas != null).OrderBy(o => o.Poyas?.Order ?? 0).Select(s => s.Poyas).Last();
                return _curPoyas;
            }
        }
        public string CurPoyasName => CurrentPoyas?.Name ?? string.Empty;

        #endregion

        #endregion
    }
}
