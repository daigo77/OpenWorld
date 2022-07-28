using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  //（追加）

/// <summary>
/// 全体を管理する監督スクリプト
/// </summary>
public class GameDirector : MonoBehaviour
{
    //ゲームオーバー関連のUI（追加）
    [SerializeField] GameObject gameOverUI;

    //メニュー画面（追加）
    [SerializeField] GameObject menuWindow;

    void Start()
    {
        //BGM再生（追加）
        GSound.Instance.PlayBgm("FieldBGM", true);

    }

    void Update()
    {

    }

    //ゲームオーバー処理（追加）
    public void GameOver()
    {
        gameOverUI.SetActive(true);
    }

    //リトライボタン（追加）
    public void OnPressResetBtn()
    {
        //現在のシーン名を再度読み込むことでシーン内をリセット
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        //ゲームオーバー時はHPを全回復させてセーブしておく（追加）
        EncryptedPlayerPrefs.SaveInt(MyPlayer.CURRENT_HP, MyPlayer.Instance.Hp);
    }

    //メニューボタン（追加）
    public void OnPressMenuBtn()
    {
        //メニューウインドウを表示してゲーム内時間を止める
        menuWindow.SetActive(true);
        Time.timeScale = 0;
    }
}