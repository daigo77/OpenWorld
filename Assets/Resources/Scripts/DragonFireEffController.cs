using UnityEngine;

/// <summary>
/// ���b�h�h���S����������f�����G�t�F�N�g�̓�������
/// </summary>
public class DragonFireEffController : MonoBehaviour
{
    //�X�s�[�h
    [SerializeField] float speed = 10f;
    //�ϐ�
    Rigidbody rigid;

    //�N���[������Shoot���\�b�h������ɌĂ΂Ȃ���Rigidbody�����蓖�ĂŃG���[�ɂȂ邽��Awake�Őݒ�
    void Awake()
    {
        //�R���|�[�l���g�擾
        rigid = GetComponent<Rigidbody>();
        //��莞�Ԍ�ɏ���
        Destroy(gameObject, Random.Range(4.0f, 8.0f));
    }

    private void OnTriggerEnter(Collider other)
    {
        //�n�ʂƏՓ˂����瓮�����~�߂�
        if (other.gameObject.tag == "Ground")
        {
            rigid.isKinematic = true;
        }
    }

    //�w�肵�������֔�΂�
    public void Shoot(Vector3 dir)
    {
        rigid.AddForce(dir, ForceMode.Force);
    }
}