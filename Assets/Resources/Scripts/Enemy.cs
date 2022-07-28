using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// �G���Ǘ�����X�[�p�[�N���X�i���N���X�j
/// </summary>
public class Enemy : MonoBehaviour
{
    //�R���|�[�l���g
    protected Animator animator;
    protected NavMeshAgent agent;

    //�O���Q��
    protected GameObject player;

    //HP�֘A
    protected int hp;
    protected int maxHp;

    //�ǐՃt���O
    public bool fieldViewFlg;

    //���G����
    protected float invincibleTime;
    //�G���Ƃ̖��G����
    protected float setInvincibleTime;

    //���S���t���O
    protected bool dieFlg;
    //���S�����ތo�ߎ���
    protected float dieDelayTime;

    protected void Awake()
    {
        //�R���|�[�l���g�擾
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        //�v���C���[���擾
        player = GameObject.FindWithTag("Player");
    }

    private void OnTriggerStay(Collider other)
    {
        //���G���Ԓ��y�ю��S��
        if (invincibleTime > 0 || hp <= 0) return;
        //�v���C���[�̍U�����󂯂�
        if (other.gameObject.tag == "Attack")
        {
            //�_���[�W�l���擾
            int d = other.GetComponent<AttackController>().Atk;
            Damage(d);
        }
    }

    //�_���[�W����
    protected virtual void Damage(int d)
    {
        //HP�����i�ύX�j
        //CeilToInt:�؂�グ��int�ɕϊ�
        hp -= d + Mathf.CeilToInt(d * MyPlayer.Instance.Atk * 0.1f);

        //���G���ԃZ�b�g
        invincibleTime = setInvincibleTime;
        //�_���[�W�A�j���[�V����
        animator.SetTrigger("DamageTrigger");
        //HP�[���Ŏ��S����
        if (hp <= 0)
        {
            Die();
        }
    }

    //���S������
    protected virtual void Die()
    {
        //���S�A�j���[�V����
        animator.SetTrigger("DieTrigger");
        GetComponent<Rigidbody>().isKinematic = true;
        //2�b��ɒ��ޏ������J�n
        StartCoroutine(DelayMethod(2.0f, () => {
            //���S�t���O�Œ��ޏ����J�n
            dieFlg = true;
            //NavMesh�@�\���I�t�ɂ��Ȃ��ƒn�ʂɂ߂荞�܂Ȃ�
            agent.enabled = false;
            //���݂���܂ł̎��ԃ^�C�����O��^�����ł�����
            Destroy(gameObject, dieDelayTime);
        }));

        //�o���l�Z�b�g�i�ǉ��j
        SetExp();
    }

    //�o���l���Z�b�g�i�ǉ��j
    protected virtual void SetExp()
    {

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

}