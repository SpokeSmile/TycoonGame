using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
 
    private ushort sensitivityX = 1000;
    private ushort sensitivityY = 1500;

    private float maxX = -1.9f;  
    private float minX = -4.9f; 
    private float maxZ = 6.15f; 
    private float minZ = 3.15f; 

    private Mouse mouse;

    private void OnEnable()
    {
        mouse = Mouse.current;
    }

    void Start()
    {
        transform.position = new Vector3(-3.4f, 6f, 4.65f); 
        transform.rotation = Quaternion.Euler(45, 145, 0); 
        Camera.main.orthographicSize = 2.25f; 
    }

    void Update()
    {
        if (mouse == null || !mouse.leftButton.isPressed)
            return;

        
        Vector2 delta = mouse.delta.ReadValue();
        Vector2 screenSize = new Vector2(Screen.width, Screen.height);
        Vector2 normalizedDelta = new Vector2(delta.x / screenSize.x, delta.y / screenSize.y);

 
        Vector3 localMove = new Vector3(normalizedDelta.x * sensitivityX, 0f, normalizedDelta.y * sensitivityY);

        Vector3 worldMove = transform.rotation * localMove;
        worldMove.y = 0f; 


        Vector3 newPosition = transform.position + (-worldMove * Time.deltaTime);


        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.z = Mathf.Clamp(newPosition.z, minZ, maxZ);

        transform.position = newPosition;
    }
}