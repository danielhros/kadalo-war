using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

// Singleton and dontDestroy on load pattern is used because this
// class is used in every scene for changing scenes and other
// parts in game rely on it.
public class LevelManager : MonoBehaviour {

    public static LevelManager Instance;
    
    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    // This method loads new scene..
    // Possibly, here is perfect place to work with loading screen
    // in the future.
    public void LoadScene(string sceneName) {
        SceneManager.LoadSceneAsync(sceneName);
    }
}
