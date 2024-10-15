using System.ComponentModel.DataAnnotations;

namespace GalaxyLearn.DataLayer.Entities.User
{
    public class TempCode
    {
        [Key]
        public int TempCodeId { get; set; }
        public int UserId { get; set; }
        [StringLength(4)]
        public string Code { get; set; }
        public DateTime SendCodeDateTime { get; set; }
        public byte Type { get; set; }

        #region Relations

        public virtual User User { get; set; }

        #endregion
    }
}
