using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 敵を管理するスーパークラス（基底クラス）
/// </summary>
public class Enemy : MonoBehaviour
{
    //コンポーネント
    protected Animator animator;
    protected NavMeshAgent agent;

    //外部参照
    protected GameObject player;

    //HP関連
    protected int hp;
    protected int maxHp;

    //追跡フラグ
    public bool fieldViewFlg;

    //無敵時間
    protected float invincibleTime;
    //敵ごとの無敵時間
    protected float setInvincibleTime;

    //死亡時フラグ
    protected bool dieFlg;
    //死亡時沈む経過時間
    protected float dieDelayTime;

    protected void Awake()
    {
        //コンポーネント取得
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        //プレイヤーを取得
        player = GameObject.FindWithTag("Player");
    }

    private void OnTriggerStay(Collider other)
    {
        //無敵時間中及び死亡時
        if (invincibleTime > 0 || hp <= 0) return;
        //プレイヤーの攻撃を受けた
        if (other.gameObject.tag == "Attack")
        {
            //ダメージ値を取得
            int d = other.GetComponent<AttackController>().Atk;
            Damage(d);
        }
    }

    //ダメージ処理
    protected virtual void Damage(int d)
    {
        //HP減少（変更）
        //CeilToInt:切り上げつつintに変換
        hp -= d + Mathf.CeilToInt(d * MyPlayer.Instance.Atk * 0.1f);

        //無敵時間セット
        invincibleTime = setInvincibleTime;
        //ダメージアニメーション
        animator.SetTrigger("DamageTrigger");
        //HPゼロで死亡処理
        if (hp <= 0)
        {
            Die();
        }
    }

    //死亡時処理
    protected virtual void Die()
    {
        //死亡アニメーション
        animator.SetTrigger("DieTrigger");
        GetComponent<Rigidbody>().isKinematic = true;
        //2秒後に沈む処理を開始
        StartCoroutine(DelayMethod(2.0f, () => {
            //死亡フラグで沈む処理開始
            dieFlg = true;
            //NavMesh機能をオフにしないと地面にめり込まない
            agent.enabled = false;
            //沈みきるまでの時間タイムラグを与え消滅させる
            Destroy(gameObject, dieDelayTime);
        }));

        //経験値セット（追加）
        SetExp();
    }

    //経験値をセット（追加）
    protected virtual void SetExp()
    {

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