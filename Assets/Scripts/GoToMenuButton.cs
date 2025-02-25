using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToMenuButton : MonoBehaviour
{
    public void Tap()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
