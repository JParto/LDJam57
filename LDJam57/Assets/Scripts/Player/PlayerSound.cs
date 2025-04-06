using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    public AudioCuePlayer audioCuePlayer;
    private ParallaxManager parallaxManager => ParallaxManager.instance;
    private ParallaxState state => parallaxManager.currentState;

    public void PlayFootstepSound()
    {
        if (audioCuePlayer != null)
        {
            switch (state)
            {
                case ParallaxState.ForeGround:
                    audioCuePlayer.PlaySound("Footstep_Fore");
                    break;
                case ParallaxState.MidGround:
                    audioCuePlayer.PlaySound("Footstep_Mid");
                    break;
                case ParallaxState.BackGround:
                    audioCuePlayer.PlaySound("Footstep_Back");
                    break;
                default:
                    break;
            }
        }
    }
}