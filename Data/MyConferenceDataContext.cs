using MongoDB.Driver;
using MongoDB.Driver.Core.Events;
using MyConferece.Data.Entities;

namespace MyConferece.Data
{
    public class MyConferenceDataContext
    {
        public IMongoCollection<Speaker> Speakers => _database.GetCollection<Speaker>("speaker");
        public IMongoCollection<Session> Sessions => _database.GetCollection<Session>("sessions");

        private readonly IMongoDatabase _database;

        public MyConferenceDataContext()
        {
            var settings = MongoClientSettings.FromConnectionString("mongodb://localhost:27017");
            settings.ClusterConfigurator = builder => builder.Subscribe<CommandStartedEvent>(started =>
            {
                Console.WriteLine("MongoDB Command: " + started.Command);
            });
            var client = new MongoClient(settings);
            _database = client.GetDatabase("MyConference");

            if (!client.ListDatabaseNames().ToList().Contains("MyConference"))
            {
                Task.Run(async () => await SeedData());
            };
        }

        private async Task SeedData()
        {
            var speaker = new Speaker
            {
                Name = "Gregor Biswanger",
                Description = "Freier Trainer, Berater, Speaker und Autor",
            };

            var speaker2 = new Speaker
            {
                Name = "Robert Mühsig",
                Description = "Softwareentwickler",
            };

            await Speakers.InsertOneAsync(speaker);
            await Speakers.InsertOneAsync(speaker2);

            var session = new Session
            {
                Title = "GraphQL für ASP.NET Core Entwickler",
                Description = "Klassische RESTful Web-Services haben eine Vielzahl an Ressourcen, die einige Abfragen abverlangen, bis die notwendigen Daten zur Verfügung stehen. Ein weiteres Problem ist oft, das zu viele Informationen geliefert werden, die überhaupt nicht Notwendig sind. Bei GraphQL handelt es sich um eine Open-Source Abfragesprache, die nur einen Endpunkt enthält. Diese gibt ebenfalls nur die Daten in einer Form zurück, die genau zu Ihrer datenintensiven Anwendung passt. Das sorgt für eine sehr leistungsfähige API. Gregor Biswanger zeigt in seinem Vortrag, was sich hinter GraphQL verbirgt und wie diese in ASP.NET Core implementiert wird.",
                Category = CategoryType.WEB_BACKEND,
                Level = LevelType.Intermediate,
                FromTime = "15:00",
                ToTime = "15:45",
                Duration = "45",
                Date = new DateTime(2022, 12, 08),
                Approved = false,
                SpeakerIds = new List<string> { speaker.Id }
            };

            var session2 = new Session
            {
                Title = "Electron.NET: Cross-Platform Desktop Software mit ASP.NET Core",
                Description = "HTML5 ist überall - im Web, Mobile und natürlich auch auf den Desktop. Die große Stärke an HTML5 ist nicht nur, dass diese Plattform übergreifend unterstützt wird, sondern dass es immer mehr Features aus der Desktop-Welt bietet. Dennoch erfordert die Entwicklung von Desktop Anwendungen auf Basis von HTML & JavaScript neue Frameworks und Sprachen. Das Open Source Projekt Electron.NET verbindet ihr bekanntes C# & ASP.NET Core KnowHow mit den Möglichkeiten von Electron. In Kombination von C# und HTML5 können hoch performante Desktop Geschäftsanwendung für Windows, Mac und Linux entwickelt werden. Sie steigen mit den Grundlagen von Electron.NET ein und werden dann mit den wichtigsten Tools und Vorgehensweisen vertraut gemacht. Mit diesen Infos steigen Sie rasch zum versierten Cross-Platform Entwickler mit .NET auf.",
                Category = CategoryType.DOTNET_FRONTEND,
                Level = LevelType.Intermediate,
                FromTime = "17:30",
                ToTime = "18:15",
                Duration = "45",
                Date = new DateTime(2022, 12, 08),
                Approved = true,
                SpeakerIds = new List<string> { speaker.Id, speaker2.Id }
            };

            var session3 = new Session
            {
                Title = "Microservices mit ASP.NET Core implementieren",
                Description = "Bei der Entwicklung von Software wurden in den vergangenen Jahren unterschiedliche Architekturmuster gehyped. Mal waren es die Schichtenarchitekturen, die all unsere Probleme lösen sollten und nun sollen es Microservices richten. Aber haben wir mit Microservices wirklich den richtigen Architekturansatz gefunden, um komplexe Softwaresysteme zu beherrschen? Was steckt eigentlich hinter der Idee von Microservices? Wie kann man diese zum Beispiel mit ASP.NET Core implementieren? Diesen spannenden Fragen wird der ASP.NET Core- und Microservice-Experte Gregor Biswanger in seiner Session mit Erfahrungen aus wirklichen Projekten nachgehen.",
                Category = CategoryType.WEB_BACKEND,
                Level = LevelType.Advanced,
                FromTime = "14:00",
                ToTime = "18:00",
                Duration = "240",
                Date = new DateTime(2022, 12, 09),
                Approved = true,
                SpeakerIds = new List<string> { speaker.Id }
            };

            await Sessions.InsertOneAsync(session);
            await Sessions.InsertOneAsync(session2);
            await Sessions.InsertOneAsync(session3);

            var updateSpeaker = Builders<Speaker>.Update
                .PushEach(speaker => speaker.SessionIds, new List<string> { session.Id, session2.Id, session3.Id });

            await Speakers.UpdateOneAsync(x => x.Id == speaker.Id, updateSpeaker);

            var updateSpeaker2 = Builders<Speaker>.Update
                .Push(speaker => speaker.SessionIds, session2.Id);

            await Speakers.UpdateOneAsync(x => x.Id == speaker2.Id, updateSpeaker2);
        }
    }
}