using UnityEngine;
using System.Collections;

public class CatController : MonoBehaviour
{
    bool isWalking;
    const int WALK_COUNT_LIMIT = 60;
    int walkCount;

    // Use this for initialization
    void Start()
    {
        if(isWalking)//歩行モーション
        {
            walkCount++;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
