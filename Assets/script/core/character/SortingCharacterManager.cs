using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace script.core.character
{
    public class SortingCharacterManager : MonoBehaviour
    {
        [SerializeField]
        List<GameObject> playerList;
        Dictionary<String, int> defaultPlayerNameDic = new Dictionary<String, int>();

        void Start()
        {
            for (int i = 0; i < playerList.Count; i++)
            {
                defaultPlayerNameDic[playerList[i].name] = i;
            }
            foreach (var playerObj in playerList.Select((value, index) => new {value, index}))
            {
                defaultPlayerNameDic[playerObj.value.name] = playerObj.index;
            }
        }

        void Update()
        {
            playerList.Sort((obj1, obj2) =>
            {
                float obj1PosY = obj1.transform.position.y;
                float obj2PosY = obj2.transform.position.y;
                int result = obj2PosY.CompareTo(obj1PosY);
                if (result == 0)
                {
                    result = defaultPlayerNameDic[obj2.name].CompareTo(defaultPlayerNameDic[obj1.name]);
                }
                return result;
            });
            foreach (var playerObj in playerList.Select((value, index) => new {value, index}))
            {
                playerObj.value.GetComponent<Renderer>().sortingOrder = playerObj.index;
            }
        }
    }
}
