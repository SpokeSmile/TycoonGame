using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    private ushort sensitivityX = 1000;
    private ushort sensitivityY = 1000;

    private Mouse mouse;
    
    

    private void OnEnable()
    {
        mouse = Mouse.current;
    }

    void Start()
    {
        transform.position = new Vector3(-5, 5, 5);
        transform.rotation = Quaternion.Euler(45, 145, 0);
        Camera.main.orthographicSize = 2f;
    }

    void Update()
    {
        if (mouse == null)
            return;

        if (mouse.leftButton.isPressed)
        {
            Vector2 delta = mouse.delta.ReadValue();
            Vector2 normalizedDelta = new Vector2(delta.x / 500, delta.y / 500);

            Vector3 localMove = new Vector3(normalizedDelta.x * sensitivityX, 0f, normalizedDelta.y * sensitivityY);

            Vector3 worldMove = transform.rotation * localMove;
            worldMove.y = 0f;
            transform.position += -worldMove * Time.deltaTime;
        }
    }
}