using UnityEngine;
using Cinemachine;

/// <summary>
/// CinemachineFreeLookCamera�𐧌�
/// </summary>
public class CMFreelookController : MonoBehaviour
{
    //�J�����̃I�t�Z�b�g�i�ǉ��j
    CinemachineCameraOffset offset;
    //�}�E�X�z�C�[���ʁi�ǉ��j
    float scrollDelta;
        

    void Start()
    {
        CinemachineCore.GetInputAxis = GetAxisCustom;

        //�R���|�[�l���g�擾�i�ǉ��j
        offset = GetComponent<CinemachineCameraOffset>();
    }

    public float GetAxisCustom(string axisName)
    {
        if (axisName == "Mouse X")
        {
            if (Input.GetMouseButton(0))
            {
                return Input.GetAxis("Mouse X");
            }
            else
            {
                return 0;
            }
        }
        else if (axisName == "Mouse Y")
        {
            if (Input.GetMouseButton(0))
            {
                return Input.GetAxis("Mouse Y");
            }
            else
            {
                return 0;
            }
        }

        //����ȊO�̑����Ԃ�
        return Input.GetAxis(axisName);
    }

    //�i�ǉ��j
    private void Update()
    {
        //�}�E�X�z�C�[���̈ړ��ʎ擾
        var delta = Input.mouseScrollDelta.y;
        //�z�C�[���ʂ����Z�i�������悷����̂ŕ␳�l������j
        scrollDelta += delta / 20;
        //�N�����v���ď��������ݒ�
        scrollDelta = Mathf.Clamp(scrollDelta, -2.0f, 1.5f);
        //�I�t�Z�b�g�l�ɓK��
        offset.m_Offset= new Vector3(0, 0, scrollDelta);
    }
}