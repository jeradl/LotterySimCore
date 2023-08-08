using System.Runtime.CompilerServices;
using System.Security.Cryptography;

using MongoDB.Bson;

namespace LotterySimCore
{
    internal class Simulation
    {     
        int[] _winningNumbers = new int[5];
        int _winningMega;
        internal string _simId;

        //return these to caller
        int _numbersMatched;
        bool _megaMatched;

        SimModel sim = new SimModel
        {
            id = ObjectId.GenerateNewId().ToString()
        };
        //SimModel sim = new();

        public Simulation()
        {
            _simId = sim.id;
        }

        public (int, bool) RunGame(int[] numbers, int mega)
        {
            DrawNumbers();
            Result(numbers, mega);          
            
            return (_numbersMatched, _megaMatched);
        }

        public void EndGame(int spent, int earned)
        {
            sim.Spent = spent;
            sim.Earned = earned;
            MongoRepository.SaveSim(sim);
        }

        private void DrawNumbers()
        {
            _winningNumbers = Enumerable.Range(1, 70).OrderBy(o => RandomNumberGenerator.GetInt32(1, 71)).Take(5).ToArray();
            _winningMega = RandomNumberGenerator.GetInt32(1, 26);            
        }

        private void Result(int[] chosenNumbers, int chosenMega)
        {
            _megaMatched = (_winningMega == chosenMega);
            _numbersMatched = _winningNumbers.Intersect(chosenNumbers).Count();

            var gameSummary = new GameModel
            {
                N1Chosen = chosenNumbers[0],
                N2Chosen = chosenNumbers[1],
                N3Chosen = chosenNumbers[2],
                N4Chosen = chosenNumbers[3],
                N5Chosen = chosenNumbers[4],
                MegaChosen = chosenMega,
                N1Drawn = _winningNumbers[0],
                N2Drawn = _winningNumbers[1],
                N3Drawn = _winningNumbers[2],
                N4Drawn = _winningNumbers[3],
                N5Drawn = _winningNumbers[4],
                MegaDrawn = _winningMega,
                Result = _numbersMatched.ToString() + (_megaMatched == true ? "M" : ""),
                SimId = _simId
            };

            sim.NumbersDistro[_winningNumbers[0]]++;
            sim.NumbersDistro[_winningNumbers[1]]++;
            sim.NumbersDistro[_winningNumbers[2]]++;
            sim.NumbersDistro[_winningNumbers[3]]++;
            sim.NumbersDistro[_winningNumbers[4]]++;

            sim.MegaDistro[_winningMega]++;

            sim.Results[gameSummary.Result]++;

            MongoRepository.SaveGame(gameSummary);      
        }
    }
}
