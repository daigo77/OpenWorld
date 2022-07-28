using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// ���b�h�h���S���̐���
/// </summary>
public class RedDragonController : Enemy
{
    //���U���𐶐�����ꏊ�i�����j
    [SerializeField] GameObject headPoint;
    //���G�t�F�N�g�v���n�u
    GameObject dragonFirePrefab;
    //�����T���U�炷��
    int attackCount;
    int attackMaxCount = 50;
    //HP�Q�[�W
    Slider bossHp;
    //�{�X��
    string bossName = "���b�h�h���S��";
    //���K���̍U���I�u�W�F�N�g
    GameObject screamAttackPrefab;
    //���K���t���O
    bool screamFlg;

    //�{�X�o�g���G���A�i�ǉ��j
    GameObject bossBattleArea;

    void Start()
    {

        //���v���n�u�擾
        dragonFirePrefab = Resources.Load<GameObject>("Prefabs/_Enemys/DragonFireEff");
        //HP������
        maxHp = 10;
        hp = maxHp;
        bossHp = GameObject.Find("BossHpGauge").GetComponent<Slider>();
        bossHp.value = (float)hp / (float)maxHp;
        //�{�X���Z�b�g
        GameObject.Find("BossHpGauge").transform.Find("Name").GetComponent<Text>().text = bossName;
        //���K�擾
        screamAttackPrefab = Resources.Load<GameObject>("Prefabs/_Enemys/ScreamAttack");
        //�ʖ��G����
        setInvincibleTime = 0f;
        //���S�����ތo�ߎ���
        dieDelayTime = 10f;

        //�{�XHP�Q�[�W��\���i�ǉ��j
        bossHp.gameObject.SetActive(false);
        //�{�X�o�g���G���A�i�ǉ��j
        bossBattleArea = GameObject.Find("BossBattleField");
        bossBattleArea.SetActive(false);
    }

    void Update()
    {

        //���S�㏙�X�ɒ���
        if (dieFlg)
        {
            transform.Translate(0, -Time.deltaTime * 0.3f, 0);
            return;
        }

        //���G���Ԓ��͓����Ȃ��悤�ɂ���
        if (invincibleTime > 0)
        {
            invincibleTime -= Time.deltaTime;
            return;
        }

        //�U���O��FlyFloat�A�j���[�V�������̂݉�]���ăv���C���[�̕���������
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Fly Float"))
        {
            //�v���C���[�̕�������������
            agent.destination = player.transform.position;
        }

        //�U���A�j���[�V������
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Fly Flame Attack"))
        {
            //���G�t�F�N�g��������Max�l�𒴂����琶�����Ȃ�
            if (attackCount > attackMaxCount) return;

            //���G�t�F�N�g����
            GameObject dragonFireEff = Instantiate(dragonFirePrefab, headPoint.transform.position, Quaternion.identity);
            //���G�t�F�N�g�ɓ����I�u�W�F�N�g�������Ă���O���փ����_���ȗ͂Ŕ�΂�
            dragonFireEff.GetComponent<DragonFireEffController>().Shoot(headPoint.transform.forward * Random.Range(300, 800));
            //�U����
            attackCount++;
        }

        //�U���I�����Land�A�j���[�V������
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Land"))
        {
            //�U���񐔂����Z�b�g
            attackCount = 0;
            //���K�t���O���Z�b�g
            screamFlg = false;
        }

        //Scream�A�j���[�V������
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Scream") && !screamFlg)
        {
            //���K�I�u�W�F�N�g����
            Instantiate(screamAttackPrefab, transform.position, Quaternion.identity);
            screamFlg = true;
        }

        //�퓬�G���A�փv���C���[���N�������i�ǉ��j
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Sleep") && fieldViewFlg)
        {
            //���K�A�j���[�V�����֑J��
            animator.SetTrigger("ScreamTrigger");
            //�{�XBGM�J�n
            GSound.Instance.PlayBgm("BossBGM", true);
            //�{�XHP�Q�[�W�\��
            bossHp.gameObject.SetActive(true);
            //�{�X�o�g���G���A
            bossBattleArea.SetActive(true);
        }
    }

    //�_���[�W����
    protected override void Damage(int d)
    {
        //���N���X��Damage
        base.Damage(d);
        //HP�Q�[�W�X�V
        bossHp.value = (float)hp / (float)maxHp;
        //SE
        GSound.Instance.PlaySe("zombieDamage");
    }

    //���S������
    protected override void Die()
    {
        //���N���X��Die
        base.Die();
        //SE
        GSound.Instance.PlaySe("zombieDie");

        //BGM��߂��i�ǉ��j
        GSound.Instance.PlayBgm("FieldBGM", true);
        //�{�XHP�Q�[�W��\���i�ǉ��j
        bossHp.gameObject.SetActive(false);
        //�{�X�o�g���G���A��\���i�ǉ��j
        bossBattleArea.SetActive(false);

        SceneManager.LoadScene("ClearScene");
    }

    //�o���l���v���C���[�ɗ^����i�ǉ��j
    protected override void SetExp()
    {
        player.GetComponent<PlayerController>().GetExp(100);
    }
}