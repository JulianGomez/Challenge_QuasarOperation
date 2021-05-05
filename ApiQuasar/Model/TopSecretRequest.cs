using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiQuasar.Model
{
    public class TopSecretRequest
    {        
        public List<SatelliteRequest> Satellites { get; set; }
    }
}
