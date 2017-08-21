using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UnLockLogic : MonoBehaviour
{
    Dictionary<int, GameObject> leftObjDic;
    Dictionary<int, GameObject> rightObjDic;
    string actual;
    static readonly string expected = "9999";
    List<int> selectList = new List<int>();


    void Start()
    {
        leftObjDic = Enumerable.Range(1, 5).ToDictionary(i => i,
            i => transform.FindChild("KeyButtons/LeftButtons/KeyButton" + i).gameObject);
        rightObjDic = Enumerable.Range(6, 5).ToDictionary(i => i == 10 ? 0 : i,
            i =>
            {
                var index = i == 10 ? 0 : i;
                return transform.FindChild("KeyButtons/RightButtons/KeyButton" + index).gameObject;
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
        if (actual == expected)
        {
            // TODO イベントが決まったら
//            EventManager.Instance.Register();
        }
        else
        {
            // TODO イベントが決まったら
//            EventManager.Instance.Register();
        }

        Destroy(gameObject);
    }
}