using System.Dynamic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using YamlDotNet.Serialization;
using static EarthQuakeData.Utils;

namespace EarthQuakeData;

//Concrete implementor that converts JSON to YML
public class YmlDataConverter : IDataConverter
{
    //After calling the ConvertJsonToYaml(), the returned value is used to write it into
    //a yml file
    //same logic for naming and outputting as the XmlDataConverter
    public void Convert(dynamic data)
    {
        string yamlString = ConvertJsonToYaml(data);
        
        string outputPath = UDPath + $"/Outputs/Yml/test{DateTime.Now.ToString("yyyy-MM-dd--HH:mm:ss")}.yml";
        File.WriteAllText(outputPath, yamlString);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Converted JSON to YML successfully");
        Console.ResetColor();
    }
    
    /*Converts the JSON to YML using library methods. It takes in a JSON Object, turns it into a
     string, deserializes it into a string, and then it is serialized into YML string, which is then returned*/
    private static string ConvertJsonToYaml(JObject jsonInput)
    {
        // Deserialize JSON to an object
        var jsonString = jsonInput.ToString(); 
        JsonConvert.DeserializeObject<object>(jsonString);
        var expConverter = new ExpandoObjectConverter();
        
        var deserializedObject = JsonConvert.DeserializeObject<ExpandoObject>(jsonString, expConverter)!;
        
        // Create a YamlDotNet serializer
        var serializer = new SerializerBuilder().Build();

        // Serialize the object to YAML
        var yamlString = serializer.Serialize(deserializedObject);

        return yamlString;
    }
}