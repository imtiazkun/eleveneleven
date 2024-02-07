using UnityEngine;
using UnityEngine.Audio;

namespace zeltatech
{
    [System.Serializable]
    public class Sound
    {

        public string name;

        public AudioClip clip;

        [Range(0f, 1f)]
        public float volume;

        [Range(0.1f, 3f)]
        public float pitch; 
        public float roll;

        public bool loop;

        [HideInInspector]
        public AudioSource source;
    }
}
