using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class WashChain : MonoBehaviour {

    [SerializeField]
    Image needle;

    [SerializeField]
    int now_chain;
    [SerializeField]
    float delay;

    Shine shine;

    private Quaternion now_rotation;
    public bool chainmax;
    public bool heal;
    public bool minus;
    float limit;
    float now_time;
    float rotate_angle;
    Quaternion before_rotation;
	// Use this for initialization
	void Start () {
        rotate_angle = 18.0f;
        now_rotation = new Quaternion(0,0,25,1);
        shine = GameObject.Find("Shine").GetComponent<Shine>();
	}
	
	// Update is called once per frame
	void Update () {
        //GetWash();
        now_time += Time.deltaTime;
        if (now_chain>0)
        {
            if (now_time > limit)
            {
                minus = true;
                now_chain--;
                if(now_chain<0)
                {
                    now_chain = 0;
                }
                SetLimit();
                SetAgree();
            }
            else if (now_chain == 10)
            {
                if(heal)
                {
                    GameObject player;
                    player = GameObject.Find("PlayerCharacter");
                    player.GetComponent<PlayerCharacterController>().WashChain();
                    heal = false;
                    ActionRecordManager.sActionRecord.WashChainCount++;
                }
                needle.rectTransform.rotation = Quaternion.Lerp(needle.rectTransform.rotation, now_rotation, (now_time / limit));
                if(now_time > 1.3f)
                {
                    chainmax = false;
                    now_chain = 0;
                    SetLimit();
                    SetAgree();
                }
            }
            else
            {
                if(now_time < limit/delay && !minus)
                {
                    needle.rectTransform.rotation = Quaternion.Lerp(needle.rectTransform.rotation, now_rotation, (now_time / limit));
                }
                else
                {
                    needle.rectTransform.rotation = Quaternion.Lerp(now_rotation, before_rotation, (now_time / limit));
                }
               // needle.rectTransform.rotation = Quaternion.Lerp(needle.rectTransform.rotation, now_rotation, (now_time / limit));
                //needle.rectTransform.rotation = Quaternion.Lerp(now_rotation, before_rotation,(now_time/limit));
            }
        }
        else
        {
            if (now_time < limit)
            {
                needle.rectTransform.rotation = Quaternion.Lerp(now_rotation, before_rotation, (now_time / limit));
            }
            else
            {
                needle.rectTransform.rotation = Quaternion.Euler(0, 0, 25.0f - rotate_angle * (0));
            }
        }
	       //needle.rectTransform.rotation =
        
	}

    void SetLimit()
    {
        now_time = 0;
        if(now_chain >= 10)
        {
            shine.StartLight();
            before_rotation = needle.rectTransform.rotation;
            now_rotation =  Quaternion.Euler(0, 0, (25.0f - rotate_angle * (now_chain)));
        }

        if (now_chain > 7) { limit = 5.0f; }
        else if (now_chain > 5) { limit = 6.0f; }
        else if (now_chain > 3) { limit = 7.0f; }
        else { limit = 8.0f; }

        if(now_chain == 0)
        {
            limit = 0.2f;
        }
    }

    void SetAgree()
    {
        //needle.rectTransform.rotation = 
        if (now_chain > 0)
        {
            now_rotation = Quaternion.Euler(0, 0, (25.0f - rotate_angle * (now_chain)));
            before_rotation = Quaternion.Euler(0, 0, 25.0f - rotate_angle * (now_chain - 1));
            //before_rotation = needle.rectTransform.rotation;
        }
        else
        {
            now_rotation = Quaternion.Euler(0, 0, (25.0f - rotate_angle * (10)));
            before_rotation = Quaternion.Euler(0, 0, 25.0f - rotate_angle * (0));
        }
    }
    public void GetWash()
    {
        //if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            now_chain++;
            if(now_chain>=10)
            {
                now_chain = 10;
                if (!chainmax)
                {
                    heal = true;
                }
                chainmax = true;
               

               // shine.StartLight();
            }
            minus = false;
            SetLimit();
            SetAgree();
        }
        
    }
}
