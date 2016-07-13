using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class Wash_Gauge : MonoBehaviour
{

    [SerializeField]
    Image gauge;
    [SerializeField]
    float startAmount;
    [SerializeField]
    float limitAmount;

    [SerializeField]
    float time_dec;

    [SerializeField]
    float gauge_gaintime;

    Shine shine;

    [SerializeField]
    float  now_Amount;
    [SerializeField]
    float   old_Amount;

    [SerializeField]
    Color startcolor;

    [SerializeField]
    Color endcolor;

    [SerializeField]
    ParticleSystem heal_effect;

    [SerializeField]
    Camera maincamera;

    [SerializeField]
    Vector3 camera_offset;

    public bool heal;
    public bool minus;
    // Use this for initialization
    void Start()
    {
        gauge.fillAmount = startAmount;
    }

    // Update is called once per frame
    void Update()
    {
       /* if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            old_Amount = now_Amount;
            now_Amount += (limitAmount - startAmount) / 10.0f;
            if (now_Amount >= limitAmount)
            {
                now_Amount = limitAmount;
                if (!heal)
                {
                    heal = true;
                }

                // shine.StartLight();
            }
            gauge_gaintime = 0.0f;
            SetLimit();
            SetAgree();
        }
        */
        //GetWash();
        if(heal)
        {
            GameObject player;
            player = GameObject.Find("PlayerCharacter");
            player.GetComponent<PlayerCharacterController>().WashChain();
            heal = false;
            minus = true;
            ActionRecordManager.sActionRecord.WashChainCount++;
            heal_effect.Play();
        }
        else
        {
            if (minus)
            {
                gauge_gaintime += Time.deltaTime;
                if(gauge_gaintime < 1.0f)
                {
                    gauge.fillAmount = limitAmount;
                }
                else if(gauge_gaintime  < 1.5f)
                {
                    gauge.fillAmount = Mathf.Lerp(limitAmount, startAmount, (gauge_gaintime - 1.0f) * 2.0f );
                }
                else
                {
                    heal_effect.Stop();
                    gauge.fillAmount = startAmount;
                    now_Amount = startAmount;
                    minus = false;
                }
            }
            else
            {
                now_Amount -= Time.deltaTime / time_dec/10.0f;
                gauge_gaintime += Time.deltaTime;
                if (now_Amount <= startAmount)
                {
                    now_Amount = startAmount;
                    gauge.fillAmount = now_Amount;
                }

                if(gauge_gaintime < 0.5f)
                {
                    gauge.fillAmount = Mathf.Lerp(old_Amount, now_Amount, gauge_gaintime * 2.0f);
                }
                else
                {
                    gauge.fillAmount = now_Amount;
                }

            }
        }

        gauge.color = Color.Lerp(startcolor, endcolor, now_Amount / limitAmount);
    }

    void Reset()
    {
        
    }

    void SetLimit()
    {
        
    }

    void SetAgree()
    {
        
    }
    public void GetWash()
    {
        //if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            old_Amount = now_Amount;
            now_Amount += (limitAmount - startAmount) /9.2f;
            if (now_Amount >= limitAmount)
            {
                now_Amount = limitAmount;
                if (!heal && !minus)
                {
                    heal = true;
                }
                
                // shine.StartLight();
            }
            BGMManager.Instance.PlaySE("Wash_Out", 1.0f + (now_Amount / limitAmount) * 0.2f);
            gauge_gaintime = 0.0f;
            SetLimit();
            SetAgree();
        }

    }
}
