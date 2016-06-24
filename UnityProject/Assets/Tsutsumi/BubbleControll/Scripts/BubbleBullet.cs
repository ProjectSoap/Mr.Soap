using UnityEngine;
using System.Collections;

public class BubbleBullet : MonoBehaviour {

    public float LIVE_TIME;
    public float MoveHeight;
    public float MoveWidth;

    private float timeCount;
    private Rigidbody rigid;

	// Use this for initialization
	void Awake () {
        rigid = GetComponent<Rigidbody>();

        Vector3 MoveVec;

        MoveVec = transform.forward * MoveWidth;
        MoveVec.y += MoveHeight;
        rigid.velocity = MoveVec;

        timeCount = 0.0f;
        if (BGMManager.Instance != null)
        {
            BGMManager.Instance.PlaySE("Wash_Fly");
        }
        transform.parent = GameObject.Find("PauseObject").transform;
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
            vec = vec *= 0.8f;

            rigid.velocity = vec;
        }
    }
}
