using UnityEngine;
using System.Collections;
<<<<<<< HEAD
=======
using UnityEngine.UI;
>>>>>>> 5e03151d84bbdbae28a1986085c13fbe5f72fb80

public class MiniMap : MonoBehaviour {

    [SerializeField]
<<<<<<< HEAD
    GameObject player;
    GameObject playerIcon;
	// Use this for initialization
	void Start () {
        playerIcon = GameObject.Find("MiniMapPlayerIcon");
	}
	
	// Update is called once per frame
	void Update () {
        if (player && playerIcon)
        {
            playerIcon.transform.position = new Vector3(player.transform.position.x, -10, player.transform.position.z);
        }
=======
    WeatherSystem m_wStytem;


	[SerializeField]
	Image m_mistFrame; 
	void Start () {
		m_wStytem = GameObject.Find("WeatherSystem").GetComponent<WeatherSystem>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (m_wStytem.NowWeather == Weather.FOG)
		{
			m_mistFrame.enabled = true;
		}
		else
		{

			m_mistFrame.enabled = false;
		}
>>>>>>> 5e03151d84bbdbae28a1986085c13fbe5f72fb80

    }
}
