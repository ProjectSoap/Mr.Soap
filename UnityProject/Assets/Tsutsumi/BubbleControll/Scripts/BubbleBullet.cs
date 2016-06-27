using UnityEngine;
using System.Collections;

public class BubbleBullet : MonoBehaviour {

    public float LIVE_TIME;
    public float MoveHeight;
    public float MoveWidth;

    private float timeCount;
    private Rigidbody rigid;

    private bool collisionFlg;
    ParticleSystem particleSystem;
    ParticleSystem particleSystem2;
    ParticleSystem particleSystem3;
	// Use this for initialization
	void Awake () {
        rigid = GetComponent<Rigidbody>();

        Vector3 MoveVec;

        MoveVec = transform.forward * MoveWidth;
        MoveVec.y += MoveHeight;
        rigid.velocity = MoveVec;

        timeCount = 0.0f;

        collisionFlg = false;
        particleSystem = GetComponent<ParticleSystem>();
        particleSystem2 = transform.FindChild("Particle System 2").GetComponent<ParticleSystem>();
        particleSystem3 = transform.FindChild("Particle System 3").GetComponent<ParticleSystem>();

        if (BGMManager.Instance != null)
        {
            BGMManager.Instance.PlaySE("Wash_Fly");
        }
        transform.parent = GameObject.Find("PauseObject").transform;
    }
	
	// Update is called once per frame
	void Update () {

        timeCount += Time.deltaTime;

        if (collisionFlg == true)
        {
            if (timeCount > 0.2f)
            {
                particleSystem.enableEmission = false;
                particleSystem2.enableEmission = false;
                particleSystem3.enableEmission = false;
            }
        }

        //時間がたったら消す
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
        Vector3 vec = rigid.velocity;
        vec = vec *= 0.8f;

        rigid.velocity = vec;

        collisionFlg = true;
    }
}
