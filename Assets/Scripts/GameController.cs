using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

    float time = 0;
    [SerializeField]
    GameObject timeText;

	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        SetTime();
    }

    void SetTime()
    {
        string t= "猫暦" + ((int)time).ToString() + "年";
        foreach (Transform child in timeText.transform)
        {
            child.GetComponent<Text>().text = t;
        }
    }
}
