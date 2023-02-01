using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MumEscape_CameraShake : MonoBehaviour
{
    private bool isShaking;
    public static MumEscape_CameraShake Instance;
    private Vector3 initialPos;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("One GameManager already exist");
            return;
        }

        Instance = this;
    }

    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.5f);
        initialPos = Camera.main.transform.position;
    }

    public void Shake(float duration, float amount, float intensity)
    {
        if (!isShaking)
            StartCoroutine(ShakeCamera(duration, amount, intensity));
    }

    IEnumerator ShakeCamera(float duration, float amount, float intensity)
    {
        float t = duration;
        Vector3 targetPos = Vector3.zero;

        while(t > 0.0f)
        {
            if(targetPos == Vector3.zero)
            {
                targetPos = initialPos + (Random.insideUnitSphere * amount);
            }

            Camera.main.transform.localPosition = Vector3.Lerp(Camera.main.transform.localPosition, targetPos, intensity * Time.deltaTime );

            if(Vector3.Distance(Camera.main.transform.localPosition, targetPos) < 0.02f)
            {
                targetPos = Vector3.zero;
            }

            t -= Time.deltaTime;
            yield return null;
        }

        Camera.main.transform.localPosition = initialPos;
        isShaking = false;
    }
}
