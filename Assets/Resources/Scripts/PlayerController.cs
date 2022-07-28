using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.ThirdPerson;   //�i�ǉ��j

/// <summary>
/// �v���C���[�̊�{����
/// </summary>
public class PlayerController : MonoBehaviour
{
    //HP
    //HP�֘A�i�ύX�j
    int maxHp;
    int hp;

    //���G����
    float invincibleTime;

    //HP�Q�[�W��Unity�����C���[�W�i�ǉ��j
    Image unityChanImage;
    Sprite unityChanImage1;
    Sprite unityChanImage2;

    //�ϐ�
    Animator animator;
    Rigidbody rigidbody;
    CapsuleCollider capsuleCollider;
    Slider playerHp;     //�i�ǉ��j
    //�U���֘A�i�ǉ��j
    bool attackFlg;

    //���S�t���O�i�ǉ��j
    bool dieFlg;

    //�o���l�i�ǉ��j
    int exp;


    void Start()
    {
        //�R���|�[�l���g�擾
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        //�Q�Ɓi�ǉ��j
        playerHp = GameObject.Find("PlayerHp").GetComponent<Slider>();

        //HP�������i�ǉ��j
        //hp = maxHp;
        //playerHp.value = (float)hp / (float)maxHp;

        //HP�Q�[�W�̃��j�e�B�����C���[�W���擾�i�ǉ��j
        unityChanImage = GameObject.Find("UnityChanImage").GetComponent<Image>();
        unityChanImage1 = Resources.Load<Sprite>("Sprites/UnityChanImage1");
        unityChanImage2 = Resources.Load<Sprite>("Sprites/UnityChanImage2");
        //UnityChanImage1���Z�b�g
        unityChanImage.sprite = unityChanImage1;

        //�Z�[�u�f�[�^�����[�h�i�ǉ��j
        MyPlayer.Instance.Level = EncryptedPlayerPrefs.LoadInt(MyPlayer.CURRENT_LEVEL, 1);
        exp = EncryptedPlayerPrefs.LoadInt(MyPlayer.CURRENT_EXP, 0);

        //�p�����[�^�������i�ǉ��j
        SetParameter();

        //�Z�[�u�f�[�^�����[�h�i�ǉ��j
        hp = EncryptedPlayerPrefs.LoadInt(MyPlayer.CURRENT_HP, maxHp);
        //������HP��0�i���S���j�̃^�C�~���O�ŃA�v�����V���b�g�_�E�������ꍇ�̑[�u
        if (hp <= 0)
        {
            hp = maxHp;
        }
        playerHp.value = (float)hp / (float)maxHp;
    }

    void Update()
    {
        //���S���ɂ͍U���������ł��Ȃ��悤�ɂ���i�ǉ��j
        if (dieFlg) return;

        //���G���Ԃ����炷
        if (invincibleTime > 0)
        {
            invincibleTime -= Time.deltaTime;

        }

        //��]�R��U���i�ǉ��j
        if (Input.GetKeyDown(KeyCode.F) && !attackFlg)
        {
            //�U���A�j���[�V�����Z�b�g
            animator.SetTrigger("AttackTrigger");
            attackFlg = true;

            //��]Fire�G�t�F�N�g�����i�ǉ��j
            Instantiate(Resources.Load<GameObject>("Prefabs/_Effects/FireEff"), transform);


            //�x����������R���[�`���i�Ăяo�����j
            StartCoroutine(DelayMethod(1.0f, () => {
                //�w�莞�Ԍo�ߌ�ɍU���t���O����
                attackFlg = false;
            }));

            //SE
            GSound.Instance.PlaySe("playerAttack");
        }
    }

    //�����ƏՓ˂��Ă���
    private void OnCollisionStay(Collision collision)
    {

        if (collision.gameObject.tag == "Enemy")
        {
            //�_���[�W���󂯂�i�ǉ��j
            PlayerDamage(1);
        }
    }

    //�����ƐڐG���Ă���
    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == "EnemyAttack")
        {
            //�_���[�W���󂯂�i�ǉ��j
            PlayerDamage(10);
            Destroy(other.gameObject);
        }
    }

    //�v���C���[�_���[�W�i�ǉ��j
    void PlayerDamage(int d)
    {

        //���G���Ԓ��y�ю��S�� �y�эU�����i�ǉ��j
        if (invincibleTime > 0 || hp <= 0 || attackFlg) return;

        hp -= d;
        playerHp.value = (float)hp / (float)maxHp;
        //HP��30%�ȉ��ɂȂ�����UnityChanImage2�ɐ؂�ւ���i�ǉ��j
        if (playerHp.value < 0.3f)
        {
            unityChanImage.sprite = unityChanImage2;
        }
        //�_���[�W���1�b�Ԗ��G�ƂȂ�
        invincibleTime = 1.0f;


        if (hp > 0)
        {
            //�_���[�W�A�j���[�V����
            animator.SetTrigger("DamageTrigger");
            //SE
            GSound.Instance.PlaySe("playerDamage");
        }
        else
        {
            //���S���A�j���[�V����
            animator.SetTrigger("DieTrigger");
            //�|�ꂽ��͓G�ɉ����ꂽ�肵�Ȃ��悤�ɏd�͖����ɂ��Ă��瓖���蔻���OFF�ɂ���
            rigidbody.isKinematic = true;
            capsuleCollider.enabled = false;
            //���ꂽ�ۂ̃A�j���[�V�������I��鎞�ԁi3�b���j�Ɏ��S���������s��
            Invoke("PlayerDie", 3f);
            //���S�t���O
            dieFlg = true;
           
        }
    }

    //�v���C���[�����ꂽ���̏���
    void PlayerDie()
    {
        //�A�j���[�V�������~�߂�i�~�߂Ȃ��Ƃ܂��N���オ���Ă��܂��A�j���[�V�����ׁ̈j
        animator.enabled = false;
        //�Q�[���I�[�o�[�֘A��UI�\��
        GameObject.Find("GameDirector").GetComponent<GameDirector>().GameOver();
    }

    /// <summary>
    /// �n���ꂽ�������w�莞�Ԍ�Ɏ��s����
    /// </summary>
    /// <param name="waitTime">�x������[�~���b]</param>
    /// <param name="action">���s����������</param>
    /// <returns></returns>
    private IEnumerator DelayMethod(float waitTime, System.Action action)
    {
        yield return new WaitForSeconds(waitTime);
        action();
    }

    //�p�����[�^�Z�b�g�i�ǉ��j
    public void SetParameter()
    {
        //CSV�f�[�^���烌�x�����Ƃ̃p�����[�^�[�l���擾�i�ǉ��j
        List<string[]> csvData = new List<string[]>();
        csvData = CSVLoader.ReadFile("Mylevel", ',');
        //List�f�[�^����J��Ԃ������Ō��݂̖��No�ƈ�v��������擾����
        foreach (string[] data in csvData)
        {
            //data.Length���`�F�b�N���ċ󔒍s�̏ꍇ�̓X�L�b�v������
            if (data.Length > 1)
            {
                //���݂̃��x���Ɠ����f�[�^���擾����
                if (int.Parse(data[0]) == MyPlayer.Instance.Level)
                {
                    MyPlayer.Instance.Hp = int.Parse(data[1]);
                    MyPlayer.Instance.Atk = int.Parse(data[2]);
                    MyPlayer.Instance.Spd = int.Parse(data[3]);
                    MyPlayer.Instance.NextExp = int.Parse(data[4]);
                }
            }
        }
        //HP�Z�b�g
        maxHp = MyPlayer.Instance.Hp;
        hp = maxHp;
        playerHp.value = (float)hp / (float)maxHp;

        //�X�s�[�h�Z�b�g
        GetComponent<ThirdPersonCharacter>().M_MoveSpeedMultiplier = 1 + (MyPlayer.Instance.Spd * 0.1f);
    }

    //�o���l�l���i�ǉ��j
    public void GetExp(int exp)
    {
        this.exp += exp;
        //���x���A�b�v�`�F�b�N
        while (this.exp >= MyPlayer.Instance.NextExp)
        {
            this.exp = this.exp - MyPlayer.Instance.NextExp;
            LevelUp();
        }
    }

    //���x���A�b�v�i�ǉ��j
    private void LevelUp()
    {
        //level�l���C���N�������g
        MyPlayer.Instance.Level++;
        //���x���̐��ɉ������p�����[�^��CSV����擾�iCSV�����͌�قǎ����j
        SetParameter();

        //���O
        Debug.Log("���x����" + MyPlayer.Instance.Level + "�ɏオ�����I");
        Debug.Log("��������HP��" + MyPlayer.Instance.Hp + "�ɏオ�����I");
        Debug.Log("��������HP��" + MyPlayer.Instance.Hp + "�ɏオ�����I");
        Debug.Log("����������" + MyPlayer.Instance.Atk + "�ɏオ�����I");
        Debug.Log("���΂₳��" + MyPlayer.Instance.Spd + "�ɏオ����");
        Debug.Log("���̃��x���܂Ōo���l��" + MyPlayer.Instance.NextExp + "�K�v�ł��I");

        //�Z�[�u�i�ǉ��j
        MySave();
    }

    //�p�����[�^�l���Z�[�u�i�ǉ��j
    private void MySave()
    {
        EncryptedPlayerPrefs.SaveInt(MyPlayer.CURRENT_LEVEL, MyPlayer.Instance.Level);
        EncryptedPlayerPrefs.SaveInt(MyPlayer.CURRENT_HP, hp);
        EncryptedPlayerPrefs.SaveInt(MyPlayer.CURRENT_EXP, exp);
    }

    // �A�v���P�[�V�������I�����钼�O�ɌĂ΂��i�ǉ��j
    void OnApplicationQuit()
    {
        //�Z�[�u
        MySave();
    }
}