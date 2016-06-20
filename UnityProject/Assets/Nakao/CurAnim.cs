using UnityEngine;
using System.Collections;

public class CurAnim : MonoBehaviour {

    [SerializeField]
    Animator awa;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            int a = Animator.StringToHash("menu_sentaku_");
            awa.Play(a);
        }
	}
}
