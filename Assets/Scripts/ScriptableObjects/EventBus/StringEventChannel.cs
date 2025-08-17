using UnityEngine;

namespace ScriptableObjects.EventBus
{
    [CreateAssetMenu(fileName = "String EventChannel", menuName = "Events/String EventChannel")]
    public class StringEventChannel : GenericEventChannelSO<string>
    {
        
    }
}
