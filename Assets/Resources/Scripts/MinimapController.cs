using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ミニマップを回転させないよう固定
/// </summary>
public class MinimapController : MonoBehaviour
{
    //変数（追加）
    Camera camera;

    void Start()
    {
        //コンポーネント取得（追加）
        camera = GetComponent<Camera>(); 
    }

    void Update()
    {
            //回転を固定
            transform.rotation = Quaternion.Euler(90, 0, 0);
    }

        //拡大プラスボタン（追加）
    public void OnPressPlusBtn() 
    {
        if (camera.orthographicSize > 20) 
        {
                camera.orthographicSize -= 10f;
        }

    }

        //縮小マイナスボタン（追加）
    public void OnPressMinusBtn() 
    {
         if (camera.orthographicSize < 80)
         {
                camera.orthographicSize += 10;
         }
    }
     
}
