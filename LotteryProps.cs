using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotterySimCore
{
    public partial class Form1
    {
        //FiveM
        private int _fiveM;
        public event EventHandler FiveMChanged;
        public int FiveM
        {
            get { return _fiveM; }
            set
            {
                if (value != _fiveM)
                {
                    _fiveM = value;
                    EventHandler handler = FiveMChanged;
                    if (handler != null)
                        handler(this, EventArgs.Empty);
                }
            }
        }

        //Five
        private int _five;
        public event EventHandler FiveChanged;
        public int Five
        {
            get { return _five; }
            set
            {
                if (value != _five)
                {
                    _five = value;
                    EventHandler handler = FiveChanged;
                    if (handler != null)
                        handler(this, EventArgs.Empty);
                }
            }
        }

        //FourM
        private int _fourM;
        public event EventHandler FourMChanged;
        public int FourM
        {
            get { return _fourM; }
            set
            {
                if (value != _fourM)
                {
                    _fourM = value;
                    EventHandler handler = FourMChanged;
                    if (handler != null)
                        handler(this, EventArgs.Empty);
                }
            }
        }

        //Four
        private int _four;
        public event EventHandler FourChanged;
        public int Four
        {
            get { return _four; }
            set
            {
                if (value != _four)
                {
                    _four = value;
                    EventHandler handler = FourChanged;
                    if (handler != null)
                        handler(this, EventArgs.Empty);
                }
            }
        }

        //ThreeM
        private int _threeM;
        public event EventHandler ThreeMChanged;
        public int ThreeM
        {
            get { return _threeM; }
            set
            {
                if (value != _threeM)
                {
                    _threeM = value;
                    EventHandler handler = ThreeMChanged;
                    if (handler != null)
                        handler(this, EventArgs.Empty);
                }
            }
        }

        //Three
        private int _three;
        public event EventHandler ThreeChanged;
        public int Three
        {
            get { return _three; }
            set
            {
                if (value != _three)
                {
                    _three = value;
                    EventHandler handler = ThreeChanged;
                    if (handler != null)
                        handler(this, EventArgs.Empty);
                }
            }
        }

        //TwoM
        private int _twoM;
        public event EventHandler TwoMChanged;
        public int TwoM
        {
            get { return _twoM; }
            set
            {
                if (value != _twoM)
                {
                    _twoM = value;
                    EventHandler handler = TwoMChanged;
                    if (handler != null)
                        handler(this, EventArgs.Empty);
                }
            }
        }

        //OneM
        private int _oneM;
        public event EventHandler OneMChanged;
        public int OneM
        {
            get { return _oneM; }
            set
            {
                if (value != _oneM)
                {
                    _oneM = value;
                    EventHandler handler = OneMChanged;
                    if (handler != null)
                        handler(this, EventArgs.Empty);
                }
            }
        }

        //Mega
        private int _mega;
        public event EventHandler MegaChanged;
        public int Mega
        {
            get { return _mega; }
            set
            {
                if (value != _mega)
                {
                    _mega = value;
                    EventHandler handler = MegaChanged;
                    if (handler != null)
                        handler(this, EventArgs.Empty);
                }
            }
        }

        //Two
        private int _two;
        public event EventHandler TwoChanged;
        public int Two
        {
            get { return _two; }
            set
            {
                if (value != _two)
                {
                    _two = value;
                    EventHandler handler = TwoChanged;
                    if (handler != null)
                        handler(this, EventArgs.Empty);
                }
            }
        }

        //One
        private int _one;
        public event EventHandler OneChanged;
        public int One
        {
            get { return _one; }
            set
            {
                if (value != _one)
                {
                    _one = value;
                    EventHandler handler = OneChanged;
                    if (handler != null)
                        handler(this, EventArgs.Empty);
                }
            }
        }

        //Zero
        private int _zero;
        public event EventHandler ZeroChanged;
        public int Zero
        {
            get { return _zero; }
            set
            {
                if (value != _zero)
                {
                    _zero = value;
                    EventHandler handler = ZeroChanged;
                    if (handler != null)
                        handler(this, EventArgs.Empty);
                }
            }
        }

        //Earned
        private int _earned;
        public event EventHandler EarnedChanged;
        public int Earned
        {
            get { return _earned; }
            set
            {
                if (value != _earned)
                {
                    _earned = value;
                    EventHandler handler = EarnedChanged;
                    if (handler != null)
                        handler(this, EventArgs.Empty);
                }
            }
        }

        //Spent
        private int _spent;
        public event EventHandler SpentChanged;
        public int Spent
        {
            get { return _spent; }
            set
            {
                if (value != _spent)
                {
                    _spent = value;
                    EventHandler handler = SpentChanged;
                    if (handler != null)
                        handler(this, EventArgs.Empty);
                }
            }
        }

        //GamesPlayed
        private int _gamesPlayed;
        public event EventHandler GamesPlayedChanged;
        public int GamesPlayed
        {
            get { return _gamesPlayed; }
            set
            {
                if (value != _gamesPlayed)
                {
                    _gamesPlayed = value;
                    EventHandler handler = GamesPlayedChanged;
                    if (handler != null)
                        handler(this, EventArgs.Empty);
                }
            }
        }    
    }
}
