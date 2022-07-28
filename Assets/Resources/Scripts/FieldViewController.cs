using UnityEngine;

/// <summary>
/// 敵キャラの視野を制御する
/// </summary>
public class FieldViewController : MonoBehaviour
{
    //基底クラス（変更）
    Enemy enemyController;

    private void Start()
    {
        //親オブジェクトからスクリプト取得（変更）
        enemyController = transform.parent.GetComponent<Enemy>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //プレイヤーが視野内に入った
        if (other.gameObject.tag == "Player")
        {
            enemyController.fieldViewFlg = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //プレイヤーが視野から外れた
        if (other.gameObject.tag == "Player")
        {
            enemyController.fieldViewFlg = false;
        }
    }
}