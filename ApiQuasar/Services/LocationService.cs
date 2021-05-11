using ApiQuasar.Data.Interfaces;
using ApiQuasar.Model;
using ApiQuasar.Services.Interfaces;
using ApiQuasar.Services.Utils;
using System;
using System.Linq;


namespace ApiQuasar.Services
{
    public class LocationService : ILocationService
    {
        private readonly IDataRepository _dataRepository;

        public LocationService(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository ?? throw new ArgumentNullException(nameof(dataRepository));
        }

        public Position GetLocation(TopSecretRequest request)
        {
            var satellites = request.Satellites.Select(
            s => new TrilaterationModel()
            {
                Name = s.Name,
                Distance = s.Distance,
                Position = _dataRepository.GetPositionByName(s.Name)
            });

            return Trilateration.GetPosition(satellites);
        }

    }
}
