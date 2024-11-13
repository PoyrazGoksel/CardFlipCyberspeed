using Extensions.System;
using UnityEngine.Events;

namespace Events
{
    public static class GameEvents
    {
        public static RPCEvent GameStarted = new();
        public static UnityAction PreLevelLoaded;
        public static UnityAction LevelLoaded;
    }
}