using System.ComponentModel.DataAnnotations;

namespace TkwEF.BLL
{
    public partial class VesKat : BIZ
    {
        //private readonly ObservableListSource<Product> _products =
        //               new ObservableListSource<Product>();
        [Required]
        public int BeginVes { get; set; }
        [Required]
        public int EndVes { get; set; }

        #region override

        public override string ToString()
        {
            return $"Id={Id}, BeginVes={BeginVes}, EndVes={EndVes}";
        }

        #endregion
    }
}
