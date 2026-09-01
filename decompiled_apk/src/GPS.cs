using System.Collections;
using UnityEngine;

public class GPS : MonoBehaviour
{
	public static string Latitude = string.Empty;

	public static string Longitude = string.Empty;

	private void Start()
	{
		((MonoBehaviour)this).StartCoroutine(StartLocationService());
	}

	private void Update()
	{
	}

	private IEnumerator StartLocationService()
	{
		if (Input.location.isEnabledByUser)
		{
			Input.location.Start(1f, 1f);
			int maxWait = 20;
			while ((int)Input.location.status == 1 && maxWait > 0)
			{
				yield return (object)new WaitForSeconds(1f);
				maxWait--;
			}
			if (maxWait > 0 && (int)Input.location.status != 3)
			{
				LocationInfo lastData = Input.location.lastData;
				Latitude = ((LocationInfo)(ref lastData)).latitude + string.Empty;
				lastData = Input.location.lastData;
				Longitude = ((LocationInfo)(ref lastData)).longitude + string.Empty;
				((MonoBehaviour)this).StartCoroutine(TrackLocation());
			}
		}
	}

	private IEnumerator TrackLocation()
	{
		while (true)
		{
			yield return (object)new WaitForSeconds(5f);
			if ((int)Input.location.status == 2)
			{
				LocationInfo lastData = Input.location.lastData;
				Latitude = ((LocationInfo)(ref lastData)).latitude + string.Empty;
				lastData = Input.location.lastData;
				Longitude = ((LocationInfo)(ref lastData)).longitude + string.Empty;
			}
		}
	}

	private void OnDestroy()
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Invalid comparison between Unknown and I4
		((MonoBehaviour)this).StopAllCoroutines();
		if ((int)Input.location.status == 2)
		{
			Input.location.Stop();
		}
	}
}
