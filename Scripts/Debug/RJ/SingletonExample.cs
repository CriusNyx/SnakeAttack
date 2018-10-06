using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Debug.RJ
{
    public class SingletonExample : MonoBehaviour
    {
        private static SingletonExample instance;
        private static SingletonExample Instance
        {
            get
            {
                if(instance == null)
                    instance = new GameObject().AddComponent<SingletonExample>();
                return instance;
            }
        }

        public static Camera gameCamera
        {
            get
            {
                return Instance.camera;
            }
        }

        new private Camera camera;

        private void Awake()
        {
            camera = FindObjectOfType<Camera>();
        }

        public static void DoThing()
        {
            Instance.PrivateDoThing();
        }

        private void PrivateDoThing()
        {

        }

        private void OnDestroy()
        {
            instance = null;
        }
    }
}