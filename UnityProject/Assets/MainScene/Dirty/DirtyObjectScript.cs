using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DirtyObjectScript : MonoBehaviour
{
    [SerializeField]
    Material[] dirtyMaterials = new Material[8];
    DityApparancePosition myPoint;
    [SerializeField]
    GameObject dirtyIcon;
    bool isDestory = false;


    public DityApparancePosition MyPoint
    {
        get { return myPoint; }
        set { myPoint = value; }
    }
    // Use this for initialization
    void Start()
    {
        GameObject obj= Instantiate(dirtyIcon, new Vector3(transform.position.x, dirtyIcon.transform.position.y, transform.position.z),dirtyIcon.transform.rotation) as GameObject;
        obj.transform.parent = transform;
    }


    public void SwitchMaterial(int num)
    {
        if (0 <= num && num < dirtyMaterials.Length)
        {
            GetComponent<MeshRenderer>().material = dirtyMaterials[num];
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Bubble" || (collision.gameObject.layer == LayerMask.NameToLayer("Player")))
        {
            if (isDestory == false)
            {
                BGMManager.Instance.PlaySE("Wash_Out");
                myPoint.NoticeDestroy();
                isDestory = true;
            }
            Destroy(gameObject);
        }
    }
}
