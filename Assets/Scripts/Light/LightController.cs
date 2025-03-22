using UnityEngine;
using UnityEngine.Rendering.Universal; // Light 2D i�in gerekli

public class LightController : MonoBehaviour
{
    public Light2D playerLight; // Karakterin �����n� ba�lamak i�in
    public float lightDuration; // I��k s�resi (saniye)
    public float minLightSize; // I����n minimum boyutu
    public float maxLightSize;  // I����n maksimum boyutu

    private float currentLightTime;

    void Start()
    {
        currentLightTime = lightDuration; // Ba�lang��ta ���k tam s�rede
    }

    void Update()
    {
        currentLightTime -= Time.deltaTime; // Zaman� azalt

        // I����n k���lmesini sa�la
        float newSize = Mathf.Lerp(minLightSize, maxLightSize, currentLightTime / lightDuration);
        playerLight.pointLightOuterRadius = newSize;

        // I��k tamamen bitince kapat
        if (currentLightTime <= 0)
        {
            playerLight.intensity = 2f;
        }

        // I��k s�resi %20'den az kald���nda yan�p s�nme efekti uygula
        if (currentLightTime < lightDuration * 0.2f)
        {
            playerLight.intensity = Mathf.PingPong(Time.time * 1.5f, 1.5f);
        }
        else
        {
            playerLight.intensity = 1f;
        }
    }

    // I���� doldurmak i�in �a�r�lacak metod
    public void RefillLight()
    {
        currentLightTime = lightDuration;
        playerLight.enabled = true;
    }
}
