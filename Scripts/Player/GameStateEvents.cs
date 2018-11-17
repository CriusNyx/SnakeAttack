using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameStateEvents
{
    public class GameOverEvent : CEvent
    {

    }

    public class GrowEvent : CEvent
    {
        public readonly int growCount;
        public GrowEvent(int growCount)
        {
            this.growCount = growCount;
        }
    }
}