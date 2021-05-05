using ApiQuasar.Adapters;
using ApiQuasar.Exceptions;
using ApiQuasar.Model;
using ApiQuasar.Services.Interfaces;
using ApiQuasar.Services.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ApiQuasar.Services
{
    public class MessageService : IMessageService
    {
        private readonly IAdapterRecoveryMessage _adapterRecoveryMessage;
        private static Dictionary<string, SatelliteRequest> messagesSaved = new Dictionary<string, SatelliteRequest>();

        public MessageService(IAdapterRecoveryMessage adapterRecoveryMessage)
        {
            _adapterRecoveryMessage = adapterRecoveryMessage ?? throw new ArgumentNullException(nameof(adapterRecoveryMessage));
        }


        public string GetMessage(TopSecretRequest request)
        {
            List<string[]> listMessages = new List<string[]>();
            request.Satellites.ForEach(x =>
            {
                listMessages.Add(x.Message);
            });

            return  _adapterRecoveryMessage.Recovery(listMessages);
		}


        public void SaveMessage(SatelliteRequest satellite)
        {
            if (messagesSaved.ContainsKey(satellite.Name.ToLower()))
            {
                //sobreescribo la información que contenía el satellite
                messagesSaved[satellite.Name.ToLower()] = satellite;
            }
            else
            {
                //agrego un nuevo satellite
                messagesSaved.Add(satellite.Name.ToLower(), satellite);
            }
        }

        public List<SatelliteRequest> GetListMessagesSaved()
        {
            return messagesSaved.Values.ToList();
        }

    }
}
