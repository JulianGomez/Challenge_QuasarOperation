using ApiQuasar.Data.Interfaces;
using ApiQuasar.Exceptions;
using ApiQuasar.Model;
using ApiQuasar.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;


namespace ApiQuasar.Test
{
    [TestClass]
    public class ValidationGeneralTest
    {
        private Mock<IDataRepository> _dataRepository;
        private ValidatorService _validatorService;


        [TestInitialize]
        public void Initialize()
        {
            _dataRepository = new Mock<IDataRepository>();

            _dataRepository.Setup(x => x.GetPositionByName("kenobi")).Returns(new Position(-500, -200));
            _dataRepository.Setup(x => x.GetPositionByName("skywalker")).Returns(new Position(100, -100));
            _dataRepository.Setup(x => x.GetPositionByName("sato")).Returns(new Position(500, 100));

            _dataRepository.Setup(x => x.GetSatellites()).Returns(new List<Satellite>() {
                new Satellite(){ Name = "kenobi",  Position = new Position(-500,-200) },
                new Satellite(){ Name = "skywalker", Position = new Position(100,-100) },
                new Satellite(){ Name = "sato", Position = new Position(500,100) }
            });

            _validatorService = new ValidatorService(_dataRepository.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(HttpException), "No se puede calcular el mensaje con la información proporcionada.")]
        public void TestExceptionLenghtTransmissions()
        {
            var transmissions = new TopSecretRequest()
            {
                Satellites = new List<TransmissionModel>() {
                    new TransmissionModel() { Name = "kenobi", Distance = 106.6 ,  Message = new string[]{ "", "es", "", "mensaje", "secreto" } },
                    new TransmissionModel() {  Name = "sato", Distance = 50.6, Message = new string[]{ "este", "", "un", "", "" } }
                }
            };

            _validatorService.General(transmissions.Satellites);
        }

        [TestMethod]
        [ExpectedException(typeof(HttpException), "No es posible recuperar el mensaje, la longitud del mensaje en la transmisión es diferente.")]
        public void TestExceptionLenghtMessages()
        {
            var transmissions = new TopSecretRequest()
            {
                Satellites = new List<TransmissionModel>() {
                    new TransmissionModel() { Name = "kenobi", Distance = 106.6 ,  Message = new string[]{ "", "es", "", "mensaje", "secreto" } },
                    new TransmissionModel() { Name = "skywalker", Distance = -300.5,  Message = new string[]{ "", "", "un", ""} },
                    new TransmissionModel() {  Name = "sato", Distance = 50.6, Message = new string[]{ "este", "", "un", "", "" } }
                }
            };

            _validatorService.General(transmissions.Satellites);
        }


        [TestMethod]
        [ExpectedException(typeof(HttpException), "No es posible encontrar el satelite 'julian'. No es un satelite válido.")]
        public void TestExceptionNameSatellite()
        {
            var transmissions = new TopSecretRequest()
            {
                Satellites = new List<TransmissionModel>() {
                    new TransmissionModel() { Name = "kenobi", Distance = 106.6 ,  Message = new string[]{ "", "es", "", "mensaje", "secreto" } },
                    new TransmissionModel() {  Name = "julian", Distance = -300.5, Message = new string[]{ "este", "", "un", "", "" } },
                    new TransmissionModel() {  Name = "sato", Distance = 50.6, Message = new string[]{ "este", "", "un", "", "secreto" } }
                }
            };

            _validatorService.General(transmissions.Satellites);
        }
    }
}
