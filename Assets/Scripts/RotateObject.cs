using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotationSpeed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(0, 0, rotationSpeed);
    }
}