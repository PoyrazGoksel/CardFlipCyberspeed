﻿using UnityEngine;
using UnityEngine.Events;

namespace Events
{
    public static class GridEvents
    {
        public static UnityAction<Bounds> GridStart;
        public static UnityAction CardShowComplete;
    }
}