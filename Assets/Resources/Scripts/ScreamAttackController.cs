using UnityEngine;

/// <summary>
/// ���̙��K�𐧌�
/// </summary>
public class ScreamAttackController : MonoBehaviour
{
    //�ϐ�
    float scale = 10f;
    float maxScale = 50f;

    

    void Update()
    {
        //Scale�l���w���֐��G�ɑ傫������
        scale += Time.deltaTime * scale;
        transform.localScale = new Vector3(scale, scale, scale);
        //��]�����邱�ƂŌ����ڂ����o�i�K�{�ł͂Ȃ��j
        transform.Rotate(new Vector3(0, scale, 0));
        //�ő�X�P�[���ɂȂ�����폜
        if (scale > maxScale)
        {
            Destroy(gameObject);
        }
    }
}