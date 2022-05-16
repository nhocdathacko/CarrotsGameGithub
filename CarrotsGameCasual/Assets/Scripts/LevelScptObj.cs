using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "LevelsData", menuName = "ScriptableObject/Create Levels")]
public class LevelScptObj : ScriptableObject
{
    public enum Operation
    {
        Plus = 1,
        SubTract = 2,
        Multiply = 3,
        Divide = 4
    }
    public List<Turn> turns;

    [System.Serializable]
    public class Turn
    {
        public Operation[] operationsSptObj;
        public int numberOfOpeSptObj;
    }
}
