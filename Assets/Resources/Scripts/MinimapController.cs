using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �~�j�}�b�v����]�����Ȃ��悤�Œ�
/// </summary>
public class MinimapController : MonoBehaviour
{
    //�ϐ��i�ǉ��j
    Camera camera;

    void Start()
    {
        //�R���|�[�l���g�擾�i�ǉ��j
        camera = GetComponent<Camera>(); 
    }

    void Update()
    {
            //��]���Œ�
            transform.rotation = Quaternion.Euler(90, 0, 0);
    }

        //�g��v���X�{�^���i�ǉ��j
    public void OnPressPlusBtn() 
    {
        if (camera.orthographicSize > 20) 
        {
                camera.orthographicSize -= 10f;
        }

    }

        //�k���}�C�i�X�{�^���i�ǉ��j
    public void OnPressMinusBtn() 
    {
         if (camera.orthographicSize < 80)
         {
                camera.orthographicSize += 10;
         }
    }
     
}
