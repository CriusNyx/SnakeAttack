using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour, ICEventHandler
{
    #region Event System Integration
    class SoundEvent : CEvent
    {
        public Vector3 position;
        public string soundName;
        public bool loop = false;

        public SoundEvent(Vector3 position, string soundName, bool loop = false)
        {
            this.position = position;
            this.soundName = soundName;
            this.loop = loop;
        }
    }

    class DestroyWhenDone : MonoBehaviour
    {
        AudioSource audioSource;

        private void Awake()
        {
            audioSource = gameObject.GetComponent<AudioSource>();
        }

        private void Update()
        {
            if(!audioSource.isPlaying)
            {
                Destroy(gameObject);
            }
        }
    }
    #endregion

    #region Singleton Implementation
    private static void EnsureInstance()
    {
        var i = Instance;
    }
    private static SoundController instance;
    private static SoundController Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new GameObject("Sound Controller").AddComponent<SoundController>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        CEventSystem.AddEventHandler(EventChannel.sound, EventSubChannel.none, this);
    }

    private void OnDestroy()
    {
        CEventSystem.RemoveEventHandler(EventChannel.sound, EventSubChannel.none, this);
    }
    #endregion



    public void AcceptEvent(CEvent e)
    {
        try
        {
            SoundEvent s = (SoundEvent)e;

            Vector3 position = s.position;
            string soundName = s.soundName;
            bool loop = s.loop;

            AudioClip clip = Resources.Load<AudioClip>(s.soundName);

            GameObject gameObject = new GameObject(s.soundName);
            gameObject.transform.position = position;

            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.spatialBlend = 1f;
            audioSource.loop = loop;
            audioSource.clip = clip;
            audioSource.pitch = 1f + Random.Range(-0.2f, 0.2f);

            audioSource.Play();

            gameObject.AddComponent<DestroyWhenDone>();
        }
        catch
        {

        }
    }



    public static void PlaySound(Vector3 position, string soundName, bool loop = false)
    {
        EnsureInstance();
        CEventSystem.BroadcastEvent(EventChannel.sound, EventSubChannel.none, new SoundEvent(position, soundName, loop));
    }

    
}