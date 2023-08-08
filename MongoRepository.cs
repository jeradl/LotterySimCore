using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace LotterySimCore
{
    internal static class MongoRepository
    {
        private const string _connectionString = "mongodb://localhost:27017";
        private const string _databaseName = "Simulation";
        public static List<GameModel> sim;
        private static readonly IMongoCollection<GameModel> _game;
        private static readonly IMongoCollection<SimModel> _sim;
        private static MongoClient _mongoClient = new MongoClient(_connectionString);
        private static IMongoDatabase _mongoDatabase = _mongoClient.GetDatabase(_databaseName);

        static MongoRepository()
        {
            _game = _mongoDatabase.GetCollection<GameModel>("Games");
            _sim = _mongoDatabase.GetCollection<SimModel>("Sim");
        }

        internal static void SaveGame(GameModel model)
        {
            _game.InsertOneAsync(model);
        }

        internal static string SaveSim(SimModel sim)
        {
            _sim.InsertOneAsync(sim);

            return sim.id;
        }

        internal static void UpdateSim(SimModel sim)
        {
            var filter = Builders<SimModel>.Filter.Eq("id", sim.id);
            var update = Builders<SimModel>.Update.Set("MegaDistro", sim.MegaDistro)
                .Set("NumbersDistro", sim.NumbersDistro)
                .Set("Results", sim.Results)
                .Set("Earned", sim.Earned)
                .Set("Spent", sim.Spent);

            _sim.UpdateOneAsync(filter, update);
        }
    }

    internal class SimModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public int Earned { get; set; }
        public int Spent { get; set; }
        public int[] NumbersDistro { get; set; }
        public int[] MegaDistro { get; set; }
        public bool RandomizeChosen { get; set; }
        public int[] ChosenDistro { get; set; }
        public int[] ChosenMegaDistro { get; set; }
        public Dictionary<string, int> Results { get; set; }   

        public SimModel()
        {
            NumbersDistro = new int[71];
            MegaDistro = new int[26];

            Results = new Dictionary<string, int>
            {
                { "5M", 0 },
                { "5", 0 },
                { "4M", 0 },
                { "4", 0 },
                { "3M", 0 },
                { "3", 0 },
                { "2M", 0 },
                { "2", 0 },
                { "1M", 0 },
                { "1", 0 },
                { "0M", 0 },
                { "0", 0 }
            };
        }
    }

    internal class GameModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public string SimId { get; set; }
        public int N1Chosen { get; set; }
        public int N2Chosen { get; set; }
        public int N3Chosen { get; set; }
        public int N4Chosen { get; set; }
        public int N5Chosen { get; set; }
        public int N1Drawn { get; set; }
        public int N2Drawn { get; set; }
        public int N3Drawn { get; set; }
        public int N4Drawn { get; set; }
        public int N5Drawn { get; set; }
        public int MegaChosen { get; set; }
        public int MegaDrawn { get; set; }
        public string Result { get; set; }
    }
}