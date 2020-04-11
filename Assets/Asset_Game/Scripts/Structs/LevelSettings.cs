using System;
using UnityEngine;

[Serializable]
public class LevelSettings
{
    public Vector2 startGarden;
    public Vector2Int gardenSize;
    public Vector2 grassSize;
    public float distanceGrass;
    public bool hasWater;
    public int waterLocation;
    public int startSpawnedEnemy;
    /// <summary>
    /// El tiempo que se espera para lanzar la primer instancia
    /// </summary>
    public float startTimeWait;
    public float partideTime;
    public Vector2 timeWaitFoNextEnemy;
    [Range(10, 1000, order = 10)]
    public float enemiesOnGame;
}