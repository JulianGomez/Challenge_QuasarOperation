using System.ComponentModel.DataAnnotations;

namespace ApiQuasar.Model
{
    public class TopSecret_SplitRequest
    {
        [Required]
        public double Distance { get; set; }

        [Required]
        public string[] Message { get; set; }
    }
}
