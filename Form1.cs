namespace NumberRecognizer
{
    public partial class Form1 : Form
    {
        // Download link with MNIST dataset:
        // https://pjreddie.com/media/files/mnist_test.tar.gz
        // https://pjreddie.com/media/files/mnist_train.tar.gz

        private const string TestPath = @"C:\Photo\test";
        private const string TrainPath = @"C:\Photo\train";

        private readonly string[] testImages = Directory.GetFiles(TestPath);
        private readonly string[] trainImages = Directory.GetFiles(TrainPath);

        private readonly NeuralNetwork neuralNetwork = new();
        private Random rnd = new Random();

        private delegate int NextNumDelegate(int n = 1);
        private NextNumDelegate NextNum;

        public Form1()
        {
            InitializeComponent();
            NextNum = rnd.Next;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void Run(int count = 1, bool isRandomized = true, bool isTraining = false)
        {
            var directory = isTraining ? trainImages : testImages;

            if (isRandomized)
            {
                for (int i = 0; i < count; i++)
                {
                    var randomNum = NextNum(directory.Length);
                    var randomImage = directory[randomNum];

                    UpdateLog(randomImage);
                    ClearLog();

                    listBox1.Items.Add(neuralNetwork.Start(randomImage, isTraining));
                }
            }
            else
            {
                int i = 1;

                foreach (string png in directory)
                {
                    UpdateLog(png);
                    ClearLog();

                    listBox1.Items.Add(neuralNetwork.Start(png, isTraining));

                    if (i++ >= count)
                    {
                        break;
                    }
                }
            }

            GC.Collect();
        }
        private void UpdateLog(string image)
        {
            pictureBox1.Image = Image.FromFile(image);

            int visibleItems = listBox1.ClientSize.Height / listBox1.ItemHeight;
            listBox1.TopIndex = Math.Max(listBox1.Items.Count - visibleItems + 1, 0);
        }
        private void ClearLog(bool isCheckingCount = true)
        {
            if (!isCheckingCount || listBox1.Items.Count > 1000)
            {
                neuralNetwork.SaveCondition();
                listBox1.Items.Clear();
            }
        }

        private void buttonNextTrain_Click(object sender, EventArgs e)
        {
            Run(1, true, true);
        }
        private void buttonAllTrain_Click(object sender, EventArgs e)
        {
            Run(trainImages.Length, false, true);
        }
        private void buttonNextTest_Click(object sender, EventArgs e)
        {
            Run(1, true);
        }
        private void buttonAllTest_Click(object sender, EventArgs e)
        {
            Run(testImages.Length, false);
        }
        private void buttonTrainCount_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox1.Text))
                Run(Convert.ToInt32(textBox1.Text), true, true);
        }
        private void buttonTestCount_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox2.Text))
                Run(Convert.ToInt32(textBox2.Text), true);
        }
        private void buttonChangeLR_Click(object sender, EventArgs e)
        {
            neuralNetwork.LearningRate = (float)Math.Round(1.0f / trackBarLR.Value, 4);
        }
        private void trackBarLR_ValueChanged(object sender, EventArgs e)
        {
            label4.Text = Convert.ToString(Math.Round(1.0f / trackBarLR.Value, 4));
        }

        private void keyPressEvent(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void resetAcceptanceRateToolStripMenuItem_Click(object sender, EventArgs e) => neuralNetwork.ResetAC();
        private void newWeightsToolStripMenuItem_Click(object sender, EventArgs e) => neuralNetwork.GenerateCondition();
    }
}
