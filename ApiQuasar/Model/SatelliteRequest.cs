using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;


namespace ApiQuasar.Model
{
    public class SatelliteRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public double Distance { get; set; }

        [Required]
        public string[] Message { get; set; }
    }
}
