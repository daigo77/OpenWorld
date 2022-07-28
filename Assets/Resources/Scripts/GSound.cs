using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// BGM�ASE���Ǘ�����N���X
/// </summary>
public class GSound
{
    //�V���O���g����
    private static GSound instance;
    public static GSound Instance
    {
        get
        {
            if (instance == null) instance = new GSound();
            return instance;
        }
    }

    //AudioClip�����[�h����N���X
    class ClipData
    {
        //AudioClip
        public AudioClip Clip;
        //�R���X�g���N�^
        public ClipData(string fileName)
        {
            //AudioClip�̎擾
            Clip = Resources.Load("Sounds/" + fileName) as AudioClip;
        }
    }

    //�T�E���h���
    enum Type
    {
        bgm,    //BGM
        se,     //SE
    }

    //�T�E���h�Đ��̂��߂̃Q�[���I�u�W�F�N�g
    GameObject obj;

    //�T�E���h���\�[�X
    AudioSource sourceBgm;  //BGM
    AudioSource sourceSe;   //SE

    //BGM�f�[�^�v�[��
    Dictionary<string, ClipData> poolBgm = new Dictionary<string, ClipData>();
    //SE�f�[�^�v�[�� 
    Dictionary<string, ClipData> poolSe = new Dictionary<string, ClipData>();

    //����
    public float bgmVolume = 1.0f;
    public float seVolume = 1.0f;

    //�������i�ǉ��j
    public GSound()
    {
        bgmVolume = PlayerPrefs.GetFloat("BgmVolume", 1.0f);
        seVolume = PlayerPrefs.GetFloat("SeVolume", 1.0f);
    }


    //AudioSource���擾����
    AudioSource GetAudioSource(Type type)
    {

        //GameObject���Ȃ���΍��
        if (obj == null)
        {
            obj = new GameObject("Sound");

            //AudioSource�쐬
            sourceBgm = obj.AddComponent<AudioSource>();
            sourceSe = obj.AddComponent<AudioSource>();
        }

        if (type == Type.bgm)
        {
            //BGM
            return sourceBgm;
        }
        else
        {
            //SE
            return sourceSe;
        }
    }

    //BGM���v�[���ɃZ�b�g
    //***Resources/Sounds�t�H���_�ɔz�u���邱��
    public void SetBgm(string fileName)
    {
        //���łɓo�^�ς݂Ȃ̂ł����������
        if (poolBgm.ContainsKey(fileName))
        {
            poolBgm.Remove(fileName);
        }
        poolBgm.Add(fileName, new ClipData(fileName));
    }

    //SE���v�[���ɃZ�b�g
    //***Resources/Sounds�t�H���_�ɔz�u���邱��
    public void SetSe(string fileName)
    {
        //���łɓo�^�ς݂Ȃ̂ł����������
        if (poolSe.ContainsKey(fileName))
        {
            poolSe.Remove(fileName);
        }
        poolSe.Add(fileName, new ClipData(fileName));
    }

    //BGM�̍Đ�
    //***���O��LoadBgm�Ń��[�h���Ă�������
    public bool PlayBgm(string fileName, bool loop)
    {
        //�w��t�@�C�����Ȃ�
        if (poolBgm.ContainsKey(fileName) == false) return false;
        //���݂�BGM���~�߂�
        StopBgm();
        //���\�[�X�̎擾
        AudioSource source = GetAudioSource(Type.bgm);
        ClipData data = poolBgm[fileName];
        source.clip = data.Clip;
        //���ʐݒ�
        source.volume = bgmVolume;
        //���[�v�ݒ�
        if (loop)
        {
            source.loop = true;
        }
        else
        {
            source.loop = false;
        }
        //�Đ�
        source.Play();

        return true;
    }

    //SE�̍Đ�
    //***���O��LoadSe�Ń��[�h���Ă�������
    public bool PlaySe(string fileName)
    {
        //�w��t�B�A�����Ȃ�
        if (poolSe.ContainsKey(fileName) == false) return false;
        //���\�[�X�̎擾
        AudioSource source = GetAudioSource(Type.se);
        ClipData data = poolSe[fileName];
        //���ʐݒ�
        source.volume = seVolume;
        //�Đ�
        source.PlayOneShot(data.Clip);

        return true;
    }

    //BGM�̒�~
    public void StopBgm()
    {
        GetAudioSource(Type.bgm).Stop();
    }

    //BGM�̉��ʕύX
    public void BgmVolumeChange()
    {
        GetAudioSource(Type.bgm).volume = bgmVolume;
    }
}
