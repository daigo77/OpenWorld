using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//回転蹴り時にエフェクトをプレイヤーの周りを回転させる
public class AttackRotateController : MonoBehaviour
{
    [SerializeField] float speed;               //回転スピード
    [SerializeField] float offsetY;             //中心となる対象オブジェクトを基準とした高さオフセット値
    [SerializeField] float radius;              //回転する半径（中心オブジェクトとの距離）

    float time;                                 //回転時の時間経過 （0から始めることで常に回転の開始地点をキャラの前方にする）
    GameObject centerObj;                       //中心となる対象オブジェクト

    void Start()
    {
        //Centerオブジェクト取得
        centerObj = transform.parent.gameObject;
        //クローン時に回転の始まる場所（キャラの正面）に配置する *この初期化をしないと1フレームだけキャラと同じ座標に存在してしまう為
        transform.position = transform.position + new Vector3(0, centerObj.transform.position.y, radius);
    }

    void Update()
    {
        //高さ（Y座標）のみセット
        Vector3 pos = new Vector3(0, centerObj.transform.position.y + offsetY, 0);

        //Sin、Cosを使って円状になるように座標を計算する
        //円の直径分を掛け合わせることで中心オブジェクトの周りを半径分で周回する
        //中心オブジェクトのx,z座標を加算するとこで円の中心座標を変更する
        pos.x = radius * Mathf.Sin(2 * Mathf.PI * speed * time) + centerObj.transform.position.x;
        pos.z = radius * Mathf.Cos(2 * Mathf.PI * speed * time) + centerObj.transform.position.z;

        //計算された座標をカメラにセット
        transform.position = pos;

        //中心オブジェクトの方を向かせる
        transform.LookAt(centerObj.transform);

        //時間経過を更新
        time += Time.deltaTime;
    }

    //Speed値のプロパティ
    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }
}