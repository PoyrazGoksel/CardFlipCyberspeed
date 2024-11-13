using Extensions.System;
using UnityEngine.Events;

namespace Events
{
    public class PlayerEvents
    {
        public static RPCEvent PlayerLoaded = new();
        public static UnityAction<int> ScoreChanged;
        public static UnityAction<int> ScoreMultiChanged;
    }
}