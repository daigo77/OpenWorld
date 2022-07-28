using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ���j���[�E�C���h�E�̐���
/// </summary>
public class MenuController : MonoBehaviour
{
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider seSlider;


    void Start()
    {
        //���݂̉��ʒl���X���C�_�[�ɓK������
        bgmSlider.value = GSound.Instance.bgmVolume;
        seSlider.value = GSound.Instance.seVolume;
    }

    //���j���[�E�C���h�E�����
    public void OnPressCloseBtn()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    //BGM�̃{�����[���ύX
    public void ChangeBgmSlider()
    {
        //�X���C�_�[�̒l�����ʒl�ɓK��������
        GSound.Instance.bgmVolume = bgmSlider.value;
        //BGM���ʃZ�b�g
        GSound.Instance.BgmVolumeChange();

        //���ʏ����Z�[�u�i�ǉ��j
        PlayerPrefs.SetFloat("BgmVolume", bgmSlider.value);
    }

    //SE�̃{�����[���ύX
    public void ChangeSeSlider()
    {
        //�X���C�_�[�̒l�����ʒl�ɓK��������
        GSound.Instance.seVolume = seSlider.value;

        //���ʏ����Z�[�u�i�ǉ��j
        PlayerPrefs.SetFloat("SeVolume", seSlider.value);
    }

    //SE�X���C�_�[�ύX��ɃN���b�N�͗����ꂽ�ۂ̃C�x���g
    public void PointUpSeSlider()
    {
        //�m�F�œK����SE��炷
        GSound.Instance.PlaySe("playerAttack");
    }
}