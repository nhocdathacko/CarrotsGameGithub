using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueManager : MonoBehaviour
{
    
    [SerializeField] private float speedFall;

    private Value[] valueChild;
    private void Awake()
    {
        valueChild = GetComponentsInChildren<Value>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(transform.position.x, speedFall * Time.deltaTime, 0f);
        if (transform.position.y <= -6)
        {
            Destroy(gameObject);
        }
    }
    internal void Init(int right, int[] wrong)
    {
        int indexRightAsnwer = Mathf.RoundToInt(Random.Range(0, valueChild.Length - 1));
        int curIndexWrong = 0;
        for (int i = 0; i < valueChild.Length; i++)
        {
            if (i == indexRightAsnwer)
            {
                valueChild[i].Init(right, "Right");
            }
            else
            {
                valueChild[i].Init(wrong[curIndexWrong], "Wrong");
                curIndexWrong++;
            }

        }
    }

}
