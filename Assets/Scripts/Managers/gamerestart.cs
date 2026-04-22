using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;


public class gamerestart : MonoBehaviour
{
    public bool whilerestarting = false;

    public void Restart()
    {
        if (!whilerestarting)
        {
            Debug.Log("¿ÁΩ√¿€");

            GetComponent<Image>().DOFade(0, 0.2f).SetUpdate(true);
            transform.GetChild(0).GetComponent<TMP_Text>().DOFade(0, 0.2f).SetUpdate(true);

            whilerestarting = true;
            battalemanager.Instance.cronometer.GetComponent<cronometer_script>().RestartTurn();
            StartCoroutine(Restart_co());
        }
        
    }

    IEnumerator Restart_co()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        yield return new WaitForSecondsRealtime(2.5f);

        AsyncOperation loadOperation = SceneManager.LoadSceneAsync("mirrorselect", LoadSceneMode.Additive);
        yield return loadOperation;

        Scene newScene = SceneManager.GetSceneByName("mirrorselect");
        SceneManager.SetActiveScene(newScene);

        battalemanager.Instance.gameObject.GetComponent<PauseManager>().ispause = false;

        uimanager.Instance.ResetUi();

        yield return SceneManager.UnloadSceneAsync(currentSceneName);

        
    }
}
