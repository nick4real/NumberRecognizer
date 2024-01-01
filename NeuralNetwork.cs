using Newtonsoft.Json;

namespace NumberRecognizer
{
    public class NeuralNetwork
    {
        private readonly string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "arrays.json");
        private readonly Random random = new Random();

        private float[] neuronInput = new float[28 * 28];
        private float[] neuronHiddenColumn1 = new float[16];
        private float[] neuronHiddenColumn2 = new float[16];
        private float[] neuronOutput = new float[10];

        private float[][] neuronHiddenColumn1Weights = new float[16][];
        private float[][] neuronHiddenColumn2Weights = new float[16][];
        private float[][] neuronOutputWeights = new float[10][];

        private float[] neuronHiddenColumn1Biases = new float[16];
        private float[] neuronHiddenColumn2Biases = new float[16];
        private float[] neuronOutputBiases = new float[10];

        public float LearningSpeed { get; set; }

        private int accepted;
        private int total;
        public float AcceptanceRateProcentes
        {
            get
            {
                return (float)accepted / total * 100.0f;
            }
        }

        public NeuralNetwork()
        {
            LearningSpeed = 0.10f;

            if (!File.Exists(filePath))
            {
                GenerateCondition();
                SaveCondition();
            }
            else
            {
                LoadCondition();
            }
        }

        public string Train(string pathToImage)
        {
            FillNeuronInput(pathToImage);

            CalculateNeurons();

            var expected = GetSourceImageNumber(pathToImage);
            var answer = Array.IndexOf(neuronOutput, neuronOutput.Max());

            return GetNetworkResult(answer, expected);
        }
        public string[] Train(string[] imageArray)
        {
            int n = imageArray.Length;
            float[] neuronErrors = new float[n];
            string[] networkResults = new string[n];

            for (int i = 0; i < n; i++)
            {
                FillNeuronInput(imageArray[i]);

                CalculateNeurons();

                var expected = GetSourceImageNumber(imageArray[i]);
                var answer = Array.IndexOf(neuronOutput, neuronOutput.Max());

                neuronErrors[i] = GetCostError(expected);

                networkResults[i] = GetNetworkResult(answer, expected);
            }

            return networkResults;
        }
        public string Test(string pathToImage)
        {
            FillNeuronInput(pathToImage);

            CalculateNeurons();

            int expected = GetSourceImageNumber(pathToImage);
            int answer = Array.IndexOf(neuronOutput, neuronOutput.Max());

            return GetNetworkResult(answer, expected);
        }

        private float GetCostError(int trueAnswer)
        {
            float costError = 0;
            for (int i = 0; i < neuronOutput.Length; i++)
            {
                if (i == trueAnswer)
                {
                    costError += (neuronOutput[i] - 1f) * (neuronOutput[i] - 1f);
                }
                else
                {
                    costError += (neuronOutput[i] - 0f) * (neuronOutput[i] - 0f);
                }
            }
            return costError;
        }
        private string GetNetworkResult(int answer, int expected)
        {
            total++;
            if (expected == answer)
            {
                accepted++;
                return $" | OK!!! Acceptance rate: {Math.Round(AcceptanceRateProcentes, 2)}% | " +
                    $"Answer: {answer} | Expected: {expected}";
            }

            return $" | FAIL!!! Acceptance rate: {Math.Round(AcceptanceRateProcentes, 2)}% | " +
                    $"Answer: {answer} | Expected: {expected}";
        }
        private void CalculateNeurons()
        {
            for (int a1 = 0; a1 < neuronHiddenColumn1.Length; a1++)
            {
                float sum = 0 + neuronHiddenColumn1Biases[a1];

                for (int w0a0 = 0; w0a0 < neuronHiddenColumn1Weights[0].Length; w0a0++)
                {
                    sum += neuronInput[w0a0] * neuronHiddenColumn1Weights[a1][w0a0];
                }

                neuronHiddenColumn1[a1] = GetSigmoid(sum);
            }

            for (int a2 = 0; a2 < neuronHiddenColumn2.Length; a2++)
            {
                float sum = 0 + neuronHiddenColumn2Biases[a2];

                for (int w1a1 = 0; w1a1 < neuronHiddenColumn2Weights[0].Length; w1a1++)
                {
                    sum += neuronHiddenColumn1[w1a1] * neuronHiddenColumn1Weights[a2][w1a1];
                }

                neuronHiddenColumn2[a2] = GetSigmoid(sum);
            }

            for (int a3 = 0; a3 < neuronOutput.Length; a3++)
            {
                float sum = 0 + neuronOutputBiases[a3];

                for (int w2a2 = 0; w2a2 < neuronOutputWeights[0].Length; w2a2++)
                {
                    sum += neuronHiddenColumn2[w2a2] * neuronHiddenColumn2Weights[a3][w2a2];
                }

                neuronOutput[a3] = GetSigmoid(sum);
            }

            neuronOutput = Softmax(neuronOutput);
        }
        private void CalibrateWeights(float[] errors)
        {
            float totalError = errors.Sum() / errors.Length;
            CalibrateWeights(totalError);
        }
        private void CalibrateWeights(float error)
        {

        }

        private void FillNeuronInput(string pathToImage)
        {
            var image = new Bitmap(pathToImage);

            int i = 0;
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    Color pixel = image.GetPixel(x, y);

                    neuronInput[i++] = pixel.R / 255.0f;
                }
            }
        }

        private float GetSigmoid(float input) => (float)(1 / (1 + Math.Pow(Math.E, -input)));
        private int GetSourceImageNumber(string image) => image[image.Length - 5] - '0';
        private float[] Softmax(float[] inputArray)
        {
            var res = new float[inputArray.Length];

            for (int i = 0; i < inputArray.Length; i++)
            {
                res[i] = (float)Math.Exp(inputArray[i]);
            }

            float sum = res.Sum();
            for (int i = 0; i < inputArray.Length; i++)
            {
                res[i] = res[i] / sum;
            }

            return res;
        }

        private void GenerateCondition()
        {
            for (int i = 0; i < neuronHiddenColumn1Weights.Length; i++)
            {
                neuronHiddenColumn1Weights[i] = new float[28 * 28];
                for (int j = 0; j < neuronHiddenColumn1Weights[i].Length; j++)
                {
                    neuronHiddenColumn1Weights[i][j] = (random.NextSingle() - 0.5f) * 2.0f;
                }
            }

            for (int i = 0; i < neuronHiddenColumn2Weights.Length; i++)
            {
                neuronHiddenColumn2Weights[i] = new float[16];
                for (int j = 0; j < neuronHiddenColumn2Weights[i].Length; j++)
                {
                    neuronHiddenColumn2Weights[i][j] = (random.NextSingle() - 0.5f) * 2.0f;
                }
            }

            for (int i = 0; i < neuronOutputWeights.Length; i++)
            {
                neuronOutputWeights[i] = new float[10];
                for (int j = 0; j < neuronOutputWeights[i].Length; j++)
                {
                    neuronOutputWeights[i][j] = (random.NextSingle() - 0.5f) * 2.0f;
                }
            }

            for (int i = 0; i < neuronHiddenColumn1Biases.Length; i++)
            {
                neuronHiddenColumn1Biases[i] = (random.NextSingle() - 0.5f) * 2.0f;
            }

            for (int i = 0; i < neuronHiddenColumn2Biases.Length; i++)
            {
                neuronHiddenColumn2Biases[i] = (random.NextSingle() - 0.5f) * 2.0f;
            }

            for (int i = 0; i < neuronOutputBiases.Length; i++)
            {
                neuronOutputBiases[i] = (random.NextSingle() - 0.5f) * 2.0f;
            }
        }
        public void SaveCondition()
        {
            string json = JsonConvert.SerializeObject(new
            {
                w1 = neuronHiddenColumn1Weights,
                w2 = neuronHiddenColumn2Weights,
                w3 = neuronOutputWeights,
                b1 = neuronHiddenColumn1Biases,
                b2 = neuronHiddenColumn2Biases,
                b3 = neuronOutputBiases
            });
            File.WriteAllText(filePath, json);
        }
        public void LoadCondition()
        {
            string json = File.ReadAllText(filePath);

            var arrays = JsonConvert.DeserializeAnonymousType(json, new
            {
                w1 = new float[16][],
                w2 = new float[16][],
                w3 = new float[10][],
                b1 = new float[16],
                b2 = new float[16],
                b3 = new float[10]
            })!;

            neuronHiddenColumn1Weights = arrays.w1;
            neuronHiddenColumn2Weights = arrays.w2;
            neuronOutputWeights = arrays.w3;
            neuronHiddenColumn1Biases = arrays.b1;
            neuronHiddenColumn2Biases = arrays.b2;
            neuronOutputBiases = arrays.b3;
        }
    }
}
