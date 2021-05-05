
using ApiQuasar.Model;
using System.Collections.Generic;

namespace ApiQuasar.Services
{
    public interface IValidatorService
    {
        void General(List<SatelliteRequest> Satellites);

        bool SatelliteValid(SatelliteRequest satellite);
    }
}
