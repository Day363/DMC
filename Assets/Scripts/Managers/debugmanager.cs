using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class debugmanager : MonoBehaviour
{

    public TMP_Dropdown dropdown;
    public debugmanager Instance;

    public void Awake()
    {
        Instance = this;
    }

    public void OnSceneButtonClick()
    {
        int index = dropdown.value;
        string value = dropdown.options[index].text;

        StartCoroutine(ExecuteScene(value));
    }

    IEnumerator ExecuteScene(string value)
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(value, LoadSceneMode.Additive);
        yield return loadOperation;

        Scene newScene = SceneManager.GetSceneByName(value);
        SceneManager.SetActiveScene(newScene);

        uimanager.Instance.ResetUi();

        yield return SceneManager.UnloadSceneAsync(currentSceneName);
    }
}
