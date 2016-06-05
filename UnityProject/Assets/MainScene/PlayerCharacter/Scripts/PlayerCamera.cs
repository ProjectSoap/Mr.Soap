using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField]
    Transform target;

    [SerializeField]
    float distance = 2.0f;

    [SerializeField]
    float height = 1.0f;

    [SerializeField]
    float smoothing = 5f;

    void FixedUpdate()
    {
        //Vector3 offset = transform.position - target.position;

        //Vector3 offsetPosition = -target.forward;

        //offsetPosition.Scale(offset);

        //Vector3     targetCameraPosition = target.position + (offsetPosition);
        ////Quaternion  targetCameraRotation = Quaternion.FromToRotation(targetCameraPosition, target.position);
        ////Quaternion  targetCameraRotation = target.rotation;
        //Quaternion  targetCameraRotation = Quaternion.LookRotation(-(targetCameraPosition - target.position)    );

        //transform.position = Vector3.Lerp(transform.position, targetCameraPosition, smoothing * Time.deltaTime);
        //transform.rotation = Quaternion.Slerp(transform.rotation, targetCameraRotation, smoothing * Time.deltaTime);

        ////transform.RotateAround(target.position, Vector3.up, 360 * Time.deltaTime);

        Vector3     toCamera = transform.position - target.transform.position;
        Vector3     targetCameraPosition = target.transform.position + (toCamera * distance);
        Quaternion  targetCameraRotation = Quaternion.LookRotation(targetCameraPosition - targetCameraPosition);

        targetCameraPosition.y += height;

        transform.position = Vector3.Lerp(transform.position, targetCameraPosition, smoothing * Time.deltaTime);
        //transform.rotation = Quaternion.Slerp(transform.rotation, targetCameraRotation, smoothing * Time.deltaTime);
    }


    //public Transform target;    // ターゲットへの参照
    //   private Vector3 offset;     // 相対座標
    //   private Transform[] array;
    //   void Start () {

    //       array = new Transform[30];

    //       for (int i = 0; i < 30; i++)
    //       {
    //           array[i] = transform;
    //       }
    //       offset = GetComponent<Transform>().position - target.position;
    //   }

    //// Update is called once per frame
    //void Update () {


    //       // 自分の座標にtargetの座標を代入する
    //       array[29].position = target.position + transform.rotation * offset;

    //       for (int i = 0;i<29;i++)
    //       {
    //           array[i] = array[i + 1];
    //       }
    //       transform.position = array[0].position;
    //   }

}
