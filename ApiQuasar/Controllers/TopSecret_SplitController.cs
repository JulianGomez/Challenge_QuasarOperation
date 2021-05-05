using ApiQuasar.Exceptions;
using ApiQuasar.Model;
using ApiQuasar.Services;
using ApiQuasar.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ApiQuasar.Controllers
{
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
            SatelliteRequest satelliteRequest = new SatelliteRequest();
            satelliteRequest.Name = nameSatellite;
            satelliteRequest.Distance = request.Distance;
            satelliteRequest.Message = request.Message;

            if (_validatorService.SatelliteValid(satelliteRequest))
            {
                _messageService.SaveMessage(satelliteRequest);
            }
            else {
                throw new HttpException("No es posible guardar el satélite '" + nameSatellite + "'. No es un satelite válido.", HttpStatusCode.NotFound);
            }

            return Ok(satelliteRequest);
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
