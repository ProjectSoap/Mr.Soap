using UnityEngine;
using System.Collections;

public class CreditQuad : MonoBehaviour {

    bool isCollision = false;
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update()
    {
        if (isCollision)
        {
            float a = GetComponent<MeshRenderer>().sharedMaterial.GetFloat("_MaskAlpha");
            if (a < 1)
            {
                GetComponent<MeshRenderer>().material.SetFloat("_MaskAlpha", a + 0.01f);

            }

        }
    }
    void OnTriggerEnter(Collider collisionObject)
    {
        isCollision = true;
         
    }
}
