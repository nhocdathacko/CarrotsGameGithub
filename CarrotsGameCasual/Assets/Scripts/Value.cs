using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Value : MonoBehaviour
{
    private int value;
    private string s;
    private Text txtValue;

    private void Awake()
    {
        txtValue = GetComponentInChildren<Text>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        
    }
    internal void Init(int _value,string isWhat)
    {
        s = isWhat;
        value = _value;
        txtValue.text = value + "";
    }
    internal string GetValue()
    {
        return s;
    }
    
}
