using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    [SerializeField] private GameObject[] objectsToSpawn;
    [SerializeField] private int count = 25;
    [SerializeField] private Vector2 areaSize = new Vector2(25f, 25f);

    [Header("Exclude Area")]
    [SerializeField] private Transform buildingTransform; // перетащи здание сюда в инспекторе
    [SerializeField] private float excludeRadius = 5f;     // радиус, вокруг которого не спавним

    void Start()
    {
        int spawned = 0;
        int attempts = 0;

        while (spawned < count && attempts < count * 10)
        {
            attempts++;

            float x = Random.Range(-areaSize.x / 2f, areaSize.x / 2f);
            float z = Random.Range(-areaSize.y / 2f, areaSize.y / 2f);
            float y = 0f;

            Vector3 position = new Vector3(x, y, z);

            // Проверка: слишком близко к зданию
            if (Vector3.Distance(position, buildingTransform.position) < excludeRadius)
                continue;

            Quaternion rotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
            float scale = Random.Range(0.8f, 1.2f);
            GameObject prefab = objectsToSpawn[Random.Range(0, objectsToSpawn.Length)];

            GameObject instance = Instantiate(prefab, position, rotation);
            instance.transform.localScale *= scale;

            spawned++;
        }
    }
}