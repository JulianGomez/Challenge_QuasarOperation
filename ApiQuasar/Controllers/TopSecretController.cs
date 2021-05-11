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
    [SwaggerTag("(POST): Obtiene la posicion y el mensaje completo de la información obtenida de cada satélite. Sí el mensaje o posición no se puede recuperar devolverá un error 404 Not Found.")]
    [ApiController]
    [Route("api/[controller]")]
    public class TopSecretController : ControllerBase
    {
        private readonly ILocationService _locationService;
        private readonly IMessageService _messageService;
        private readonly IValidatorService _validatorService;

        public TopSecretController(ILocationService locationService, IMessageService messageService, IValidatorService validatorService)
        {
            _locationService = locationService ?? throw new ArgumentNullException(nameof(locationService));
            _messageService = messageService ?? throw new ArgumentNullException(nameof(messageService));
            _validatorService = validatorService ?? throw new ArgumentNullException(nameof(validatorService));
        }


        [HttpPost]
        public ActionResult Post(TopSecretRequest request)
        {
            //if (!ModelState.IsValid)
            //{
            //    throw new HttpException("No se puede calcular la posición o el mensaje con la información proporcionada.", HttpStatusCode.NotFound);
            //}

            _validatorService.General(request.Satellites);

            var message = _messageService.GetMessage(request);
            var location = _locationService.GetLocation(request);
            var response = new TopSecretResponse(new Position(location.X, location.Y), message);

            return Ok(response);
        }

    }
}
