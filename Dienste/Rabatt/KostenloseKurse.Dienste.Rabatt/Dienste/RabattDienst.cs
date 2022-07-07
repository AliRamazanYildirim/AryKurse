using Dapper;
using KostenloseKurse.Shared.Düo;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace KostenloseKurse.Dienste.Rabatt.Dienste
{
    public class RabattDienst : IRabattDienst
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _dbConnection;

        public RabattDienst(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbConnection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql"));
        }

        public async Task<Antwort<KeinInhaltDüo>> Aktualisieren(Modell.Rabatt rabatt)
        {
            var aktualisierenStatus = await _dbConnection.ExecuteAsync
                ("update rabatt set benutzerid=@BenutzerID,rate=@Rate,code=@Code where ID=@id",
                new { ID = rabatt.ID, BenutzerID = rabatt.BenutzerID, Code = rabatt.Code, Rate = rabatt.Rate }); 
            if(aktualisierenStatus>0)
            {
                return Antwort<KeinInhaltDüo>.Erfolg(204);
            }
            return Antwort<KeinInhaltDüo>.Fehlschlag("Rabatt wurde nicht gefunden!",404);
        }

        public async Task<Antwort<KeinInhaltDüo>> Löschen(int ID)
        {
            var löschenStatus = await _dbConnection.ExecuteAsync("Delete from rabatt where ID=@id",new { id = ID });
            return löschenStatus > 0 ? Antwort<KeinInhaltDüo>.Erfolg(204) :
                Antwort<KeinInhaltDüo>.Fehlschlag("Rabatt wurde nicht gefunden!", 404);


        }

        public async Task<Antwort<List<Modell.Rabatt>>> RufenAlleDatenAuf()
        {
            var rabatte = await _dbConnection.QueryAsync<Modell.Rabatt>("Select * from rabatt");
            return Antwort<List<Modell.Rabatt>>.Erfolg(rabatte.ToList(), 200);
        }

        public async Task<Antwort<Modell.Rabatt>> RufenNachCodeUndBenutzerIDAuf(string code, string benutzerID)
        {
            var rabatt = await _dbConnection.QueryAsync<Modell.Rabatt>
                ("select * from rabatt where benutzerid=@BenutzerID and code=@Code",
                new {BenutzerID=benutzerID,Code=code});
            var esgibtRabatt = rabatt.FirstOrDefault();
            if(esgibtRabatt==null)
            {
                return Antwort<Modell.Rabatt>.Fehlschlag("Rabatt wurde nicht gefunden!", 404);
            }
            return Antwort<Modell.Rabatt>.Erfolg(esgibtRabatt, 200);
        }

        public async Task<Antwort<Modell.Rabatt>> RufenNachIDAuf(int ID)
        {
            var rabatt = (await _dbConnection.QueryAsync<Modell.Rabatt>
                ("Select * from rabatt where ID=@id", new {id=ID})).SingleOrDefault();
            if(rabatt==null)
            {
                return Antwort<Modell.Rabatt>.Fehlschlag("Rabatt wurde nicht gefunden", 404);
            }
            return Antwort<Modell.Rabatt>.Erfolg(rabatt, 200);
        }

        public async Task<Antwort<KeinInhaltDüo>> Speichern(Modell.Rabatt rabatt)
        {
            var speichernStatus = await _dbConnection.ExecuteAsync
                ("Insert Into rabatt(benutzerid,rate,code)Values(@BenutzerID,@Rate,@Code)", rabatt);
            if(speichernStatus>0)
            {
                return Antwort<KeinInhaltDüo>.Erfolg(204);
            }
            return Antwort<KeinInhaltDüo>.Fehlschlag("Beim Hinzufügen ist ein Fehler aufgetreten!", 500);

        }
    }
}
