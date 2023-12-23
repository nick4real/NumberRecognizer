
using System.IO;
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

            Run();

            var expected = GetImageNumber(pathToImage);
            var answer = Array.IndexOf(neuronOutput, neuronOutput.Max());

            total++;
            if (expected == answer)
            {
                accepted++;
                return pathToImage + $" | OK!!! Acceptance rate: {Math.Round(AcceptanceRateProcentes, 2)}% | " +
                    $"Answer: {answer} | Expected: {expected}";
            }

            return pathToImage + $" | FAIL!!! Acceptance rate: {Math.Round(AcceptanceRateProcentes, 2)}% | " +
                    $"Answer: {answer} | Expected: {expected}";
        }

        public string Test(string pathToImage)
        {
            FillNeuronInput(pathToImage);

            Run();

            var expected = GetImageNumber(pathToImage);
            var answer = Array.IndexOf(neuronOutput, neuronOutput.Max());

            total++;
            if (expected == answer)
            {
                accepted++;
                return pathToImage + $" | OK!!! Acceptance rate: {Math.Round(AcceptanceRateProcentes, 2)}% | " +
                    $"Answer: {answer} | Expected: {expected}";
            }

            return pathToImage + $" | FAIL!!! Acceptance rate: {Math.Round(AcceptanceRateProcentes, 2)}% | " +
                    $"Answer: {answer} | Expected: {expected}";
        }

        private void Run()
        {
            for (int a1 = 0; a1 < neuronHiddenColumn1.Length; a1++)
            {
                float bias = 0;
                float sum = 0 + bias;
                
                for (int w0a0 = 0; w0a0 < neuronHiddenColumn1Weights[0].Length; w0a0++)
                {
                    sum += neuronInput[w0a0] * neuronHiddenColumn1Weights[a1][w0a0];
                }

                neuronHiddenColumn1[a1] = GetSigmoid(sum);
            }

            for (int a2 = 0; a2 < neuronHiddenColumn2.Length; a2++)
            {
                float bias = 0;
                float sum = 0 + bias;

                for (int w1a1 = 0; w1a1 < neuronHiddenColumn2Weights[0].Length; w1a1++)
                {
                    sum += neuronHiddenColumn1[w1a1] * neuronHiddenColumn1Weights[a2][w1a1];
                }

                neuronHiddenColumn2[a2] = GetSigmoid(sum);
            }

            for (int a3 = 0; a3 < neuronOutput.Length; a3++)
            {
                float bias = 0;
                float sum = 0 + bias;

                for (int w2a2 = 0; w2a2 < neuronOutputWeights[0].Length; w2a2++)
                {
                    sum += neuronHiddenColumn2[w2a2] * neuronHiddenColumn2Weights[a3][w2a2];
                }

                neuronOutput[a3] = GetSigmoid(sum);
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
        private int GetImageNumber(string image) => image[image.Length - 5] - '0';

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
        }
        public void SaveCondition()
        {
            string json = JsonConvert.SerializeObject(new
            {
                w1 = neuronHiddenColumn1Weights,
                w2 = neuronHiddenColumn2Weights,
                w3 = neuronOutputWeights
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
                w3 = new float[10][]
            })!;

            neuronHiddenColumn1Weights = arrays.w1;
            neuronHiddenColumn2Weights = arrays.w2;
            neuronOutputWeights = arrays.w3;
        }
    }
}
