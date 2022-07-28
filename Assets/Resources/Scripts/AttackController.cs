using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �U�����̃_���[�W����
/// </summary>
public class AttackController : MonoBehaviour
{

    //�U���̎��
    public enum AttackType
    {
        rotatingKick,
        other
    }
    [SerializeField] AttackType type;

    //�U����
    int atk;

    void Start()
    {

        switch (type)
        {
            //��]�R��
            case AttackType.rotatingKick:
                atk = 1;
                //�����蔻��̃R���C�_�[�͉��G�t�F�N�g���������؍݂���]���Ă��邽��1�b��ɂ͓����蔻��𖳌��ɂ���
                StartCoroutine(DelayMethod(1, () => {
                    GetComponent<SphereCollider>().enabled = false;
                }));
                break;

            //���̑�
            case AttackType.other:
                atk = 0;
                break;
        }
    }

    //�U����Atk�l�̃v���p�e�B
    public int Atk
    {
        get { return atk; }
        set { atk = value; }
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