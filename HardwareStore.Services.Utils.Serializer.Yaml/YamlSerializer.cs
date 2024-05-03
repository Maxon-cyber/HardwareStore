using HardwareStore.Services.Utils.Serializer.Abstractions;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace HardwareStore.Services.Utils.Serializer.Yaml;

public sealed class YamlSerializer : IObjectSerializer
{
    private static readonly ISerializer _serializer = new SerializerBuilder()
                                                   .WithNamingConvention(PascalCaseNamingConvention.Instance)
                                                   .Build();

    private static readonly IDeserializer _deserializer = new DeserializerBuilder()
                                                   .WithNamingConvention(PascalCaseNamingConvention.Instance)
                                                   .Build();

    public string Serialize<TObject>(TObject obj)
    {
        string result = _serializer.Serialize(obj);

        return result;
    }

    public TObject Deserialize<TObject>(string input)
    {
        TObject result = _deserializer.Deserialize<TObject>(input);

        return result;
    }

    public TObject? DeserializeByKey<TObject>(object key, string input)
    {
        using StringReader reader = new StringReader(input);

        YamlStream documents = [];
        documents.Load(reader);

        int documentsLength = documents.Documents.Count;

        for (int index = 0; index < documentsLength; index++)
        {
            YamlMappingNode root = (YamlMappingNode)documents.Documents[index].RootNode;

            foreach (KeyValuePair<YamlNode, YamlNode> child in root.Children)
            {
                string? value = ((YamlScalarNode)child.Key).Value;

                if (value == (string)key)
                    return _deserializer.Deserialize<TObject>(value);
            }
        }

        return default;
    }
}