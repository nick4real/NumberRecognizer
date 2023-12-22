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
            Run(1, true);
        }

        private void Run(int count = 1, bool isRandomized = true, bool isTraining = true)
        {
            var directory = isTraining ? trainImages : testImages;

            if (isRandomized)
            {
                for (int i = 0; i < count; i++)
                {
                    var randomNum = NextNum(directory.Length);
                    var randomImage = directory[randomNum];

                    UpdateLog(randomImage);

                    if (isTraining)
                        listBox1.Items.Add(neuralNetwork.Train(randomImage));
                    else
                        listBox1.Items.Add(neuralNetwork.Test(randomImage));
                }
            }
            else
            {
                int i = 1;

                foreach (string png in directory)
                {
                    UpdateLog(png);

                    if (isTraining)
                        listBox1.Items.Add(neuralNetwork.Train(png));
                    else
                        listBox1.Items.Add(neuralNetwork.Test(png));

                    if (i++ >= count)
                    {
                        break;
                    }
                }
            }
        }
        private void UpdateLog(string image)
        {
            pictureBox1.Image = Image.FromFile(image);

            int visibleItems = listBox1.ClientSize.Height / listBox1.ItemHeight;
            listBox1.TopIndex = Math.Max(listBox1.Items.Count - visibleItems + 1, 0);
        }

        private void buttonNextTrain_Click(object sender, EventArgs e)
        {
            Run(1, true);
        }
        private void buttonAllTrain_Click(object sender, EventArgs e)
        {
            Run(1000, false);
        }
        private void buttonNextTest_Click(object sender, EventArgs e)
        {
            Run(1, true, false);
        }
        private void buttonAllTest_Click(object sender, EventArgs e)
        {
            Run(1000, false, false);
        }
        private void buttonTrainCount_Click(object sender, EventArgs e)
        {
            Run(Convert.ToInt32(textBox1.Text), true);
        }
        private void buttonTestCount_Click(object sender, EventArgs e)
        {
            Run(Convert.ToInt32(textBox2.Text), true, false);
        }

        private void keyPressEvent(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
