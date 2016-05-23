using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour {

	public Transform target;    // ターゲットへの参照
    private Vector3 offset;     // 相対座標
    private Transform[] array;
    void Start () {

        array = new Transform[30];

        for (int i = 0; i < 30; i++)
        {
            array[i] = transform;
        }
        offset = GetComponent<Transform>().position - target.position;
    }
	
	// Update is called once per frame
	void Update () {


        // 自分の座標にtargetの座標を代入する
        array[29].position = target.position + transform.rotation * offset;

        for (int i = 0;i<29;i++)
        {
            array[i] = array[i + 1];
        }
        transform.position = array[0].position;
    }
}
