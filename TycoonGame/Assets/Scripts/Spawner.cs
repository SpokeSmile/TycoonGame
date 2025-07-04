using UnityEngine;


public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject motherboard_board;
    [SerializeField] private GameObject EndPosition;
    [SerializeField] private GameObject SpawnPosition;
    private float spawnInterval = 5f;
    private float nextSpawnTime = 1f; 

   void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            GameObject newBoard = Instantiate(motherboard_board, SpawnPosition.transform.position, Quaternion.identity);

            ConveyorMovement conveyorMovement = newBoard.GetComponent<ConveyorMovement>();
            if (conveyorMovement != null)
            {
                conveyorMovement.EndPosition = EndPosition.transform;
            }

            nextSpawnTime = Time.time + spawnInterval;
            

            }
    }
}