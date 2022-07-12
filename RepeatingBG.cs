using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingBG : MonoBehaviour
{
    // Vertical sprite size in pixels.
    // ***For proper operation, the height of the sprite must be greater than the height of the camera.
    // ***You can use the BOX collider to measure the height of the sprite.
    public float vertical_Size;
    // Sprite up offset.
    private Vector2 _offSet_Up;

    private void Update()
    {
        // if the sprite is completely gone
        if (transform.position.y < -vertical_Size)
        {
            // Call the RepeatBackground method
            RepeatBackground();
        }
    }
    // Method RepeatBackground
    // Moves two sprites one after the other, creating an endless background.
    void RepeatBackground()
    {
        // Set the offset twice the height of the sprite.
        _offSet_Up = new Vector2(0, vertical_Size * 2f);
        // Set a new position for the sprite.
        transform.position = (Vector2)transform.position + _offSet_Up;
    }
}
