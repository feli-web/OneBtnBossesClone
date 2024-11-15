using UnityEngine;

public class Background : MonoBehaviour
{
    public float moveSpeed = 0.5f; 
    private Material material;    
    private Vector2 offset;       

    void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            material = sr.material;
        }

        if (material == null)
        {
            Debug.LogError("No material found on SpriteRenderer! Ensure the material supports texture offset.");
        }
    }

    void Update()
    {
        if (material != null)
        {
            offset = new Vector2(Time.time * moveSpeed, 0f);
            material.mainTextureOffset = offset;
        }
    }

    public void Stop()
    {
        moveSpeed = 0f;
    }
}
