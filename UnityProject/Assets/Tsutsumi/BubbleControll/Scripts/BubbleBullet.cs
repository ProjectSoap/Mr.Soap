using UnityEngine;
using System.Collections;

public class BubbleBullet : MonoBehaviour {

    public float LIVE_TIME;
    private float timeCount;
    private Rigidbody rigid;

	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody>();

        Vector3 MoveVec;

        MoveVec = Vector3.forward*5.0f;
        MoveVec.y += 10.0f;
        rigid.velocity = MoveVec;

        timeCount = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {

        //時間がたったら消す
        timeCount += Time.deltaTime;
        if (timeCount > LIVE_TIME)
        {
            Destroy(gameObject);
        }
	}

    public void Burst(Vector3 pos, Vector3 vec)
    {
        transform.position = pos;
        rigid.velocity = vec;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Building"))
        {
            Vector3 vec = rigid.velocity;
            vec.x = vec.z = 0.0f;

            rigid.velocity = vec;
        }
    }
}
