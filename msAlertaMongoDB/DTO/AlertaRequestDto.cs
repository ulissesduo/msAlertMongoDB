namespace msAlertaMongoDB.DTO
{
    public class AlertaRequestDto
    {
        public string Id_licenca { get; set; }
        public DateTime Data_alerta { get; set; }
        public string Mensagem { get; set; }
        public char Enviado { get; set; }
    }
}
