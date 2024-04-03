using Newtonsoft.Json;
using SOTags.DataAccess.Entities;

namespace SOTags.ApplicationServices.Components
{
    public class StackOverflowJsonReader : IStackOverflowJsonReader
    {
        public List<Tag> ReadFile()
        {
            // Ścieżka do pliku JSON
            string filePath = Config.stackOverflowJsonFile;

            // Lista przechowująca obiekty Tag
            List<Tag> tagList = new List<Tag>();

            // Odczyt pliku JSON do stringa
            using (var reader = File.OpenText(filePath))
            {
                string jsonText = reader.ReadLine();

                while (jsonText != null)
                {
                    // Deserializacja JSON do obiektu
                    var jsonObj = JsonConvert.DeserializeObject<dynamic>(jsonText);

                    // Przetwarzanie elementów z pliku JSON
                    foreach (var item in jsonObj.items)
                    {
                        Tag tag = new Tag
                        {
                            Name = item.name.ToString(),
                            Count = Convert.ToInt32(item.count.ToString())
                        };
                        tagList.Add(tag);
                    }
                    jsonText = reader.ReadLine();
                }
            }
            return tagList;
        }
    }
}
