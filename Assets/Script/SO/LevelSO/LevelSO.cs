using System;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Level", menuName = "Scriptable Objects/LevelSO")]

public class LevelSO : ScriptableObject
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public LevelSO DeepCopy(LevelSO other) // this for copy constructor 
    {
        return new LevelSO
        {
            cats = other.cats,
            objectPrefabs = other.objectPrefabs,
            totalOjb = other.totalOjb,

        };
    }
    public LevelSO()
    {

    }
    public List<CatData> cats = new List<CatData>();
    public ChasingSO chasingSO;

    public List<Transform> objectPrefabs = new();
    public int totalOjb = 5;

    public static LevelSO GetLevelSOBaseOnThisSO(LevelSO thisLevelSO, int levelMultiplyer)
    {
        Debug.Log("levelMultiplyer" + levelMultiplyer);
        LevelSO newLevelSO = thisLevelSO.DeepCopy(thisLevelSO);

        newLevelSO.chasingSO = ChasingSO.GetChasingSOBaseOnThisSO(thisLevelSO.chasingSO, levelMultiplyer);

        List<CatData> newCatData = new List<CatData>();
        const float roadWidthMax = 100;
        float phase2Distance = newLevelSO.chasingSO.phases[1].distance;
        for (int i = 0; i < thisLevelSO.cats.Count * levelMultiplyer; i++)
        {
            CatData catData = new();
            catData.prefab = thisLevelSO.cats[0].prefab;//copy the first prefab for now

            float randomWidth = UnityEngine.Random.Range(0, roadWidthMax);
            float randomLength = UnityEngine.Random.Range(0, phase2Distance);//spawn object only at phase 2 not last phase
            Vector3 randomPosOnRoad = MyUnit.GetMyVecUnit(new Vector3(randomWidth, 0, randomLength)); // random pos that cat spawn
            catData.spawnPositionXZ.x = randomPosOnRoad.x;
            catData.spawnPositionXZ.y = randomPosOnRoad.z;

            newCatData.Add(catData);
        }
        newLevelSO.cats = newCatData;
        newLevelSO.totalOjb *= levelMultiplyer;
        return newLevelSO;
    }
}
[Serializable]
public class CatData
{
    [SerializeField] public Vector2 spawnPositionXZ;
    public Transform prefab;
    public float roamingRange = 1;//
    public float moveSpeed = 1;

    public Vector3 GetCatPos()
    {// Change to my game unit
        return MyUnit.GetMyVecUnit(new Vector3(spawnPositionXZ.x, 0, spawnPositionXZ.y));
    }
}