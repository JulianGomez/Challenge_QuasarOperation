using ApiQuasar.Model;

namespace ApiQuasar.Services.Interfaces
{
    public interface ILocationService
    {
        Position GetLocation(TopSecretRequest satellites);

    }
}
