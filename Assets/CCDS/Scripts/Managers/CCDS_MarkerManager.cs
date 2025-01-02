//----------------------------------------------
//        City Car Driving Simulator
//
// Copyright © 2014 - 2024 BoneCracker Games
// https://www.bonecrackergames.com
// Ekrem Bugra Ozdoganlar
//
//----------------------------------------------

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Marker manager and observer in the scene.
/// </summary>
[AddComponentMenu("BoneCracker Games/CCDS/Managers/CCDS Marker Manager")]
public class CCDS_MarkerManager : ACCDS_Manager {

    private static CCDS_MarkerManager instance;

    /// <summary>
    /// Instance of the class.
    /// </summary>
    public static CCDS_MarkerManager Instance {

        get {

            if (instance == null)
                instance = FindObjectOfType<CCDS_MarkerManager>();

            return instance;

        }

    }

    /// <summary>
    /// Markers.
    /// </summary>
    public List<CCDS_Marker> allMarkers = new List<CCDS_Marker>();

    private void Awake() {

        //  Getting all markers, even if they are disabled.
        GetAllMarkers();

    }

    private void OnEnable() {

        //  Listening events when mission changes or mission complete on enable.
        CCDS_Events.OnMissionChanged += CCDS_Events_OnMissionChanged;
        CCDS_Events.OnMissionCompleted += CCDS_Events_OnMissionCompleted;

    }

    private void OnDisable() {

        //  Unregistering the listeners on disable.
        CCDS_Events.OnMissionChanged -= CCDS_Events_OnMissionChanged;
        CCDS_Events.OnMissionCompleted -= CCDS_Events_OnMissionCompleted;

    }

    /// <summary>
    /// When mission changed.
    /// </summary>
    /// <param name="gameMode"></param>
    private void CCDS_Events_OnMissionChanged() {

        CheckMarkers();

    }

    /// <summary>
    /// When mission completed.
    /// </summary>
    private void CCDS_Events_OnMissionCompleted() {

        CheckMarkers();

    }

    /// <summary>
    /// Gets all markers.
    /// </summary>
    public void GetAllMarkers() {

        //  Checking the list. Creating if it's null.
        if (allMarkers == null)
            allMarkers = new List<CCDS_Marker>();

        //  Clearing the list.
        allMarkers.Clear();

        //  Getting all markers, even if they are disabled.
        allMarkers = GetComponentsInChildren<CCDS_Marker>(true).ToList();

    }

    /// <summary>
    /// Checking the markers. Enabling them if player is not on any mission, disabling them if player is on a mission.
    /// </summary>
    public void CheckMarkers() {

        //  Return if gameplay manager couldn't found.
        if (!CCDS_GameplayManager.Instance)
            return;

        //  Disable all markers on mission. Otherwise, enable all.
        if (!CCDS_GameplayManager.Instance.OnMission)
            ActivateAllMarkers();
        else
            DeactivateAllMarkers();

    }

    /// <summary>
    /// Deactivates all markers.
    /// </summary>
    public void DeactivateAllMarkers() {

        for (int i = 0; i < allMarkers.Count; i++) {

            if (allMarkers[i] != null) {

                if (allMarkers[i].gameObject.activeSelf)
                    allMarkers[i].gameObject.SetActive(false);

            }

        }

    }

    /// <summary>
    /// Enables all markers.
    /// </summary>
    public void ActivateAllMarkers() {

        for (int i = 0; i < allMarkers.Count; i++) {

            if (allMarkers[i] != null) {

                if (!allMarkers[i].gameObject.activeSelf)
                    allMarkers[i].gameObject.SetActive(true);

            }

        }

    }

    /// <summary>
    /// Enables all markers with delay.
    /// </summary>
    public void ActivateAllMarkers(float delay) {

        StartCoroutine(ActivateAllMarkersDelayed(delay));

    }

    /// <summary>
    /// Enables all markers with delay.
    /// </summary>
    public IEnumerator ActivateAllMarkersDelayed(float delay) {

        yield return new WaitForSeconds(delay);

        ActivateAllMarkers();

    }

    /// <summary>
    /// Adds a new marker to the list.
    /// </summary>
    public void AddNewMarker(CCDS_Marker newMarker) {

        allMarkers.Add(newMarker);

    }

    private void Reset() {

        GetAllMarkers();

    }

}
