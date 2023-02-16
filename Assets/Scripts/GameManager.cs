using System;
using Managers.Audio_Manager;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        private void Awake()
        {
            if (instance == null)
                instance = this;

            DontDestroyOnLoad(gameObject);
        }
    }
}