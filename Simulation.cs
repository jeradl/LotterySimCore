using System.Security.Cryptography;

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

        public (int, bool) RunGame(int[] numbers, int mega)
        {
            DrawNumbers();
            Result(numbers, mega);          

            return (_numbersMatched, _megaMatched);
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

            var game = new SimModel
            {
                NumbersDrawn = _winningNumbers,
                MegaDrawn = _winningMega,
                id = _simId,
                Result = _numbersMatched.ToString() + (_megaMatched == true ? "P" : "")
            };
          
        }
    }
}
