using System.Collections;
using System.Collections.Generic;
using GameStateEvents;
using UnityEngine;

public class CameraControl : MonoBehaviour, ICEventHandler {
    ///tips and things to remember
    ///get player position using Player.Instance
    ///but use null check to prevent race condition with player and camera
    AsymtoticTweener tween;
    void Awake()
    {
        CEventSystem.AddEventHandler(EventChannel.gameState, EventSubChannel.none, this);
        //tween = gameObject.AddComponent<AsymtoticTweener>();
    }
    public Player target;
    public Vector3 targetV;
    public Vector3 localPos = Vector3.back;
    const float speed = 0.95f;
    void LateUpdate()
    {
        if (target == null) {
            target = Player.Instance;
        }
        if (target == null) {
            return;
        }
        targetV = Vector3.zero;
        
        targetV.z = -5;
        targetV += Ahead() + Zoom();
        //tween.movementRatio = speed;
        //tween.target = targetV;
        localPos = Vector3.Lerp(localPos, targetV, speed * Time.deltaTime);
        transform.position = localPos + target.transform.position;
        
    }
    Vector3 Ahead() {
        Vector3 dist = Vector3.zero;
        switch (target.direction) {
            case Direction.up:
                return Vector3.up * 2;
            case Direction.down:
                return Vector3.down * 2;
            case Direction.left:
                return Vector3.left * 2;
            case Direction.right:
                return Vector3.right * 2;
            default:
                return Vector3.zero;
        }
    }

    Vector3 Zoom() {
        Vector3 zoom = Vector3.zero;
        if (target.GrowCount > 0) {
            zoom = Vector3.back * (10 / (target.GrowCount + 2) - 10);
        }
        return zoom;
    }

    public void AcceptEvent(CEvent e)
    {
        if (e is GameOverEvent) {
            Destroy(this);

        }
    }

    private void OnDestroy()
    {
        CEventSystem.RemoveEventHandler(EventChannel.gameState, EventSubChannel.none, this);
    }
}
