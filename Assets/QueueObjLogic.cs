using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QueueObjLogic : MonoBehaviour
{
    [SerializeField]
    QueuedObjectData[] quedeData;
    bool canStart;
    [SerializeField]
    Image sprite;
    [SerializeField] float timeToDissapear;
    float normalizedValue;
    [SerializeField] Image panelTimer;
    public bool teamA, teamB;
    [SerializeField]
    UIManager counter;

    private void Awake()
    {
        counter = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canStart) 
        {
            timeToDissapear -= Time.deltaTime;
            if (timeToDissapear <= 0) 
            {
                if (teamA) 
                {
                    counter.DeValueCounterA();
                }
                else if (teamB) 
                {
                    counter.DeValueCounterB();
                }
                Destroy(gameObject);
            }
        }
        panelTimer.fillAmount = timeToDissapear / normalizedValue;
    }
    public void ChangeAppearence(int ToQue) 
    {
        switch (ToQue) 
        {
            case 0:
                sprite.sprite = quedeData[0].spriteImg;
                timeToDissapear = quedeData[0].Timer;
                normalizedValue = timeToDissapear;
                canStart = true;
                //StartCoroutine(Matate());
                break;      
                
            case 1:
                sprite.sprite = quedeData[1].spriteImg;
                timeToDissapear = quedeData[1].Timer;
                normalizedValue = timeToDissapear;
                canStart = true;
                //StartCoroutine(Matate());
                break;    
                
            case 2:
                sprite.sprite = quedeData[2].spriteImg;
                timeToDissapear = quedeData[2].Timer;
                normalizedValue = timeToDissapear;
                canStart = true;
                //StartCoroutine(Matate());
                break;
        }
    }

    IEnumerator Matate() 
    {
        yield return new WaitForSeconds(timeToDissapear);
        Destroy(gameObject);
        yield break;
    }
}
