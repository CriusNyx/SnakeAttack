using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InputEvents
{
    public class InputEvent : CEvent
    {

    }

    public class DirectionEvent : InputEvent
    {
        public readonly Direction direction;
        public DirectionEvent(Direction direction)
        {
            this.direction = direction;
        }
    }

    public class ReenqueuedDirectionEvent : DirectionEvent
    {
        public ReenqueuedDirectionEvent(DirectionEvent e) : base(e.direction)
        {

        }
    }

    public class PauseEvent : InputEvent
    {

    }
}