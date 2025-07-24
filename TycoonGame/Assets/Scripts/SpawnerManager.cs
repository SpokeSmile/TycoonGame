using UnityEngine;
using System.Collections.Generic;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] private GameObject boardPrefab;
    [SerializeField] private Transform spawnPoint;
    public static float GlobalSpeed = 0.5f;

    private float spawnDelay = 1f;
    private float lastSpawnTime = 0f;

    private Queue<GameObject> boardQueue = new Queue<GameObject>();
    private List<WorkStation> workstations = new List<WorkStation>();

    void Start()
    {
        // Поиск станков
        WorkStation[] stations = FindObjectsByType<WorkStation>(FindObjectsSortMode.None);
        foreach (var station in stations)
        {
            workstations.Add(station);
            station.OnWorkFinished += HandleStationFreed;
        }

        // Первый спавн
        SpawnBoard();
    }

    void Update()
    {
        TryAssignBoardToStation();
    }

    void SpawnBoard()
    {
        GameObject board = Instantiate(boardPrefab, spawnPoint.position, Quaternion.identity);
        boardQueue.Enqueue(board);
        lastSpawnTime = Time.time;
    }

    void TryAssignBoardToStation()
    {
        if (Time.time < lastSpawnTime + spawnDelay) return;

        foreach (var station in workstations)
        {
            if (!station.IsBusy && boardQueue.Count > 0)
            {
                var board = boardQueue.Dequeue();
                station.AssignBoard(board);
                SpawnBoard(); // Поддерживаем очередь
                break;
            }
        }
    }

    void HandleStationFreed(WorkStation station)
    {
        TryAssignBoardToStation();
    }
}