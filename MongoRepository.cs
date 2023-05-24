using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace LotterySimCore
{
    internal static class MongoRepository
    {
        private const string _connectionString = "mongodb://localhost:27017";
        private const string _databaseName = "LotterySimTest";
        private static readonly IMongoCollection<GameModel> _games;
        private static readonly IMongoCollection<SimulationModel> _sim;
        private static MongoClient _mongoClient = new MongoClient(_connectionString);
        private static IMongoDatabase _mongoDatabase = _mongoClient.GetDatabase(_databaseName);

        static MongoRepository()
        {
            _games = _mongoDatabase.GetCollection<GameModel>("Game");
            _sim = _mongoDatabase.GetCollection<SimulationModel>("Simulation");
        }
        
        internal async static void SaveGame(GameModel model)
        {
            await _games.InsertOneAsync(model);     
        }

        internal async static Task<string> CreateSimulation(SimulationModel model)
        {
            await _sim.InsertOneAsync(model);
            return model.id;
        }

        internal async static void UpdateSimulation(string id, SimulationModel sim)
        {
            await _sim.ReplaceOneAsync(r => r.id == id, sim);
        }
    }

    internal class GameModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public int[] NumbersDrawn { get; set; }
        public int MegaDrawn { get; set; }
        public string SimID { get; set; }
        public string Result { get; set; }
    }

    internal class SimulationModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public int Earned { get; set; }
        public int Spent { get; set; }
        public int GamesPlayed { get; set; }
        public int[] NumbersChosen { get; set; }
        public int MegaChosen { get; set; }
        public int[] TotalNumbersDrawn { get; set; }
        public int[] TotalMegaDrawn { get; set; }
        public List<ResultModel> AggregateResult { get; set; }
    }

    internal class AllSimulations
    {
        public int[] TotalNumbersDrawn { get; set; }
        public int Earnings { get; set; }
        public int Spent { get; set; }
        public int Played { get; set; }
        public List<ResultModel> AggregateResult { get; set; }
    }

    internal class ResultModel
    {
        public string Result { get; set; }
        public int Count { get; set; }
        public int ActualOdds { get; set; }
        public int TotalPrize { get; set; }
    }

    internal class Aggregates
    {

    }

}
