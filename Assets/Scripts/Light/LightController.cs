using UnityEngine;
using UnityEngine.Rendering.Universal; // Light 2D için gerekli

public class LightController : MonoBehaviour
{
    public Light2D playerLight; // Karakterin ýþýðýný baðlamak için
    public float lightDuration; // Iþýk süresi (saniye)
    public float minLightSize; // Iþýðýn minimum boyutu
    public float maxLightSize;  // Iþýðýn maksimum boyutu

    private float currentLightTime;

    void Start()
    {
        currentLightTime = lightDuration; // Baþlangýçta ýþýk tam sürede
    }

    void Update()
    {
        currentLightTime -= Time.deltaTime; // Zamaný azalt

        // Iþýðýn küçülmesini saðla
        float newSize = Mathf.Lerp(minLightSize, maxLightSize, currentLightTime / lightDuration);
        playerLight.pointLightOuterRadius = newSize;

        // Iþýk tamamen bitince kapat
        if (currentLightTime <= 0)
        {
            playerLight.intensity = 2f;
        }

        // Iþýk süresi %20'den az kaldýðýnda yanýp sönme efekti uygula
        if (currentLightTime < lightDuration * 0.2f)
        {
            playerLight.intensity = Mathf.PingPong(Time.time * 1.5f, 1.5f);
        }
        else
        {
            playerLight.intensity = 1f;
        }
    }

    // Iþýðý doldurmak için çaðrýlacak metod
    public void RefillLight()
    {
        currentLightTime = lightDuration;
        playerLight.enabled = true;
    }
}
