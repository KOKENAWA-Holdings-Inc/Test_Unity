using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    //public GameManager GameManager;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        //Debug.Log("Start SceneÇ™äJénÇµÇ‹ÇµÇΩÅBåªç›ÇÃTime.timeScaleÇÕ: " + Time.timeScale);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartButto()
    {
        Debug.Log("ok");
        SceneManager.LoadScene("Game Main Scene");
        //GameManager.ResumeGame();
    }
}
