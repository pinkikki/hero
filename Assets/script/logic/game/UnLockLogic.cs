using System.Collections.Generic;
using System.Linq;
using script.core.@event;
using script.core.scene;
using UnityEngine;
using UnityEngine.UI;

public class UnLockLogic : MonoBehaviour
{
    Dictionary<int, GameObject> leftObjDic;
    Dictionary<int, GameObject> rightObjDic;
    string actual;
    static readonly string firstExpected = "1234";
    static readonly string secondExpected = "9876";
    List<int> selectList = new List<int>();


    void Start()
    {
        leftObjDic = Enumerable.Range(1, 5).ToDictionary(i => i,
            i => transform.Find("KeyButtons/LeftButtons/KeyButton" + i).gameObject);
        rightObjDic = Enumerable.Range(6, 5).ToDictionary(i => i == 10 ? 0 : i,
            i =>
            {
                var index = i == 10 ? 0 : i;
                return transform.Find("KeyButtons/RightButtons/KeyButton" + index).gameObject;
            });
    }

    void Update()
    {
    }

    public void Click(int num)
    {
        if (selectList.Contains(num))
        {
            return;
        }
        
        var selectObj = leftObjDic.ContainsKey(num) ? leftObjDic[num] : rightObjDic[num];
        var button = selectObj.GetComponent<Button>();
        var colors = button.colors;
        colors.normalColor = Color.yellow;
        colors.highlightedColor = Color.yellow;
        colors.pressedColor = Color.yellow;
        button.colors = colors;

        selectList.Add(num);
        if (selectList.Count <= 4)
        {
            actual += num;
            if (selectList.Count == 4)
            {
                Release();
            }
        }
    }

    void Release()
    {
        if (!SceneStatus.IsFinishedFirstUnLocking)
        {
            if (actual == firstExpected)
            {
                EventManager.Instance.Register(806);
                SceneStatus.IsFinishedFirstUnLocking = true;
            }
            else if (actual == secondExpected)
            {
                EventManager.Instance.Register(807);
                SceneStatus.IsFinishedSecondUnLocking = true;
                SceneLoadManager.Instance.LoadLevelInLoading(1.0f, "chickenroom", null);
                return;
            }
            else
            {
                EventManager.Instance.Register(808);
            }
        }
        else
        {
            if (actual == secondExpected)
            {
                EventManager.Instance.Register(807);
                SceneStatus.IsFinishedSecondUnLocking = true;
                SceneLoadManager.Instance.LoadLevelInLoading(1.0f, "chickenroom", null);
                return;
            }
            else
            {
                EventManager.Instance.Register(809);
            }
        }
        Destroy(gameObject);
    }
}