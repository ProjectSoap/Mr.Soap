using UnityEngine;
using System.Collections;

public class MaterialSwitcher : MonoBehaviour {

    [SerializeField]
    Material[] m_materials;

    MeshRenderer m_mr;
    // Use this for initialization
	void Start () {
	}

    public bool SwitchMaterial(int select)
    {
        m_mr = GetComponent<MeshRenderer>();
        {
            m_mr.material = m_materials[select];

            return true;
        }
        return false;
    }

	// Update is called once per frame
	void Update () {
	
	}
}
