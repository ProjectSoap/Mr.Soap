using UnityEngine;
using System.Collections;

public class CreditQuad : MonoBehaviour {
	public float speed = 0.009f;
    bool isCollision = false;
    // Use this for initialization
    void Start()
	{
		GetComponent<MeshRenderer>().material.SetFloat("_MaskAlpha", -0.01f);

		GetComponent<MeshRenderer>().enabled = false;
	}

    // Update is called once per frame
    void Update()
    {
        if (isCollision)
        {
            float a = GetComponent<MeshRenderer>().sharedMaterial.GetFloat("_MaskAlpha");
            if (a < 1)
            {
                GetComponent<MeshRenderer>().material.SetFloat("_MaskAlpha", a + speed * Time.deltaTime);

            }

        }
    }
    void OnTriggerEnter(Collider collisionObject)
    {
        isCollision = true;
		GetComponent<MeshRenderer>().enabled = true;
         
    }
}
