using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MTAssets.EasyMinimapSystem;

public class MinimapController : MonoBehaviour
{
    [SerializeField] private MinimapRenderer _minimapRenderer;

    private void OnEnable()
    {
        RCCP_Events.OnRCCPSpawned += OnRCCPSpawn;
    }


    private void OnDisable()
    {
        RCCP_Events.OnRCCPSpawned -= OnRCCPSpawn;
    }

    private void Start()
    {

    }

    private void OnRCCPSpawn(RCCP_CarController rccp)
    {
        Debug.Log(rccp.name);
        MinimapCamera minimapCamera = rccp.GetComponent<MinimapCamera>();

        if (minimapCamera != null)
        {
	        _minimapRenderer.minimapCameraToShow = minimapCamera;
            
	        if(!_minimapRenderer.gameObject.activeInHierarchy)
	        	_minimapRenderer.gameObject.SetActive(true);

            //foreach (MinimapItem item in _minimapRenderer.minimapItemsToHightlight)
            //{
            //    item.customGameObjectToFollowRotation = rccp.transform;
            //}
        }


    }
}
