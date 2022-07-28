using UnityEngine;

/// <summary>
/// Soundデータ初期化
/// </summary>
public class SoundDirector : MonoBehaviour
{

    //Startメソッドよりも先にサウンドデータを読み込む
    void Awake()
    {
        //SE
        GSound.Instance.SetSe("jump");
        GSound.Instance.SetSe("playerDamage");
        GSound.Instance.SetSe("playerDie");
        GSound.Instance.SetSe("playerAttack");
        GSound.Instance.SetSe("fireAttack");
        GSound.Instance.SetSe("zombieDamage");
        GSound.Instance.SetSe("zombieDie");
        //BGM
        //BGM再生（追加）
        GSound.Instance.SetBgm("FieldBGM");
        GSound.Instance.SetBgm("BossBGM");
    }
}