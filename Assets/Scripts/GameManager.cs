using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<GameObject> Roads = new List<GameObject>();
    [SerializeField] Transform carSpawn;
    [SerializeField] private float roadTileSize = 20f; // Prefabın gerçek Z uzunluğuna göre ayarla!

    private Transform player;
    private float previousPlayerZ;
    private float roadLength = 0f;

    // Arkada 1, önde 3 yol = toplam 5
    private int roadsBeforePlayer = 3;
    private int roadsAheadPlayer = 10;

    private List<GameObject> spawnedRoads = new List<GameObject>();

    void Awake()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;
        else
            Debug.LogError("Sahnede 'Player' tag'li obje bulunamadı!");
    }

    void Start()
    {
        if (Roads == null || Roads.Count == 0)
        {
            Debug.LogError("Roads listesi boş! Inspector'dan prefab ekle.");
            return;
        }

        if (player == null) return;

        previousPlayerZ = player.position.z;

        // Arkadaki yoldan başla
        // Örn: roadsBeforePlayer=1 ise oyuncunun 1 tile gerisinden başla
        roadLength = player.position.z - (roadsBeforePlayer * roadTileSize);

        int totalRoads = roadsBeforePlayer + 1 + roadsAheadPlayer; // 1+1+3 = 5
        for (int i = 0; i < totalRoads; i++)
        {
            CreateRoad();
        }
    }

    private void FixedUpdate()
    {
        if (player == null) return;

        float deltaZ = player.position.z - previousPlayerZ;
        carSpawn.position += new Vector3(0, 0, deltaZ);
        previousPlayerZ = player.position.z;
    }

    void Update()
    {
        if (player == null || spawnedRoads.Count == 0) return;

        GameObject oldestRoad = spawnedRoads[0];

        if (oldestRoad == null)
        {
            spawnedRoads.RemoveAt(0);
            return;
        }

        // En arkadaki yolun ön kenarı oyuncunun 1 tile gerisine düştüğünde:
        // → onu sil, en öne yeni yol ekle
        float roadFrontEdge = oldestRoad.transform.position.z + roadTileSize;
        float deleteThreshold = player.position.z - (roadsBeforePlayer * roadTileSize);

        if (roadFrontEdge < deleteThreshold)
        {
            Destroy(oldestRoad);
            spawnedRoads.RemoveAt(0);
            CreateRoad();
        }
    }

    void CreateRoad()
    {
        if (Roads == null || Roads.Count == 0) return;

        int randomIndex = Random.Range(0, Roads.Count);
        GameObject selectedRoad = Roads[randomIndex];

        if (selectedRoad == null)
        {
            Debug.LogError($"Roads[{randomIndex}] null!");
            return;
        }

        Vector3 spawnPos = new Vector3(0f, 0f, roadLength);
        GameObject newRoad = Instantiate(selectedRoad, spawnPos, Quaternion.identity);
        spawnedRoads.Add(newRoad);

        roadLength += roadTileSize;
    }
}