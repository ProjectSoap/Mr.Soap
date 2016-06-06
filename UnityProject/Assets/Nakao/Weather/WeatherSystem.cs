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

    bool[] weatherflg = new bool[3];
    short numWeather;

    [SerializeField]
    List<GameObject> weathers;

	// Use this for initialization
	void Start () {
        numWeather = 2;
        nowWeather = Weather.SUN;
        ActiveWeather();
        minute = 0;
        weatherflg[0] = true;
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
	}

    void ChangeWeather()
    {
        float par;
        if (numWeather == 0)
        {
            nowWeather = Weather.SUN;
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
                    if (weatherflg[0])
                    {
                        nowWeather = Weather.RAIN;
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
                        nowWeather = Weather.FOG;
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
