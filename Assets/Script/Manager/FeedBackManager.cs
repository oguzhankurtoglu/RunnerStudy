using System;
using UnityEngine;

namespace Script.Manager
{
    public class FeedBackManager : MonoBehaviour
    {
        public AudioSource audio;
        public float increaseValue;

        private void Awake()
        {
            audio = transform.GetComponent<AudioSource>();
        }

        public void PlaySound(bool isTimingCorrect)
        {
            if (isTimingCorrect)
            {
                audio.pitch += increaseValue;
                audio.Play();
            }
            else
            {
                audio.pitch = 1;
                audio.Play();
            }
        }
    }
}