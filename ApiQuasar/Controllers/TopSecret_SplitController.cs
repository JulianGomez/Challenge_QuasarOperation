using ApiQuasar.Exceptions;
using ApiQuasar.Model;
using ApiQuasar.Services;
using ApiQuasar.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Net;

namespace ApiQuasar.Controllers
{
    [SwaggerTag("(POST): Recibirá en su header el nombre del satelite como parámetro de ruta. En su body recibirá la distancia y el mensaje incompleto. Toda ésta información quedará guardada en memoria. \n\n  (GET) Obtiene la posicion y el mensaje completo de la información guardada en memoria por el endpoint [Topsecret_SplitPOST]. Sí la cantidad de satelites no es la correcta o el mensaje/posición no se puede recuperar devolverá un error 404 Not Found.")]
    [ApiController]
    [Route("api/[controller]")]
    public class TopSecret_SplitController : ControllerBase
    {
        private readonly ILocationService _locationService;
        private readonly IMessageService _messageService;
        private readonly IValidatorService _validatorService;

        public TopSecret_SplitController(ILocationService locationService, IMessageService messageService, IValidatorService validatorService)
        {
            _locationService = locationService ?? throw new ArgumentNullException(nameof(locationService));
            _messageService = messageService ?? throw new ArgumentNullException(nameof(messageService));
            _validatorService = validatorService ?? throw new ArgumentNullException(nameof(validatorService));
        }


        [HttpPost]
        public ActionResult Post([FromHeader]string nameSatellite, [FromBody] TopSecret_SplitRequest request)
        {
            TransmissionModel transmissionRequest = new TransmissionModel();
            transmissionRequest.Name = nameSatellite;
            transmissionRequest.Distance = request.Distance;
            transmissionRequest.Message = request.Message;

            if (_validatorService.SatelliteValid(transmissionRequest))
            {
                _messageService.SaveMessage(transmissionRequest);
            }
            else {
                throw new HttpException("No es posible guardar la transmisión de '" + nameSatellite + "'. No es un satelite válido.", HttpStatusCode.NotFound);
            }

            return Ok(transmissionRequest);
        }



        [HttpGet]
        public ActionResult Get()
        {
            TopSecretRequest request = new TopSecretRequest()
            {
                Satellites = _messageService.GetListMessagesSaved()
            };

            _validatorService.General(request.Satellites);

            var message = _messageService.GetMessage(request);
            var location = _locationService.GetLocation(request);
            var response = new TopSecretResponse(new Position(location.X, location.Y), message);

            return Ok(response);
        }

    }
}
