using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{

    private readonly int totalMap = 5;

    public static DataManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    public void SaveData(int level, int star)
    {
        PlayerPrefs.SetInt("StarofMap" + level, star);
        if(star > 0 || level <= 5)
        {
            //open next map
            PlayerPrefs.SetInt("Map" + (level + 1),1);
        }
        PlayerPrefs.Save();
    }
    public List<Map> GetAll()
    {
        List<Map> maps = new List<Map>();
        Map map = new Map();
        for (int i = 0; i < totalMap; i++)
        {
            map.level = PlayerPrefs.GetInt("Map" + (i+1), 0);
            map.star = PlayerPrefs.GetInt("StarofMap" + (i+1), 0);
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
}
