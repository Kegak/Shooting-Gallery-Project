using JetBrains.Annotations;
using UnityEngine;

public class Effect : MonoBehaviour
{

    AudioSource effectAudio;
    GameObject particleEffect;
    float volume = 0.3f;
    
    void Start()
    {
        effectAudio = gameObject.GetComponent<AudioSource>();
    }

    public void Play(RaycastHit hit, AudioClip hitSound, GameObject hitEffect, float effectDuration)
    {
        if (hitEffect != null)
        {
            particleEffect = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(particleEffect, effectDuration);
        }

        effectAudio.PlayOneShot(hitSound, volume);
    }
}
