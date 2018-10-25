using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameStateEvents;

public class TailPiece : MonoBehaviour
{
    GridTransform gridTransform;
    LinearTweener tweener;

    GridTransform leader;

    public static TailPiece Create(Player player, GridTransform leader)
    {
        return new GameObject("TailPiece").AddComponent<TailPiece>().Init(player, leader);
    }

    private TailPiece Init(Player player, GridTransform leader)
    {
        this.leader = leader;

        gridTransform = gameObject.AddComponent<GridTransform>();
        gridTransform.Events = new GridEventHandlers(OnCollision: OnGridCollision);

        tweener = gameObject.AddComponent<LinearTweener>();
        tweener.autoTarget = () => gridTransform.Target;
        tweener.speed = player.speed;

        GameObject.CreatePrimitive(PrimitiveType.Quad).transform.SetParent(transform);
        transform.position = leader.transform.position;
        return this;
    }

    private void OnGridCollision(GridTransform other)
    {
        if(other.GetComponent<Player>() != null)
        {
            CEventSystem.BroadcastEvent(EventChannel.gameState, EventSubChannel.none, new GameOverEvent());
        }
    }

    public void UpdatePosition()
    {
        gridTransform.MoveTo(leader.CurrentNode);
    }
}