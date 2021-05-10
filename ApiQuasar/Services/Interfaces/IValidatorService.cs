
using ApiQuasar.Model;
using System.Collections.Generic;

namespace ApiQuasar.Services
{
    public interface IValidatorService
    {
        void General(List<TransmissionModel> satellites);

        bool SatelliteValid(TransmissionModel satellite);
    }
}
