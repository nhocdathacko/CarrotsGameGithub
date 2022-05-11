using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnManager : MonoBehaviour
{

    #region Pool
    [System.Serializable]
    public class ObjectPool
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private Transform parent;

        [SerializeField] private List<GameObject> activeObj;
        [SerializeField] private List<GameObject> inactiveObj;

        public GameObject SpawnObjInPool(Transform _position)
        {
            GameObject gameObj;
            if (inactiveObj.Count > 0)
            {
                //nếu có tồn tại gameObject thì lấy ra một gameobj
                gameObj = inactiveObj[0];
                activeObj.Add(gameObj);
                inactiveObj.Remove(gameObj);
                gameObj.transform.position = _position.position;
                gameObj.SetActive(true);
            }
            else
            {
                //không thì Instantiate
                gameObj = Instantiate(prefab, _position.position, prefab.gameObject.transform.rotation, parent);
                activeObj.Add(gameObj);
            }
            return gameObj;
        }
        public void RemoveObjInPool(GameObject gameObject)
        {
            if (activeObj.Contains(gameObject))
            {
                activeObj.Remove(gameObject);
                inactiveObj.Add(gameObject);
                gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("Not found " + gameObject.name + "Bug logic.Fix Bug!!!");
            }
        }
        public void ClearObjInPool()
        {
            activeObj.Clear();
            inactiveObj.Clear();
        }
    }
    #endregion

    public ObjectPool itemPool;
    public ObjectPool answersPool;

    public static SpawnManager instance;
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
