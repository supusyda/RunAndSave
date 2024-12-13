
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.GlobalIllumination;

public class SpawnManager : MonoBehaviour
{

    public static SpawnManager Instance { get; private set; }

    public static UnityEvent<LevelSO> Init = new();


    [SerializeField] private FinishLine finishLine1;
    // [SerializeField] private FinishLine finishLine2;

    void OnEnable()
    {

        Init.AddListener(InitGame);
    }

    private void InitGame(LevelSO level)
    {
        SpawnCat(level);
        SpawnFinishline(level);
        SpawnObstacleObj(level);

    }

    void OnDisable()
    {

        Init.RemoveListener(InitGame);


    }
    private void SpawnCat(LevelSO level)
    {
        level.cats.ForEach(cat =>
        {
            Transform spawn = Spawn(cat.prefab, cat.GetCatPos());
            Debug.Log("SPAWN CATTTTTTTTTTTTTTTTTTTTTTTTTTt" + spawn);
        });


    }
    private void SpawnObstacleObj(LevelSO level)
    {
        const float roadWidthMax = 100;
        float phase2Distance = level.chasingSO.phases[1].distance;
        for (int i = 0; i < level.totalOjb; i++)
        {
            int randPrefab = Random.Range(0, level.objectPrefabs.Count);
            float randomWidth = Random.Range(0, roadWidthMax);
            float randomLength = Random.Range(0, phase2Distance);//spawn object only at phase 2 not last phase

            Vector3 randomPosOnRoad = MyUnit.GetMyVecUnit(new Vector3(randomWidth, 0, randomLength));
            Transform spawn = Spawn(level.objectPrefabs[randPrefab], randomPosOnRoad);
            spawn.gameObject.SetActive(true);
        }




    }
    private void SpawnFinishline(LevelSO level)
    {
        for (int i = 0; i < level.chasingSO.phases.Count; i++)
        {
            if (i == 0) continue; // Skip the first phase
            Debug.Log("SKIP i == 0" + i);
            Phase phase = level.chasingSO.phases[i];

            Vector3 finishLinePos = MyUnit.GetMyVecUnit(new Vector3(0, 0, phase.distance));
            finishLinePos.x = 5;// 5 is slight offset

            Transform spawnedFinishLine = Spawn(finishLine1.transform, finishLinePos);
            spawnedFinishLine.GetComponent<FinishLine>().thisFinishID = i;
            spawnedFinishLine.gameObject.SetActive(true);
        }

    }
    private Transform Spawn(Transform prefab, Vector3 spawnPos)
    {
        Transform spawnedTrans = Instantiate(prefab);
        spawnedTrans.transform.position = spawnPos;
        return spawnedTrans;

    }
    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this; // Set the instance
                         // DontDestroyOnLoad(gameObject); // Keep the instance across scenes
    }

}
