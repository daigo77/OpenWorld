using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// メニューウインドウの制御
/// </summary>
public class MenuController : MonoBehaviour
{
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider seSlider;


    void Start()
    {
        //現在の音量値をスライダーに適応する
        bgmSlider.value = GSound.Instance.bgmVolume;
        seSlider.value = GSound.Instance.seVolume;
    }

    //メニューウインドウを閉じる
    public void OnPressCloseBtn()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    //BGMのボリューム変更
    public void ChangeBgmSlider()
    {
        //スライダーの値を音量値に適応させる
        GSound.Instance.bgmVolume = bgmSlider.value;
        //BGM音量セット
        GSound.Instance.BgmVolumeChange();

        //音量情報をセーブ（追加）
        PlayerPrefs.SetFloat("BgmVolume", bgmSlider.value);
    }

    //SEのボリューム変更
    public void ChangeSeSlider()
    {
        //スライダーの値を音量値に適応させる
        GSound.Instance.seVolume = seSlider.value;

        //音量情報をセーブ（追加）
        PlayerPrefs.SetFloat("SeVolume", seSlider.value);
    }

    //SEスライダー変更後にクリックは離された際のイベント
    public void PointUpSeSlider()
    {
        //確認で適当なSEを鳴らす
        GSound.Instance.PlaySe("playerAttack");
    }
}