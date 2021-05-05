using ApiQuasar.Exceptions;
using ApiQuasar.Model;
using ApiQuasar.Services.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ApiQuasar.Services
{
    public class ValidatorService : IValidatorService
    {
        public void General(List<SatelliteRequest> satellites)
        {
            this.cantSatellites(satellites);
            this.validatorSatellites(satellites);
            this.lengthArrayMessage(satellites);
        }


        private void cantSatellites(List<SatelliteRequest> listSatellites)
        {
            if (listSatellites.Count != 3)
            {
                throw new HttpException("No se puede calcular el mensaje con la información proporcionada.", HttpStatusCode.NotFound);
            }
        }

        private void validatorSatellites(List<SatelliteRequest> listSatellites)
        {
            listSatellites.ForEach(x =>
            {
                Satellite satellite = Global.satellites.Where(y => y.Name.ToLower().Equals(x.Name.ToLower())).FirstOrDefault();

                if (satellite == null)
                {
                    throw new HttpException("No es posible encontrar el satelite '" + x.Name + "'. No es un satelite válido.", HttpStatusCode.NotFound);
                }

            });
        }

        private void lengthArrayMessage(List<SatelliteRequest> listSatellites)
        {
            int lengthFirstSatellite = listSatellites[0].Message.Length;

            bool differentLength = listSatellites.Any(x => x.Message.Length != lengthFirstSatellite);

            if (differentLength)
            {
                throw new HttpException("No es posible recuperar el mensaje, la longitud del mensaje en la transmisión es diferente.", HttpStatusCode.NotFound);
            }
        }



        public bool SatelliteValid(SatelliteRequest satellite)
        {
            bool encontrado = false;

            encontrado = Global.satellites.Any(x => x.Name.ToLower().Equals(satellite.Name.ToLower()));

            return encontrado;

        }
    }
}
