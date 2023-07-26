using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace LotterySimCore
{
    internal static class MongoRepository
    {
        private const string _connectionString = "mongodb://localhost:27017";
        private const string _databaseName = "Simulation";
        private static readonly IMongoCollection<SimModel> _sim;
        private static MongoClient _mongoClient = new MongoClient(_connectionString);
        private static IMongoDatabase _mongoDatabase = _mongoClient.GetDatabase(_databaseName);

        static MongoRepository()
        {
            _sim = _mongoDatabase.GetCollection<SimModel>("Games");        
        }`

        internal async static Task SaveGame(SimModel model)
        {
            await _sim.InsertOneAsync(model);
        }
    }

    internal class SimModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public int Earned { get; set; }
        public int Spent { get; set; }
        public int GamesPlayed { get; set; }
        public int[] NumbersChosen { get; set; }
        public int[] NumbersDrawn { get; set; }
        public int MegaChosen { get; set; }
        public int MegaDrawn { get; set; }
        public string Result { get; set; }
    }
}