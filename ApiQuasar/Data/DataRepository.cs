using ApiQuasar.Data.Interfaces;
using ApiQuasar.Exceptions;
using ApiQuasar.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ApiQuasar.Data
{
    public class DataRepository : IDataRepository
    {
        //----------------------NIVEL 2----------------------------

        private static List<Satellite> satellites = new List<Satellite>() {
            new Satellite(){ Name = "kenobi", Position = new Position(-500,-200) },
            new Satellite(){ Name = "skywalker", Position = new Position(100,-100) },
            new Satellite(){ Name = "sato", Position = new Position(500, 100) }
        };

        public List<Satellite> GetSatellites()
        {
            return satellites;
        }

        public Position GetPositionByName(string name_satellite)
        {
            Satellite satellite = satellites.Where(x => x.Name.ToLower().Equals(name_satellite.ToLower())).FirstOrDefault();

            if (satellite != null)
            {
                return satellite.Position;
            }
            else
            {
                throw new HttpException("No se pudo encontrar la posición de " + name_satellite + ". No es un satelite válido.", HttpStatusCode.NotFound);
            }
        }


        //----------------------NIVEL 3----------------------------


        private static Dictionary<string, TransmissionModel> messagesSaved = new Dictionary<string, TransmissionModel>();

        public Dictionary<string, TransmissionModel> GetMessagesSaved()
        {
            return messagesSaved;
        }


        public bool SatelliteAlreadySave(TransmissionModel transmission)
        {
            return messagesSaved.ContainsKey(transmission.Name.ToLower());
        }

        public void UpdateDataTransmission(TransmissionModel transmission)
        {
            messagesSaved[transmission.Name.ToLower()] = transmission;
        }

        public void AddDataTransmission(TransmissionModel transmission)
        {
            messagesSaved.Add(transmission.Name.ToLower(), transmission);
        }
    }
}
