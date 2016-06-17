using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Weather
{
    SUN,
    RAIN,
    FOG,
    WINDRIGHT,
    WINDLEFT
}


public class WeatherSystem : MonoBehaviour {
    [SerializeField]
    float time;
    int minute;
    [SerializeField]
    Weather nowWeather;

    PlayerCharacterController.WeatherState nowWeathers;

    bool[] weatherflg = new bool[3];
    short numWeather;

    [SerializeField]
    List<GameObject> weathers;

	PlayerCharacterController m_player;

	// Use this for initialization
	void Start () {
        numWeather = 3;
        nowWeather = Weather.SUN;
        nowWeathers = PlayerCharacterController.WeatherState.Sunny;
        ActiveWeather();
        minute = 0;
        weatherflg[0] = true;
		m_player = GameObject.Find("PlayerCharacter").GetComponent<PlayerCharacterController>();

		LoadSave();
	}

    void LoadSave()
    {
        if(PlayerPrefs.GetInt("RainPlayFlg", -1) == 1)
        {
            weatherflg[1] = true;
			numWeather++;
        }
        if(PlayerPrefs.GetInt("FogPlayFlg", -1) == 1)
        {
            weatherflg[2] = true;
			numWeather++;
		}
        if(PlayerPrefs.GetInt("WindPlayFlg", -1) == 1)
        {
			//weatherflg[3] = true;
			// numWeather++;
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

	}

    void ChangeWeather()
    {
        float par;
        if (numWeather == 0)
        {
            nowWeather = Weather.SUN;
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
                                nowWeather = Weather.SUN;
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
                                nowWeather = Weather.SUN;
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
                                nowWeather = Weather.SUN;
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
                                nowWeather = Weather.SUN;
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
                                nowWeather = Weather.SUN;
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
                                nowWeather = Weather.SUN;
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
                                nowWeather = Weather.SUN;
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
                                nowWeather = Weather.SUN;
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
                            nowWeather = Weather.SUN;
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
                                nowWeather = Weather.SUN;
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
                                nowWeather = Weather.SUN;
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
                                nowWeather = Weather.SUN;
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
        switch(nowWeather)
        {
            case Weather.SUN:
                {
                    for (int i = 0; i < weathers.Count; i++)
                    {
                        weathers[i].active = false;
                    }
                    
                    break;
                }
            case Weather.RAIN:
                {
                    for (int i = 0; i < weathers.Count; i++)
                    {
                        weathers[i].active = false;
                    }
                    weathers[0].active = true;
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
                    break;
                }
            case Weather.WINDLEFT:
                {
                    for (int i = 0; i < weathers.Count; i++)
                    {
                        weathers[i].active = false;
                    }

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
                    if (weatherflg[1])
                    {
                        nowWeather = Weather.RAIN;
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
                    if (weatherflg[2])
                    {
                        nowWeather = Weather.FOG;
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
                        if (Random.Range(0, 2) == 1)
                        {
                            nowWeather = Weather.WINDRIGHT;
                        }
                        else
                        {
                            nowWeather = Weather.WINDLEFT;
                        }
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
        return nowWeather;
    }
}
