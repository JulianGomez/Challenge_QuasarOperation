using ApiQuasar.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiQuasar.Services.Interfaces
{
    public interface ILocationService
    {
        Position GetLocation(TopSecretRequest satellites);

    }
}
