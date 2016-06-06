using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DirtyObjectScript : MonoBehaviour
{
    [SerializeField]
    Material[] dirtyMaterials = new Material[8];
    DityApparancePosition myPoint;
    public DityApparancePosition MyPoint
    {
        get { return myPoint; }
        set { myPoint = value; }
    }
    // Use this for initialization
    void Start()
    {

    }


    void SwitchMaterial(int num)
    {
        if (0 <= num && num < dirtyMaterials.Length)
        {
            GetComponent<MeshRenderer>().materials[0] = dirtyMaterials[num];
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Bubble")
        {
            myPoint.NoticeDestroy();
            Destroy(gameObject);
        }
    }
}
