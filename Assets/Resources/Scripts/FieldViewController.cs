using UnityEngine;

/// <summary>
/// �G�L�����̎���𐧌䂷��
/// </summary>
public class FieldViewController : MonoBehaviour
{
    //���N���X�i�ύX�j
    Enemy enemyController;

    private void Start()
    {
        //�e�I�u�W�F�N�g����X�N���v�g�擾�i�ύX�j
        enemyController = transform.parent.GetComponent<Enemy>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //�v���C���[��������ɓ�����
        if (other.gameObject.tag == "Player")
        {
            enemyController.fieldViewFlg = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //�v���C���[�����삩��O�ꂽ
        if (other.gameObject.tag == "Player")
        {
            enemyController.fieldViewFlg = false;
        }
    }
}