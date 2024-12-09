using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.GlobalIllumination;

public class SpawnManager : MonoBehaviour
{

    public static SpawnManager Instance { get; private set; }
    public static UnityEvent<LevelSO> OnSpawnCat = new();
    public static UnityEvent<LevelSO> OnSpawnFinishLine = new();

    [SerializeField] private FinishLine finishLine1;
    // [SerializeField] private FinishLine finishLine2;

    void OnEnable()
    {
        OnSpawnCat.AddListener(SpawnCat);
        OnSpawnFinishLine.AddListener(SpawnFinishline);
    }
    void OnDisable()
    {
        OnSpawnCat.RemoveListener(SpawnCat);
        OnSpawnFinishLine.RemoveListener(SpawnFinishline);


    }
    private void SpawnCat(LevelSO level)
    {
        level.cats.ForEach(cat =>
        {
            Spawn(cat.prefab, cat.GetCatPos());
        });
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
