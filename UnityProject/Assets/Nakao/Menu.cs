using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {
    [SerializeField]
    Frame menu;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.Return))
        {
            switch(menu.SelectNow)
            {
                case 0:
                    {
                        Fade.ChangeScene("Select");
                        break;
                    }
                case 1:
                    {
                        Fade.ChangeScene("PlayRecordScene");
                        break;
                    }
                case 2:
                    {
                        Fade.ChangeScene("Ranking");
                        break;
                    }
                default :
                    {
                        break;
                    }
            }
        }

        if (Input.GetKey(KeyCode.Escape))
        {

            Fade.ChangeScene("sekTitle");
        }
	}
}
