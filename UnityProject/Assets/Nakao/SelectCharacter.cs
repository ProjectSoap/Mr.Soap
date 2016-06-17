using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectCharacter : MonoBehaviour {

    [SerializeField]
    List<Transform> position;

    [SerializeField]
    List<Sprite> Hatena;

    [SerializeField]
    List<Animator> soapsAnimation;

    [SerializeField]
    int charno;
    [SerializeField]
    int charMax;
    [SerializeField]
    float nowrad;
    [SerializeField]
    float selectrad;
    [SerializeField]
    float rotspead;
	// Use this for initialization
	void Start () {
      //  Axis = this.GetComponent<Transform>();
          
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 trans;
        Quaternion qt = new Quaternion(0,0,0,0);
        if (Fade.FadeEnd())
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) && nowrad == selectrad)
            {
                charno--;
                if (nowrad == 0.0f)
                {
                    nowrad = 360.0f;
                }
                if (charno < 0)
                {
                    charno = charMax - 1;
                }
                if (charno >= charMax)
                {
                    charno = 0;
                }
                selectrad = charno * 360.0f / charMax;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && nowrad == selectrad)
            {
                charno++;
                if (nowrad == 360.0f)
                {
                    nowrad = 0;
                }

                selectrad = charno * 360.0f / charMax;
                if (charno < 0)
                {
                    charno = charMax;
                }
                if (charno >= charMax)
                {
                    charno = 0;
                }
            }

            if (nowrad < selectrad)
            {
                nowrad += rotspead;
                if (nowrad >= selectrad)
                {
                    // nowrad = selectrad;
                }
                //       Axis.transform.rotation = rot;
            }
            else if (nowrad > selectrad)
            {
                nowrad -= rotspead;
                if (nowrad >= selectrad)
                {
                    //nowrad = selectrad;
                }
                //          Axis.transform.rotation = rot;
            }
        }
        for (int i = 0; i < charMax; ++i)
        {
             trans.x = -1.5f * Mathf.Sin((nowrad + (i * 360.0f / charMax)) * Mathf.Deg2Rad) * 1.2f;
             trans.y = position[i].transform.position.y;
             trans.z = -1.5f * Mathf.Cos((nowrad + (i * 360.0f / charMax)) * Mathf.Deg2Rad) * 1.2f;

             position[i].transform.position = trans;
             position[i].transform.rotation = qt;
             position[i].transform.Rotate(new Vector3(0, 1, 0), (nowrad + (i * 360.0f / charMax)));
         }
        /*
        if (Input.GetKey(KeyCode.Backspace))
        {
            Application.LoadLevel("Menu");
        }
        if (Input.GetKey(KeyCode.Return))
        {
            Application.LoadLevel("main");
        }
         * */
        for (int i = 0; i < soapsAnimation.Count; ++i )
        {
            if(i == charno)
            {
                soapsAnimation[i].SetBool("Select", true);
            }
            else
            {
                soapsAnimation[i].SetBool("Select", false);
            }
        }
            
	}

    public int GetCharNo()
    {
        return charno;
    }

    public bool EndRotation()
    {
        if(selectrad == nowrad)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
