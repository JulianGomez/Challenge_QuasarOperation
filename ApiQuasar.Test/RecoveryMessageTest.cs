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
    public class RecoveryMessageTest
    {
        private Mock<IDataRepository> _dataRepository;
        private ValidatorService _validatorService;
        private MessageService _messageService;


        [TestInitialize]
        public void Initialize()
        {
            _dataRepository = new Mock<IDataRepository>();
            _dataRepository.Setup(x => x.GetSatellites()).Returns(new List<Satellite>() {
                new Satellite(){ Name = "kenobi",  Position = new Position(-500,-200) },
                new Satellite(){ Name = "skywalker", Position = new Position(100,-100) },
                new Satellite(){ Name = "sato", Position = new Position(500,100) }
            });

            _validatorService = new ValidatorService(_dataRepository.Object);
            _messageService = new MessageService(_dataRepository.Object);
        }


        [TestMethod]
        public void TestMessageSuccess()
        {
            var transmissions = new TopSecretRequest() {
                Satellites = new List<TransmissionModel>() {
                    new TransmissionModel() { Name = "kenobi", Distance = 106.6 ,  Message = new string[]{ "", "es", "", "mensaje", "" } },
                    new TransmissionModel() { Name = "skywalker", Distance = -300,  Message = new string[]{ "", "", "un", "", "secreto" } },
                    new TransmissionModel() {  Name = "sato", Distance = 50, Message = new string[]{ "este", "", "un", "", "secreto" } }
                }
            };

            _validatorService.General(transmissions.Satellites);            
            var actual = _messageService.GetMessage(transmissions);

            Assert.AreEqual("este es un mensaje secreto", actual);
        }


        [TestMethod]
        [ExpectedException(typeof(HttpException), "No se puede recuperar el mensaje. El mensaje está incompleto")]
        public void TestExceptionRecovery()
        {
            var transmissions = new TopSecretRequest()
            {
                Satellites = new List<TransmissionModel>() {
                    new TransmissionModel() { Name = "kenobi", Distance = 106.6 ,  Message = new string[]{ "", "es", "", "", "" } },
                    new TransmissionModel() { Name = "skywalker", Distance = -300,  Message = new string[]{ "", "", "un", "", "secreto" } },
                    new TransmissionModel() {  Name = "sato", Distance = 50, Message = new string[]{ "este", "", "un", "", "" } }
                }
            };

            _validatorService.General(transmissions.Satellites);
            _messageService.GetMessage(transmissions);
        }
        
    }
}
