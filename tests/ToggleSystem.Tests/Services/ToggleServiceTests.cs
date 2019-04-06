using AutoFixture;
using AutoMapper;
using Moq;
using NUnit.Framework;
using Shouldly;
using System.Threading.Tasks;
using ToggleSystem.Domain.DTOs;
using ToggleSystem.Domain.Entities;
using ToggleSystem.Domain.Interfaces.Repositories;
using ToggleSystem.Domain.Interfaces.Services;
using ToggleSystem.Domain.Services;

namespace ToggleSystem.Tests.Services
{
    public class ToggleServiceTests
    {
        private IToggleService _service;

        private Mock<IToggleRepository> _toggleRepository;

        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _toggleRepository = new Mock<IToggleRepository>();

            _service = new ToggleService(Mapper.Instance, _toggleRepository.Object);
        }

        [OneTimeSetUp]
        public void OneSetup()
        {
            Mapper.Initialize(cfg => new EntityToDtoMappingProfile());
        }

        [Test]
        public async Task ShouldGetTogglesFromClient()
        {
            _toggleRepository.Setup(s => s.GetAll("test", 1)).ReturnsAsync(_fixture.CreateMany<Toggle>(2));

            var toggles = await _service.GetAll("test", 1);

            toggles.ShouldNotBeEmpty();
        }
    }
}
