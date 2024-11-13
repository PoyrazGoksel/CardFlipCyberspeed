using Extensions.System;
using UnityEngine.Events;

namespace Events
{
    public static class GameEvents
    {
        public static RPCEvent GameStarted = new();
        public static RPCEvent PlayerLoaded = new();
        public static UnityAction PreLevelLoaded;
    }
}