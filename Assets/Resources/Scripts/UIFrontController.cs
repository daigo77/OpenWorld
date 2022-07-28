using UnityEngine;

/// <summary>
/// UIを常に正面（カメラ側）に向かせる
/// </summary>
public class UIFrontController : MonoBehaviour
{
    void Update()
    {
        transform.LookAt(Camera.main.transform);
    }
}