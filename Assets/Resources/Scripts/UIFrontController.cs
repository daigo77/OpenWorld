using UnityEngine;

/// <summary>
/// UI����ɐ��ʁi�J�������j�Ɍ�������
/// </summary>
public class UIFrontController : MonoBehaviour
{
    void Update()
    {
        transform.LookAt(Camera.main.transform);
    }
}