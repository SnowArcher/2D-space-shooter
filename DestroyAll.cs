using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAll : MonoBehaviour
{
    // Reference to the BoxCollider2D component.
    private BoxCollider2D _boundare_Collider;
    // Vector for storing sizes Camera
    private Vector2 _viewport_Size;

    private void Awake()
    {
        // Setting up the references.
        // In the current object, find the BoxCollider2D component
        _boundare_Collider = GetComponent<BoxCollider2D>();
    }
    private void Start()
    {
        // Call the ResizeCollider method
        ResizeCollider();
    }
    // Method Resize Collider
    // The size of the collider is adjusted to the size of the camera.
    void ResizeCollider()
    {
        // Get the size of the upper right corner and multiply it by 2.
        _viewport_Size = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)) * 2;
        Debug.Log(_viewport_Size);
        //Increase the width by 1.5.
        _viewport_Size.x *= 1.5f;
        // Increase the height by 1.5.
        _viewport_Size.y *= 1.5f;
        // Change the size.
        _boundare_Collider.size = _viewport_Size;
    }

    public void OnTriggerExit2D(Collider2D coll)
    {
        // Destroy the collider if it has a tag...
        switch (coll.tag)
        {
            case "Planet":
                Destroy(coll.gameObject);
                break;
            case "Bullet":
                Destroy(coll.gameObject);
                break;
            case "Bonus":
                Destroy(coll.gameObject);
                break;
        }
    }
}
