using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform[] posSpawnAnswer_Item;
    [SerializeField] private int lowerLimit, highestLimit;

    public int number;
    private readonly float time = 2f;
    private float curTime;
    private SpawnManager instanceSM;
    private List<int> numbers;
    private string[] numberShowText;
    private string[] operationShowText;
    private List<int> operations;
    private int result;
    private void Awake()
    {
        operations = new List<int>();
        numbers = new List<int>();
    }
    // Start is called before the first frame update
    void Start()
    {
        curTime = time;
        instanceSM = SpawnManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        HandleQuestion();
        curTime -= Time.deltaTime;
        if (curTime <= 0)
        {
            curTime = time;
            NextTurn();
        }
    }
    public void NextTurn()
    {

        //int a = Mathf.RoundToInt(Random.Range(1f, 10f));
        //int b = Mathf.RoundToInt(Random.Range(1f, 10f));
        //int operation = Mathf.RoundToInt(Random.Range(1f, 4f));
        //string s = "5 + 6";
        ////switch (operation)
        ////{
        ////    case 1:
        ////        s = '+';
        ////        break;
        ////    case 2:
        ////        s = '-';
        ////        break;
        ////    case 3:
        ////        s = '*';
        ////        break;
        ////    case 4:
        ////        s = '/';
        ////        break;
        ////}
        //Debug.Log("a: " + a);
        //Debug.Log("b: " + b);
        //Debug.Log("c: " + s);
        //var aa = a;
        //var bb = b;
        //var ss = s;
        //var k = aa + bb;
        //Debug.Log(k);
        //Initial Question

        //Initial Answer
        //for (int i = 0; i < posSpawnAnswer_Item.Length; i++)
        //{
        //    instanceSM.answersPool.SpawnObjInPool(posSpawnAnswer_Item[i]);

        //}
        //Initial Item
    }
    private void HandleQuestion()
    {
        numberShowText = new string[number - 1];
        operationShowText = new string[number - 2];
        //số có thể chia hết
        int divide;
        for (int i = 0; i < number; i++)
        {
            numbers.Add(-1);
        }
        //for cho từng phần tử phép tính
        for (int i = 0; i < number - 1; i++)
        {
            //random tìm phép tính
            int operation = 4;   
            //trường hợp phép * / chia thì phải xử lý trước phép tính + -
            //tìm 2 số thuộc phép tính đó
            switch (operation)
            {
                case 1:
                    break;
                case 2:
                    break;
                //trường hợp *   
                case 3:
                    //ở trường hợp phép nhân này ta phải random một giá trị nhưng phải xét điều kiện nó có chia hết hay không,không thì lặp cho đến khi tìm được số chia hết
                    do
                    {
                        divide = RandomNumber(lowerLimit, highestLimit);
                    } while (highestLimit % divide != 0);
                    //gán giá trị vừa random(giá trị có thể chia hết) cho số trước của phép tính
                    numbers[i] = divide;
                    //tìm số sau của phép tính trên:
                    //tìm ra kết quả của giới hạn cao nhất / số trước: "highestLimit / numbers[i]"
                    //sau đó random giữa giới hạn thấp nhất với kết quả vừa tìm để có được số sau của phép tính trên
                    numbers[i + 1] = RandomNumber(lowerLimit, highestLimit / numbers[i]);
                    //thực hiện phép tính giữa 2 số vừa tìm là trong numbers
                    result = Calculate(numbers[i], numbers[i + 1], operation);
                    break;
                //trường hợp /
                case 4:
                    //ở trường hợp phép chia này ta phải random một giá trị nhưng phải xét điều kiện nó có chia hết hay không,không thì lặp cho đến khi tìm được số chia hết
                    do
                    {
                        divide = RandomNumber(lowerLimit, highestLimit);
                    } while (divide % lowerLimit != 0);
                    //random số trước của phép tính trên 
                    numbers[i] = divide;
                    //tìm số sau của phép tính trên:
                    //tìm ra kết quả của số trước / giới hạn thấp nhất : "numbers[i] / lowerLimit"
                    //sau đó random giữa giới hạn thấp nhất với kết quả vừa tìm để có được số sau của phép tính trên sau đó lặp cho đến khi số trước chia hết số sau
                    do
                    {
                        numbers[i + 1] = RandomNumber(lowerLimit, numbers[i] / lowerLimit);
                    } while (numbers[i] % numbers[i + 1] != 0);
                    //thực hiện phép tính giữa 2 số vừa tìm là trong numbers
                    result = Calculate(numbers[i], numbers[i + 1], operation);
                    break;
            }
        }
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
}
