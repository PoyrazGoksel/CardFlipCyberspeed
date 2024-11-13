using UnityEngine;
using UnityEngine.Events;

namespace Events
{
    public static class GridEvents
    {
        public static UnityAction<Bounds> GridStart;
        public static UnityAction CardShowComplete;
        public static UnityAction GridComplete;
        public static UnityAction Match;
        public static UnityAction MatchFail;
    }
}