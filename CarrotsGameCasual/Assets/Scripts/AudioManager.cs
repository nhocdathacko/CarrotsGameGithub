using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //music:
    //nhạc trang chủ
    //nhạc 4 con đường chạy trong game
    //sfx:
    //click chọn map,tabtoplay
    //thỏ ăn đúng trái cà rốt
    //thỏ nhảy ngang
    //thỏ nhận item
    //kết thúc game

    /// <summary>
    /// Singleton 
    /// </summary>
    public static AudioManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
