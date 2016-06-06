using UnityEngine;
using System.Collections;

public class MiniMap : MonoBehaviour {

    [SerializeField]
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

    }
}
