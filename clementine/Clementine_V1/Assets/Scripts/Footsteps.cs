using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public AudioSource footstepsAudio;
    public float speed = 5f;

    void Update()
    {
        float move = Input.GetAxis("Horizontal");

        if (move != 0)
        {
            if (!footstepsAudio.isPlaying)
            {
                footstepsAudio.Play();
            }
        }
        else
        {
            footstepsAudio.Stop();
        }
    }
}
