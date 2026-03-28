using UnityEngine;
using Unity.Cinemachine;

public class Camera : MonoBehaviour
{
    private CinemachineCamera camera;
    private float shakeStartTime;
   
    void Start()
    {
        camera = this.gameObject.GetComponent<CinemachineCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shakeStartTime > 0)
        {
            shakeStartTime -= Time.deltaTime;
            if (shakeStartTime <= 0)
            {
                CinemachineBasicMultiChannelPerlin perlin = camera.GetComponent<CinemachineBasicMultiChannelPerlin>();
                perlin.AmplitudeGain = 0;
            }
        }
    }

    public void Shake(float shakeDuration, float intensity)
    {
        shakeStartTime = shakeDuration;
        CinemachineBasicMultiChannelPerlin perlin = camera.GetComponent<CinemachineBasicMultiChannelPerlin>();
        perlin.AmplitudeGain = intensity;
    }
}
