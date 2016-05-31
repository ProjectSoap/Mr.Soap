using UnityEngine;
using System.Collections;

public class DirtyObjectScript : MonoBehaviour
{

    DirtyCreater myCreater;
    public DirtyCreater MyCreater
    {
        get { return myCreater; }
        set { myCreater = value; }
    }
    public AudioClip audioClip;
    AudioSource audioSource;
    // Use this for initialization
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = audioClip;

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Bubble")
        {
            myCreater.NoticeDestroy();
            audioSource.Play();
            Destroy(gameObject);
        }
    }
}
