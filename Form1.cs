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
            bw.RunWorkerCompleted += bw_SimCompleted;
        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            simProgress.Value = e.ProgressPercentage;
            GamesPlayed++;
        }

        private void bw_SimCompleted(object sender, EventArgs e)
        {
            txtSpent.Text = $"${Spent:n0}";
            txtEarned.Text = $"${Earned:n0}";
            GetActuals();
            btnStartStop.Text = "Start";
            btnReset.Enabled = true;
            lblSpendEarningRatio.Text = $"$1 spent for every ${(double)Earned / (double)Spent:n} earned";
            lblEarningSpendRatio.Text = $"$1 earned for every ${(double)Spent / (double)Earned:n} spent";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //set up initial random chosen numbers
            GenerateNew();

            //set up field data bindings
            txtFiveM.DataBindings.Add("Text", this, "FiveM", true, DataSourceUpdateMode.OnPropertyChanged, null, "n0");
            txtFive.DataBindings.Add("Text", this, "Five", true, DataSourceUpdateMode.OnPropertyChanged, null, "n0");
            txtFourM.DataBindings.Add("Text", this, "FourM", true, DataSourceUpdateMode.OnPropertyChanged, null, "n0");
            txtFour.DataBindings.Add("Text", this, "Four", true, DataSourceUpdateMode.OnPropertyChanged, null, "n0");
            txtThreeM.DataBindings.Add("Text", this, "ThreeM", true, DataSourceUpdateMode.OnPropertyChanged, null, "n0");
            txtThree.DataBindings.Add("Text", this, "Three", true, DataSourceUpdateMode.OnPropertyChanged, null, "n0");
            txtTwoM.DataBindings.Add("Text", this, "TwoM", true, DataSourceUpdateMode.OnPropertyChanged, null, "n0");
            txtOneM.DataBindings.Add("Text", this, "OneM", true, DataSourceUpdateMode.OnPropertyChanged, null, "n0");
            txtMega.DataBindings.Add("Text", this, "Mega", true, DataSourceUpdateMode.OnPropertyChanged, null, "n0");
            txtTwo.DataBindings.Add("Text", this, "Two", true, DataSourceUpdateMode.OnPropertyChanged, null, "n0");
            txtOne.DataBindings.Add("Text", this, "One", true, DataSourceUpdateMode.OnPropertyChanged, null, "n0");
            txtZero.DataBindings.Add("Text", this, "Zero", true, DataSourceUpdateMode.OnPropertyChanged, null, "n0");
            txtEarned.DataBindings.Add("Text", this, "Earned", true, DataSourceUpdateMode.OnPropertyChanged, null, "n0");
            txtSpent.DataBindings.Add("Text", this, "Spent", true, DataSourceUpdateMode.OnPropertyChanged, null, "n0");
            txtGamesPlayed.DataBindings.Add("Text", this, "GamesPlayed", true, DataSourceUpdateMode.OnPropertyChanged, null, "n0");
        }

        private void GetActuals()
        {
            lblOdds5MA.Text = FiveM > 0 ? $"1 in {Math.Round(((double)GamesPlayed / (double)FiveM), 2):n}" : "N/A";
            lblPrize5M.Text = $"${FiveM * (int)PrizeAmounts.Jackpot:n0}";

            lblOdds5A.Text = Five > 0 ? $"1 in {Math.Round(((double)GamesPlayed / (double)Five), 2):n}" : "N/A";
            lblPrize5.Text = $"${Five * (int)PrizeAmounts.Five:n0}";

            lblOdds4MA.Text = FourM > 0 ? $"1 in {Math.Round(((double)GamesPlayed / (double)FourM), 2):n}" : "N/A";
            lblPrize4M.Text = $"${FourM * (int)PrizeAmounts.FourM:n0}";

            lblOdds4A.Text = Four > 0 ? $"1 in {Math.Round(((double)GamesPlayed / (double)Four), 2):n}" : "N/A";
            lblPrize4.Text = $"${Four * (int)PrizeAmounts.Four:n0}";

            lblOdds3MA.Text = ThreeM > 0 ? $"1 in {Math.Round(((double)GamesPlayed / (double)ThreeM), 2):n}" : "N/A";
            lblPrize3M.Text = $"${ThreeM * (int)PrizeAmounts.ThreeM:n0}";

            lblOdds3A.Text = Three > 0 ? $"1 in {Math.Round(((double)GamesPlayed / (double)Three), 2):n}" : "N/A";
            lblPrize3.Text = $"${Three * (int)PrizeAmounts.Three:n0}";

            lblOdds2MA.Text = TwoM > 0 ? $"1 in {Math.Round(((double)GamesPlayed / (double)TwoM), 2):n}" : "N/A";
            lblPrize2M.Text = $"${TwoM * (int)PrizeAmounts.TwoM:n0}";

            lblOdds1MA.Text = OneM > 0 ? $"1 in {Math.Round(((double)GamesPlayed / (double)OneM), 2):n}" : "N/A";
            lblPrize1M.Text = $"${OneM * (int)PrizeAmounts.OneM:n0}";

            lblOddsMegaA.Text = Mega > 0 ? $"1 in {Math.Round(((double)GamesPlayed / (double)Mega), 2):n}" : "N/A";
            lblPrizeMega.Text = $"${Mega * (int)PrizeAmounts.Mega:n0}";

            lblOdds2A.Text = Two > 0 ? $"1 in {Math.Round(((double)GamesPlayed / (double)Two), 2):n}" : "N/A";

            lblOdds1A.Text = One > 0 ? $"1 in {Math.Round(((double)GamesPlayed / (double)One), 2):n}" : "N/A";

            lblOddsZeroA.Text = Zero > 0 ? $"1 in {Math.Round(((double)GamesPlayed / (double)Zero), 2):n}" : "N/A";
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
            lblPrize3M.Text = String.Empty;
            lblOdds3A.Text = String.Empty;
            lblPrize3.Text = String.Empty;
            lblOdds2MA.Text = String.Empty;
            lblPrize2M.Text = String.Empty;
            lblOdds1MA.Text = String.Empty;
            lblPrize1M.Text = String.Empty;
            lblOddsMegaA.Text = String.Empty;
            lblPrizeMega.Text = String.Empty;
            lblOdds2A.Text = String.Empty;
            lblOdds1A.Text = String.Empty;
            lblOddsZeroA.Text = String.Empty;
            lblEarningSpendRatio.Text = String.Empty;
            lblSpendEarningRatio.Text = String.Empty;
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            Simulation sim = new Simulation();
            SimModel simModel = new SimModel();
            
            //MongoRepository.CreateSim(simModel);

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

            simModel.Earned = Earned;
            simModel.Spent = Spent;
            simModel.GamesPlayed = GamesPlayed;
            simModel.NumbersChosen = new int[5]; 
            simModel.NumbersChosen[0] = int.Parse(txtNum1.Text);
            simModel.NumbersChosen[1] = int.Parse(txtNum2.Text);
            simModel.NumbersChosen[2] = int.Parse(txtNum3.Text);
            simModel.NumbersChosen[3] = int.Parse(txtNum4.Text);
            simModel.NumbersChosen[4] = int.Parse(txtNum5.Text);
            simModel.MegaChosen = int.Parse(txtNumMega.Text);
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

        private void lblFourM_Click(object sender, EventArgs e)
        {

        }

        private void lblPrize4M_Click(object sender, EventArgs e)
        {
        }

        private void lblOdds4MA_Click(object sender, EventArgs e)
        {
        }

        private void label8_Click(object sender, EventArgs e)
        {
        }

        private void lblOdds4M_Click(object sender, EventArgs e)
        {
        }

        private void txtFourM_TextChanged(object sender, EventArgs e)
        {
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