using System.ComponentModel.DataAnnotations;

namespace SCDT52CW2Models
{
    public class Assets
    {
        [Key]
        public int Id { get; set; }

        [Required, Display(Name="Asset ID"), MaxLength(20)]
        public string AssetID { get; set; }

        [Required, Display(Name = "Asset Description")]
        public string Desc { get; set; }

        [Required, Display(Name = "Asset Location")]
        public string Location { get; set; }


    }
}
