using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputEvents;

public class PlayerKeyboardController : MonoBehaviour
{
    private void Update()
    {
        //Get possible up inputs
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            BroadcastDirectionEvent(Direction.right);
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            BroadcastDirectionEvent(Direction.right);
        }
        //Get possible down inputs
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            BroadcastDirectionEvent(Direction.left);
        }
        if(Input.GetKeyDown(KeyCode.A))
        {
            BroadcastDirectionEvent(Direction.left);
        }
        //Get possible right inputs
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            BroadcastDirectionEvent(Direction.up);
        }
        if(Input.GetKeyDown(KeyCode.W))
        {
            BroadcastDirectionEvent(Direction.up);
        }
        //Get possible down inputs
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            BroadcastDirectionEvent(Direction.down);
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            BroadcastDirectionEvent(Direction.down);
        }
    }

    private void BroadcastDirectionEvent(Direction direction)
    {
        CEventSystem.BroadcastEvent(EventChannel.input, EventSubChannel.player1, new DirectionEvent(direction));
    }
}