using UnityEngine;

/// <summary>
/// レッドドラゴンが口から吐く炎エフェクトの動き制御
/// </summary>
public class DragonFireEffController : MonoBehaviour
{
    //スピード
    [SerializeField] float speed = 10f;
    //変数
    Rigidbody rigid;

    //クローン時にShootメソッドよりも先に呼ばないとRigidbody未割り当てでエラーになるためAwakeで設定
    void Awake()
    {
        //コンポーネント取得
        rigid = GetComponent<Rigidbody>();
        //一定時間後に消滅
        Destroy(gameObject, Random.Range(4.0f, 8.0f));
    }

    private void OnTriggerEnter(Collider other)
    {
        //地面と衝突したら動きを止める
        if (other.gameObject.tag == "Ground")
        {
            rigid.isKinematic = true;
        }
    }

    //指定した方向へ飛ばす
    public void Shoot(Vector3 dir)
    {
        rigid.AddForce(dir, ForceMode.Force);
    }
}