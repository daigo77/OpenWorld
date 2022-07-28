using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵キャラをクローンするスポナー
/// </summary>
public class EnemySpawnerController : MonoBehaviour
{
    //敵種
    enum EnemyType
    {
        Zombie
    }
    [SerializeField] EnemyType enemyType;

    //生成間隔
    [SerializeField] float span = 5;

    //変数
    GameObject enemyPrefab;
    float delta;

    //最大数（追加）
    int maxEnemy;
    //現在数（追加）
    int currentEnemy;

    //配置場所リスト＜空き情報（配置場所オブジェクト, false:空＞（追加）
    Dictionary<Transform, bool> spawnList = new Dictionary<Transform, bool>();


    void Start()
    {
        enemyPrefab = Resources.Load<GameObject>("Prefabs/_Enemys/" + enemyType.ToString());

        //配置場所リスト取得（追加）
        int i = 0;
        foreach (Transform child in transform)
        {
            if (child.name == "ZombieSpawn" + i)
            {
                spawnList.Add(child, false);
                i++;
            }
        }
        //最大数セット（追加）
        maxEnemy = spawnList.Count;
    }

    void Update()
    {
        //最大数を超えたらクローン生成はストップ（追加）
        if (currentEnemy >= maxEnemy) return;

        //一定時間ごとに敵をクローンする（追加）
        if (delta > span)
        {
            GameObject zombie = Instantiate(enemyPrefab, transform.position, Quaternion.identity);

            //SpawnList内で空いている目的地を検索してゾンビの目的地へセットする（追加）
            foreach (KeyValuePair<Transform, bool> child in spawnList)
            {
                if (!child.Value)
                {
                    zombie.GetComponent<ZombieController>().SetDestination(child.Key);
                    spawnList[child.Key] = true;
                    break;
                }
            }
            currentEnemy++;
            delta = 0;
        }
        delta += Time.deltaTime;
    }
}