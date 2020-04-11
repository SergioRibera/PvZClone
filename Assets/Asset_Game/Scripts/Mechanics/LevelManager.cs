using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour
{
    public Transform gardenParent;
    public EnemyScriptable EnemiesDB;
    public LevelPrefabs prefabs;
    public LevelSettings settings;

    List<GameObject> cells = new List<GameObject>();
    List<Path> Paths = new List<Path>();

    internal List<PlayerSettings> plantasSeleccionadas = new List<PlayerSettings>();
    public static LevelManager Singleton;

    private void Start()
    {
        if (Singleton == null)
            Singleton = this;
        GenerateGarden();
        GeneratePath();
        SettingPodadoras();
        ae = settings.enemiesOnGame;
    }
    public void Init()
    {
        SelectEquip.Singleton.ResetPlants();
        StartCoroutine(StartGame());
    }

    private void SettingPodadoras()
    {
        for (int i = 0; i < Paths.Count; i++)
        {
            Path p = Paths[i];
            cells[i].GetComponent<Podadora>().Init(p);
        }
    }

    IEnumerator StartGame()
    {
        yield return settings.startTimeWait;
        StartCoroutine(CreatePlayers());
    }
    int auxTime = 1;
    public float ae = 0;
    IEnumerator CreatePlayers()
    {
        while (auxTime < settings.partideTime)
        {
            float l = (Mathf.Log10(auxTime));
            float a = ae / (auxTime / l);
            yield return new WaitForSecondsRealtime(2 * a);
            auxTime++;
            ae -= l * 2;
            StartCoroutine(CreateEnemy(a));
        }
    }
    void GenerateGarden()
    {
        if (gardenParent.childCount > 0)
            while (gardenParent.childCount > 0)
                Destroy(gardenParent.GetChild(0));
        for (int x = 0; x < settings.gardenSize.x; x++)
        {
            for (int y = 0; y < settings.gardenSize.y; y++)
            {
                Transform t = null;
                if (x == 0)
                {
                    t = Instantiate(prefabs.podadoraPrefab).transform;
                }
                else
                {
                    if (settings.hasWater)
                    {
                        if (y == (settings.waterLocation - 1))
                            t = Instantiate(prefabs.waterPrefab).transform;
                        else
                            t = Instantiate(prefabs.grassPrefab).transform;
                    }
                    else
                        t = Instantiate(prefabs.grassPrefab).transform;
                }
                t.name = "x:" + x + "|y:" + y;
                cells.Add(t.gameObject);
                t.SetParent(gardenParent);
                Vector3 v = Vector3.zero;
                if (x != 0)
                {
                    v = new Vector3(settings.startGarden.x + ((settings.grassSize.x + settings.distanceGrass) * (x % settings.gardenSize.x)), settings.startGarden.y + ((settings.grassSize.y + settings.distanceGrass) * (y % settings.gardenSize.y)), 0);
                    t.localScale = new Vector3(settings.grassSize.x, settings.grassSize.y, 1);
                }
                else
                {
                    v = new Vector3(settings.startGarden.x + ((settings.grassSize.x + settings.distanceGrass) * (x % settings.gardenSize.x)), (settings.startGarden.y + ((settings.grassSize.y + settings.distanceGrass) * (y % settings.gardenSize.y))) - .2f, 0);
                }
                t.position = v;
            }
        }
    }
    void GeneratePath()
    {
        Transform c = GenerateCenterEndPositionEnemy();
        for (int i = 0; i < settings.gardenSize.y; i++)
        {
            Path p = new Path();
            p.startPos = GenerateSpawnedPositionsEnemy(i).position;
            p.endPos = GenerateEndPositionEnemy(i).position;
            p.centerPos = c.position;
            if (settings.hasWater)
            {
                if (i == (settings.waterLocation - 1))
                    p.typePath = TypePath.Water;
                else
                    p.typePath = TypePath.Grass;
            }else
                p.typePath = TypePath.Grass;
            Paths.Add(p);
        }
    }
    Transform GenerateSpawnedPositionsEnemy(int y)
    {
        Transform t = Instantiate(prefabs.emptyPrefab).transform;
        t.name = "y: " + y;
        cells.Add(t.gameObject);
        t.SetParent(gardenParent);
        Vector3 v = new Vector3(settings.startSpawnedEnemy + ((settings.grassSize.x + settings.distanceGrass) * (0 % settings.gardenSize.x)), settings.startGarden.y + ((settings.grassSize.y + settings.distanceGrass) * (y % settings.gardenSize.y)), -.2f);
        t.position = v;
        t.localScale = new Vector3(settings.grassSize.x, settings.grassSize.y, 1);
        return t;
    }
    Transform GenerateEndPositionEnemy(int y)
    {
        Transform t = Instantiate(prefabs.emptyPrefab).transform;
        t.name = "End Position: " + y;
        cells.Add(t.gameObject);
        t.SetParent(gardenParent);
        Vector3 v = new Vector3((settings.startGarden.x - 1) + ((settings.grassSize.x + settings.distanceGrass) * (0 % settings.gardenSize.x)), settings.startGarden.y + ((settings.grassSize.y + settings.distanceGrass) * (y % settings.gardenSize.y)), -.2f);
        t.position = v;
        t.localScale = new Vector3(settings.grassSize.x, settings.grassSize.y, 1);
        return t;
    }
    Transform GenerateCenterEndPositionEnemy()
    {
        Transform t = Instantiate(prefabs.emptyPrefab).transform;
        t.name = "Center Position: " + 2;
        cells.Add(t.gameObject);
        t.SetParent(gardenParent);
        Vector3 v = new Vector3((settings.startGarden.x - 3) + ((settings.grassSize.x + settings.distanceGrass) * (0 % settings.gardenSize.x)), settings.startGarden.y + ((settings.grassSize.y + settings.distanceGrass) * (2 % settings.gardenSize.y)), -.2f);
        t.position = v;
        t.localScale = new Vector3(settings.grassSize.x, settings.grassSize.y, 1);
        return t;
    }

    IEnumerator CreateEnemy(float f)
    {
        yield return new WaitForSeconds(f);
        Transform e = Instantiate(prefabs.enemyPrefab).transform;
        EnemyManager em = e.GetComponent<EnemyManager>();
        Path p = GetRandomPath();
        EnemySettings es = GetRandomSetting();
        e.position = p.startPos;
        em.Init(p, es);
    }
    EnemySettings GetRandomSetting()
    {
        int r = Random.Range(0, 1);

        return EnemiesDB.FindEnemyPerType((EnemyType)r);
    }
    Path GetRandomPath()
    {
        return Paths[Random.Range(0, Paths.Count)];
    }
}