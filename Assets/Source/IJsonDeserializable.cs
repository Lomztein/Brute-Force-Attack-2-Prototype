using Newtonsoft.Json.Linq;

namespace Lomztein.BruteForceAttackSequel
{

    public interface IJsonDeserializable
    {
        void Deserialize(JToken source);
    }

}
