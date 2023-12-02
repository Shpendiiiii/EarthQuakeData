using System.Dynamic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using YamlDotNet.Serialization;
using static EarthQuakeData.Utils;

namespace EarthQuakeData;

public class YmlDataConverter : IDataConverter
{
    public void Convert(dynamic data)
    {
        string yamlString = ConvertJsonToYaml(data);

        Console.WriteLine(yamlString);
        string outputPath = UDPath + $"/Outputs/Yml/test{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}.yml";
        File.WriteAllText(outputPath, yamlString);
    }
    
    private static string ConvertJsonToYaml(JObject jsonInput)
    {
        // Deserialize JSON to an object
        var jsonString = jsonInput.ToString(); 
        JsonConvert.DeserializeObject<object>(jsonString);
        var expConverter = new ExpandoObjectConverter();
        
        dynamic deserializedObject = JsonConvert.DeserializeObject<ExpandoObject>(jsonString, expConverter)!;
        
        // Create a YamlDotNet serializer
        var serializer = new SerializerBuilder().Build();

        // Serialize the object to YAML
        var yamlString = serializer.Serialize(deserializedObject);

        return yamlString;
    }
}