using ApiQuasar.Exceptions;
using ApiQuasar.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ApiQuasar.Services.Utils
{
    public static class Global
    {
        public static readonly List<Satellite> satellites = new List<Satellite>() {
            new Satellite(){ Name = "kenobi", Position = new Position(-500,-200) },
            new Satellite(){ Name = "skywalker", Position =  new Position(100,-100) },
            new Satellite(){ Name = "sato", Position = new Position(500,100) }
        };

        public static Dictionary<string, TransmissionModel> messagesSaved = new Dictionary<string, TransmissionModel>();
    }
}
