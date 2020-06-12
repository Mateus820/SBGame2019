using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private void Update() {
        if(Input.GetKey("Escape")){
            QuitGame();
        }    
    }

    public void ChangeScene(string sceneName){
        SceneManager.LoadScene(sceneName);
    }
    
    public void QuitGame(){
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
