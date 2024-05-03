namespace HardwareStore.Providers.Relational.DataTools.ADO.Exceptions;

[Serializable]
public class UnsupportedDbProviderException : Exception
{
    public UnsupportedDbProviderException() { }
    public UnsupportedDbProviderException(string message) : base(message) { }
    public UnsupportedDbProviderException(string message, Exception inner) : base(message, inner) { }
    protected UnsupportedDbProviderException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}