using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��]�R�莞�ɃG�t�F�N�g���v���C���[�̎������]������
public class AttackRotateController : MonoBehaviour
{
    [SerializeField] float speed;               //��]�X�s�[�h
    [SerializeField] float offsetY;             //���S�ƂȂ�ΏۃI�u�W�F�N�g����Ƃ��������I�t�Z�b�g�l
    [SerializeField] float radius;              //��]���锼�a�i���S�I�u�W�F�N�g�Ƃ̋����j

    float time;                                 //��]���̎��Ԍo�� �i0����n�߂邱�Ƃŏ�ɉ�]�̊J�n�n�_���L�����̑O���ɂ���j
    GameObject centerObj;                       //���S�ƂȂ�ΏۃI�u�W�F�N�g

    void Start()
    {
        //Center�I�u�W�F�N�g�擾
        centerObj = transform.parent.gameObject;
        //�N���[�����ɉ�]�̎n�܂�ꏊ�i�L�����̐��ʁj�ɔz�u���� *���̏����������Ȃ���1�t���[�������L�����Ɠ������W�ɑ��݂��Ă��܂���
        transform.position = transform.position + new Vector3(0, centerObj.transform.position.y, radius);
    }

    void Update()
    {
        //�����iY���W�j�̂݃Z�b�g
        Vector3 pos = new Vector3(0, centerObj.transform.position.y + offsetY, 0);

        //Sin�ACos���g���ĉ~��ɂȂ�悤�ɍ��W���v�Z����
        //�~�̒��a�����|�����킹�邱�ƂŒ��S�I�u�W�F�N�g�̎���𔼌a���Ŏ��񂷂�
        //���S�I�u�W�F�N�g��x,z���W�����Z����Ƃ��ŉ~�̒��S���W��ύX����
        pos.x = radius * Mathf.Sin(2 * Mathf.PI * speed * time) + centerObj.transform.position.x;
        pos.z = radius * Mathf.Cos(2 * Mathf.PI * speed * time) + centerObj.transform.position.z;

        //�v�Z���ꂽ���W���J�����ɃZ�b�g
        transform.position = pos;

        //���S�I�u�W�F�N�g�̕�����������
        transform.LookAt(centerObj.transform);

        //���Ԍo�߂��X�V
        time += Time.deltaTime;
    }

    //Speed�l�̃v���p�e�B
    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }
}