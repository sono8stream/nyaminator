using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CommandController : MonoBehaviour {

    Vector2 touchDownPos, touchUpPos;
    bool isPushing;
    bool isShooting;
    [SerializeField]
    StatusController myStatus;
    [SerializeField]
    GameObject catBullet;
    [SerializeField]
    GameObject catSet;
    List<GameObject> catSets;

    // Use this for initialization
    void Start()
    {
        int catCount = myStatus.civilization / 7;
        catSets = new List<GameObject>();
        for (int i = 0; i < catCount; i++)
        {
            catSets.Add(Instantiate(catSet));
            catSets[i].transform.position = new Vector3(3 - i * 2.5f, 1 + Random.value);
            catSets[i].transform.FindChild("猫").GetComponent<Animator>().SetBool("walking", true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0) && isPushing&&!isShooting)
        {
            touchUpPos = Input.mousePosition;
            Debug.Log(touchUpPos);
            if (isPushing)
            {
                SetAngle();
                isShooting = true;
            }
            isPushing = false;
        }
        if(isShooting)
        {

        }
    }

    //猫ボックスobjectのbuttonコンポーネントで処理
    public void GetPosition()
    {
        touchDownPos = Input.mousePosition;
        Debug.Log(touchDownPos);
        isPushing = true;
    }

    void SetAngle()
    {
        Vector2 vector = touchUpPos - touchDownPos;
        float angle = Mathf.Atan2(vector.y, vector.x);
        angle *= 180 / Mathf.PI;
        Debug.Log(angle);
        if (0 < angle && angle < 60)//議論選択時
        {
            myStatus.cooperation += 10;
            //myStatus.power -= 4;
            myStatus.civilization -= 4;
            angle = 30;
        }
        else if (60 <= angle && angle < 120)//探索選択
        {
            myStatus.civilization += 3;
            angle = 90;
        }
        else if (120 <= angle && angle < 180)//闘争選択
        {
            myStatus.power += 7;
            myStatus.civilization += 6;
            myStatus.cooperation -= 15;
            angle = 150;
        }
        StartCoroutine(Shoot(angle));
    }

    void SelectCommand()
    {
        Vector2 vector = touchUpPos - touchDownPos;
        float angle = Mathf.Atan2(vector.y, vector.x);
        angle *= 180 / Mathf.PI;
        Debug.Log(angle);
        if (0 < angle && angle < 60)//議論選択時
        {
            myStatus.cooperation += 10;
            //myStatus.power -= 4;
            myStatus.civilization -= 4;
        }
        else if (60 <= angle && angle < 120)//探索選択
        {
            myStatus.civilization += 3;
        }
        else if (120 <= angle && angle < 180)//闘争選択
        {
            myStatus.power += 7;
            myStatus.civilization += 6;
            myStatus.cooperation -= 15;
        }
        ChangeGauge();
    }

    void ChangeGauge()
    {
        float height = 60;
        myStatus.transform.FindChild("Potential")
            .transform.FindChild("gauge").GetComponent<RectTransform>().sizeDelta=new Vector2(myStatus.power * 2,height);
        myStatus.transform.FindChild("Civilization")
            .transform.FindChild("gauge").GetComponent<RectTransform>().sizeDelta = new Vector2(myStatus.civilization * 2, height);
        myStatus.transform.FindChild("Cooperation")
            .transform.FindChild("gauge").GetComponent<RectTransform>().sizeDelta = new Vector2(myStatus.cooperation * 2, height);
    }

    IEnumerator Shoot(float angle)
    {
        catBullet.GetComponent<Image>().enabled = true;
        float speed = 20;
        angle *= Mathf.PI / 180;
        Vector3 vector = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle))*speed;
        int count = 0;
        int length = 500;
        while (speed * count < length)
        {
            catBullet.transform.position += vector;
            count++;
            Debug.Log(count);
            yield return null;
        }
        ChangeGauge();
        isShooting = false;
        catBullet.GetComponent<Image>().enabled = false;
        catBullet.transform.position = transform.position;
    }
}
