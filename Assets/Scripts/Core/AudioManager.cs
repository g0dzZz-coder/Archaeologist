using UnityEngine;

namespace Archaeologist.Core
{
    public class AudioManager
    {
        public static float Volume { get; set; } = 1f;

        [RuntimeInitializeOnLoadMethod()]
        private void Init()
        {
            CheckAudio();
        }

        public static void Play(AudioSource source, AudioClip clip, bool loop = false, bool flag = false)
        {
            if (source == null || clip == null)
                return;

            source.volume = Volume;
            source.clip = clip;
            source.loop = loop;

            if (flag && source.isPlaying)
                return;

            source.Stop();
            source.Play();
        }

        public static void Stop(AudioSource source)
        {
            if (source == null)
                return;

            source.Stop();
        }

        public static void UpdateVolume(float volume)
        {
            Volume = volume;

            PlayerPrefs.SetFloat("volume", Volume);
            PlayerPrefs.Save();
        }

        private void CheckAudio()
        {
            if (PlayerPrefs.HasKey("volume"))
            {
                Volume = PlayerPrefs.GetFloat("volume");
            }
            else
            {
                Volume = 1f;
                PlayerPrefs.SetFloat("volume", Volume);
            }

            PlayerPrefs.Save();
        }
    }
}