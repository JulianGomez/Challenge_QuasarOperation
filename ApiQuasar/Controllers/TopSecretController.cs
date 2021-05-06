﻿using ApiQuasar.Exceptions;
using ApiQuasar.Model;
using ApiQuasar.Services;
using ApiQuasar.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ApiQuasar.Controllers
{
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
            if (!ModelState.IsValid)
            {
                throw new HttpException("No se puede calcular la posición o el mensaje con la información proporcionada.", HttpStatusCode.NotFound);
            }

            _validatorService.General(request.Satellites);

            var message = _messageService.GetMessage(request);
            var location = _locationService.GetLocation(request);
            var response = new TopSecretResponse(new Position(location.X, location.Y), message);

            return Ok(response);
        }

    }
}