using Newtonsoft.Json.Linq;

namespace JRoeSelenium.Helpers
{
    public static class TestHelper
    {
        static readonly string dataFile = $"TestData/test-data.json";

        public static string GetTestData(string key)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "/" + dataFile);
            JObject testData = JObject.Parse(File.ReadAllText(path));
            string value = testData.SelectToken(key).Value<string>();

            return value;
        }
    }
}
