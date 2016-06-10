using UnityEngine;
using System.Collections;

public class RecoverySoapObject : MonoBehaviour {

    RecoverySoapCreater parent;

    public RecoverySoapCreater Parent
    {
        get { return parent; }
        set { parent = value; }
    }

    [SerializeField]
    float lifeTime;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            parent.IsHaveRevoverySoap = false;
            Destroy(gameObject);
        }
	}


    void OnTriggerEnter(Collider collisionObject)
    {
        if (collisionObject.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Parent.IsHaveRevoverySoap = false;
            PlayerCharacterController player = collisionObject.gameObject.GetComponent<PlayerCharacterController>();
            player.size = player.size + 50;
            if (player.size > 100)
            {
                player.size = 100;
            }
            BGMManager.Instance.PlaySE("Recovery");
            Destroy(gameObject);
        }
    }}
