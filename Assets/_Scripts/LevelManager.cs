using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public static LevelManager Instance;

    [SerializeField] private GameObject _loaderCanvas;

    // Start is called before the first frame update
    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    public async void LoadScene(string sceneName) {

        var scene = SceneManager.LoadSceneAsync(sceneName);

        //scene.allowSceneActivation = false;

        //_loaderCanvas.SetActive(true);


        //do {
        //    await Task.Delay(100);
        //} while (scene.progress < 0.9f);

        //scene.allowSceneActivation = true;

        //_loaderCanvas.SetActive(false);
    }
}
