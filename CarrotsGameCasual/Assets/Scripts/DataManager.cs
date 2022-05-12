using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{

    private readonly int totalMap = 5;
    /// <summary>
    /// Singleton 
    /// </summary>
    public static DataManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    ///<sumary>
    ///Lưu star,score,map vào PlayerPref
    ///</sumary>
    ///<param name="level">Level</param>
    ///<param name="star">Star</param>
    ///<param name="score">Score</param>
    public void SaveData(int level, int star, int score)
    {
        PlayerPrefs.SetInt("StarOfMap" + level, star);
        PlayerPrefs.SetInt("ScoreOfMap" + level, score);
        if (star > 0 || level <= 5)
        {
            //open next map
            PlayerPrefs.SetInt("Map" + level, 1);
        }
        PlayerPrefs.Save();

        
    }

    /// <summary>
    /// Get toàn bộ dữ liệu của 5 map
    /// </summary>
    /// <returns>List Map</returns>
    public List<Map> GetAll()
    {
        List<Map> maps = new List<Map>();
        Map map = new Map();
        for (int i = 0; i < totalMap; i++)
        {
            map.level = PlayerPrefs.GetInt("Map" + (i + 1), 0);
            map.star = PlayerPrefs.GetInt("StarOfMap" + (i + 1), 0);
            map.score = PlayerPrefs.GetInt("ScoreOfMap" + (i + 1), 0);
            maps.Add(map);
        }
        PlayerPrefs.Save();
        return maps;
    }
}
public class Map
{
    public int level;
    public int star;
    public int score;
}
