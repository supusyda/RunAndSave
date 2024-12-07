using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnManager : MonoBehaviour
{

    public static SpawnManager Instance { get; private set; }
    public static UnityEvent<LevelSO> OnSpawnCat = new();
    void OnEnable()
    {
        OnSpawnCat.AddListener(SpawnCat);
    }

    private void SpawnCat(LevelSO level)
    {
        level.cats.ForEach(cat =>
        {
            Spawn(cat.prefab, cat.GetCatPos());
        });
    }

    private void Spawn(Transform prefab, Vector3 spawnPos)
    {
        Transform spawnedTrans = Instantiate(prefab);
        spawnedTrans.transform.position = spawnPos;

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
