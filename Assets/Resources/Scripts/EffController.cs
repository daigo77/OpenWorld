using UnityEngine;

/// <summary>
/// �G�t�F�N�g�̎����폜
/// </summary>
public class EffController : MonoBehaviour
{
    //�G�t�F�N�g��Duration����
    float duration;

    void Start()
    {
        if (GetComponent<ParticleSystem>() != null)
        {
            //���g���G�t�F�N�g�̏ꍇ�͎��g��Duration���Ԃ��擾
            duration = GetComponent<ParticleSystem>().main.duration;
        }
        else
        {
            //���g���G�t�F�N�g�łȂ��ꍇ�͎q�I�u�W�F�N�g�̒�����ParticleSystem���Z�b�g����Ă���I�u�W�F�N�g��Duration���Ԃ��擾����
            foreach (Transform child in transform)
            {
                if (child.GetComponent<ParticleSystem>() != null)
                {
                    duration = child.GetComponent<ParticleSystem>().main.duration;
                }
            }
        }
        //Duration���Ԍo�ߌ�Ɏ����폜
        Destroy(gameObject, duration);
    }
}