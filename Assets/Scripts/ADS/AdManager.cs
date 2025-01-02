using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdManager : MonoBehaviour
{
	
	[SerializeField] private AdsInitializer _adsInitializer;
	[SerializeField] private UnityInterstitial _unityInterstitial;
	private static AdManager _instance;
	
	public static AdManager Instance
	{
		get
		{
			if(_instance == null)
			{
				_instance = FindObjectOfType<AdManager>();
			}
			
			if(_instance == null)
			{
				Debug.LogError($"Cant Find {nameof(AdManager)} instance");
			}
			
			return _instance;
		}
	}
	
	private	void Awake()
	{
		//if(_instance != null && _instance != this)
		//{
		//	Destroy(gameObject);
		//}
		//else
		//{
		//	_instance = this;
		//	DontDestroyOnLoad(gameObject);
		//}
		
		_adsInitializer.InitializeAds();
	}
	
	private void OnEnable()
	{
		CCDS_Events.OnMissionCompleted += OnMissionCompleted;
	}
	
	private void OnDisable()
	{
		CCDS_Events.OnMissionCompleted -= OnMissionCompleted;
	}
	
	private void OnMissionCompleted()
	{
		StartCoroutine(ShowInterstitialWithDelay(1.5f));
	}
	
	public void ShowInterstitial()
	{
		_unityInterstitial.ShowAd();
	}
	
	private IEnumerator ShowInterstitialWithDelay(float delay)
	{
		yield return new WaitForSeconds(delay);
		
		ShowInterstitial();
	}
}
