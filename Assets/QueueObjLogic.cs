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
    [SerializeField] Sprite imageA, imageB;

    private void Awake()
    {
        counter = GameObject.Find("UIManager").GetComponent<UIManager>();
    }
    private void Start()
    {
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
                    panelTimer.sprite = imageA;

                }
                else if (teamB) 
                {
                    counter.DeValueCounterB();
                    panelTimer.sprite = imageB;
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
                if (teamA)
                {
                    panelTimer.sprite = imageA;
                }
                if (teamB)
                {
                    panelTimer.sprite = imageB;
                }
                canStart = true;
                break;      
                
            case 1:
                sprite.sprite = quedeData[1].spriteImg;
                timeToDissapear = quedeData[1].Timer;
                normalizedValue = timeToDissapear;
                if (teamA)
                {
                    panelTimer.sprite = imageA;
                }
                if (teamB)
                {
                    panelTimer.sprite = imageB;
                }
                canStart = true;
                break;    
                
            case 2:
                sprite.sprite = quedeData[2].spriteImg;
                timeToDissapear = quedeData[2].Timer;
                normalizedValue = timeToDissapear;
                if (teamA)
                {
                    panelTimer.sprite = imageA;
                }
                if (teamB)
                {
                    panelTimer.sprite = imageB;
                }
                canStart = true;
                break;

            case 3:
                sprite.sprite = quedeData[3].spriteImg;
                timeToDissapear = quedeData[3].Timer;
                normalizedValue = timeToDissapear;
                if (teamA)
                {
                    panelTimer.sprite = imageA;
                }
                if (teamB)
                {
                    panelTimer.sprite = imageB;
                }
                canStart = true;
                break;
            case 4:
                sprite.sprite = quedeData[4].spriteImg;
                timeToDissapear = quedeData[4].Timer;
                normalizedValue = timeToDissapear;
                if (teamA)
                {
                    panelTimer.sprite = imageA;
                }
                if (teamB)
                {
                    panelTimer.sprite = imageB;
                }
                canStart = true;
                break;
            case 5:
                sprite.sprite = quedeData[5].spriteImg;
                timeToDissapear = quedeData[5].Timer;
                normalizedValue = timeToDissapear;
                if (teamA)
                {
                    panelTimer.sprite = imageA;
                }
                if (teamB)
                {
                    panelTimer.sprite = imageB;
                }
                canStart = true;
                break;
            case 6:
                sprite.sprite = quedeData[6].spriteImg;
                timeToDissapear = quedeData[6].Timer;
                normalizedValue = timeToDissapear;
                if (teamA)
                {
                    panelTimer.sprite = imageA;
                }
                if (teamB)
                {
                    panelTimer.sprite = imageB;
                }
                canStart = true;
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
