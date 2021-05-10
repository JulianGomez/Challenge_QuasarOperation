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

        public MessageService(IAdapterRecoveryMessage adapterRecoveryMessage)
        {
            _adapterRecoveryMessage = adapterRecoveryMessage ?? throw new ArgumentNullException(nameof(adapterRecoveryMessage));
        }


        public string GetMessage(TopSecretRequest request)
        {
            List<string[]> listTransmissionsMessages = new List<string[]>();
            request.Satellites.ForEach(x =>
            {
                listTransmissionsMessages.Add(x.Message);
            });

            return  _adapterRecoveryMessage.Recovery(listTransmissionsMessages);
		}


        public void SaveMessage(TransmissionModel satellite)
        {
            if (Global.messagesSaved.ContainsKey(satellite.Name.ToLower()))
            {
                //sobreescribo la información que contenía el satellite
                Global.messagesSaved[satellite.Name.ToLower()] = satellite;
            }
            else
            {
                //agrego un nuevo satellite
                Global.messagesSaved.Add(satellite.Name.ToLower(), satellite);
            }
        }

        public List<TransmissionModel> GetListMessagesSaved()
        {
            return Global.messagesSaved.Values.ToList();
        }

    }
}
