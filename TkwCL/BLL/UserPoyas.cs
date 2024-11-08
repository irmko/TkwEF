using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TkwEF.BLL
{
    public class UserPoyas : BIZ
    {
        public int UserId { get; set; }
        [Required]
        [Index("INDX_USERPOYAS", Order = 1, IsUnique = true)]
        public virtual User User { get; set; }

        public int PoyasId { get; set; }
        [Required]
        [Index("INDX_USERPOYAS", Order = 2, IsUnique = true)]
        public virtual Poyas Poyas { get; set; }

        /// <summary>
        /// Дата получения
        /// </summary>
        [Required]
        public DateTime Delivery { get; set; }
        [Required]
        [DefaultValue(true)]
        public bool Actual { get; set; }
        [Timestamp]
        public byte[] Timestamp { get; set; }

        #region ctor

        public UserPoyas()
        {
            Actual = true;
        }

        #endregion

        #region override

        public override string ToString()
        {
            return string.Format("User = {0}, Пояс = {1} ( Id = {2} )", User?.FIO, Poyas?.Name, Id);
        }

        #endregion
    }
}
