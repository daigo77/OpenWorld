using UnityEngine;

/// <summary>
/// 竜の咆哮を制御
/// </summary>
public class ScreamAttackController : MonoBehaviour
{
    //変数
    float scale = 10f;
    float maxScale = 50f;

    

    void Update()
    {
        //Scale値を指数関数敵に大きくする
        scale += Time.deltaTime * scale;
        transform.localScale = new Vector3(scale, scale, scale);
        //回転させることで見た目を演出（必須ではない）
        transform.Rotate(new Vector3(0, scale, 0));
        //最大スケールになったら削除
        if (scale > maxScale)
        {
            Destroy(gameObject);
        }
    }
}