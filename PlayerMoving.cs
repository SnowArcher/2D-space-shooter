using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Serializable allows you to embed a Borders class in an inspector.
[System.Serializable]
public class Borders
{
    //Offset from the left border
    public float minX_Offset = 1.1f;
    //Offset from the right border
    public float maxX_Offset = 1.1f;
    //Offset from the bottom border
    public float minY_Offset = 1.1f;
    //Offset from the top border
    public float maxY_Offset = 1.1f;
    // HideInInspector makes variables that are not displayed in the inspector, but are serialized
    // These variables create boundaries that the player cannot leave.
    [HideInInspector]
    public float minX, maxX, minY, maxY;
}
public class PlayerMoving : MonoBehaviour
{
    // Static reference to the PlayerMoving (can be used in other scripts).
    public static PlayerMoving instance;
    // Reference to the borders.
    public Borders borders;
    // The speed at which the player moves.
    public int speed_Player = 5;
    // Reference private to the Camera.
    private Camera _camera;
    // Saveds the 2D coordinates(xy) where the player is moving.
    private Vector2 _mouse_Position;
    private void Awake()
    {
        // Setting up the references.
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        _camera = Camera.main;
    }
    private void Start()
    {
        // Call the Resize Borders method
        ResizeBorders();
    }
    private void Update()
    {
        // If the mouse botton is down.
        if (Input.GetMouseButton(0))
        {
            // Get 2D coordinates(xy) click on screen.
            _mouse_Position = _camera.ScreenToWorldPoint(Input.mousePosition);
            _mouse_Position.y += 1.5f;
            // Move our player to the 2D coordinates click at a given speed.
            transform.position = Vector2.MoveTowards(transform.position, _mouse_Position, speed_Player * Time.deltaTime);
        }
        // If a player is trying to go abroad, do not let him.
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, borders.minX, borders.maxX), Mathf.Clamp(transform.position.y, borders.minY, borders.maxY));
    }
    // Method Resize Borders 
    //The calculation depends on the size of the camera and the specified offset in the Borders class.
    private void ResizeBorders()
    {
        // Border left.
        borders.minX = _camera.ViewportToWorldPoint(Vector2.zero).x + borders.minX_Offset;
        // Border bottom.
        borders.minY = _camera.ViewportToWorldPoint(Vector2.zero).y + borders.minY_Offset;
        // Border right.
        borders.maxX = _camera.ViewportToWorldPoint(Vector2.right).x - borders.maxX_Offset;
        // Border top.
        borders.maxY = _camera.ViewportToWorldPoint(Vector2.up).y - borders.maxY_Offset;
    }
}
