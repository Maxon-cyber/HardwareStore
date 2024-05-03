namespace HardwareStore.Services.Utils.Serializer.Abstractions;

public interface IObjectSerializer
{
    string Serialize<TObject>(TObject obj);

    TObject Deserialize<TObject>(string input);

    TObject? DeserializeByKey<TObject>(object key, string input);
}