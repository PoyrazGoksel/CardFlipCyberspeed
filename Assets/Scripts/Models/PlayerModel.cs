using Extensions.Unity.Utils;

namespace Models
{
    public class PlayerModel : IJsonCallBackReceiver
    {
        public int Level;
        
        public void OnBeforeSerialize() {}

        public void OnAfterDeserialize() {}
    }
}