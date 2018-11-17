using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputEvents;
using GameStateEvents;
using System.Linq;

public class Player : MonoBehaviour, ICEventHandler
{
    //static fields
    private static Player instance;
    public static Player Instance
    {
        get
        {
            return instance;
        }
    }

    //Dependant components
    GridTransform gridTransform;
    PlayerKeyboardController keyboardController;
    LinearTweener tweener;

    //Fields
    Queue<InputEvent> inputBuffer = new Queue<InputEvent>();
    public Direction direction = Direction.up;
    int growCount = 0;
    List<TailPiece> tailPieces = new List<TailPiece>();
    public int TailCount {
        get {
            return tailPieces.Count;
        }
    }

    public float speed = 100f;




    private void Awake()
    {
        CEventSystem.AddEventHandler(EventChannel.input, EventSubChannel.player1, this);
        CEventSystem.AddEventHandler(EventChannel.gameState, EventSubChannel.none, this);
        instance = this;
    }

    private void Start()
    {
        //Add dependant components
        gridTransform = gameObject.AddComponent<GridTransform>();
        keyboardController = gameObject.AddComponent<PlayerKeyboardController>();
        tweener = gameObject.AddComponent<LinearTweener>();
        //Set the auto target for the tweener
        tweener.autoTarget = () => gridTransform.Target;
        tweener.speed = this.speed;

        //Warp the player to the grid
        var gridNode = GridSystem.GetNode(0, 0);
        gridTransform.Warp(gridNode);
    }

    private void OnDestroy()
    {
        CEventSystem.RemoveEventHandler(EventChannel.input, EventSubChannel.player1, this);
        CEventSystem.RemoveEventHandler(EventChannel.gameState, EventSubChannel.none, this);

        SnakeDestroyer.Create(gameObject, tailPieces.Select(x => x.gameObject));
        instance = null;
    }

    private void Update()
    {
        if(tweener.IsDone())
        {
            UpdateInput();
            if(gridTransform.CanMoveTo(direction))
            {
                UpdateGrowing();
                UpdateMovement();
            }
        }
        UpdateDebug();
    }

    /// <summary>
    /// Input processing for update
    /// </summary>
    private void UpdateInput()
    {
        //Begin dqueueing events until one is reached that can be executed
        while(inputBuffer.Count > 0)
        {
            var e = inputBuffer.Dequeue();
            //get a direction event
            if(e is DirectionEvent)
            {
                var directionEvent = e as DirectionEvent;
                //if the event can be executed, break out of the while loop
                if(SetDirection(directionEvent.direction))
                {
                    break;
                }
                //If this event can't be executed, reenqueue it so that it can be executed next frame
                if (!(directionEvent is ReenqueuedDirectionEvent))
                {
                    //The type needs to be changed to prevent an infinate loop
                    inputBuffer.Enqueue(new ReenqueuedDirectionEvent(directionEvent));
                }
            }
        }
        //Dequeue events until only 1 is left.
        //This way, if the player presses two inputs very rapidly, they don't get lost by the game engine.
        while(inputBuffer.Count > 1)
        {
            inputBuffer.Dequeue();
        }
    }

    /// <summary>
    /// Process growing for update
    /// </summary>
    private void UpdateGrowing()
    {
        if(growCount > 0)
        {
            TailPiece tp = TailPiece.Create(this, tailPieces.Count > 0 ? tailPieces[tailPieces.Count - 1].GetComponent<GridTransform>() : gridTransform);
            tailPieces.Add(tp);
            growCount--;
        }
    }

    /// <summary>
    /// Movement processing for update
    /// </summary>
    private void UpdateMovement()
    {
        for(int i = tailPieces.Count - 1; i >= 0; i--)
        {
            tailPieces[i].UpdatePosition();
        }
        gridTransform.Move(direction);
    }

    private void UpdateDebug()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            CEventSystem.BroadcastEvent(EventChannel.gameState, EventSubChannel.none, new GrowEvent(1));
        }
    }


    /// <summary>
    /// Grow the snake
    /// </summary>
    /// <param name="length"></param>
    public void Grow(int length = 1)
    {
        growCount += length;
    }



    /// <summary>
    /// Sets the players direction.
    /// Returns true if the assigned direction is valid.
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    private bool SetDirection(Direction direction)
    {
        if(this.direction == Direction.up && direction != Direction.down)
        {
            this.direction = direction;
            return true;
        }
        if(this.direction == Direction.down && direction != Direction.up)
        {
            this.direction = direction;
            return true;
        }
        if(this.direction == Direction.right && direction != Direction.left)
        {
            this.direction = direction;
            return true;
        }
        if(this.direction == Direction.left && direction != Direction.right)
        {
            this.direction = direction;
            return true;
        }
        return false;
    }

    public void AcceptEvent(CEvent e)
    {
        //push input events to the input buffer
        if(e is InputEvent)
        {
            inputBuffer.Enqueue(e as InputEvent);
        }
        //upon receiving game over, destoy this
        //Normally, the game object would be destroyed, but that behaviour is taken over by the snake destroyer
        if(e is GameOverEvent)
        {
            Destroy(this);
        }
        //Execute the grop method when a grow event is received.
        if(e is GrowEvent)
        {
            var grow = e as GrowEvent;
            Grow(grow.growCount);
        }
    }
}