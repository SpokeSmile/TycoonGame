using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
 
    private ushort sensitivityX = 1000;
    private ushort sensitivityY = 1500;

    private float maxX = -2.5f;  
    private float minX = -5.5f; 
    private float maxZ = 7f; 
    private float minZ = 4f; 

    private Mouse mouse;

    private void OnEnable()
    {
        mouse = Mouse.current;
    }

    void Start()
    {
        transform.position = new Vector3(-4f, 6f, 5.5f); 
        transform.rotation = Quaternion.Euler(45, 145, 0); 
        Camera.main.orthographicSize = 2f; 
    }

    void Update()
    {
        if (mouse == null || !mouse.leftButton.isPressed)
            return;

        
        Vector2 delta = mouse.delta.ReadValue();
        Vector2 screenSize = new Vector2(Screen.width, Screen.height);
        Vector2 normalizedDelta = new Vector2(delta.x / screenSize.x, delta.y / screenSize.y);

 
        Vector3 localMove = new Vector3(normalizedDelta.x * sensitivityX * 5f, 0f, normalizedDelta.y * sensitivityY * 5f);

        Vector3 worldMove = transform.rotation * localMove;
        worldMove.y = 0f; 


        Vector3 newPosition = transform.position + (-worldMove * Time.deltaTime);


        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.z = Mathf.Clamp(newPosition.z, minZ, maxZ);

        transform.position = newPosition;
    }
}