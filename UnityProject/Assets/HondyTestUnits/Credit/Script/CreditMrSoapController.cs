using UnityEngine;
using System.Collections;

public class CreditMrSoapController : MonoBehaviour
{

    public enum CreditMrSoapState
    {
        STOP,
        MOVE
    }
    CreditMrSoapState state = CreditMrSoapState.STOP;
    public CreditMrSoapController.CreditMrSoapState State
    {
        get { return state; }
        set { state = value; }
    }
    [SerializeField, Header("移動速度")]
    float m_velocity = 0.05f;
    Animator m_animator;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case CreditMrSoapState.STOP:
                break;
            case CreditMrSoapState.MOVE:
<<<<<<< HEAD
                transform.position += transform.forward * -m_velocity;
=======
                transform.position += transform.forward * m_velocity;
>>>>>>> 5e03151d84bbdbae28a1986085c13fbe5f72fb80
                break;
            default:
                break;

        }
    }
}
