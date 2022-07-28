using UnityEngine;

/// <summary>
/// エフェクトの自動削除
/// </summary>
public class EffController : MonoBehaviour
{
    //エフェクトのDuration時間
    float duration;

    void Start()
    {
        if (GetComponent<ParticleSystem>() != null)
        {
            //自身がエフェクトの場合は自身のDuration時間を取得
            duration = GetComponent<ParticleSystem>().main.duration;
        }
        else
        {
            //自身がエフェクトでない場合は子オブジェクトの中からParticleSystemがセットされているオブジェクトのDuration時間を取得する
            foreach (Transform child in transform)
            {
                if (child.GetComponent<ParticleSystem>() != null)
                {
                    duration = child.GetComponent<ParticleSystem>().main.duration;
                }
            }
        }
        //Duration時間経過後に自動削除
        Destroy(gameObject, duration);
    }
}