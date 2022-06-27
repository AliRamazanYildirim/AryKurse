using AutoMapper;
using KostenloseKurse.Dienste.Katalog.Düo;
using KostenloseKurse.Dienste.Katalog.Einstellungen;
using KostenloseKurse.Dienste.Katalog.Modelle;
using KostenloseKurse.Shared.Düo;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KostenloseKurse.Dienste.Katalog.Dienste
{
    public class KategorieDienst:IKategorieDienst
    {
        private readonly IMongoCollection<Kategorie> _kategorieCollection;
        private readonly IMapper _mapper;

        public KategorieDienst(IMapper mapper,IDatenbankEinstellungen datenbankEinstellungen)
        {
            var client=new MongoClient(datenbankEinstellungen.AnschlussString);
            var datenbank=client.GetDatabase(datenbankEinstellungen.DatenbankName);
            _kategorieCollection = datenbank.GetCollection<Kategorie>(datenbankEinstellungen.KategorieKollektionName);
            _mapper = mapper;
        }
        public async Task<Antwort<List<KategorieDüo>>>RufAlleDatenAsync()
        {
            var kategorien = await _kategorieCollection.Find(kategorie => true).ToListAsync();
            return Antwort<List<KategorieDüo>>.Erfolg(_mapper.Map<List<KategorieDüo>>(kategorien), 200);
        }
        public async Task<Antwort<KategorieDüo>>ErstellenAsync(Kategorie kategorie)
        {
            await _kategorieCollection.InsertOneAsync(kategorie);
            return Antwort<KategorieDüo>.Erfolg(_mapper.Map<KategorieDüo>(kategorie), 200);
        }
        public async Task<Antwort<KategorieDüo>>RufZurIDAsync(string ID)
        {
            var kategorie = await _kategorieCollection.Find<Kategorie>(x => x.ID == ID).FirstOrDefaultAsync();
            if(kategorie==null)
            {
                return Antwort<KategorieDüo>.Fehlschlagen("Kategorie wurde nicht gefunden.", 404);
            }
            return Antwort<KategorieDüo>.Erfolg(_mapper.Map<KategorieDüo>(kategorie), 200);
            
        }
    }
}
