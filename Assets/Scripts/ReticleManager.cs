using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ReticleManager : MonoBehaviour
{
	public static ReticleManager Instance { get; private set;}

    [Header ("Timer")]
	public Image imgTimer;
	[Range (0f, 5f)] public float timeTotal = 1;
	
	[Header ("Events")]
	public UnityEvent[] timerEvents;
	public int timerEventID;
	
	float timeCurrent;
	bool isEnable;

	private void Awake(){
        Instance = this;
    }

	void Start ()
	{
		Timer_Exit();
	}

	void Update()
	{
		Timer();
	}

	private void Timer()
	{
		if (isEnable)
		{
			timeCurrent += Time.deltaTime;
			imgTimer.fillAmount = timeCurrent / timeTotal;

			if (timeCurrent >= timeTotal)
			{
				timerEvents[timerEventID].Invoke();
				Timer_Exit();
			}
		}
	}

	public void Timer_Enter(int _ID)
	{
		Debug.Log("Timer_Enter");
		isEnable = true;
		timerEventID = _ID;
	}

	public void Timer_Exit()
	{
		isEnable = false;
		imgTimer.fillAmount = 0;
		timeCurrent = 0;
	}
}
