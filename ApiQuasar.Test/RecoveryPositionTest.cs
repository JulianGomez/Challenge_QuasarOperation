using ApiQuasar.Data.Interfaces;
using ApiQuasar.Model;
using ApiQuasar.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace ApiQuasar.Test
{
    [TestClass]
    public class RecoveryPositionTest
    {
        private Mock<IDataRepository> _dataRepository;
        private ValidatorService _validatorService;
        private LocationService _locationService;


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
            _locationService = new LocationService(_dataRepository.Object);
        }


        [TestMethod]
        public void TestPositionSuccess()
        {
            var transmissions = new TopSecretRequest()
            {
                Satellites = new List<TransmissionModel>() {
                    new TransmissionModel() { Name = "kenobi", Distance = 106.6 ,  Message = new string[]{ "", "es", "", "mensaje", "" } },
                    new TransmissionModel() { Name = "skywalker", Distance = -300,  Message = new string[]{ "", "", "un", "", "secreto" } },
                    new TransmissionModel() {  Name = "sato", Distance = 50, Message = new string[]{ "este", "", "un", "", "secreto" } }
                }
            };

            _validatorService.General(transmissions.Satellites);
            var expectd = new Position(-640.48305000000232, 2099.7161000000078);
            var actual = _locationService.GetLocation(transmissions);

            Assert.AreEqual(expectd.X, actual.X);
            Assert.AreEqual(expectd.Y, actual.Y);
        }
    }
}
