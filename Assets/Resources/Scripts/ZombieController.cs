using UnityEngine;
using UnityEngine.UI;

//�K�{�R���|�[�l���g
[RequireComponent(typeof(CapsuleCollider))]

/// <summary>
/// �]���r�𐧌�
/// </summary>
public class ZombieController : Enemy
{

    //�ړ����x
    [SerializeField] float speed = 4f;
    //HP�Q�[�W
    Slider enemyHp;
    //�ړI�n�Z�b�g�t���O
    bool destinationFlg;
    //�ړI�n�I�u�W�F�N�g
    GameObject destinationObj;

    //Awake��
    void Awake()
    {

        //���N���X��Awake
        base.Awake();

        //HP������
        maxHp = 10;
        hp = maxHp;
        enemyHp = transform.Find("EnemyHp/Slider").GetComponent<Slider>();
        enemyHp.value = (float)hp / (float)maxHp;

        //�ړ����x
        agent.speed = 0;
        //�ʖ��G����
        setInvincibleTime = 2f;
        //���S�����ތo�ߎ���
        dieDelayTime = 3f;
    }

    void Update()
    {

        //���S�㏙�X�ɒ���
        if (dieFlg)
        {
            transform.Translate(0, -Time.deltaTime * 0.1f, 0);
            return;
        }

        //���G���Ԓ��͓����Ȃ��悤�ɂ���
        if (invincibleTime > 0)
        {
            invincibleTime -= Time.deltaTime;
            return;
        }

        //������փv���C���[���N������
        if (fieldViewFlg)
        {
            //�ړI�n���v���C���[�ɐݒ�
            agent.destination = player.transform.position;
            //�ړ����x�����X�ɏグ��
            speed += Time.deltaTime;
            speed = Mathf.Clamp(speed, 0, 4f);
            agent.speed = speed;
            //�A�j���[�V�����ؑ�
            animator.SetBool("RunFlg", true);

            //�ړI�n���Z�b�g����Ă���ꍇ
        }
        else if (destinationFlg)
        {
            //�ړ����x�ݒ�
            agent.speed = 1;
            //�A�j���[�V�����ؑ�
            animator.SetBool("RunFlg", true);

        }
        else
        {
            //�ړI�n�����g�ɂ��ē������~�߂�
            agent.destination = transform.position;
            //�ړ����x����U���Z�b�g
            agent.speed = 0;
            //�A�j���[�V�����ؑ�
            animator.SetBool("RunFlg", false);
        }
    }

    //�Փˎ�
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            //�U���A�j���[�V������ON
            animator.SetBool("AttackFlg", true);
        }
    }

    //�Փˉ���
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            //�U���A�j���[�V������OFF
            animator.SetBool("AttackFlg", false);
        }
    }

    //�ڐG��
    private void OnTriggerEnter(Collider other)
    {
        //�ړI�n�ƐڐG�����ꍇ
        if (other.gameObject == destinationObj)
        {
            //�ړI�n����
            destinationObj = null;
            destinationFlg = false;
        }
    }

    //�_���[�W����
    protected override void Damage(int d)
    {

        //���N���X��Damage
        base.Damage(d);

        //HP�Q�[�W�X�V
        enemyHp.value = (float)hp / (float)maxHp;
        //�ړ����x��0�ɂ��ē������~�߂�
        agent.speed = 0;
        //������΂�����
        Vector3 force = (transform.position - player.transform.position).normalized;
        Rigidbody rd = GetComponent<Rigidbody>();
        rd.AddForce(force * 10, ForceMode.Impulse);
        //SE
        GSound.Instance.PlaySe("zombieDamage");
    }

    //���S������
    protected override void Die()
    {
        //���N���X��Die
        base.Die();
        //�����蔻��𖳌�
        GetComponent<CapsuleCollider>().enabled = false;
        //SE
        GSound.Instance.PlaySe("zombieDie");
    }

    //�ړI�n���Z�b�g�i�ǉ��j
    public void SetDestination(Transform obj)
    {
        //�ړI�n�ݒ�
        destinationObj = obj.gameObject;
        agent.destination = obj.position;
        destinationFlg = true;
    }

    //�o���l���v���C���[�ɗ^����i�ǉ��j
    protected override void SetExp()
    {
        player.GetComponent<PlayerController>().GetExp(20);
    }
}