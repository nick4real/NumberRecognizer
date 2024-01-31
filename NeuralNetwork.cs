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
        private float[] neuronInput = new float[inputSize];
        private float[] nhc1 = new float[nhc1Size];
        private float[] nhc2 = new float[nhc2Size];
        private float[] neuronOutput = new float[outputSize];

        private float[,] nhc1Weights = new float[nhc1Size, inputSize];
        private float[,] nhc2Weights = new float[nhc2Size, nhc1Size];
        private float[,] neuronOutputWeights = new float[outputSize, nhc2Size];

        private float[] nhc1Biases = new float[nhc1Size];
        private float[] nhc2Biases = new float[nhc2Size];
        private float[] neuronOutputBiases = new float[outputSize];

        private float learningRate;
        public float LearningRate
        {
            get
            {
                return learningRate;
            }
            set
            {
                // 0.1 <= value <= 0.001
                learningRate = Math.Min(0.1f, Math.Max(value, 0.001f));
            }
        }

        private int accepted;
        private int total;
        public float AcceptanceRateProcentes
        {
            get
            {
                return (float)accepted / total * 100.0f;
            }
        }

        // Main logic
        public NeuralNetwork()
        {
            LearningRate = 0.05f;

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

            ForwardProp();

            var expected = GetSourceImageNumber(pathToImage);
            var answer = Array.IndexOf(neuronOutput, neuronOutput.Max());

            var error = GetCost(expected);
            //var error = GetCrossEntropy(expected);

            if (isTraining) BackwardProp(expected);

            return GetNetworkResult(answer, expected, error);
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

                    neuronInput[i++] = pixel.GetBrightness();
                }
            }
        }
        private void ForwardProp()
        {
            for (int i = 0; i < nhc1Size; i++)
            {
                float sum = nhc1Biases[i];

                for (int j = 0; j < inputSize; j++)
                {
                    sum += neuronInput[j] * nhc1Weights[i, j];
                }

                nhc1[i] = GetSigmoid(sum);
            }

            for (int i = 0; i < nhc2Size; i++)
            {
                float sum = nhc2Biases[i];

                for (int j = 0; j < nhc1Size; j++)
                {
                    sum += nhc1[j] * nhc1Weights[i, j];
                }

                nhc2[i] = GetSigmoid(sum);
            }

            for (int i = 0; i < outputSize; i++)
            {
                float sum = neuronOutputBiases[i];

                for (int j = 0; j < nhc2Size; j++)
                {
                    sum += nhc2[j] * nhc2Weights[i, j];
                }

                //neuronOutput[i] = GetSigmoid(sum);
                neuronOutput[i] = sum;
            }

            neuronOutput = GetSoftmax(neuronOutput);
        }
        private void BackwardProp(int trueAnswer)
        {
            //gradient[column] also equals gradient[column]Biases
            float[] expected = new float[outputSize];
            expected[trueAnswer] = 1;

            float[] gradientOutput = new float[outputSize];
            float[] gradientNhc2 = new float[nhc2Size];
            float[] gradientNhc1 = new float[nhc1Size];

            float[,] gradientOutputW = new float[outputSize, nhc2Size];
            float[,] gradientNhc2W = new float[nhc2Size, nhc1Size];
            float[,] gradientNhc1W = new float[nhc1Size, inputSize];

            // output
            for (int i = 0; i < outputSize; i++)
            {
                gradientOutput[i] = 2 * (neuronOutput[i] - expected[i]) * GetSigmoidDer(neuronOutput[i]);
            }
            for (int i = 0; i < outputSize; i++)
            {
                for (int j = 0; j < nhc2Size; j++)
                {
                    gradientOutputW[i, j] = gradientOutput[i] * nhc2[j];
                }
            }

            // nhc 2
            for (int i = 0; i < nhc2Size; i++)
            {
                for (int j = 0; j < outputSize; j++)
                {
                    gradientNhc2[i] += gradientOutput[j] * neuronOutputWeights[j, i];
                }
                gradientNhc2[i] *= GetSigmoidDer(nhc2[i]);
            }
            for (int i = 0; i < nhc2Size; i++)
            {
                for (int j = 0; j < nhc1Size; j++)
                {
                    gradientNhc2W[i, j] = gradientNhc2[i] * nhc1[j];
                }
            }

            // nhc 1
            for (int i = 0; i < nhc1Size; i++)
            {
                for (int j = 0; j < nhc2Size; j++)
                {
                    gradientNhc1[i] += gradientNhc2[j] * nhc2Weights[j, i];
                }
                gradientNhc1[i] *= GetSigmoidDer(nhc1[i]);
            }
            for (int i = 0; i < nhc1Size; i++)
            {
                for (int j = 0; j < inputSize; j++)
                {
                    gradientNhc1W[i, j] = gradientNhc1[i] * neuronInput[j];
                }
            }

            // update
            for (int i = 0; i < inputSize; i++)
            {
                for (int j = 0; j < nhc1Size; j++)
                {
                    nhc1Weights[j, i] += LearningRate * gradientNhc1W[j, i];
                    //nhc1Weights[j, i] -= LearningRate * gradientNhc1W[j, i];
                }
            }
            for (int i = 0; i < nhc1Size; i++)
            {
                for (int j = 0; j < nhc2Size; j++)
                {
                    nhc2Weights[j, i] += LearningRate * gradientNhc2W[j, i];
                    //nhc2Weights[j, i] -= LearningRate * gradientNhc2W[j, i];
                }
            }
            for (int i = 0; i < nhc2Size; i++)
            {
                for (int j = 0; j < outputSize; j++)
                {
                    neuronOutputWeights[j, i] += LearningRate * gradientOutputW[j, i];
                    //neuronOutputWeights[j, i] -= LearningRate * gradientOutputW[j, i];
                }
            }
            for (int i = 0; i < nhc1Size; i++)
            {
                nhc1Biases[i] += LearningRate * gradientNhc1[i];
                //nhc1Biases[i] -= LearningRate * gradientNhc1[i];
            }
            for (int i = 0; i < nhc2Size; i++)
            {
                nhc2Biases[i] += LearningRate * gradientNhc2[i];
                //nhc2Biases[i] -= LearningRate * gradientNhc2[i];
            }
            for (int i = 0; i < outputSize; i++)
            {
                neuronOutputBiases[i] += LearningRate * gradientOutput[i];
                //neuronOutputBiases[i] -= LearningRate * gradientOutput[i];
            }
        }


        // Math methods
        private float[] GetSoftmax(float[] input)
        {
            float[] result = new float[input.Length];
            float maxInput = input[0];

            for (int i = 1; i < input.Length; i++)
            {
                if (input[i] > maxInput)
                {
                    maxInput = input[i];
                }
            }

            float sumExp = 0f;
            for (int i = 0; i < input.Length; i++)
            {
                result[i] = (float)Math.Exp(input[i] - maxInput);
                sumExp += result[i];
            }

            for (int i = 0; i < result.Length; i++)
            {
                result[i] /= sumExp;
            }

            return result;
        }
        private float GetCrossEntropy(int expected)
        {
            float crossEntropy = 0;
            for (int i = 0; i < outputSize; i++)
            {
                float answer = i == expected ? 1 : 0;
                crossEntropy += answer * (float)Math.Log(neuronOutput[i]);
            }
            return -crossEntropy;
        }
        private float GetCost(int expected)
        {
            float cost = 0;
            for (int i = 0; i < outputSize; i++)
            {
                float answer = i == expected ? 1 : 0;
                cost += (float)Math.Pow(neuronOutput[i] - answer, 2);
            }
            return cost;
        }
        private float GetSigmoid(float input) => (float)(1f / (1f + Math.Exp(-input)));
        private float GetSigmoidDer(float sigmoid) => sigmoid * (1f - sigmoid);

        // Help logic
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
        private int GetSourceImageNumber(string image) => image[image.Length - 5] - '0';

        // Other
        public void GenerateCondition()
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
        public void ResetAC()
        {
            accepted = 0;
            total = 0;
        }
    }
}