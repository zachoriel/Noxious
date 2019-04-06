using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public static ItemSpawner instance;

    List<GameObject> itemList = new List<GameObject>();
    public GameObject injection, vial, gasMask, pickupsParent;

    List<GameObject> enemyList = new List<GameObject>();
    public GameObject enemy1, enemyParent;

    public GameObject playerPrefab, portalPrefab;

    public Vector3 center, size;
    float radius;
    public Collider[] colliders;
    public LayerMask mask;
    public LayerMask groundMask;
    public bool mapBelow;

    [Tooltip("Don't touch these, they're just visible here so that I can see how many there are in any particular session")]
    [SerializeField] int totalInjections, totalVials, totalGasMasks, totalEnemies;
    [SerializeField] float waveIntervalTime = 15f;
    int enemiesThisWave;

    void Awake()
    {
        #region Singleton
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        #endregion
    }

    // Use this for initialization
    void Start()
    {
        radius = size.magnitude;

        AddPrefabsToList();
        SetChancesForItems();
        SetEnemies();

        SpawnItems();
        SpawnParasites();
        SpawnPlayer();
        SpawnPortal();
    }

    #region Spawner Loops
    void SpawnItems()
    {
        for (int injectionsNumber = 0; injectionsNumber < totalInjections; injectionsNumber++)
        {
            SpawnInjections();
        }

        for (int vialsNumber = 0; vialsNumber < totalVials; vialsNumber++)
        {
            SpawnVials();
        }

        for (int gasMaskNumber = 0; gasMaskNumber < totalGasMasks; gasMaskNumber++)
        {
            SpawnGasMasks();
        }
    }

    void SpawnParasites()
    {
        for (int enemiesNumber = 0; enemiesNumber < totalEnemies; enemiesNumber++)
        {
            SpawnEnemies();
        }
    }
    #endregion

    #region Spawners
    void SpawnPlayer()
    {
        Vector3 spawnPos = new Vector3(0f, 0f, 0f);
        bool canSpawnHere = false;
        int safetyNet = 0;

        while (!canSpawnHere)
        {
            spawnPos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2));
            canSpawnHere = PreventSpawnOverlap(spawnPos);

            if (canSpawnHere)
            {
                break;
            }

            safetyNet++;

            if (safetyNet > 100)
            {
                break;
            }
        }

        GameObject player = Instantiate(playerPrefab, spawnPos, Quaternion.identity);
    }

    void SpawnPortal()
    {
        Vector3 spawnPos = new Vector3(0f, 0f, 0f);
        bool canSpawnHere = false;
        int safetyNet = 0;

        while (!canSpawnHere)
        {
            spawnPos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2));
            canSpawnHere = PreventSpawnOverlap(spawnPos);

            if (canSpawnHere)
            {
                break;
            }

            safetyNet++;

            if (safetyNet > 500)
            {
                break;
            }
        }

        GameObject portal = Instantiate(portalPrefab, spawnPos, Quaternion.identity);
    }

    void SpawnInjections() // Health boosters
    {
        Vector3 spawnPos = new Vector3(0f, 0f, 0f);
        bool canSpawnHere = false;
        int safetyNet = 0;

        while (!canSpawnHere)
        {
            spawnPos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2));
            canSpawnHere = PreventSpawnOverlap(spawnPos);

            if (canSpawnHere)
            {
                break;
            }

            safetyNet++;

            if (safetyNet > 100)
            {
                break;
            }
        }

        GameObject BoosterInjection = Instantiate(itemList[0], spawnPos, Quaternion.identity, pickupsParent.transform);
    }

    void SpawnVials() // Cure ingredients
    {
        Vector3 spawnPos = new Vector3(0f, 0f, 0f);
        bool canSpawnHere = false;
        int safetyNet = 0;

        while (!canSpawnHere)
        {
            spawnPos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2));
            canSpawnHere = PreventSpawnOverlap(spawnPos);

            if (canSpawnHere)
            {
                break;
            }

            safetyNet++;

            if (safetyNet > 100)
            {
                break;
            }
        }

        GameObject HealthVial = Instantiate(itemList[1], spawnPos, Quaternion.identity, pickupsParent.transform);
    }

    void SpawnGasMasks() // Damage reduction
    {
        Vector3 spawnPos = new Vector3(0f, 0f, 0f);
        bool canSpawnHere = false;
        int safetyNet = 0;

        while (!canSpawnHere)
        {
            spawnPos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2));
            canSpawnHere = PreventSpawnOverlap(spawnPos);

            if (canSpawnHere)
            {
                break;
            }

            safetyNet++;

            if (safetyNet > 100)
            {
                break;
            }
        }

        GameObject GasMask = Instantiate(itemList[2], spawnPos, Quaternion.identity, pickupsParent.transform);
    }

    void SpawnEnemies()
    {
        Vector3 spawnPos = new Vector3(0f, 0f, 0f);
        bool canSpawnHere = false;
        int safetyNet = 0;

        while (!canSpawnHere)
        {
            spawnPos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2));
            canSpawnHere = PreventSpawnOverlap(spawnPos);

            if (canSpawnHere)
            {
                break;
            }

            safetyNet++;

            if (safetyNet > 100)
            {
                break;
            }
        }

        GameObject Enemy = Instantiate(enemyList[0], spawnPos, Quaternion.identity, enemyParent.transform);
    }

    IEnumerator SpawnWavesOfEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(waveIntervalTime);
            enemiesThisWave = Random.Range(2, 5);
            for (int enemiesNumber = 0; enemiesNumber < enemiesThisWave; enemiesNumber++)
            {
                SpawnEnemies();
            }
        }       
    }
    #endregion

    bool PreventSpawnOverlap(Vector3 pos)
    {
        colliders = Physics.OverlapSphere(center, radius, mask);

        for (int i = 0; i < colliders.Length; i++)
        {
            Vector3 centerPoint = colliders[i].bounds.center;
            float width = colliders[i].bounds.extents.x;
            float height = colliders[i].bounds.extents.y;

            float leftExtent = centerPoint.x - width;
            float rightExtent = centerPoint.x + width;
            float lowerExtent = centerPoint.y - height;
            float upperExtent = centerPoint.y + height;

            if (pos.x >= leftExtent && pos.x <= rightExtent)
            {
                if (pos.y >= lowerExtent && pos.y <= upperExtent)
                {
                    return false;
                }
            }
        }

        RaycastHit hit;
        mapBelow = Physics.Raycast(pos, Vector3.down, out hit, 5f, groundMask);

        if (!mapBelow)
        {
            return false;
        }

        return true;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(center, size);
    }

    void AddPrefabsToList()
    {
        itemList.AddMany(injection, vial, gasMask);
        enemyList.Add(enemy1);
    }

    void SetChancesForItems()
    {
        switch (DifficultySelection.instance.difficulty)
        {
            case DifficultySelection.Difficulties.easy:
                totalVials = 10;
                totalInjections = Random.Range(10, 20);
                totalGasMasks = 2;
                break;
            case DifficultySelection.Difficulties.normal:
                totalVials = 10;
                totalInjections = Random.Range(10, 20);
                totalGasMasks = 1;
                break;
            case DifficultySelection.Difficulties.hard:
                totalVials = 15;
                totalInjections = Random.Range(10, 15);
                totalGasMasks = 1;
                break;
            case DifficultySelection.Difficulties.insane:
                totalVials = 20;
                totalInjections = 10;
                totalGasMasks = 1;
                break;
        }
    }

    void SetEnemies()
    {
        switch (DifficultySelection.instance.difficulty)
        {
            case DifficultySelection.Difficulties.easy:
                totalEnemies = 0;
                break;
            case DifficultySelection.Difficulties.normal:
                totalEnemies = 0;
                break;
            case DifficultySelection.Difficulties.hard:
                totalEnemies = Random.Range(10, 20);
                break;
            case DifficultySelection.Difficulties.insane:
                totalEnemies = Random.Range(10, 20);
                StartCoroutine(SpawnWavesOfEnemies());
                break;
        }
    }
}
