using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceShooter
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField]
        private string levelName;

        public void StartGame()
        {
            SceneManager.LoadScene(levelName);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
