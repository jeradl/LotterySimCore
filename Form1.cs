using System.ComponentModel;
using System.Security.Cryptography;

namespace LotterySimCore
{
    public partial class Form1 : Form
    {
        BackgroundWorker bw = new BackgroundWorker();
        bool running = false;
        int rounds;
        int megaNumber;
        int[] chosenNumbers = new int[5];

        public Form1()
        {
            InitializeComponent();
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += bw_DoWork;
            bw.ProgressChanged += bw_ProgressChanged!;
        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            simProgress.Value = e.ProgressPercentage;
            GamesPlayed++;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //set up initial random chosen numbers
            GenerateNew();

            //set up field data bindings
            txtFiveM.DataBindings.Add("Text", this, "FiveM");
            txtFive.DataBindings.Add("Text", this, "Five");
            txtFourM.DataBindings.Add("Text", this, "FourM");
            txtFour.DataBindings.Add("Text", this, "Four");
            txtThreeM.DataBindings.Add("Text", this, "ThreeM");
            txtThree.DataBindings.Add("Text", this, "Three");
            txtTwoM.DataBindings.Add("Text", this, "TwoM");
            txtOneM.DataBindings.Add("Text", this, "OneM");
            txtMega.DataBindings.Add("Text", this, "Mega");
            txtTwo.DataBindings.Add("Text", this, "Two");
            txtOne.DataBindings.Add("Text", this, "One");
            txtZero.DataBindings.Add("Text", this, "Zero");
            txtEarned.DataBindings.Add("Text", this, "Earned");
            txtSpent.DataBindings.Add("Text", this, "Spent");
            txtGamesPlayed.DataBindings.Add("Text", this, "GamesPlayed");
        }

        private void btnStartStop_Click(object sender, EventArgs e)
        {
            if (!bw.IsBusy)
            {
                btnStartStop.Text = "Stop";
                rounds = int.Parse(txtNumberToRun.Text);
                megaNumber = int.Parse(txtNumMega.Text);
                chosenNumbers[0] = int.Parse(txtNum1.Text);
                chosenNumbers[1] = int.Parse(txtNum2.Text);
                chosenNumbers[2] = int.Parse(txtNum3.Text);
                chosenNumbers[3] = int.Parse(txtNum4.Text);
                chosenNumbers[4] = int.Parse(txtNum5.Text);
                btnReset.Enabled = false;
                
                bw.RunWorkerAsync();
            }
            else
            {
                bw.CancelAsync();

                btnReset.Enabled = true;
                btnStartStop.Text = "Start";
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetUI();
        }

        private void btnGenerateNew_Click(object sender, EventArgs e)
        {
            GenerateNew();
        }

        private void GenerateNew()
        {
            int[] initNums = Enumerable.Range(1, 70).OrderBy(o => RandomNumberGenerator.GetInt32(1, 71)).Take(5).ToArray();
            int initMega = RandomNumberGenerator.GetInt32(1, 26);
            txtNumMega.Text = initMega.ToString();
            txtNum1.Text = initNums[0].ToString();
            txtNum2.Text = initNums[1].ToString();
            txtNum3.Text = initNums[2].ToString();
            txtNum4.Text = initNums[3].ToString();
            txtNum5.Text = initNums[4].ToString();
        }

        private void ResetUI()
        {
            FiveM = 0;
            Five = 0;
            FourM = 0;
            Four = 0;
            ThreeM = 0;
            Three = 0;
            TwoM = 0;
            OneM = 0;
            Mega = 0;
            Two = 0;
            One = 0;
            Zero = 0;
            simProgress.Value = 0;
            GamesPlayed = 0;
            Spent = 0;
            Earned = 0;
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            Simulation sim = new Simulation();

            for (int i = 1; i <= rounds; i++)
            {
                if (bw.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                var results = sim.RunGame(chosenNumbers, megaNumber);
                HandleResults(results.Item1, results.Item2);
                bw.ReportProgress(i * 100 / rounds);
            }
        }

        private void HandleResults(int matchCount, bool mega)
        {
            Invoke(() =>
            {
                Spent += 2;

                switch (matchCount)
                {
                    case 5:
                        Matched5(mega);
                        break;
                    case 4:
                        Matched4(mega);
                        break;
                    case 3:
                        Matched3(mega);
                        break;
                    case 2:
                        Matched2(mega);
                        break;
                    case 1:
                        Matched1(mega);
                        break;
                    case 0:
                        Matched0(mega);
                        break;
                    default:
                        break;
                }
            });
        }

        private void Matched5(bool mega)
        {
            if (mega)
            {
                FiveM++;
                Earned += (int)PrizeAmounts.Jackpot;
            }
            else
            {
                Five++;
                Earned += (int)PrizeAmounts.Five;
            }
        }

        private void Matched4(bool mega)
        {
            if (mega)
            {
                FourM++;
                Earned += (int)PrizeAmounts.FourM;
            }
            else
            {
                Four++;
                Earned += (int)PrizeAmounts.Four;
            }
        }

        private void Matched3(bool mega)
        {
            if (mega)
            {
                ThreeM++;
                Earned += (int)PrizeAmounts.ThreeM;
            }
            else
            {
                Three++;
                Earned += (int)PrizeAmounts.Three;
            }
        }

        private void Matched2(bool mega)
        {
            if (mega)
            {
                TwoM++;
                Earned += (int)PrizeAmounts.TwoM;
            }
            else
            {
                Two++;
                Earned += (int)PrizeAmounts.Two;
            }
        }

        private void Matched1(bool mega)
        {
            if (mega)
            {
                OneM++;
                Earned += (int)PrizeAmounts.OneM;
            }
            else
            {
                One++;
                Earned += (int)PrizeAmounts.One;
            }
        }

        private void Matched0(bool mega)
        {
            if (mega)
            {
                Mega++;
                Earned += (int)PrizeAmounts.Mega;
            }
            else
            {
                Zero++;
                Earned += (int)PrizeAmounts.Zero;
            }
        }

        private enum PrizeAmounts
        {
            Jackpot = 25000000,
            Five = 1000000,
            FourM = 10000,
            Four = 500,
            ThreeM = 200,
            Three = 10,
            TwoM = 10,
            OneM = 4,
            Mega = 2,
            Two = 0,
            One = 0,
            Zero = 0
        }
    }
}