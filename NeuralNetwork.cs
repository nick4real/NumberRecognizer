using Newtonsoft.Json;

namespace NumberRecognizer
{
    public class NeuralNetwork
    {
        private readonly string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "arrays.json");
        private readonly Random random = new Random();

        private const int inputSize = 28 * 28;
        private const int nhc1Size = 16;
        private const int nhc2Size = 16;
        private const int outputSize = 10;
        // neuronHiddenColumn - nhc
        private float[] neuronInput = new float[28 * 28];
        private float[] nhc1 = new float[16];
        private float[] nhc2 = new float[16];
        private float[] neuronOutput = new float[10];

        private float[,] nhc1Weights = new float[16, 28 * 28];
        private float[,] nhc2Weights = new float[16, 16];
        private float[,] neuronOutputWeights = new float[10, 16];

        private float[] nhc1Biases = new float[16];
        private float[] nhc2Biases = new float[16];
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
            LearningSpeed = 0.001f;

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

        public string Start(string pathToImage, bool isTraining = false)
        {
            FillNeuronInput(pathToImage);

            CalculateNeurons();

            var expected = GetSourceImageNumber(pathToImage);
            var answer = Array.IndexOf(neuronOutput, neuronOutput.Max());

            var vectorError = GetVectorError(expected);
            var error = vectorError.Sum();

            if (isTraining) CalibrateWeights(expected);

            return GetNetworkResult(answer, expected, error);
        }

        private float[] GetVectorError(int expected)
        {
            float[] vectorError = new float[neuronOutput.Length];
            for (int i = 0; i < vectorError.Length; i++)
            {
                float answer = i == expected ? 1 : 0;
                vectorError[i] = (float)Math.Pow(neuronOutput[i] - answer, 2);
            }
            return vectorError;
        }
        private string GetNetworkResult(int answer, int expected, float error)
        {
            total++;
            if (expected == answer)
            {
                accepted++;
                return $" | OK!!! Acceptance rate: {Math.Round(AcceptanceRateProcentes, 2)}% | " +
                    $"Answer: {answer} | Expected: {expected} | Error: {Math.Round(error, 2)}";
            }

            return $" | FAIL!!! Acceptance rate: {Math.Round(AcceptanceRateProcentes, 2)}% | " +
                    $"Answer: {answer} | Expected: {expected} | Error: {Math.Round(error, 2)}";
        }
        private void CalculateNeurons()
        {
            for (int a1 = 0; a1 < nhc1Size; a1++)
            {
                float sum = 0 + nhc1Biases[a1];

                for (int w0a0 = 0; w0a0 < inputSize; w0a0++)
                {
                    sum += neuronInput[w0a0] * nhc1Weights[a1,w0a0];
                }

                nhc1[a1] = GetSigmoid(sum);
            }

            for (int a2 = 0; a2 < nhc2Size; a2++)
            {
                float sum = 0 + nhc2Biases[a2];

                for (int w1a1 = 0; w1a1 < nhc1Size; w1a1++)
                {
                    sum += nhc1[w1a1] * nhc1Weights[a2,w1a1];
                }

                nhc2[a2] = GetSigmoid(sum);
            }

            for (int a3 = 0; a3 < outputSize; a3++)
            {
                float sum = 0 + neuronOutputBiases[a3];

                for (int w2a2 = 0; w2a2 < nhc2Size; w2a2++)
                {
                    sum += nhc2[w2a2] * nhc2Weights[a3,w2a2];
                }

                neuronOutput[a3] = GetSigmoid(sum);
            }
        }
        //TODO: 
        private void CalibrateWeights(int trueAnswer)
        {
            // backprop
            float[] dE_dA3 = GetVectorError(trueAnswer);
            float[,] dE_dW3 = new float[outputSize, nhc2Size];

            //gradient[column] also equals gradient[column]Biases
            float[] gradientOutput = new float[outputSize];
            float[] gradientNhc2 = new float[nhc2Size];
            float[] gradientNhc1 = new float[nhc1Size];

            float[,] gradientOutputW = new float[outputSize,nhc2Size];
            float[,] gradientNhc2W = new float[nhc2Size, nhc1Size];
            float[,] gradientNhc1W = new float[nhc1Size, inputSize];




            // update
            for (int i = 0; i < inputSize; i++)
            {
                for (int j = 0; j < nhc1Size; j++)
                {
                    nhc1Weights[j, i] = nhc1Weights[j, i] - LearningSpeed * gradientNhc1W[j,i];
                }
            }
            for (int i = 0; i < nhc1Size; i++)
            {
                for (int j = 0; j < nhc2Size; j++)
                {
                    nhc2Weights[j, i] = nhc2Weights[j, i] - LearningSpeed * gradientNhc2W[j,i];
                }
            }
            for (int i = 0; i < nhc2Size; i++)
            {
                for (int j = 0; j < outputSize; j++)
                {
                    neuronOutputWeights[j, i] = neuronOutputWeights[j, i] - LearningSpeed * gradientOutputW[j,i];
                }
            }
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
        private float GetSigmoidDerivative(float input)
        {
            float siqmoid = GetSigmoid(input);
            return siqmoid * (1 - siqmoid);
        }
        private int GetSourceImageNumber(string image) => image[image.Length - 5] - '0';

        private void GenerateCondition()
        {
            for (int i = 0; i < nhc1Size; i++)
            {
                for (int j = 0; j < inputSize; j++)
                {
                    nhc1Weights[i,j] = (random.NextSingle() - 0.5f) * 2.0f;
                }
            }
            for (int i = 0; i < nhc2Size; i++)
            {
                for (int j = 0; j < nhc1Size; j++)
                {
                    nhc2Weights[i,j] = (random.NextSingle() - 0.5f) * 2.0f;
                }
            }
            for (int i = 0; i < outputSize; i++)
            {
                for (int j = 0; j < nhc2Size; j++)
                {
                    neuronOutputWeights[i,j] = (random.NextSingle() - 0.5f) * 2.0f;
                }
            }

            for (int i = 0; i < nhc1Biases.Length; i++)
            {
                nhc1Biases[i] = (random.NextSingle() - 0.5f) * 2.0f;
            }
            for (int i = 0; i < nhc2Biases.Length; i++)
            {
                nhc2Biases[i] = (random.NextSingle() - 0.5f) * 2.0f;
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
                w1 = nhc1Weights,
                w2 = nhc2Weights,
                w3 = neuronOutputWeights,
                b1 = nhc1Biases,
                b2 = nhc2Biases,
                b3 = neuronOutputBiases
            });
            File.WriteAllText(filePath, json);
        }
        public void LoadCondition()
        {
            string json = File.ReadAllText(filePath);

            var arrays = JsonConvert.DeserializeAnonymousType(json, new
            {
                w1 = new float[16, 28 * 28],
                w2 = new float[16, 16],
                w3 = new float[10, 16],
                b1 = new float[16],
                b2 = new float[16],
                b3 = new float[10]
            })!;

            nhc1Weights = arrays.w1;
            nhc2Weights = arrays.w2;
            neuronOutputWeights = arrays.w3;
            nhc1Biases = arrays.b1;
            nhc2Biases = arrays.b2;
            neuronOutputBiases = arrays.b3;
        }
    }
}