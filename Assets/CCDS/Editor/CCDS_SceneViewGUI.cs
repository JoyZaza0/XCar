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
using UnityEditor;

public class CCDS_SceneViewGUI : EditorWindow {

    public static bool enabled;
    public static bool showButtons;

    static CCDS_MarkerManager markerManager;
    static CCDS_MissionObjectiveManager missionObjectiveManager;
    static CCDS_MissionObjectivePositionsManager missionObjectivePositionsManager;
    static CCDS_CopsManager copsManager;

    [InitializeOnLoadMethod]
    public static void CheckAgain() {

        EditorApplication.delayCall += () => {

            bool enableSceneView = SessionState.GetBool("CCDS_Editor_ShowSceneViewErrors", true);

            if (enableSceneView)
                EnableView();
            else
                DisableView();

        };

    }

    public static void EnableView() {

        SceneView.duringSceneGui += SceneView_duringSceneGui;

    }

    public static void DisableView() {

        SceneView.duringSceneGui -= SceneView_duringSceneGui;

    }

    private static void SceneView_duringSceneGui(SceneView obj) {

        markerManager = CCDS_MarkerManager.Instance;
        missionObjectiveManager = CCDS_MissionObjectiveManager.Instance;
        missionObjectivePositionsManager = CCDS_MissionObjectivePositionsManager.Instance;
        copsManager = CCDS_CopsManager.Instance;

        Handles.BeginGUI();

        float windowWidth = 280f;

        GUILayout.BeginArea(new Rect((windowWidth / 5f), 3f, windowWidth, 55f));

        GUILayout.Label("City Car Driving Simulator", EditorStyles.centeredGreyMiniLabel);
        GUILayout.Label("BoneCracker Games | Ekrem Bugra Ozdoganlar", EditorStyles.centeredGreyMiniLabel);

        GUILayout.EndArea();

        if (markerManager == null && missionObjectiveManager == null && missionObjectivePositionsManager == null && copsManager == null) {

            //GUILayout.Label("CCDS_SceneManager couldn't found", EditorStyles.centeredGreyMiniLabel);

        } else {

            ShowButtons();
            ShowErrors();

        }

        Handles.EndGUI();

    }

    static void ShowButtons() {

        float windowWidth = 280f;

        GUILayout.BeginArea(new Rect((windowWidth / 5f), 35f, windowWidth, 55f));

        Color guiColor = GUI.color;

        EditorGUILayout.BeginHorizontal(GUI.skin.box);

        if (markerManager == null)
            GUI.enabled = false;

        if (GUILayout.Button("Markers"))
            Selection.activeGameObject = markerManager.gameObject;

        GUI.enabled = true;

        if (missionObjectiveManager == null)
            GUI.enabled = false;

        if (GUILayout.Button("Missions"))
            Selection.activeGameObject = missionObjectiveManager.gameObject;

        GUI.enabled = true;

        if (missionObjectivePositionsManager == null)
            GUI.enabled = false;

        if (GUILayout.Button("Locations"))
            Selection.activeGameObject = missionObjectivePositionsManager.gameObject;

        GUI.enabled = true;

        if (copsManager == null)
            GUI.enabled = false;

        if (GUILayout.Button("Cops"))
            Selection.activeGameObject = copsManager.gameObject;

        GUI.enabled = true;

        EditorGUILayout.EndHorizontal();

        GUI.color = guiColor;
        GUILayout.EndArea();

    }

    static void ShowErrors() {

        GUILayout.BeginArea(new Rect(5f, 5f, Screen.width - 10f, Screen.height - 75f));

        Color guiColor = GUI.color;
        GUI.color = Color.red;

        string[] errors = CCDS_SceneChecker.GetErrors();

        if (errors.Length > 0) {

            GUILayout.FlexibleSpace();

            for (int i = 0; i < errors.Length; i++) {

                EditorGUILayout.BeginHorizontal(GUI.skin.box);

                GUILayout.Label(errors[i], EditorStyles.boldLabel);

                if (errors[i].Contains("Marker Manager")) {

                    if (GUILayout.Button("Select Marker Manager")) {

                        EditorApplication.delayCall += () => { Selection.activeGameObject = CCDS_MarkerManager.Instance.gameObject; };

                    }

                }

                if (errors[i].Contains("Mission Manager")) {

                    if (GUILayout.Button("Select Mission Objective Manager")) {

                        EditorApplication.delayCall += () => { Selection.activeGameObject = CCDS_MissionObjectiveManager.Instance.gameObject; };

                    }

                }

                if (errors[i].Contains("Mission Positions Manager")) {

                    if (GUILayout.Button("Select Mission Objective Position Manager")) {

                        EditorApplication.delayCall += () => { Selection.activeGameObject = CCDS_MissionObjectivePositionsManager.Instance.gameObject; };

                    }

                }

                if (errors[i].Contains("Cops Manager")) {

                    if (GUILayout.Button("Select Cops Manager")) {

                        EditorApplication.delayCall += () => { Selection.activeGameObject = CCDS_CopsManager.Instance.gameObject; };

                    }

                }

                if (errors[i].Contains("Spawn Point")) {

                    if (GUILayout.Button("Select Spawn Point")) {

                        EditorApplication.delayCall += () => {

                            Selection.activeGameObject = CCDS_GameplayManager.Instance.spawnPoint.gameObject;
                            SceneView.FrameLastActiveSceneView();

                        };

                    }

                }

                if (errors[i].Contains("Multiple cameras")) {

                    if (GUILayout.Button("Destroy Other Cameras")) {

                        EditorApplication.delayCall += () => { CCDS_SceneChecker.CheckCameras(); };


                    }

                }

                if (errors[i].Contains("Multiple audiolisteners")) {

                    if (GUILayout.Button("Destroy Other AudioListeners")) {

                        EditorApplication.delayCall += () => { CCDS_SceneChecker.CheckAudioListeners(); };

                    }

                }

                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();

            }

        }

        GUI.color = guiColor;
        GUILayout.EndArea();

    }

}
