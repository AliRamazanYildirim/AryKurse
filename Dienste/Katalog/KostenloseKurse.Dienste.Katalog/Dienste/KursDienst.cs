using AutoMapper;
using KostenloseKurse.Dienste.Katalog.Düo;
using KostenloseKurse.Dienste.Katalog.Einstellungen;
using KostenloseKurse.Dienste.Katalog.Modelle;
using KostenloseKurse.Shared.Düo;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KostenloseKurse.Dienste.Katalog.Dienste
{
    public class KursDienst:IKursDienst
    {
        private readonly IMongoCollection<Kurs> _kursCollection;
        private readonly IMongoCollection<Kategorie> _kategorieCollection;
        private readonly IMapper _mapper;

        public KursDienst(IMapper mapper,IDatenbankEinstellungen datenbankEinstellungen)
        {
            var client = new MongoClient(datenbankEinstellungen.AnschlussString);
            var datenbank = client.GetDatabase(datenbankEinstellungen.DatenbankName);
            _kursCollection = datenbank.GetCollection<Kurs>(datenbankEinstellungen.KursKollektionName);
            _kategorieCollection = datenbank.GetCollection<Kategorie>(datenbankEinstellungen.KategorieKollektionName);
            _mapper = mapper;
        }

        public async Task<Antwort<List<KursDüo>>> RufAlleDatenAsync()
        {
            var kurse = await _kursCollection.Find(kurs => true).ToListAsync();
          
            if(kurse.Any())
            {
                foreach (var kurs in kurse)
                {
                    kurs.Kategorie = await _kategorieCollection.Find<Kategorie>(x => x.ID == kurs.KategorieID).FirstAsync();
                }
            }
            else
            {
                kurse = new List<Kurs>();
            }
            return Antwort<List<KursDüo>>.Erfolg(_mapper.Map<List<KursDüo>>(kurse), 200);
    

        }

        public async Task<Antwort<KursDüo>> RufZurIDAsync(string ID)
        {
            var kurs = await _kursCollection.Find<Kurs>(x => x.ID == ID).FirstOrDefaultAsync();
            if (kurs == null)
            {
                return Antwort<KursDüo>.Fehlschlagen("Kurs wurde nicht gefunden.", 404);
            }
            kurs.Kategorie = await _kategorieCollection.Find<Kategorie>(x => x.ID == kurs.KategorieID).FirstAsync();
            return Antwort<KursDüo>.Erfolg(_mapper.Map<KursDüo>(kurs), 200);

        }
        public async Task<Antwort<List<KursDüo>>> RufZurBenutzerIDAsync(string benutzerID)
        {
            var kurse = await _kursCollection.Find<Kurs>(x => x.BenutzerID == benutzerID).ToListAsync();
            if (kurse.Any())
            {
                foreach (var kurs in kurse)
                {
                    kurs.Kategorie = await _kategorieCollection.Find<Kategorie>(x => x.ID == kurs.KategorieID).FirstAsync();
                }
            }
            else
            {
                kurse = new List<Kurs>();
            }
            return Antwort<List<KursDüo>>.Erfolg(_mapper.Map<List<KursDüo>>(kurse), 200);
        }
        public async Task<Antwort<KursDüo>> ErstellenAsync(KursErstellenDüo kursErstellenDüo)
        {
            var neuerKurs=_mapper.Map<Kurs>(kursErstellenDüo);
            neuerKurs.Erstellungsdatum = DateTime.Now;
            await _kursCollection.InsertOneAsync(neuerKurs);
            return Antwort<KursDüo>.Erfolg(_mapper.Map<KursDüo>(neuerKurs), 200);
        }
        public async Task<Antwort<KeinInhaltDüo>>AktualisierenAsync(KursAktualisierenDüo kursAktualisierenDüo)
        {
            var aktualisierenKurs = _mapper.Map<Kurs>(kursAktualisierenDüo);
            var resultat = await _kursCollection.FindOneAndReplaceAsync(x => x.ID == kursAktualisierenDüo.ID, aktualisierenKurs);
            if(resultat==null)
            {
                return Antwort<KeinInhaltDüo>.Fehlschlagen("Kurs wurde nicht gefunden.",404);
            }
            return Antwort<KeinInhaltDüo>.Erfolg(204);
        }
        public async Task<Antwort<KeinInhaltDüo>> LöschenAsync(string ID)
        {
            var resultat = await _kursCollection.DeleteOneAsync(x => x.ID==ID);
            if(resultat.DeletedCount>0)
            {
                return Antwort<KeinInhaltDüo>.Erfolg(204);
            }
            else
            {
                return Antwort<KeinInhaltDüo>.Fehlschlagen("Kurs wurde nicht gefunden", 404);
            }
        }
    }
}
