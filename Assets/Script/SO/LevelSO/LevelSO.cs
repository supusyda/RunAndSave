using System;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Level", menuName = "Scriptable Objects/LevelSO")]

public class LevelSO : ScriptableObject
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public List<CatData> cats = new List<CatData>();
    public ChasingSO chasingSO;
    public List<Phase> phases = new List<Phase>();
    public List<Transform> objectPrefabs = new();
    public int totalOjb = 5;


}
[Serializable]
public class CatData
{
    [SerializeField] private Vector2 spawnPositionXZ;
    public Transform prefab;
    public float roamingRange = 1;
    public float moveSpeed = 1;

    public Vector3 GetCatPos()
    {// Change to my game unit
        return MyUnit.GetMyVecUnit(new Vector3(spawnPositionXZ.x, 0, spawnPositionXZ.y));
    }
}