using UnityEngine;

public class ConveyorMovement : MonoBehaviour
{
    [SerializeField] private GameObject board_lvl1;
    [SerializeField] private MeshRenderer mesh_board;
    [SerializeField] private MeshRenderer mesh_motherboard;
    [SerializeField] private MeshRenderer mesh_CPU;
    [SerializeField] private MeshRenderer mesh_funCPU;
    [SerializeField] private MeshRenderer mesh_GPU;
    [SerializeField] private MeshRenderer mesh_RAM;
    [SerializeField] private MeshRenderer mesh_case;
    [SerializeField] private MeshRenderer mesh_case_door;

    private bool isTouchingStation = false;
    public bool IsTouchingStation => isTouchingStation;

    public Transform EndPosition;
    public float speed = 0.5f;

    void Start()
    {
        mesh_board.enabled = true;

        mesh_motherboard.enabled = false;
        mesh_CPU.enabled = false;
        mesh_funCPU.enabled = false;
        mesh_GPU.enabled = false;
        mesh_RAM.enabled = false;
        mesh_case.enabled = false;
        mesh_case_door.enabled = false;
    }

    void Update()
    {


        if (isTouchingStation)
        {
            MoveToTarget();
            mesh_board.enabled = false;
            mesh_motherboard.enabled = true;
            
        }
        else
        {
            MoveToTarget();
        }

        if (Vector3.Distance(transform.position, EndPosition.transform.position) < 0.1f)
        {
            Destroy(gameObject);
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