using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour
{
    //[SerializeField]
    //Transform m_target;

    //[SerializeField]
    //float m_distance;

    //[SerializeField]
    //float m_height;

    //[SerializeField]
    //float m_smooth;

    //[SerializeField]
    //float m_maxSpeed;

    //Vector3 m_currentVelocity;

    //void Start()
    //{
    //    Debug.Assert(!m_target, "Target Not Set");
    //}

    //void LateUpdate()
    //{
    //    Vector3 targetPosition = m_target.position + ((m_target.forward * -1) * m_distance) + new Vector3(.0f, m_distance, .0f);

    //    transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref m_currentVelocity, m_smooth, m_maxSpeed);

    //    transform.LookAt(m_target, Vector3.up);
    //}

    [SerializeField]
    private float m_distance = 5.0f;

    [SerializeField]
    private float m_followRate = 0.1f;

    [SerializeField]
    private Vector3 m_offset = new Vector3(0f, 3.5f, -5.0f);

    [SerializeField]
    private Vector3 m_lookDown = new Vector3(-5.0f, 0f, 0f);

    [SerializeField]
    Transform target;

    void Start()
    {
        if(target == null)
        {
            target = GameObject.Find("PlayerCharacter").transform;
        }

        transform.position = target.TransformPoint(m_offset);
        transform.LookAt(target, Vector3.up);
    }

    void FixedUpdate()
    {
        Vector3 desiredPosition = target.TransformPoint(m_offset);
        Vector3 lerp = Vector3.Lerp(transform.position, desiredPosition, m_followRate);
        Vector3 toTarget = target.position - lerp;
        toTarget.Normalize();
        toTarget *= m_distance;
        transform.position = target.position - toTarget;
        transform.LookAt(target, Vector3.up);
        transform.Rotate(m_lookDown);
    }
}
