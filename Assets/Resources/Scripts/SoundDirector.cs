using UnityEngine;

/// <summary>
/// Sound�f�[�^������
/// </summary>
public class SoundDirector : MonoBehaviour
{

    //Start���\�b�h������ɃT�E���h�f�[�^��ǂݍ���
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
        //BGM�Đ��i�ǉ��j
        GSound.Instance.SetBgm("FieldBGM");
        GSound.Instance.SetBgm("BossBGM");
    }
}