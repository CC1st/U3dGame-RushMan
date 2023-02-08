using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIControl : MonoBehaviour
{
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    //重开按钮
    public void ButtonReplay()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
    //退出按钮
    public void ButtonQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
    //下一关按钮
    public void ButtonNext()
    {

    }
}
