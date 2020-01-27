using Newtonsoft.Json.Linq;

public interface IJsonSerializable 
{
    JToken Serialize();
}
