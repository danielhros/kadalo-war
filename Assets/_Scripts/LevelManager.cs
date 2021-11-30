using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public static LevelManager Instance;

    // Start is called before the first frame update
    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    public void LoadScene(string sceneName) {

        SceneManager.LoadSceneAsync(sceneName);

        // TODO: add loading screen
        //scene.allowSceneActivation = false;
        //_loaderCanvas.SetActive(true);
        //do {
        //} while (scene.progress < 0.9f);
        //scene.allowSceneActivation = true;
        //_loaderCanvas.SetActive(false);
    }
}
