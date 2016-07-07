using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MaterialChanger : MonoBehaviour {

    public enum ESekkenMaterial{
        Sad,
        Shy,
        Joy
    };

    [SerializeField]
    private Material[] materialData = new Material[3];

    [SerializeField]
    private List<SkinnedMeshRenderer> changeMaterialList;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ChangeMaterial(ESekkenMaterial no)
    {
        for (int i = 0; i < changeMaterialList.Count; ++i)
        {
            changeMaterialList[i].material = materialData[(int)no];
        }
    }
}
