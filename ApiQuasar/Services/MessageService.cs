using ApiQuasar.Data.Interfaces;
using ApiQuasar.Model;
using ApiQuasar.Services.Interfaces;
using ApiQuasar.Services.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiQuasar.Services
{
    public class MessageService : IMessageService
    {
        private readonly IDataRepository _dataRepository;

        public MessageService(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository ?? throw new ArgumentNullException(nameof(dataRepository));
        }

        public string GetMessage(TopSecretRequest request)
        {
            List<string[]> listTransmissionsMessages = new List<string[]>();
            request.Satellites.ForEach(x =>
            {
                listTransmissionsMessages.Add(x.Message);
            });

            return RecoveryMessage.Recovery(listTransmissionsMessages);
		}


        public List<TransmissionModel> GetListMessagesSaved()
        {
            return _dataRepository.GetMessagesSaved().Values.ToList();
        }

        public void SaveMessage(TransmissionModel transmission)
        {
            if (_dataRepository.SatelliteAlreadySave(transmission))
            {
                //sobreescribo la información que contenía la transmision
                _dataRepository.UpdateDataTransmission(transmission);
            }
            else
            {
                //agrego una nueva transmision en memoria
                _dataRepository.AddDataTransmission(transmission); 
            }
        }
    }
}
