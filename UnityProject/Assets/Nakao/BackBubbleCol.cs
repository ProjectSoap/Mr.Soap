using UnityEngine;
using System.Collections;

public class BackBubbleCol : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter2D(Collider2D c)
    {
        string layerName = LayerMask.LayerToName(c.gameObject.layer);
        if (this.gameObject.transform.localScale.x >= c.gameObject.transform.localScale.x)
        {
            
            this.gameObject.transform.position = (this.gameObject.transform.position + c.gameObject.transform.position) / 2.0f;
            
            this.gameObject.transform.localScale = this.gameObject.transform.localScale * 1.2f;
            if(this.gameObject.transform.localScale.x > 1.0f)
            {
                Vector3 scale = this.gameObject.transform.localScale;
                scale.x = scale.y = 1.0f + Random.Range(-0.1f,0.1f);
                this.gameObject.transform.localScale = scale;
                
            }
            
            Vector3 trans = new Vector3(100, 0, 0);
            c.gameObject.transform.position = trans;
             
        }
        else if (this.gameObject.transform.localScale.x < c.gameObject.transform.localScale.x)
        {
            /*
            Vector3 trans = new Vector3(100, 0, 0);
            this.gameObject.transform.position = trans;
        
            c.gameObject.transform.position = (this.gameObject.transform.position + c.gameObject.transform.position) / 2.0f;
            c.gameObject.transform.localScale = c.gameObject.transform.localScale * 1.2f;
            if (c.gameObject.transform.localScale.x > 2.0f)
            {
                Vector3 scale = c.gameObject.transform.localScale;
                scale.x = scale.y = 2.0f;
                c.gameObject.transform.localScale = scale;
            }
             * */
           // Vector3 trans = new Vector3(100, 0, 0);
            //this.gameObject.transform.position = trans;
        }
        /*
        if (layerName == "DummyBubble")
        {
            Vector3 trans = new Vector3(100,0,0);
            this.gameObject.transform.position = trans;
        }
        if (layerName == "Bubble")
        {
            this.gameObject.transform.position = (this.gameObject.transform.position + c.gameObject.transform.position) / 2.0f;
            this.gameObject.transform.localScale = this.gameObject.transform.localScale * 1.2f;
            // Destroy(c.gameObject);
        }
         * */
    }
}
