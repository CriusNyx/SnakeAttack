using GameStateEvents;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GreenSlime : MonoBehaviour
{
    /// <summary>
    /// This slime has a size of 4 blocks
    /// Therefore, it is represented by 4 grid transforms.
    /// It's base is in the lower left corner of the sprite
    /// right is the transform directly to the right, up is directly up, and one is right + up
    /// </summary>
    GridTransform gridTransform, gridRight, gridUp, gridOne;
    GridTransform[] gridTransforms;
    GameObject childRight, childUp, childOne;
    GameObject animationChild, animationParent;
    LinearTweener tweener;

    Texture2D front, left, right, back;
    Material mat;

    private void Start()
    {
        childRight = MakeChild("childRight", Vector2.right);
        childUp = MakeChild("childUp", Vector2.up);
        childOne = MakeChild("childOne", Vector2.one);

        gridTransform = gameObject.GetComponent<GridTransform>();
        gridRight = childRight.AddComponent<GridTransform>();
        gridUp = childUp.AddComponent<GridTransform>();
        gridOne = childOne.AddComponent<GridTransform>();

        gridTransforms = new GridTransform[] { gridTransform, gridRight, gridUp, gridOne };

        foreach(var gTransform in gridTransforms)
        {
            gTransform.Events = new GridEventHandlers(OnCollision: (x) => OnGridCollision(x));
        }

        tweener = gameObject.AddComponent<LinearTweener>();
        tweener.speed = 10f;
        tweener.autoTarget = () => gridTransform.Target;

        gridTransform.Warp(GridSystem.GetNode((int)transform.position.x, (int)transform.position.y));

        CEventSystem.BroadcastEvent(EventChannel.gameState, EventSubChannel.none, new GameState.EnemySpawnEvent());

        animationParent = new GameObject("animationParent");
        animationParent.transform.SetParent(transform, false);
        animationParent.transform.localPosition = new Vector3(0.5f, -0.5f, 0f);

        animationChild = GameObject.CreatePrimitive(PrimitiveType.Quad);
        animationChild.name = "AnimationChild";
        animationChild.transform.SetParent(animationParent.transform, false);
        animationChild.transform.localPosition = Vector2.up;
        animationChild.transform.localScale = new Vector3(2f, 2f, 1f);

        Sprite[] sheet = Resources.LoadAll<Sprite>("Sprites/slime-sprite-sheet-FINAL-PNG");

        front = textureFromSprite(sheet[8]);
        right = textureFromSprite(sheet[5]);
        left = textureFromSprite(sheet[2]);
        back = textureFromSprite(sheet[11]);

        mat = new Material(Shader.Find("Unlit/Transparent"));
        mat.SetTexture("_MainTex", front);
        animationChild.GetComponent<MeshRenderer>().material = mat;
    }

    public static Texture2D textureFromSprite(Sprite sprite)
    {
        if(sprite.rect.width != sprite.texture.width)
        {
            Texture2D newText = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
            Color[] newColors = sprite.texture.GetPixels((int)sprite.textureRect.x,
                                                         (int)sprite.textureRect.y,
                                                         (int)sprite.textureRect.width,
                                                         (int)sprite.textureRect.height);
            newText.SetPixels(newColors);
            newText.Apply();
            newText.filterMode = FilterMode.Point;
            return newText;
        }
        else
            return sprite.texture;
    }

    private IEnumerator SlimeAnimation()
    {
        float startTime = Time.time;
        float total = .1f;
        Vector3 startPos = new Vector3(0.5f, -0.5f, 0f);
        while(Time.time < startTime + total)
        {
            float t = (Time.time - startTime) / total;
            t = 4f * -t * (t - 1f);
            animationParent.transform.localPosition = startPos + Vector3.up * t * 0.2f;
            yield return null;
        }
        animationParent.transform.localPosition = startPos;
        yield return null;
    }

    private void MakeMove(Direction direction)
    {
        if(CanMove(direction))
        {
            gridTransform.Move(direction);
            StartCoroutine(SlimeAnimation());
            switch(direction)
            {
                case Direction.left:
                    mat.SetTexture("_MainTex", left);
                    break;
                case Direction.right:
                    mat.SetTexture("_MainTex", right);
                    break;
                case Direction.down:
                    mat.SetTexture("_MainTex", front);
                    break;
                case Direction.up:
                    mat.SetTexture("_MainTex", back);
                    break;
            }
        }
    }

    private bool CanMove(Direction direction)
    {
        return gridTransforms.All(x => x.CanMoveTo(direction));
    }

    private void Update()
    {
        if(Time.frameCount % 20 == 0)
            MakeDecision();

        GridNode node = gridTransform.CurrentNode;
        GridNode right = node.right;
        GridNode up = node.top;
        GridNode one = up.right;

        if(gridRight.CurrentNode != right)
            gridRight.Warp(right);
        if(gridUp.CurrentNode != up)
            gridUp.Warp(up);
        if(gridOne.CurrentNode != one)
            gridOne.Warp(one);

        animationParent.transform.localScale = Vector3.Lerp(new Vector3(1.1f, 0.9f, 1f), new Vector3(0.9f, 1.1f, 1f), Mathf.Pow(Mathf.Sin(Time.time * 3f), 2f));
    }

    private void MakeDecision()
    {
        int r = Random.Range(0, 9);
        Direction dir;
        switch(r)
        {
            case 0:
                dir = Direction.left;
                break;
            case 1:
                dir = Direction.right;
                break;
            case 2:
                dir = Direction.down;
                break;
            case 3:
                dir = Direction.up;
                break;
            default:
                dir = Direction.none;
                break;
        }

        if(dir != Direction.none)
        {
            if(CanMove(dir))
                MakeMove(dir);
        }
    }

    private GameObject MakeChild(string name, Vector3 localPos)
    {
        var output = new GameObject(name);
        output.transform.SetParent(transform, false);
        output.transform.localPosition = localPos;
        return output;
    }

    private void OnGridCollision(GridTransform other)
    {
        GreenSlime slime = other.GetComponentInParent<GreenSlime>();
        if(slime == this)
            return;

        Player player = other.GetComponent<Player>();
        Destroy(gameObject);
        CEventSystem.BroadcastEvent(EventChannel.gameState, EventSubChannel.none, new GrowEvent(10));
        CEventSystem.BroadcastEvent(EventChannel.gameState, EventSubChannel.none, new GameState.EnemyDestroyedEvent());
    }
}