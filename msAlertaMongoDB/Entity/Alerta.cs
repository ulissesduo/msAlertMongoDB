using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace msAlertaMongoDB.Entity
{
    public class Alerta
    {
        [BsonRepresentation(BsonType.ObjectId)] // store as ObjectId in Mongo, represent as string in C#
        public string Id { get; set; }
        public string Id_licenca { get; set; }
        public DateTime Data_alerta { get; set; }
        public string Mensagem { get; set; }
        public char Enviado { get; set; }

        public Alerta()
        {

        }

        public Alerta(string id, string id_licenca, DateTime data_alerta, string mensagem, char enviado)
        {
            this.Id = id;
            this.Data_alerta = data_alerta;
            this.Enviado = enviado;
            this.Id_licenca = id_licenca;
        }
    }
}
