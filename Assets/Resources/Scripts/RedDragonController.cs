using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// レッドドラゴンの制御
/// </summary>
public class RedDragonController : Enemy
{
    //炎攻撃を生成する場所（頭部）
    [SerializeField] GameObject headPoint;
    //炎エフェクトプレハブ
    GameObject dragonFirePrefab;
    //炎を撒き散らす個数
    int attackCount;
    int attackMaxCount = 50;
    //HPゲージ
    Slider bossHp;
    //ボス名
    string bossName = "レッドドラゴン";
    //咆哮時の攻撃オブジェクト
    GameObject screamAttackPrefab;
    //咆哮中フラグ
    bool screamFlg;

    //ボスバトルエリア（追加）
    GameObject bossBattleArea;

    void Start()
    {

        //炎プレハブ取得
        dragonFirePrefab = Resources.Load<GameObject>("Prefabs/_Enemys/DragonFireEff");
        //HP初期化
        maxHp = 10;
        hp = maxHp;
        bossHp = GameObject.Find("BossHpGauge").GetComponent<Slider>();
        bossHp.value = (float)hp / (float)maxHp;
        //ボス名セット
        GameObject.Find("BossHpGauge").transform.Find("Name").GetComponent<Text>().text = bossName;
        //咆哮取得
        screamAttackPrefab = Resources.Load<GameObject>("Prefabs/_Enemys/ScreamAttack");
        //個別無敵時間
        setInvincibleTime = 0f;
        //死亡時沈む経過時間
        dieDelayTime = 10f;

        //ボスHPゲージ非表示（追加）
        bossHp.gameObject.SetActive(false);
        //ボスバトルエリア（追加）
        bossBattleArea = GameObject.Find("BossBattleField");
        bossBattleArea.SetActive(false);
    }

    void Update()
    {

        //死亡後徐々に沈む
        if (dieFlg)
        {
            transform.Translate(0, -Time.deltaTime * 0.3f, 0);
            return;
        }

        //無敵時間中は動かないようにする
        if (invincibleTime > 0)
        {
            invincibleTime -= Time.deltaTime;
            return;
        }

        //攻撃前のFlyFloatアニメーション時のみ回転してプレイヤーの方向を向く
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Fly Float"))
        {
            //プレイヤーの方向を向かせる
            agent.destination = player.transform.position;
        }

        //攻撃アニメーション時
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Fly Flame Attack"))
        {
            //炎エフェクト生成数がMax値を超えたら生成しない
            if (attackCount > attackMaxCount) return;

            //炎エフェクト生成
            GameObject dragonFireEff = Instantiate(dragonFirePrefab, headPoint.transform.position, Quaternion.identity);
            //炎エフェクトに頭部オブジェクトが向いている前方へランダムな力で飛ばす
            dragonFireEff.GetComponent<DragonFireEffController>().Shoot(headPoint.transform.forward * Random.Range(300, 800));
            //攻撃回数
            attackCount++;
        }

        //攻撃終了後のLandアニメーション時
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Land"))
        {
            //攻撃回数をリセット
            attackCount = 0;
            //咆哮フラグリセット
            screamFlg = false;
        }

        //Screamアニメーション時
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Scream") && !screamFlg)
        {
            //咆哮オブジェクト生成
            Instantiate(screamAttackPrefab, transform.position, Quaternion.identity);
            screamFlg = true;
        }

        //戦闘エリアへプレイヤーが侵入した（追加）
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Sleep") && fieldViewFlg)
        {
            //咆哮アニメーションへ遷移
            animator.SetTrigger("ScreamTrigger");
            //ボスBGM開始
            GSound.Instance.PlayBgm("BossBGM", true);
            //ボスHPゲージ表示
            bossHp.gameObject.SetActive(true);
            //ボスバトルエリア
            bossBattleArea.SetActive(true);
        }
    }

    //ダメージ処理
    protected override void Damage(int d)
    {
        //基底クラスのDamage
        base.Damage(d);
        //HPゲージ更新
        bossHp.value = (float)hp / (float)maxHp;
        //SE
        GSound.Instance.PlaySe("zombieDamage");
    }

    //死亡時処理
    protected override void Die()
    {
        //基底クラスのDie
        base.Die();
        //SE
        GSound.Instance.PlaySe("zombieDie");

        //BGMを戻す（追加）
        GSound.Instance.PlayBgm("FieldBGM", true);
        //ボスHPゲージ非表示（追加）
        bossHp.gameObject.SetActive(false);
        //ボスバトルエリア非表示（追加）
        bossBattleArea.SetActive(false);

        SceneManager.LoadScene("ClearScene");
    }

    //経験値をプレイヤーに与える（追加）
    protected override void SetExp()
    {
        player.GetComponent<PlayerController>().GetExp(100);
    }
}