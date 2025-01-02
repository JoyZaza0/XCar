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
using UnityEngine;

/// <summary>
/// Mission marker. It's basically a trigger collider. Once player triggers it, gameplay manager script will be used (CCDS_GameplayManager.Instance.EnteredMarker(this)).
/// </summary>
[AddComponentMenu("BoneCracker Games/CCDS/Missions/CCDS Marker")]
public class CCDS_Marker : ACCDS_Component {

    public ACCDS_Mission connectedMission;

    /// <summary>
    /// UI canvas for camera rotation.
    /// </summary>
    [Space()] public Transform UI;

    private void Update() {

        //  Set rotation of the UI canvas.
        if (UI && Camera.main)
            UI.rotation = Camera.main.transform.rotation;

    }

    /// <summary>
    /// On trigger.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other) {

        //  Return if gameplay manager not found.
        if (!CCDS_GameplayManager.Instance) {

            Debug.LogError("CCDS_GameplayManager couldn't found, can't start the mission! Create CCDS_SceneManager and check the scene setup. Tools --> BCG --> CCDS --> Create --> Scene Managers --> Gameplay --> CCDS Scene Manager");
            return;

        }

        //  Finding the player vehicle.
        CCDS_Player player = CCDS_GameplayManager.Instance.player;

        //  Return if player not found.
        if (!player) {

            Debug.LogError("Couldn't found the player vehicle!");
            return;

        }

        CCDS_Player triggeredPlayer = other.GetComponentInParent<CCDS_Player>();

        if (!triggeredPlayer)
            return;

        //  If triggered vehicle and local player vehicle is the same, load the main menu.
        if (!Equals(player.gameObject, triggeredPlayer.gameObject))
            return;

        //  Calling ''EnteredMarker'' on the gameplay manager to initialize and start the mission.
        CCDS_GameplayManager.Instance.EnteredMarker(this);

    }

}
