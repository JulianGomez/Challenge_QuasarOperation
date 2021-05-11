using ApiQuasar.Model;
using System.Collections.Generic;


namespace ApiQuasar.Data.Interfaces
{
    public interface IDataRepository
    {
        List<Satellite> GetSatellites();
        Position GetPositionByName(string name_satellite);
        Dictionary<string, TransmissionModel> GetMessagesSaved();
        bool SatelliteAlreadySave(TransmissionModel transmission);
        void UpdateDataTransmission(TransmissionModel transmission);
        void AddDataTransmission(TransmissionModel transmission);
    }
}
