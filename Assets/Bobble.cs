using UnityEngine;
using System.Collections;

public class Bobble : MonoBehaviour {

    [SerializeField]
    float velocityZ; // z方向の速度

    [SerializeField]
    float lifeTime; // 生存期間
    
    float timeElapsed;

    // Use this for initialization
    void Start ()
    {

        int layer1 = LayerMask.NameToLayer("Bubble");
        int layer2 = LayerMask.NameToLayer("Player");
        // 衝突を無視するように設定
        Physics.IgnoreLayerCollision(layer1, layer1);
        Physics.IgnoreLayerCollision(layer1, layer2);
    }
	
	// Update is called once per frame
	void Update ()
    {
        GetComponent<Rigidbody>().position += new Vector3(0, 0, velocityZ * Time.deltaTime);
        Debug.Log(transform.position);
	    if (lifeTime <= timeElapsed)
	    {
            Destroy(gameObject);
        }
        timeElapsed += Time.deltaTime;
    }
}
