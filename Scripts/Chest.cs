using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static GameState;

public class Chest : MonoBehaviour
{
    GameObject arrow;

    public static void EnableChest()
    {
        var chest = FindObjectOfType<Chest>();
        if(chest != null)
            chest.PrivateEnable();
    }

    private void PrivateEnable()
    {
        GridTransform gTransform = gameObject.AddComponent<GridTransform>();
        gTransform.Events = new GridEventHandlers((x) => OnGridCollision(x));
        gTransform.Warp(GridSystem.GetNode((int)transform.position.x, (int)transform.position.y));

        transform.position = gTransform.Target;

        GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
        quad.transform.SetParent(transform, false);
        quad.transform.localPosition = Vector3.back * 0.01f;

        Material chestMat = new Material(Shader.Find("Unlit/Transparent"));
        chestMat.SetTexture("_MainTex", Resources.Load<Texture>("Sprites/Chest"));
        quad.GetComponent<MeshRenderer>().material = chestMat;

        arrow = new GameObject("Arrow");
        GameObject aQuad = GameObject.CreatePrimitive(PrimitiveType.Quad);
        aQuad.transform.SetParent(arrow.transform, false);
        aQuad.transform.localPosition = Vector3.back * 0.11f;
        aQuad.transform.localScale = Vector3.one * 3f;

        Material arrowMat = new Material(Shader.Find("Unlit/Transparent"));
        arrowMat.SetTexture("_MainTex", Resources.Load<Texture>("Sprites/Arrow"));
        aQuad.GetComponent<MeshRenderer>().material = arrowMat;

        SoundController.PlaySound(Player.Instance.transform.position, "Sounds/ChestOpen");
    }

    private void LateUpdate()
    {
        if(arrow != null)
        {
            if(Time.frameCount % 30 == 0)
            {
                arrow.SetActive(!arrow.activeSelf);
            }
            Player player = Player.Instance;
            if(player != null)
            {
                Vector3 offset = transform.position - player.transform.position;
                float dis = 3f;
                if(offset.magnitude > dis)
                {
                    offset = offset.normalized * dis;
                    arrow.transform.position = player.transform.position + offset;
                    arrow.transform.rotation = Quaternion.LookRotation(Vector3.forward, -offset);
                }
            }
            else
            {
                arrow.SetActive(false);
            }
        }
    }

    private void OnGridCollision(GridTransform other)
    {
        if(other.GetComponent<Player>() != null)
        {
            Destroy(gameObject);
            CEventSystem.BroadcastEvent(EventChannel.gameState, EventSubChannel.none, new GameState.WinLevelEvent());
        }
    }
}