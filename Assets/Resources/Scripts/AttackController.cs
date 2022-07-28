using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 攻撃時のダメージ処理
/// </summary>
public class AttackController : MonoBehaviour
{

    //攻撃の種類
    public enum AttackType
    {
        rotatingKick,
        other
    }
    [SerializeField] AttackType type;

    //攻撃力
    int atk;

    void Start()
    {

        switch (type)
        {
            //回転蹴り
            case AttackType.rotatingKick:
                atk = 1;
                //当たり判定のコライダーは炎エフェクトよりも長く滞在し回転しているため1秒後には当たり判定を無効にする
                StartCoroutine(DelayMethod(1, () => {
                    GetComponent<SphereCollider>().enabled = false;
                }));
                break;

            //その他
            case AttackType.other:
                atk = 0;
                break;
        }
    }

    //攻撃力Atk値のプロパティ
    public int Atk
    {
        get { return atk; }
        set { atk = value; }
    }

    /// <summary>
    /// 渡された処理を指定時間後に実行する
    /// </summary>
    /// <param name="waitTime">遅延時間[ミリ秒]</param>
    /// <param name="action">実行したい処理</param>
    /// <returns></returns>
    private IEnumerator DelayMethod(float waitTime, System.Action action)
    {
        yield return new WaitForSeconds(waitTime);
        action();
    }
}