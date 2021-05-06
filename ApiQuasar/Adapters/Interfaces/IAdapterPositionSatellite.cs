﻿using ApiQuasar.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiQuasar.Adapters.Interfaces
{
    public interface IAdapterPositionSatellite
    {
        Position GetPositionByTrilateration_V1(IEnumerable<TrilaterationModel> satellites);
    }
}