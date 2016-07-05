using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
    Transform m_target;

    [SerializeField]
    Material m_alphaMaterial;

    Dictionary<int, Material> m_origineMaterials;

    public float distance
    {
        get { return m_distance; }
        set { m_distance = value; }
    }

    public float followRate
    {
        get { return m_followRate; }
        set { m_followRate = value; }
    }

    public Vector3 offset
    {
        get { return m_offset; }
        set { m_offset = value; }
    }

    public Vector3 lookDown
    {
        get { return m_lookDown; }
        set { m_lookDown = value; }
    }

    public Transform target
    {
        get { return m_target; }
        set { m_target = value; }
    }

    void Start()
    {
        m_origineMaterials = new Dictionary<int, Material>();

        if(m_target == null)
        {
            m_target = GameObject.Find("PlayerCharacter").transform;
        }

        transform.position = m_target.TransformPoint(m_offset);
        transform.LookAt(m_target, Vector3.up);
    }

    void FixedUpdate()
    {
        Vector3 desiredPosition = m_target.TransformPoint(m_offset);
        Vector3 lerp = Vector3.Lerp(transform.position, desiredPosition, m_followRate);
        Vector3 toTarget = m_target.position - lerp;
        toTarget.Normalize();
        toTarget *= m_distance;
        transform.position = m_target.position - toTarget;
        transform.LookAt(m_target, Vector3.up);
        transform.Rotate(m_lookDown);
    }

    struct LerpMaterialArgs
    {
        public MeshRenderer meshRenderer;
        public Material origineMaterial;
        public Material targetMaterial;

        public LerpMaterialArgs(MeshRenderer _meshRenderer, Material _origineMaterial, Material _targetMaterial)
        {
            meshRenderer    = _meshRenderer;
            origineMaterial = _origineMaterial;
            targetMaterial  = _targetMaterial;
        }
    }

    IEnumerator LerpMaterial(LerpMaterialArgs args)
    {
        float durationTime = 0.0f;

        while(durationTime < 1.0f)
        {
            args.meshRenderer.material.Lerp(args.origineMaterial, args.targetMaterial, durationTime);

            durationTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        yield return null;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Building"))
        {
            MeshRenderer meshRenderer = collider.gameObject.GetComponent<MeshRenderer>();

            if(meshRenderer != null)
            {
                Material material = meshRenderer.material;

                if (material != null)
                {
                    m_origineMaterials.Add(collider.gameObject.GetInstanceID(), meshRenderer.material);

                    Texture mainTexture = meshRenderer.material.mainTexture;

                    meshRenderer.material = m_alphaMaterial;

                    if(mainTexture != null)
                        meshRenderer.material.mainTexture = mainTexture;

                    //StartCoroutine("LerpMaterial", new LerpMaterialArgs(meshRenderer, meshRenderer.material, m_alphaMaterial));

                    //MaterialLerp materialLerp = collider.gameObject.AddComponent(typeof(MaterialLerp)) as MaterialLerp;

                    //materialLerp.endTime                    = 5.0f;
                    //materialLerp.targetMaterial             = m_alphaMaterial;
                    //materialLerp.targetMaterial.mainTexture = mainTexture;
                }
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Building"))
        {
            MeshRenderer meshRenderer = collider.gameObject.GetComponent<MeshRenderer>();

            if (meshRenderer != null)
            {
                Material material = meshRenderer.material;

                if (material != null)
                {
                    int         instanceID      = collider.gameObject.GetInstanceID();
                    Material    origineMaterial = m_origineMaterials[instanceID];

                    if (origineMaterial != null)
                    {
                        meshRenderer.material = origineMaterial;

                        m_origineMaterials.Remove(instanceID);
                    }
                }
            }
        }
    }
}
