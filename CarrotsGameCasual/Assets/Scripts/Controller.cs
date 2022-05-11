using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    [SerializeField] private GameObject over;

    public static Controller instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }   
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    internal void OverGame()
    {
        over.SetActive(true);
        Time.timeScale = 0f;
    }
   
}

