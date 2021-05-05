using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
