using UnityEngine;
using System.Collections;

public class BubbleDriftShooter : MonoBehaviour
{
    [SerializeField, Tooltip("撃つ泡")]
    GameObject m_bubble;

    [SerializeField, Tooltip("泡を撃つ数")]
    int m_shotBullet = 3;

    [SerializeField, Tooltip("泡を撃つ間隔")]
    float m_shotIntervelTime = 0.5f;

    [SerializeField, Tooltip("泡を撃つ角度(Y軸)")]
    float m_shotAngle = 90.0f;

    int     m_shotCount = 0;

    void Start()
    {
    }

    IEnumerator ShotCorutine()
    {
        while(true)
        {
            m_shotCount++;

            Debug.Log(transform.rotation.eulerAngles.ToString());
            Instantiate(m_bubble, transform.position, transform.rotation);
            
            if (m_shotCount < m_shotBullet)
                yield return new WaitForSeconds(m_shotIntervelTime);
            else
                break;
        }        
    }

    public void ShotRight()
    {
        transform.rotation = transform.parent.rotation;
        transform.Rotate(.0f, -m_shotAngle, .0f);

        m_shotCount = 0;

        StartCoroutine(ShotCorutine());
    }

    public void ShotLeft()
    {
        transform.rotation = transform.parent.rotation;
        transform.Rotate(.0f, m_shotAngle, .0f);

        m_shotCount = 0;

        StartCoroutine(ShotCorutine());
    }
}
