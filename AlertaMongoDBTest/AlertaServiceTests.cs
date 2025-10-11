using Moq;
using msAlertaMongoDB.Entity;
using msAlertaMongoDB.Repository;
using msAlertaMongoDB.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlertaMongoDBTest
{
    public class AlertaServiceTests
    {
        private readonly Mock<IAlertaRepository> _repoMock;
        private readonly AlertaService _service;

        public AlertaServiceTests()
        {
            _repoMock = new Mock<IAlertaRepository>();
            _service = new AlertaService(_repoMock.Object);
        }

        [Fact]
        public async Task CreateAlert_ThrowsArgumentNullException_WhenAlertaIsNull()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _service.CreateAlert(null));
        }

        [Fact]
        public async Task CreateAlert_ThrowsArgumentException_WhenMensagemIsNullOrEmpty()
        {
            var alerta = new Alerta { Mensagem = "" };
            await Assert.ThrowsAsync<ArgumentException>(() => _service.CreateAlert(alerta));
        }

        [Fact]
        public async Task CreateAlert_CallsRepositoryAndReturnsAlerta()
        {
            var alerta = new Alerta { Mensagem = "Test" };
            var createdAlert = new Alerta { Id = "1", Mensagem = "Test" };

            _repoMock.Setup(r => r.CreateAlerta(alerta)).ReturnsAsync(createdAlert);

            var result = await _service.CreateAlert(alerta);

            Assert.Equal("1", result.Id);
            Assert.Equal("Test", result.Mensagem);
            _repoMock.Verify(r => r.CreateAlerta(alerta), Times.Once);
        }

        [Fact]
        public async Task GetAlertaById_ReturnsAlerta_WhenExists()
        {
            var alerta = new Alerta { Id = "1", Mensagem = "Test" };
            _repoMock.Setup(r => r.AlertaById("1")).ReturnsAsync(alerta);

            var result = await _service.GetAlertaById("1");

            Assert.Equal("1", result.Id);
            Assert.Equal("Test", result.Mensagem);
        }

        [Fact]
        public async Task GetAllAlertas_ReturnsListOfAlertas()
        {
            var alertas = new List<Alerta> { new Alerta { Id = "1", Mensagem = "Test" } };
            _repoMock.Setup(r => r.ListaAlertas()).ReturnsAsync(alertas);

            var result = await _service.GetAllAlertas();

            Assert.Single(result);
            Assert.Equal("1", result[0].Id);
        }

        [Fact]
        public async Task UpdateAlert_ReturnsNull_WhenAlertaDoesNotExist()
        {
            _repoMock.Setup(r => r.AlertaById("1")).ReturnsAsync((Alerta)null);

            var result = await _service.UpdateAlert("1", new Alerta { Mensagem = "Update" });

            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateAlert_CallsRepositoryAndReturnsUpdatedAlerta()
        {
            var existing = new Alerta { Id = "1", Mensagem = "Old" };
            var updated = new Alerta { Id = "1", Mensagem = "New" };

            _repoMock.Setup(r => r.AlertaById("1")).ReturnsAsync(existing);
            _repoMock.Setup(r => r.UpdateAlerta("1", updated)).ReturnsAsync(updated);

            var result = await _service.UpdateAlert("1", updated);

            Assert.Equal("New", result.Mensagem);
            _repoMock.Verify(r => r.UpdateAlerta("1", updated), Times.Once);
        }

        [Fact]
        public async Task DeleteAlert_ReturnsFalse_WhenAlertaDoesNotExist()
        {
            _repoMock.Setup(r => r.AlertaById("1")).ReturnsAsync((Alerta)null);

            var result = await _service.DeleteAlert("1");

            Assert.False(result);
        }

        [Fact]
        public async Task DeleteAlert_ReturnsTrue_WhenDeletionSucceeds()
        {
            var alerta = new Alerta { Id = "1" };
            _repoMock.Setup(r => r.AlertaById("1")).ReturnsAsync(alerta);
            _repoMock.Setup(r => r.DeleteAlerta("1")).ReturnsAsync(true);

            var result = await _service.DeleteAlert("1");

            Assert.True(result);
        }
    }
}
