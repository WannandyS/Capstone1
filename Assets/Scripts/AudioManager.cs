using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource collectDiamondSound;
    public AudioSource attackSound;
    public AudioSource hurtSound;
    public AudioSource jumpSound;
    public AudioSource diedSound;
    public AudioSource collectGemSound;
    public AudioSource collectHPSound;
    public AudioSource projectileSound;

    public void PlayProjectileSound()
    {
        projectileSound.Play();
    }

    public void PlayCollectHPSound()
    {
        collectHPSound.Play();
    }

    public void PlayCollectGemSound()
    {
        collectGemSound.Play();
    }

    public void PlayCollectDiamondSound()
    {
        collectDiamondSound.Play();
    }

    public void PlayDiedSound()
    {
        diedSound.Play();
    }

    public void PlayJumpSound()
    {
        jumpSound.Play();
    }

    public void PlayHurtSound()
    {
        hurtSound.Play();
    }

    public void PlayAttackSound()
    {
        attackSound.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
