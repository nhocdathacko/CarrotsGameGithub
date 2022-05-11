using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Maths : MonoBehaviour
{
    [SerializeField] private Caculatior[] caculatiors;
    [SerializeField] private GameObject horizontalAnswer;
    [SerializeField] private Transform parent;
    [SerializeField] private Transform posBoardQuestion;
    [SerializeField] private Text txtQuestion;
    [SerializeField] private float time;

    private float curTime;
    private int amoutQuestion;
    private int curIndexQuestion;
    // Start is called before the first frame update
    void Start()
    {
        amoutQuestion = caculatiors.Length;
        curIndexQuestion = 0;
        curTime = time;
    }

    // Update is called once per frame
    void Update()
    {
        curTime -= Time.deltaTime;
        if(curTime <= 0)
        {
            curTime = time;
            NextQuestion(); 
        }
    }
    private void NextQuestion()
    {
        if(curIndexQuestion == amoutQuestion)
        {
            curIndexQuestion = 0;
        }
        Caculatior caculatior = caculatiors[curIndexQuestion];
        txtQuestion.text = caculatior.numberOne + caculatior.operation + caculatior.numberTwo + "=?";
        Instantiate(horizontalAnswer, posBoardQuestion.position, Quaternion.identity, parent).GetComponent<ValueManager>().
            Init(caculatior.rightAnswer,new int[] { caculatior.answerA, caculatior.answerB, caculatior.answerC });
        Instantiate(posBoardQuestion.gameObject, posBoardQuestion.position, Quaternion.identity, parent);
        curIndexQuestion++;
    }
    [System.Serializable]
    class Caculatior
    {
        public int numberOne;
        public string operation;
        public int numberTwo;
        public int rightAnswer;
        public int answerA;
        public int answerB;
        public int answerC;
    }
}
