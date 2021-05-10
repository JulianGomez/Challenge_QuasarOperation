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
        public void General(List<TransmissionModel> satellitesTransmission)
        {
            this.countTransmissions(satellitesTransmission);
            this.validatorSatellites(satellitesTransmission);
            this.lengthArrayMessage(satellitesTransmission);
        }


        private void countTransmissions(List<TransmissionModel> satellitesTransmission)
        {
            if (satellitesTransmission.Count != 3)
            {
                throw new HttpException("No se puede calcular el mensaje con la información proporcionada.", HttpStatusCode.NotFound);
            }
        }

        private void validatorSatellites(List<TransmissionModel> satellitesTransmission)
        {
            satellitesTransmission.ForEach(x =>
            {
                Satellite satellite = Global.satellites.Where(y => y.Name.ToLower().Equals(x.Name.ToLower())).FirstOrDefault();

                if (satellite == null)
                {
                    throw new HttpException("No es posible encontrar el satelite '" + x.Name + "'. No es un satelite válido.", HttpStatusCode.NotFound);
                }

            });
        }

        private void lengthArrayMessage(List<TransmissionModel> satellitesTransmission)
        {
            int lengthFirstSatellite = satellitesTransmission[0].Message.Length;

            bool differentLength = satellitesTransmission.Any(x => x.Message.Length != lengthFirstSatellite);

            if (differentLength)
            {
                throw new HttpException("No es posible recuperar el mensaje, la longitud del mensaje en la transmisión es diferente.", HttpStatusCode.NotFound);
            }
        }



        public bool SatelliteValid(TransmissionModel satelliteTransmission)
        {
            bool encontrado = false;

            encontrado = Global.satellites.Any(x => x.Name.ToLower().Equals(satelliteTransmission.Name.ToLower()));

            return encontrado;

        }
    }
}
