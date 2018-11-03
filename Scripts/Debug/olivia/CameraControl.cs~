using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
    ///tips and things to remember
    ///get player position using Player.Instance
    ///but use null check to prevent race condition with player and camera
    AsymtoticTweener tween;
    void Awake()
    {
        transform.position = new Vector3(0, 0, -5);
        //tween = gameObject.AddComponent<AsymtoticTweener>();
    }
    public Player target;
    public Vector3 targetV;
    public float speed = 0.95f;
    void LateUpdate()
    {
        if (target == null) {
            target = Player.Instance;
        }
        if (target == null) {
            return;
        }
        targetV = target.transform.position;
        targetV.z = -5;
        //tween.movementRatio = speed;
        //tween.target = targetV;
        transform.position = targetV;


    }
}
