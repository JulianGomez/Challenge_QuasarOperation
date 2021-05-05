using System.Collections.Generic;

namespace ApiQuasar.Adapters
{
    public interface IAdapterRecoveryMessage
    {
        public string Recovery(List<string[]> listSatellitesMessage);
    }
}
