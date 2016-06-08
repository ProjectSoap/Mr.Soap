using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BackGround : MonoBehaviour {
    [SerializeField]
    Image back;

    float v;
    Color color;
    // スクロールするスピード
    public float speed = 0.1f;

    void Start()
    {
        color = back.color;
    }

    void Update ()
    {
        // 時間によってYの値が0から1に変化していく。1になったら0に戻り、繰り返す。
        float y = Mathf.Repeat (Time.time * speed, 1.0f);

        // Yの値がずれていくオフセットを作成
        Vector2 offset = new Vector2 (y, y);
        color.b = 0.6f + Mathf.PingPong(Time.time * speed, 0.4f);

        // マテリアルにオフセットを設定する
        GetComponent<Renderer>().sharedMaterial.SetTextureOffset ("_MainTex", offset);
        back.color = color;
    }
}
