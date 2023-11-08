using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ReticleManager : MonoBehaviour
{
    [Header ("Timer")]
	public Image imgTimer;
	[Range (0f, 5f)] public float timeTotal = 1;
	
	[Header ("Events")]
	public UnityEvent[] timerEvents;
	
	float timeCurrent;
	bool isEnable;

	void Start ()
	{
		Timer_Exit ();
	}

	void Update ()
	{
		Timer ();
	}

	private void Timer ()
	{
		if (isEnable)
		{
			timeCurrent += Time.deltaTime;
			imgTimer.fillAmount = timeCurrent / timeTotal;

			if (timeCurrent >= timeTotal)
			{
				isEnable = false;
			}
		}
	}

	public void Timer_Enter ()
	{
		isEnable = true;
	}

	public void Timer_Exit ()
	{
		isEnable = false;
		imgTimer.fillAmount = 0;
		timeCurrent = 0;
	}
}
