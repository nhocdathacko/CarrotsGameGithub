using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    enum StateGame
    {
        GameOver, GamePlay, GameWait
    }
    [SerializeField] private Transform[] posSpawnAnswer_Item;
    [SerializeField] private int lowerLimit, highestLimit;

    public Text txtQuestion;
    public Text txtRightAnswer;

    List<int> listParams;
    List<int> paramAfter, opeAfter;
    List<int> paramFull, operationFull;
    List<Group> groups;
    int firstParameter, secondParameter;
    int indexGroup;
    private readonly float time = 5f;
    private float curTime;
    private SpawnManager instanceSM;
    private int rightAnswer;
    private void Awake()
    {
        listParams = new List<int>();
        paramAfter = new List<int>();
        opeAfter = new List<int>();
        paramFull = new List<int>();
        operationFull = new List<int>();
        groups = new List<Group>();
    }
    // Start is called before the first frame update
    void Start()
    {
        curTime = time;
        instanceSM = SpawnManager.instance;
        //HandleQuestion(3);
    }

    // Update is called once per frame
    void Update()
    {
        //curTime -= Time.deltaTime;
        //if (curTime <= 0)
        //{
        //    HandleQuestion();
        //    NextTurn();
        //    curTime = time;
        //}
    }
    public void NextTurn()
    {

        //Initial Question

        //Initial Answer

        //Initial Item
    }
    private void HandleQuestion(int numberOfOperation,int from,int to)
    {
        listParams.Clear(); 
        paramAfter.Clear();
        opeAfter.Clear();
        paramFull.Clear();
        operationFull.Clear();
        groups.Clear();
        indexGroup = -1;
        //for numberOfOperation tìm nhân chia để gộp hoặc tách
        for (int i = 0; i < numberOfOperation; i++)
        {
            int operation = RandomNumber(from, to);
            operationFull.Add(operation);
            //tìm giá trị chung x or / để gộp
            if (i > 0)
            {
                if ((operationFull[i] == 1 || operationFull[i] == 2))
                {
                    //vế riêng (là phép + -)
                    opeAfter.Add(operationFull[i]);
                }
                else if ((operationFull[i] == 3 || operationFull[i] == 4) && (operationFull[i - 1] == 3 || operationFull[i - 1] == 4))
                {
                    //gộp
                    groups[indexGroup].operationsInGroup.Add(operationFull[i]);
                }
                else
                {
                    //tách
                    indexGroup++;
                    Group group = new Group(this);
                    group.operationsInGroup.Add(operationFull[i]);
                    group.indexGroup = i;
                    groups.Add(group);
                }
            }
            else
            {
                //i = 0
                if (operation == 1 || operation == 2)
                {
                    opeAfter.Add(operation);
                }
                else
                {
                    indexGroup++;
                    Group group = new Group(this);
                    group.operationsInGroup.Add(operationFull[i]);
                    group.indexGroup = i;
                    groups.Add(group);
                }
            }
        }
        // đây là trường hợp mà trong 4 phép tính được random ban đầu không có + - chỉ có nhân chia 
        if(opeAfter.Count == 0)
        {
            paramAfter.Add(RandomNumber(lowerLimit, highestLimit));
        }
        else
        {
            paramAfter = HandleLogicOperation(opeAfter, lowerLimit, highestLimit);
        }
        
            bool y_n = false;
        for (int i = 0; i < paramAfter.Count; i++)
        {
            for (int j = 0; j < groups.Count; j++)
            {
                if (groups[j].indexGroup == i)
                {
                    groups[j].GetAnswers(paramAfter[i]);
                    y_n = true;
                    foreach (var item in listParams)
                    {
                        paramFull.Add(item);
                    }
                    break;
                }
            }
            if (!y_n)
                paramFull.Add(paramAfter[i]);
        }
        for (int i = 0; i < paramFull.Count; i++)
        {
            if(i == paramFull.Count - 1)
            {
                ShowQuestionText(paramFull[i], 0);
            }
            else
            {
                ShowQuestionText(paramFull[i], operationFull[i]);
            }
        }
        //xử lý lấy rightansnwer
        for (int i = 0; i < paramFull.Count; i++)
        {
            if (i == 0)
            {
                rightAnswer = paramFull[i];
            }
            else
            {
                rightAnswer = Calculate(rightAnswer, paramFull[i], operationFull[i - 1]);
            }
        }
        ShowRightAnswerText(rightAnswer);

    }
    private int FindOperation(List<int> _operation)
    {
        int index = RandomNumber(1, _operation.Count);
        return _operation[index];
    }
    private void ShowQuestionText(int param, int operation)
    {
        string txtOperation = "";
        switch (operation)
        {
            case 1:
                txtOperation = "+";
                break;
            case 2:
                txtOperation = "-";
                break;
            case 3:
                txtOperation = "x";
                break;
            case 4:
                txtOperation = ":";
                break;
            default:
                txtOperation = "";
                break;
        }
        txtQuestion.text += param.ToString();
        txtQuestion.text += txtOperation;
    }
    private void ShowRightAnswerText(int right)
    {
        txtRightAnswer.text = right.ToString();
    }
    private int RandomNumber(int first, int second)
    {
        return Mathf.RoundToInt(Random.Range((float)first, (float)second));
    }
    private int Calculate(int first, int second, int ope)
    {
        int kq = 0;
        switch (ope)
        {
            // +
            case 1:
                kq = first + second;
                break;
            // -
            case 2:
                kq = first - second;
                break;
            // *
            case 3:
                kq = first * second;
                break;
            // /
            case 4:
                kq = first / second;
                break;
        }
        return kq;
    }
    private List<int> HandleLogicOperation(List<int> listOperation, int lowerLimit, int highestLimit)
    {
        listParams.Clear();
        for (int i = 0; i < listOperation.Count; i++)
        {
            if (i == 0)
            {
                // *
                if (listOperation[i] == 3)
                {
                    //ở trường hợp phép nhân này ta phải random một giá trị nhưng phải xét điều kiện nó có chia hết hay không,không thì lặp cho đến khi tìm được số chia hết
                    do
                    {
                        firstParameter = Mathf.RoundToInt(Random.Range(lowerLimit, highestLimit));
                    } while (highestLimit % firstParameter != 0);
                    listParams.Add(firstParameter);
                    firstParameter = LogicTwoParameters(firstParameter, listOperation[i], lowerLimit, highestLimit);
                }
                // :
                else if (listOperation[i] == 4)
                {
                    //ở trường hợp phép nhân này ta phải random một giá trị nhưng phải xét điều kiện nó có chia hết hay không,không thì lặp cho đến khi tìm được số chia hết
                    do
                    {
                        firstParameter = Mathf.RoundToInt(Random.Range(lowerLimit, highestLimit));
                    } while (firstParameter % lowerLimit != 0);
                    listParams.Add(firstParameter);
                    firstParameter = LogicTwoParameters(firstParameter, listOperation[i], lowerLimit, highestLimit);
                }
                // + or -
                else
                {
                    firstParameter = RandomNumber(lowerLimit, highestLimit);
                    listParams.Add(firstParameter);
                    firstParameter = LogicTwoParameters(firstParameter, listOperation[i], lowerLimit, highestLimit);
                }
            }
            else
            {
                switch (listOperation[i])
                {
                    case 1:
                        firstParameter = LogicTwoParameters(firstParameter, listOperation[i], lowerLimit, highestLimit);
                        break;
                    case 2:
                        firstParameter = LogicTwoParameters(firstParameter, listOperation[i], lowerLimit, highestLimit);
                        break;
                    case 3:
                        firstParameter = LogicTwoParameters(firstParameter, listOperation[i], lowerLimit, highestLimit);
                        break;
                    case 4:
                        firstParameter = LogicTwoParameters(firstParameter, listOperation[i], lowerLimit, highestLimit);
                        break;
                }
            }
        }
        return listParams;
    }
    //function này xử lý tìm tham số thứ 2 từ 1 vd:first (operation) second = lowewrLimit or highestLimit
    //add tham số thứ 2 vào listParams trả về tham số 1
    private int LogicTwoParameters(int first, int operation, int lowerLimit, int highestLimit)
    {
        int second = 0;
        switch (operation)
        {
            case 1:
                second = RandomNumber(lowerLimit, highestLimit - first);
                break;
            case 2:
                second = RandomNumber(lowerLimit, first - lowerLimit);
                break;
            case 3:
                second = Mathf.RoundToInt(Random.Range(lowerLimit, highestLimit / first));
                break;
            case 4:
                do
                {
                    second = Mathf.RoundToInt(Random.Range(lowerLimit, first / lowerLimit));
                } while (first % second != 0);
                break;
        }
        listParams.Add(second);
        firstParameter = Calculate(first, second, operation);
        return firstParameter;
    }

    /// <summary>
    /// Trạng thái game
    /// </summary>
    /// <param name="state"></param>
    private void SetState(StateGame state)
    {
        switch (state)
        {
            case StateGame.GamePlay:
                //Màn hình TabToPlay sẽ invisible
                Time.timeScale = 1f;
                break;
            case StateGame.GameWait:
                //Màn hình TabToPlay sẽ visible
                Time.timeScale = 0f;
                break;
            case StateGame.GameOver:
                Time.timeScale = 0f;
                break;
        }
    }

    class Group
    {
        public List<int> operationsInGroup = new List<int>();
        public int indexGroup;

        private int lowerLimit;
        private GameManager gameManager;

        public Group(GameManager gameManager)
        {
            this.gameManager = gameManager;
            lowerLimit = this.gameManager.lowerLimit;
        }

        public void GetAnswers(int resultGroup)
        {
            gameManager.HandleLogicOperation(operationsInGroup, lowerLimit, resultGroup);
        }
    }
}
