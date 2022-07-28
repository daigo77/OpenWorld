using UnityEngine;

/// <summary>
/// UI‚ğí‚É³–ÊiƒJƒƒ‰‘¤j‚ÉŒü‚©‚¹‚é
/// </summary>
public class UIFrontController : MonoBehaviour
{
    void Update()
    {
        transform.LookAt(Camera.main.transform);
    }
}