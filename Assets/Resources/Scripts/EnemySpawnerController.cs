using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �G�L�������N���[������X�|�i�[
/// </summary>
public class EnemySpawnerController : MonoBehaviour
{
    //�G��
    enum EnemyType
    {
        Zombie
    }
    [SerializeField] EnemyType enemyType;

    //�����Ԋu
    [SerializeField] float span = 5;

    //�ϐ�
    GameObject enemyPrefab;
    float delta;

    //�ő吔�i�ǉ��j
    int maxEnemy;
    //���ݐ��i�ǉ��j
    int currentEnemy;

    //�z�u�ꏊ���X�g���󂫏��i�z�u�ꏊ�I�u�W�F�N�g, false:�󁄁i�ǉ��j
    Dictionary<Transform, bool> spawnList = new Dictionary<Transform, bool>();


    void Start()
    {
        enemyPrefab = Resources.Load<GameObject>("Prefabs/_Enemys/" + enemyType.ToString());

        //�z�u�ꏊ���X�g�擾�i�ǉ��j
        int i = 0;
        foreach (Transform child in transform)
        {
            if (child.name == "ZombieSpawn" + i)
            {
                spawnList.Add(child, false);
                i++;
            }
        }
        //�ő吔�Z�b�g�i�ǉ��j
        maxEnemy = spawnList.Count;
    }

    void Update()
    {
        //�ő吔�𒴂�����N���[�������̓X�g�b�v�i�ǉ��j
        if (currentEnemy >= maxEnemy) return;

        //��莞�Ԃ��ƂɓG���N���[������i�ǉ��j
        if (delta > span)
        {
            GameObject zombie = Instantiate(enemyPrefab, transform.position, Quaternion.identity);

            //SpawnList���ŋ󂢂Ă���ړI�n���������ă]���r�̖ړI�n�փZ�b�g����i�ǉ��j
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