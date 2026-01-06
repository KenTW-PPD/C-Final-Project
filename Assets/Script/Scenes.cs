using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEditor;
public class Scenes : MonoBehaviour
{
    public Vector2 Size;
    [SerializeField] private int Score = 0;
    public Character character;
    [SerializeField] private float time;
    [SerializeField] public List<Monsters> monstersList;
    [SerializeField] private float spawnFrequency = 1.5f;
    [SerializeField] private int amount,limit;
    [SerializeField] private int level = 0;
    [SerializeField] private int monsterLevel = 0;

    WaitForSeconds SpawnFrequencyUP = new WaitForSeconds(60);
    WaitForSeconds LevelUP = new WaitForSeconds(120);

    // 生怪邏輯
    IEnumerator Spawn()
    {
        while (true)
        {
            if (amount < limit)
            {
                int random = Random.Range(1, 101);
                Vector3 randomPosition = new Vector3(Random.Range(-Size.x, Size.x), Random.Range(-Size.y, Size.y), 0);
                // zombie 40% skeleton 20% ranged skeleton 20% nothing 20%
                switch (random)
                {
                    case > 60:
                        Instantiate(monstersList[0], randomPosition, Quaternion.identity);
                        amount = GameObject.FindGameObjectsWithTag("Monsters").Length;
                        break;
                    case > 40:
                        Instantiate(monstersList[1], randomPosition, Quaternion.identity);
                        amount = GameObject.FindGameObjectsWithTag("Monsters").Length;
                        break;
                    case > 20:
                        Instantiate(monstersList[2], randomPosition, Quaternion.identity);
                        amount = GameObject.FindGameObjectsWithTag("Monsters").Length;
                        break;
                    default:
                        break;

                }
            }
            yield return new WaitForSeconds(spawnFrequency);
        }
        
    }
    public void addScore(int score)
    {
        Score += score;
    }
    public int getScore()
    {
        return Score;
    }
    public float getTime()
    {
        return time;
    }
    // 隨時間加快生成速度與加大總怪物上限
    IEnumerator changeSpawnFrequency()
    {
        while (true)
        {
            spawnFrequency = Mathf.Max(0.5f, (spawnFrequency - 0.1f * level));
            limit = limit + level * 5;
            level++;
            yield return SpawnFrequencyUP;
        }
    }
    // 隨時間提升怪物等級
    IEnumerator MonstersLevelUP()
    {
        while (true)
        {
            foreach (Monsters monster in monstersList)
            {
                monster.level = monsterLevel;
            }
            monsterLevel++;
            yield return LevelUP;
        }
    }
    void Start()
    {
        Score = 0;
        time = 30 * 60;
        amount = 0;
        limit = 20;
        StartCoroutine(Spawn());
        StartCoroutine(changeSpawnFrequency());
        StartCoroutine(MonstersLevelUP());
    }
    void Update()
    {
        if (time > 0) 
        { 
            time -= Time.deltaTime;
            
        }
    }
}
