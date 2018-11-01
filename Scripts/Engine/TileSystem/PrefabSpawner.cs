using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    public GameObject prefab;

    private void Awake()
    {
        int x = (int)transform.position.x;
        int y = (int)transform.position.y;

        GridNode node = GridSystem.GetNode(x, y);

        GameObject newObject = Instantiate(prefab);
        GridTransform gridTransform = newObject.GetComponent<GridTransform>();
        if(gridTransform != null && node != null)
        {
            gridTransform.Warp(node);
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 ll, lr, ul, ur;
        ll = new Vector3(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.y), -0.1f);
        lr = ll + Vector3.right;
        ul = ll + Vector3.up;
        ur = ll + Vector3.right + Vector3.up;

        Gizmos.DrawLine(ll, lr);
        Gizmos.DrawLine(ul, ur);
        Gizmos.DrawLine(ll, ul);
        Gizmos.DrawLine(lr, ur);
    }
}