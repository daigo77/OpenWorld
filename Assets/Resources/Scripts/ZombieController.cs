using UnityEngine;
using UnityEngine.UI;

//必須コンポーネント
[RequireComponent(typeof(CapsuleCollider))]

/// <summary>
/// ゾンビを制御
/// </summary>
public class ZombieController : Enemy
{

    //移動速度
    [SerializeField] float speed = 4f;
    //HPゲージ
    Slider enemyHp;
    //目的地セットフラグ
    bool destinationFlg;
    //目的地オブジェクト
    GameObject destinationObj;

    //Awakeに
    void Awake()
    {

        //基底クラスのAwake
        base.Awake();

        //HP初期化
        maxHp = 10;
        hp = maxHp;
        enemyHp = transform.Find("EnemyHp/Slider").GetComponent<Slider>();
        enemyHp.value = (float)hp / (float)maxHp;

        //移動速度
        agent.speed = 0;
        //個別無敵時間
        setInvincibleTime = 2f;
        //死亡時沈む経過時間
        dieDelayTime = 3f;
    }

    void Update()
    {

        //死亡後徐々に沈む
        if (dieFlg)
        {
            transform.Translate(0, -Time.deltaTime * 0.1f, 0);
            return;
        }

        //無敵時間中は動かないようにする
        if (invincibleTime > 0)
        {
            invincibleTime -= Time.deltaTime;
            return;
        }

        //視野内へプレイヤーが侵入した
        if (fieldViewFlg)
        {
            //目的地をプレイヤーに設定
            agent.destination = player.transform.position;
            //移動速度を徐々に上げる
            speed += Time.deltaTime;
            speed = Mathf.Clamp(speed, 0, 4f);
            agent.speed = speed;
            //アニメーション切替
            animator.SetBool("RunFlg", true);

            //目的地がセットされている場合
        }
        else if (destinationFlg)
        {
            //移動速度設定
            agent.speed = 1;
            //アニメーション切替
            animator.SetBool("RunFlg", true);

        }
        else
        {
            //目的地を自身にして動きを止める
            agent.destination = transform.position;
            //移動速度を一旦リセット
            agent.speed = 0;
            //アニメーション切替
            animator.SetBool("RunFlg", false);
        }
    }

    //衝突時
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            //攻撃アニメーションをON
            animator.SetBool("AttackFlg", true);
        }
    }

    //衝突解除
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            //攻撃アニメーションをOFF
            animator.SetBool("AttackFlg", false);
        }
    }

    //接触時
    private void OnTriggerEnter(Collider other)
    {
        //目的地と接触した場合
        if (other.gameObject == destinationObj)
        {
            //目的地解除
            destinationObj = null;
            destinationFlg = false;
        }
    }

    //ダメージ処理
    protected override void Damage(int d)
    {

        //基底クラスのDamage
        base.Damage(d);

        //HPゲージ更新
        enemyHp.value = (float)hp / (float)maxHp;
        //移動速度を0にして動きを止める
        agent.speed = 0;
        //吹っ飛ばし効果
        Vector3 force = (transform.position - player.transform.position).normalized;
        Rigidbody rd = GetComponent<Rigidbody>();
        rd.AddForce(force * 10, ForceMode.Impulse);
        //SE
        GSound.Instance.PlaySe("zombieDamage");
    }

    //死亡時処理
    protected override void Die()
    {
        //基底クラスのDie
        base.Die();
        //当たり判定を無効
        GetComponent<CapsuleCollider>().enabled = false;
        //SE
        GSound.Instance.PlaySe("zombieDie");
    }

    //目的地をセット（追加）
    public void SetDestination(Transform obj)
    {
        //目的地設定
        destinationObj = obj.gameObject;
        agent.destination = obj.position;
        destinationFlg = true;
    }

    //経験値をプレイヤーに与える（追加）
    protected override void SetExp()
    {
        player.GetComponent<PlayerController>().GetExp(20);
    }
}