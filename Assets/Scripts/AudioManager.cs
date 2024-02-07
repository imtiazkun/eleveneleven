using System;
using UnityEngine;
using UnityEngine.Audio;

namespace zeltatech
{

    public class AudioManager : MonoBehaviour
    {

        public Sound[] sounds;


        public static AudioManager instance;

        // Start is called before the first frame update
        void Awake()
        {

            instance = this;

            foreach (Sound s in sounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;
            }
        }

        public void Play(string name)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);

            if (s == null)
            {
                Debug.LogError($"{name} sfx not found.");
                return;
            }
            s.source.Play();
        }
    }

}