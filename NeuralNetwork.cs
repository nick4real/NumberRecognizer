
using System.IO;
using Newtonsoft.Json;

namespace NumberRecognizer
{
    public class NeuralNetwork
    {
        private readonly string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "arrays.json");

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

            for (int i = 0; i < neuronHiddenColumn1Weights.Length; i++)
                neuronHiddenColumn1Weights[i] = new float[16];

            for (int i = 0; i < neuronHiddenColumn2Weights.Length; i++)
                neuronHiddenColumn2Weights[i] = new float[16];

            for (int i = 0; i < neuronOutputWeights.Length; i++)
                neuronOutputWeights[i] = new float[10];

            if (!File.Exists(filePath))
            {
                SaveCondition();
            }
            else
            {
                LoadCondition();
            }
        }

        public string Train(string input)
        {
            FillNeuronInput(input);

            var expected = GetImageNumber(input);
            var answer = Array.IndexOf(neuronOutput, neuronOutput.Max());

            total++;
            if (expected == answer)
            {
                accepted++;
                return $"OK!!! Acceptance rate: {Math.Round(AcceptanceRateProcentes, 2)}% | " +
                    $"Answer: {answer} | Expected: {expected}";
            }

            return $"FAIL!!! Acceptance rate: {Math.Round(AcceptanceRateProcentes, 2)}% | " +
                    $"Answer: {answer} | Expected: {expected}";
        }

        public string Test(string input)
        {
            FillNeuronInput(input);

            var expected = GetImageNumber(input);
            var answer = Array.IndexOf(neuronOutput, neuronOutput.Max());

            total++;
            if (expected == answer)
            {
                accepted++;
                return $"OK!!! Acceptance rate: {Math.Round(AcceptanceRateProcentes, 2)}% | " +
                    $"Answer: {answer} | Expected: {expected}";
            }

            return $"FAIL!!! Acceptance rate: {Math.Round(AcceptanceRateProcentes, 2)}% | " +
                    $"Answer: {answer} | Expected: {expected}";
        }

        private void Run()
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
        private char GetImageNumber(string image) => image[image.Length - 5];

        private void SaveCondition()
        {
            string json = JsonConvert.SerializeObject(new
            {
                w1 = neuronHiddenColumn1Weights,
                w2 = neuronHiddenColumn2Weights,
                w3 = neuronOutputWeights
            });
            File.WriteAllText(filePath, json);
        }
        private void LoadCondition()
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
