using UnityEngine;

public class ConveyorMovement : MonoBehaviour
{
    public MeshRenderer mesh_board;
    public MeshRenderer mesh_motherboard;
    // public MeshRenderer mesh_CPU;
    public MeshRenderer mesh_funCPU;
    public MeshRenderer mesh_GPU;
    public MeshRenderer mesh_RAM;
    public MeshRenderer mesh_case;
    public MeshRenderer mesh_case_door;
    
    private bool isTouchingStation = false;
    public bool IsTouchingStation => isTouchingStation;

    public Transform EndPosition;
    public float speed = 0.5f;

    void Start()
    {
        mesh_board.enabled = true;

        mesh_motherboard.enabled = false;
        // mesh_CPU.enabled = false;
        mesh_funCPU.enabled = false;
        mesh_GPU.enabled = false;
        mesh_RAM.enabled = false;
    }

    void Update()
    {
        
        mesh_case.enabled = false;
        mesh_case_door.enabled = false;
        if (isTouchingStation)
        {
            MoveToTarget();
            mesh_motherboard.enabled = true;
            mesh_board.enabled = false;
        }
        else
        {
            MoveToTarget();
        }
    }

    void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, EndPosition.position, speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("machine.01.01"))
        {
            isTouchingStation = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("machine.01.01"))
        {
            isTouchingStation = false;
        }
    }

}