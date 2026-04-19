using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(eat("사과"));
        Debug.Log(eat("오렌지"));
    }

    string eat(string fruitName)
    {
        string resultString = "";

        string str_0 = "손으로 ";
        string str_1 = " 집는다 ";
        string str1 = string.Concat(str_0, fruitName, str_1);

        string str_2 = " 입에 넣는다.";
        string str2 = string.Concat(fruitName, str_2);

        string str_3 = "입은 ";
        string str_4 = " 씹어 먹는다. ";
        string str3 = string.Concat(str_3, fruitName, str_4);

        resultString = string.Concat(str1, str2, str3);

        return resultString;
    }
}
