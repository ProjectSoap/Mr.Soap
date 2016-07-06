using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Weather
{
    SUN,
    RAIN,
    FOG,
    WIND
}


public class WeatherSystem : MonoBehaviour {
    [SerializeField]
    float time;
    int minute;
    [SerializeField]
    Weather nowWeather;
	public Weather NowWeather
	{
		get { return nowWeather; }
		set { nowWeather = value; }
	}
	PlayerCharacterController.WeatherState nowWeathers;

    bool[] weatherflg = new bool[3];
    short numWeather;

    [SerializeField]
    List<GameObject> weathers;

	PlayerCharacterController m_player;

    Skybox m_skybox;

    [SerializeField]
    Material m_skyboxMaterialSunny;

    [SerializeField]
    Material m_skyboxMaterialCloudiness;

    CharacterWordsUI TSUTSUMISAN;
    public SaveDataManager save;


	// Use this for initialization
	void Start () {
        numWeather = 0;
        NowWeather = Weather.SUN;

        TSUTSUMISAN = GameObject.Find("SekkenSayUI").GetComponent<CharacterWordsUI>();
        save = GameObject.Find("SaveDataManager").GetComponent<SaveDataManager>();
        nowWeathers = PlayerCharacterController.WeatherState.Sunny;
        LoadSave();


        m_skybox = GameObject.Find("MainCamera").GetComponent<Skybox>();

      //  ChangeWeather();
        ActiveWeather();
        minute = 0;
        
		m_player = GameObject.Find("PlayerCharacter").GetComponent<PlayerCharacterController>();
	}

    void LoadSave()
    {

        if (save.GetComponentInChildren<CheckRecordCondition>().CheckRecordConditionClear(CheckRecordCondition.ERecordName.PlayMeizin))
        {
            weatherflg[0] = true;
			numWeather++;
        }
        if (save.GetComponentInChildren<CheckRecordCondition>().CheckRecordConditionClear(CheckRecordCondition.ERecordName.PlayTatuzin))
        {
            weatherflg[1] = true;
			numWeather++;
		}
        if(save.GetComponentInChildren<CheckRecordCondition>().CheckRecordConditionClear(CheckRecordCondition.ERecordName.Muteki))
        {
			weatherflg[2] = true;
			numWeather++;
		}


	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if(time>60.0f)
        {
            time -= 60.0f;
            minute++;
            ChangeWeather();
        }
		transform.position = new Vector3( m_player.transform.position.x, 0, m_player.transform.position.z);
        transform.rotation = m_player.transform.rotation;
	}

    void ChangeWeather()
    {
        float par;
        if (numWeather == 0)
        {
            NowWeather = Weather.SUN;
            nowWeathers = PlayerCharacterController.WeatherState.Sunny;
            GameObject we;
            we = GameObject.Find("PlayerCharacter");
            we.GetComponent<PlayerCharacterController>().weatherState = nowWeathers;
        }
        else
        {
            switch (minute)
            {
                case 0:
                    {
                        if (numWeather == 1)
                        {
                            par = Random.Range(0.0f, 1.0f);
                            if (par < 0.1f)
                            {
                                CullWeatherType();
                            }
                            else
                            {
                                NowWeather = Weather.SUN;
                                nowWeathers = PlayerCharacterController.WeatherState.Sunny;
                                GameObject we;
                                we = GameObject.Find("PlayerCharacter");
                                we.GetComponent<PlayerCharacterController>().weatherState = nowWeathers;
                            }
                        }
                        else if (numWeather >= 2)
                        {
                            par = Random.Range(0.0f, 1.0f);
                            if (par < 0.2f)
                            {
                                CullWeatherType();
                            }
                            else
                            {
                                NowWeather = Weather.SUN;
                                nowWeathers = PlayerCharacterController.WeatherState.Sunny;
                                GameObject we;
                                we = GameObject.Find("PlayerCharacter");
                                we.GetComponent<PlayerCharacterController>().weatherState = nowWeathers;
                            }
                        }
                        break;
                    }
                case 1:
                    {
                        if (numWeather == 1)
                        {
                            par = Random.Range(0.0f, 1.0f);
                            if (par < 0.15f)
                            {
                                CullWeatherType();
                            }
                            else
                            {
                                NowWeather = Weather.SUN;
                                nowWeathers = PlayerCharacterController.WeatherState.Sunny;
                                GameObject we;
                                we = GameObject.Find("PlayerCharacter");
                                we.GetComponent<PlayerCharacterController>().weatherState = nowWeathers;
                            }
                        }
                        else if (numWeather >= 2)
                        {
                            par = Random.Range(0.0f, 1.0f);
                            if (par < 0.3f)
                            {
                                CullWeatherType();
                            }
                            else
                            {
                                NowWeather = Weather.SUN;
                                nowWeathers = PlayerCharacterController.WeatherState.Sunny;
                                GameObject we;
                                we = GameObject.Find("PlayerCharacter");
                                we.GetComponent<PlayerCharacterController>().weatherState = nowWeathers;
                            }
                        }
                        break;
                    }
                case 2:
                    {
                        if (numWeather == 1)
                        {
                            par = Random.Range(0.0f, 1.0f);
                            if (par < 0.25f)
                            {
                                CullWeatherType();
                            }
                            else
                            {
                                NowWeather = Weather.SUN;
                                nowWeathers = PlayerCharacterController.WeatherState.Sunny;
                                GameObject we;
                                we = GameObject.Find("PlayerCharacter");
                                we.GetComponent<PlayerCharacterController>().weatherState = nowWeathers;
                            }
                        }
                        else if (numWeather >= 2)
                        {
                            par = Random.Range(0.0f, 1.0f);
                            if (par < 0.4f)
                            {
                                CullWeatherType();
                            }
                            else
                            {
                                NowWeather = Weather.SUN;
                                nowWeathers = PlayerCharacterController.WeatherState.Sunny;
                                GameObject we;
                                we = GameObject.Find("PlayerCharacter");
                                we.GetComponent<PlayerCharacterController>().weatherState = nowWeathers;
                            }
                        }
                        break;
                    }
                case 3:
                    {
                        if (numWeather == 1)
                        {
                            par = Random.Range(0.0f, 1.0f);
                            if (par < 0.4f)
                            {
                                CullWeatherType();
                            }
                            else
                            {
                                NowWeather = Weather.SUN;
                                nowWeathers = PlayerCharacterController.WeatherState.Sunny;
                                GameObject we;
                                we = GameObject.Find("PlayerCharacter");
                                we.GetComponent<PlayerCharacterController>().weatherState = nowWeathers;
                            }
                        }
                        else if (numWeather >= 2)
                        {
                            par = Random.Range(0.0f, 1.0f);
                            if (par < 0.5f)
                            {
                                CullWeatherType();
                            }
                            else
                            {
                                NowWeather = Weather.SUN;
                                nowWeathers = PlayerCharacterController.WeatherState.Sunny;
                                GameObject we;
                                we = GameObject.Find("PlayerCharacter");
                                we.GetComponent<PlayerCharacterController>().weatherState = nowWeathers;
                            }
                        }
                        break;
                    }
                case 4:
                    {
                        par = Random.Range(0.0f, 1.0f);
                        if (par < 0.6f)
                        {
                            CullWeatherType();
                        }
                        else
                        {
                            NowWeather = Weather.SUN;
                            nowWeathers = PlayerCharacterController.WeatherState.Sunny;
                            GameObject we;
                            we = GameObject.Find("PlayerCharacter");
                            we.GetComponent<PlayerCharacterController>().weatherState = nowWeathers;
                        }
                        break;
                    }
                default:
                    {
                        if (numWeather == 1)
                        {
                            par = Random.Range(0.0f, 1.0f);
                            if (par < 0.7f)
                            {
                                CullWeatherType();
                            }
                            else
                            {
                                NowWeather = Weather.SUN;
                                nowWeathers = PlayerCharacterController.WeatherState.Sunny;
                                GameObject we;
                                we = GameObject.Find("PlayerCharacter");
                                we.GetComponent<PlayerCharacterController>().weatherState = nowWeathers;
                            }
                        }
                        else if (numWeather == 2)
                        {
                            par = Random.Range(0.0f, 1.0f);
                            if (par < 0.75f)
                            {
                                CullWeatherType();
                            }
                            else
                            {
                                NowWeather = Weather.SUN;
                                nowWeathers = PlayerCharacterController.WeatherState.Sunny;
                                GameObject we;
                                we = GameObject.Find("PlayerCharacter");
                                we.GetComponent<PlayerCharacterController>().weatherState = nowWeathers;
                            }
                        }
                        else if (numWeather == 3)
                        {
                            par = Random.Range(0.0f, 1.0f);
                            if (par < 0.8f)
                            {
                                CullWeatherType();
                            }
                            else
                            {
                                NowWeather = Weather.SUN;
                                nowWeathers = PlayerCharacterController.WeatherState.Sunny;
                                GameObject we;
                                we = GameObject.Find("PlayerCharacter");
                                we.GetComponent<PlayerCharacterController>().weatherState = nowWeathers;
                            }
                        }
                        break;
                    }

            }
        }
        ActiveWeather();
    }

    void ActiveWeather()
    {
        weathers[1].GetComponent<FogGenerator>().End();
        switch(NowWeather)
        {
            case Weather.SUN:
                {
                    for (int i = 0; i < weathers.Count; i++)
                    {
                        weathers[i].active = false;
                    }

                    m_skybox.material = m_skyboxMaterialSunny;
                    
                    break;
                }
            case Weather.RAIN:
                {
                    for (int i = 0; i < weathers.Count; i++)
                    {
                        weathers[i].active = false;
                    }
                    weathers[0].active = true;
                    ActionRecordManager.sActionRecord.isRain = true;
                    TSUTSUMISAN.DrawSayTexture(CharacterWordsUI.ESayTexName.RAIN);
                    m_skybox.material= m_skyboxMaterialCloudiness;
                    break;
                }
            case Weather.FOG:
                {
                    for (int i = 0; i < weathers.Count; i++)
                    {
                        weathers[i].active = false;
                    }
                    weathers[1].active =  true;
                    weathers[1].GetComponent<FogGenerator>().ChangeCreateMode();
                    ActionRecordManager.sActionRecord.isFog = true;
                    TSUTSUMISAN.DrawSayTexture(CharacterWordsUI.ESayTexName.FOG);
                    m_skybox.material = m_skyboxMaterialSunny;
                    break;
                }
            case Weather.WIND:
                {
                    for (int i = 0; i < weathers.Count; i++)
                    {
                        weathers[i].active = false;
                    }
                    weathers[2].active = true;
                    ActionRecordManager.sActionRecord.isWind = true;

                    TSUTSUMISAN.DrawSayTexture(CharacterWordsUI.ESayTexName.WIND);
                    m_skybox.material = m_skyboxMaterialSunny;
                    break;
                }
        }
    }

    void CullWeatherType()
    {
        int par = Random.Range(0,3);
        while (true)
        {
            if (EntWeather(par))
            {
                break;
            }
            par = Random.Range(0, 3);
        }
    }

    bool EntWeather(int num)
    {
        switch(num)
        {
            case 0:
                {
                    if (weatherflg[0])
                    {
                        NowWeather = Weather.RAIN;
                        nowWeathers = PlayerCharacterController.WeatherState.Rain;
                        GameObject we;
                        we = GameObject.Find("PlayerCharacter");
                        we.GetComponent<PlayerCharacterController>().weatherState = nowWeathers;
                        
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            case 1:
                {
                    if (weatherflg[1])
                    {
                        NowWeather = Weather.FOG;
                        nowWeathers = PlayerCharacterController.WeatherState.Fog;
                        GameObject we;
                        we = GameObject.Find("PlayerCharacter");
                        we.GetComponent<PlayerCharacterController>().weatherState = nowWeathers;
                        
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            case 2 :
                {
                    if (weatherflg[2])
                    {
                        NowWeather = Weather.WIND;
                        nowWeathers = PlayerCharacterController.WeatherState.Wind;
                        GameObject we;
                        we = GameObject.Find("PlayerCharacter");
                        we.GetComponent<PlayerCharacterController>().weatherState = nowWeathers;
                        
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            default :
                {
                    return false;
                }
        }
    }
    public Weather GetWeater()
    {
        return NowWeather;
    }
}
