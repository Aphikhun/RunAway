using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Animator transition;

    public void LoadNextLevel()
    {
        StartCoroutine(LevelLoad(SceneManager.GetActiveScene().buildIndex + 1));
    }
    public void LoadSameLevel()
    {
        //StartCoroutine(LevelLoad(SceneManager.GetActiveScene().buildIndex));
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator LevelLoad(int index)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(index);
    }
}
