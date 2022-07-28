using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  //�i�ǉ��j

/// <summary>
/// �S�̂��Ǘ�����ēX�N���v�g
/// </summary>
public class GameDirector : MonoBehaviour
{
    //�Q�[���I�[�o�[�֘A��UI�i�ǉ��j
    [SerializeField] GameObject gameOverUI;

    //���j���[��ʁi�ǉ��j
    [SerializeField] GameObject menuWindow;

    void Start()
    {
        //BGM�Đ��i�ǉ��j
        GSound.Instance.PlayBgm("FieldBGM", true);

    }

    void Update()
    {

    }

    //�Q�[���I�[�o�[�����i�ǉ��j
    public void GameOver()
    {
        gameOverUI.SetActive(true);
    }

    //���g���C�{�^���i�ǉ��j
    public void OnPressResetBtn()
    {
        //���݂̃V�[�������ēx�ǂݍ��ނ��ƂŃV�[���������Z�b�g
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        //�Q�[���I�[�o�[����HP��S�񕜂����ăZ�[�u���Ă����i�ǉ��j
        EncryptedPlayerPrefs.SaveInt(MyPlayer.CURRENT_HP, MyPlayer.Instance.Hp);
    }

    //���j���[�{�^���i�ǉ��j
    public void OnPressMenuBtn()
    {
        //���j���[�E�C���h�E��\�����ăQ�[�������Ԃ��~�߂�
        menuWindow.SetActive(true);
        Time.timeScale = 0;
    }
}