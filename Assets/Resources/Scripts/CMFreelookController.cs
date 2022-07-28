using UnityEngine;
using Cinemachine;

/// <summary>
/// CinemachineFreeLookCameraを制御
/// </summary>
public class CMFreelookController : MonoBehaviour
{
    //カメラのオフセット（追加）
    CinemachineCameraOffset offset;
    //マウスホイール量（追加）
    float scrollDelta;
        

    void Start()
    {
        CinemachineCore.GetInputAxis = GetAxisCustom;

        //コンポーネント取得（追加）
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

        //それ以外の操作を返す
        return Input.GetAxis(axisName);
    }

    //（追加）
    private void Update()
    {
        //マウスホイールの移動量取得
        var delta = Input.mouseScrollDelta.y;
        //ホイール量を加算（反応がよすぎるので補正値を入れる）
        scrollDelta += delta / 20;
        //クランプして上限下限を設定
        scrollDelta = Mathf.Clamp(scrollDelta, -2.0f, 1.5f);
        //オフセット値に適応
        offset.m_Offset= new Vector3(0, 0, scrollDelta);
    }
}