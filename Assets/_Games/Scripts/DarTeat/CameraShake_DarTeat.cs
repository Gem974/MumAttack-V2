using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake_DarTeat : MonoBehaviour
{

    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    public Transform camTransform;

    // How long the object should shake for.
    public float shakeDuration = 0f;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.05f;
    public float decreaseFactor = 1.0f;

    public bool shaketrue = false;
    Vector3 originalPos;

    public static CameraShake_DarTeat instance;

    void Awake()
    {
        shaketrue = true;

        if (instance != null)
            Debug.Log("Il y a plus d'une instance dans la scène");

        instance = this;

        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    void OnEnable()
    {
        originalPos = camTransform.localPosition;
    }

    void Update()
    {
        if (shaketrue)
        {
            if (shakeDuration > 0)
            {
                camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

                shakeDuration -= Time.deltaTime * decreaseFactor;
            }
            else
            {
                shakeDuration = .25f;
                camTransform.localPosition = originalPos;
                shaketrue = false;
            }
        }
    }

    public void ShakeCamera()
    {
        shaketrue = true;
    }
}