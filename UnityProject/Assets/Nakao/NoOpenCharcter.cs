using UnityEngine;
using System.Collections;

public class NoOpenCharcter : MonoBehaviour {

    public float time;
    [SerializeField]
    float limit;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if(time > limit)
        {
            this.gameObject.SetActive(false);
        }
	}

    public void Active()
    {
        time = 0.0f;
        this.gameObject.SetActive(true);
    }

    public void Sleep()
    {
        time = 0.0f;
        this.gameObject.SetActive(false);
    }

}
