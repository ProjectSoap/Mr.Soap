using UnityEngine;
using System.Collections;

public class MaterialSwitcher : MonoBehaviour {

    [SerializeField]
    Material[] m_materials;

    MeshRenderer m_mr;
    // Use this for initialization
	void Start () {
        m_mr = GetComponent<MeshRenderer>();
	}

    public bool SwitchMaterial(int select)
    {
        if (
            0 < m_materials.Length &&
            0 < select &&
            select < m_materials.Length
            ) 
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
