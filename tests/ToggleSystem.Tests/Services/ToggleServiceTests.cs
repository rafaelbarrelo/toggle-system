using Moq;
using NUnit.Framework;
using Shouldly;
using System.Linq;
using System.Threading.Tasks;
using ToggleSystem.Domain.DTOs;
using ToggleSystem.Domain.Entities;
using ToggleSystem.Domain.Extensions;
using ToggleSystem.Domain.Interfaces.Repositories;
using ToggleSystem.Domain.Interfaces.Services;
using ToggleSystem.Domain.Services;

namespace ToggleSystem.Tests.Services
{
    public class ToggleServiceTests
    {
        private IToggleService _service;

        private Mock<IToggleRepository> _toggleRepository;

        [SetUp]
        public void Setup()
        {
            _toggleRepository = new Mock<IToggleRepository>();

            _service = new ToggleService(_toggleRepository.Object);
        }

        [Test]
        public async Task ShouldGetTogglesDefault()
        {
            var dto1 = new ToggleDto
            {
                Name = "isButtonBlue",
                DefaultValue = ToggleValue.True,
                ToggleValue = null
            };

            var dto2 = new ToggleDto
            {
                Name = "isButtonGreen",
                DefaultValue = ToggleValue.True,
                ToggleValue = null
            };

            var dto3 = new ToggleDto
            {
                Name = "isButtonRed",
                DefaultValue = ToggleValue.True,
                ToggleValue = null
            };


            _toggleRepository.Setup(s => s.GetAll("ABC", 1)).ReturnsAsync(new[] { dto1, dto2, dto3 });

            var toggles = await _service.GetAll("ABC", 1);

            toggles.ShouldNotBeEmpty();
            toggles.Count().ShouldBe(3);

            foreach (var toggle in toggles)
            {
                toggle.ToBoolValue().ShouldBeTrue();
            }
        }

        [Test]
        public async Task ShouldGetTogglesAndOverrideIsButtonBlue()
        {
            var dto1 = new ToggleDto
            {
                Name = "isButtonBlue",
                DefaultValue = ToggleValue.True,
                ToggleValue = ToggleValue.False
            };

            _toggleRepository.Setup(s => s.GetAll("ABC", 1)).ReturnsAsync(new[] { dto1 });

            var toggles = await _service.GetAll("ABC", 1);

            toggles.ShouldNotBeEmpty();
            toggles.Count().ShouldBe(1);
            toggles.Last().ToBoolValue().ShouldBeFalse();
        }

        [Test]
        public async Task ShouldNotGetExclusive()
        {
            var dto1 = new ToggleDto
            {
                Name = "isButtonBlue",
                DefaultValue = ToggleValue.True,
                ToggleValue = null
            };

            var dto2 = new ToggleDto
            {
                Name = "isButtonGreen",
                DefaultValue = ToggleValue.Exclusive,
                ToggleValue = null
            };


            _toggleRepository.Setup(s => s.GetAll("ABC", 1)).ReturnsAsync(new[] { dto1, dto2 });

            var toggles = await _service.GetAll("ABC", 1);

            toggles.ShouldNotBeEmpty();
            toggles.Count().ShouldBe(1);

            toggles.First().Name.ShouldBe("isButtonBlue");
            toggles.First().ToBoolValue().ShouldBeTrue();
        }

        [Test]
        public async Task ShouldGetExclusive()
        {
            var dto1 = new ToggleDto
            {
                Name = "isButtonBlue",
                DefaultValue = ToggleValue.True,
                ToggleValue = null
            };

            var dto2 = new ToggleDto
            {
                Name = "isButtonGreen",
                DefaultValue = ToggleValue.Exclusive,
                ToggleValue = ToggleValue.True
            };


            _toggleRepository.Setup(s => s.GetAll("ABC", 1)).ReturnsAsync(new[] { dto1, dto2 });

            var toggles = await _service.GetAll("ABC", 1);

            toggles.ShouldNotBeEmpty();
            toggles.Count().ShouldBe(2);

            foreach (var toggle in toggles)
            {
                toggle.ToBoolValue().ShouldBeTrue();
            }
        }

        [Test]
        public async Task ShouldNotGetExcluded()
        {
            var dto1 = new ToggleDto
            {
                Name = "isButtonBlue",
                DefaultValue = ToggleValue.True,
                ToggleValue = null
            };

            var dto2 = new ToggleDto
            {
                Name = "isButtonRed",
                DefaultValue = ToggleValue.True,
                ToggleValue = ToggleValue.Excluded
            };

            _toggleRepository.Setup(s => s.GetAll("ABC", 1)).ReturnsAsync(new[] { dto1, dto2 });

            var toggles = await _service.GetAll("ABC", 1);

            toggles.ShouldNotBeEmpty();
            toggles.Count().ShouldBe(1);

            toggles.First().Name.ShouldBe("isButtonBlue");
            toggles.First().ToBoolValue().ShouldBeTrue();
        }
    }
}
