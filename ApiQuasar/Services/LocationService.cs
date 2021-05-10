﻿using ApiQuasar.Adapters.Interfaces;
using ApiQuasar.Exceptions;
using ApiQuasar.Model;
using ApiQuasar.Services.Interfaces;
using ApiQuasar.Services.Utils;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ApiQuasar.Services
{
    public class LocationService : ILocationService
    {
        private readonly IAdapterPositionShip _adapterPositionShip;

        public LocationService(IAdapterPositionShip adapterPositionShip)
        {
            _adapterPositionShip = adapterPositionShip ?? throw new ArgumentNullException(nameof(adapterPositionShip));
        }

        public Position GetLocation(TopSecretRequest request)
        {
            var satellites = request.Satellites.Select(
            s => new TrilaterationModel()
            {
                Name = s.Name,
                Distance = s.Distance,
                Position = this.GetPositionByName(s.Name)
            });

            return _adapterPositionShip.GetPositionByTrilateration_V1(satellites);
        }


        private Position GetPositionByName(string name)
        {
            Satellite satellite = Global.satellites.Where(x => x.Name.ToUpper().Equals(name.ToUpper())).FirstOrDefault();

            if (satellite != null)
            {
                return satellite.Position;
            }
            else
            {
                throw new HttpException("No se pudo encontrar la posición de " + name + ". No es un satelite válido.", HttpStatusCode.NotFound);
            }
        }

    }
}
