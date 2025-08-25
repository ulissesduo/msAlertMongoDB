using Microsoft.AspNetCore.Http.HttpResults;
using msAlertaMongoDB.Entity;
using msAlertaMongoDB.Repository;
using ZstdSharp;

namespace msAlertaMongoDB.Service
{
    public class AlertaService : IAlertaService
    {
        private readonly IAlertaRepository _alertaRepository;
        public AlertaService(IAlertaRepository alertaRepository)
        {
            _alertaRepository = alertaRepository;
        }

        public Task<Alerta> CreateAlert(Alerta alerta)
        {
            if (alerta == null) {
                throw new ArgumentNullException(nameof(alerta), "Alerta não pode ser nula.");
            }
            if (string.IsNullOrWhiteSpace(alerta.Mensagem))
                throw new ArgumentException("Mensagem não pode ser nula");

            return _alertaRepository.CreateAlerta(alerta);
        }

        public async Task<bool> DeleteAlert(string id)
        {
            var alertaById = await _alertaRepository.AlertaById(id);
            if (alertaById == null) return false;
            return await _alertaRepository.DeleteAlerta(id);
        }

        public async Task<Alerta> GetAlertaById(string id)
        {
            return await _alertaRepository.AlertaById(id);
        }

        public async Task<List<Alerta>> GetAllAlertas()
        {
            return await _alertaRepository.ListaAlertas();
        }

        public async Task<Alerta> UpdateAlert(string id, Alerta alerta)
        {
            if (alerta == null) throw new ArgumentNullException(nameof(alerta));
            var existingAlert = await _alertaRepository.AlertaById(id);
            if (existingAlert == null) { return null; }
            return await _alertaRepository.UpdateAlerta(id, alerta);
        }
    }
}
