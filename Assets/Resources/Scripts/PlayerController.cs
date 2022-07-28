using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.ThirdPerson;   //（追加）

/// <summary>
/// プレイヤーの基本処理
/// </summary>
public class PlayerController : MonoBehaviour
{
    //HP
    //HP関連（変更）
    int maxHp;
    int hp;

    //無敵時間
    float invincibleTime;

    //HPゲージのUnityちゃんイメージ（追加）
    Image unityChanImage;
    Sprite unityChanImage1;
    Sprite unityChanImage2;

    //変数
    Animator animator;
    Rigidbody rigidbody;
    CapsuleCollider capsuleCollider;
    Slider playerHp;     //（追加）
    //攻撃関連（追加）
    bool attackFlg;

    //死亡フラグ（追加）
    bool dieFlg;

    //経験値（追加）
    int exp;


    void Start()
    {
        //コンポーネント取得
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        //参照（追加）
        playerHp = GameObject.Find("PlayerHp").GetComponent<Slider>();

        //HP初期化（追加）
        //hp = maxHp;
        //playerHp.value = (float)hp / (float)maxHp;

        //HPゲージのユニティちゃんイメージを取得（追加）
        unityChanImage = GameObject.Find("UnityChanImage").GetComponent<Image>();
        unityChanImage1 = Resources.Load<Sprite>("Sprites/UnityChanImage1");
        unityChanImage2 = Resources.Load<Sprite>("Sprites/UnityChanImage2");
        //UnityChanImage1をセット
        unityChanImage.sprite = unityChanImage1;

        //セーブデータをロード（追加）
        MyPlayer.Instance.Level = EncryptedPlayerPrefs.LoadInt(MyPlayer.CURRENT_LEVEL, 1);
        exp = EncryptedPlayerPrefs.LoadInt(MyPlayer.CURRENT_EXP, 0);

        //パラメータ初期化（追加）
        SetParameter();

        //セーブデータをロード（追加）
        hp = EncryptedPlayerPrefs.LoadInt(MyPlayer.CURRENT_HP, maxHp);
        //万が一HPが0（死亡時）のタイミングでアプリをシャットダウンした場合の措置
        if (hp <= 0)
        {
            hp = maxHp;
        }
        playerHp.value = (float)hp / (float)maxHp;
    }

    void Update()
    {
        //死亡時には攻撃処理等できないようにする（追加）
        if (dieFlg) return;

        //無敵時間を減らす
        if (invincibleTime > 0)
        {
            invincibleTime -= Time.deltaTime;

        }

        //回転蹴り攻撃（追加）
        if (Input.GetKeyDown(KeyCode.F) && !attackFlg)
        {
            //攻撃アニメーションセット
            animator.SetTrigger("AttackTrigger");
            attackFlg = true;

            //回転Fireエフェクト生成（追加）
            Instantiate(Resources.Load<GameObject>("Prefabs/_Effects/FireEff"), transform);


            //遅延処理するコルーチン（呼び出し側）
            StartCoroutine(DelayMethod(1.0f, () => {
                //指定時間経過後に攻撃フラグ解除
                attackFlg = false;
            }));

            //SE
            GSound.Instance.PlaySe("playerAttack");
        }
    }

    //何かと衝突している
    private void OnCollisionStay(Collision collision)
    {

        if (collision.gameObject.tag == "Enemy")
        {
            //ダメージを受ける（追加）
            PlayerDamage(1);
        }
    }

    //何かと接触している
    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == "EnemyAttack")
        {
            //ダメージを受ける（追加）
            PlayerDamage(10);
            Destroy(other.gameObject);
        }
    }

    //プレイヤーダメージ（追加）
    void PlayerDamage(int d)
    {

        //無敵時間中及び死亡時 及び攻撃中（追加）
        if (invincibleTime > 0 || hp <= 0 || attackFlg) return;

        hp -= d;
        playerHp.value = (float)hp / (float)maxHp;
        //HPが30%以下になったらUnityChanImage2に切り替える（追加）
        if (playerHp.value < 0.3f)
        {
            unityChanImage.sprite = unityChanImage2;
        }
        //ダメージ後は1秒間無敵となる
        invincibleTime = 1.0f;


        if (hp > 0)
        {
            //ダメージアニメーション
            animator.SetTrigger("DamageTrigger");
            //SE
            GSound.Instance.PlaySe("playerDamage");
        }
        else
        {
            //死亡時アニメーション
            animator.SetTrigger("DieTrigger");
            //倒れた後は敵に押されたりしないように重力無効にしてから当たり判定をOFFにする
            rigidbody.isKinematic = true;
            capsuleCollider.enabled = false;
            //やられた際のアニメーションが終わる時間（3秒頃）に死亡時処理を行う
            Invoke("PlayerDie", 3f);
            //死亡フラグ
            dieFlg = true;
           
        }
    }

    //プレイヤーがやられた時の処理
    void PlayerDie()
    {
        //アニメーションを止める（止めないとまた起き上がってしまうアニメーションの為）
        animator.enabled = false;
        //ゲームオーバー関連のUI表示
        GameObject.Find("GameDirector").GetComponent<GameDirector>().GameOver();
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

    //パラメータセット（追加）
    public void SetParameter()
    {
        //CSVデータからレベルごとのパラメーター値を取得（追加）
        List<string[]> csvData = new List<string[]>();
        csvData = CSVLoader.ReadFile("Mylevel", ',');
        //Listデータから繰り返し処理で現在の問題Noと一致する情報を取得する
        foreach (string[] data in csvData)
        {
            //data.Lengthをチェックして空白行の場合はスキップさせる
            if (data.Length > 1)
            {
                //現在のレベルと同じデータを取得する
                if (int.Parse(data[0]) == MyPlayer.Instance.Level)
                {
                    MyPlayer.Instance.Hp = int.Parse(data[1]);
                    MyPlayer.Instance.Atk = int.Parse(data[2]);
                    MyPlayer.Instance.Spd = int.Parse(data[3]);
                    MyPlayer.Instance.NextExp = int.Parse(data[4]);
                }
            }
        }
        //HPセット
        maxHp = MyPlayer.Instance.Hp;
        hp = maxHp;
        playerHp.value = (float)hp / (float)maxHp;

        //スピードセット
        GetComponent<ThirdPersonCharacter>().M_MoveSpeedMultiplier = 1 + (MyPlayer.Instance.Spd * 0.1f);
    }

    //経験値獲得（追加）
    public void GetExp(int exp)
    {
        this.exp += exp;
        //レベルアップチェック
        while (this.exp >= MyPlayer.Instance.NextExp)
        {
            this.exp = this.exp - MyPlayer.Instance.NextExp;
            LevelUp();
        }
    }

    //レベルアップ（追加）
    private void LevelUp()
    {
        //level値をインクリメント
        MyPlayer.Instance.Level++;
        //レベルの数に応じたパラメータをCSVから取得（CSV処理は後ほど実装）
        SetParameter();

        //ログ
        Debug.Log("レベルが" + MyPlayer.Instance.Level + "に上がった！");
        Debug.Log("さいだいHPが" + MyPlayer.Instance.Hp + "に上がった！");
        Debug.Log("さいだいHPが" + MyPlayer.Instance.Hp + "に上がった！");
        Debug.Log("こうげきが" + MyPlayer.Instance.Atk + "に上がった！");
        Debug.Log("すばやさが" + MyPlayer.Instance.Spd + "に上がった");
        Debug.Log("次のレベルまで経験値が" + MyPlayer.Instance.NextExp + "必要です！");

        //セーブ（追加）
        MySave();
    }

    //パラメータ値をセーブ（追加）
    private void MySave()
    {
        EncryptedPlayerPrefs.SaveInt(MyPlayer.CURRENT_LEVEL, MyPlayer.Instance.Level);
        EncryptedPlayerPrefs.SaveInt(MyPlayer.CURRENT_HP, hp);
        EncryptedPlayerPrefs.SaveInt(MyPlayer.CURRENT_EXP, exp);
    }

    // アプリケーションが終了する直前に呼ばれる（追加）
    void OnApplicationQuit()
    {
        //セーブ
        MySave();
    }
}