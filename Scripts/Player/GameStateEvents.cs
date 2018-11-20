using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameStateEvents
{
    /// <summary>
    /// Push a game over event on the channel (gamestate, none)
    /// </summary>
    public class GameOverEvent : CEvent
    {

    }

    /// <summary>
    /// Push a grow event over the channel (gamestate, none)
    /// </summary>
    public class GrowEvent : CEvent
    {
        public readonly int growCount;
        public GrowEvent(int growCount)
        {
            this.growCount = growCount;
        }
    }
}