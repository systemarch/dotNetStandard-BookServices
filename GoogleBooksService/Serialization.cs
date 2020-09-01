using Newtonsoft.Json;

namespace GoogleBooksService
{
    public static class Serialization
    {
        public static T DeserializeJson<T>(string jsonString)
        {
            return JsonConvert.DeserializeObject<T>(jsonString);
        }
    }
}
