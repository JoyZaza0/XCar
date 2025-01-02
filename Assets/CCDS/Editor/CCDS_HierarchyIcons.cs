//----------------------------------------------
//        City Car Driving Simulator
//
// Copyright © 2014 - 2024 BoneCracker Games
// https://www.bonecrackergames.com
// Ekrem Bugra Ozdoganlar
//
//----------------------------------------------

using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[InitializeOnLoad]
static class CCDS_HierarchyIcons {

    static UnityEngine.Object managerTexture;

    static string managerTexturePath;

    // add your components and the associated icons here
    static Dictionary<Type, GUIContent> typeIcons = new Dictionary<Type, GUIContent>() { };

    // cached game object information
    static Dictionary<int, GUIContent> labeledObjects = new Dictionary<int, GUIContent>();
    static HashSet<int> unlabeledObjects = new HashSet<int>();
    static GameObject[] previousSelection = null; // used to update state on deselect

    // set up all callbacks needed
    static CCDS_HierarchyIcons() {

        EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyGUI;

        // callbacks for when we want to update the object GUI state:
        ObjectFactory.componentWasAdded += c => UpdateObject(c.gameObject.GetInstanceID());
        // there's no componentWasRemoved callback, but we can use selection as a proxy:
        Selection.selectionChanged += OnSelectionChanged;

    }

    static void OnHierarchyGUI(int id, Rect rect) {

        if (unlabeledObjects.Contains(id))
            return; // don't draw things with no component of interest

        if (ShouldDrawObject(id, out GUIContent icon)) {

            // GUI code here
            rect.xMin = rect.xMax - 20; // right-align the icon
            GUI.Label(rect, icon);

        }

    }

    static bool ShouldDrawObject(int id, out GUIContent icon) {

        if (labeledObjects.TryGetValue(id, out icon))
            return true;

        // object is unsorted, add it and get icon, if applicable
        return SortObject(id, out icon);

    }

    static bool SortObject(int id, out GUIContent icon) {

        managerTexture = Resources.Load("Editor Icons/CCDS_EditorIcon_Manager");

        if (managerTexture)
            managerTexture.hideFlags = HideFlags.None;

        managerTexturePath = managerTexture != null ? (RCCP_GetAssetPath.GetAssetPath(managerTexture)) : "";

        typeIcons = new Dictionary<Type, GUIContent>() {

        { typeof(CCDS_SceneManager), EditorGUIUtility.IconContent( managerTexturePath ) },
        { typeof(ACCDS_Manager), EditorGUIUtility.IconContent( managerTexturePath ) },
        { typeof(ACCDS_Mission), EditorGUIUtility.IconContent( managerTexturePath ) },
        { typeof(ACCDS_Vehicle), EditorGUIUtility.IconContent( managerTexturePath ) },
        { typeof(CCDS_Camera), EditorGUIUtility.IconContent( managerTexturePath ) },
        { typeof(CCDS_SpawnPoint), EditorGUIUtility.IconContent( managerTexturePath ) },
        { typeof(CCDS_MissionObjectivePosition), EditorGUIUtility.IconContent( managerTexturePath ) },
        { typeof(CCDS_Camera_Orbit), EditorGUIUtility.IconContent( managerTexturePath ) },
        { typeof(CCDS_Marker), EditorGUIUtility.IconContent( managerTexturePath ) },
        { typeof(CCDS_Marker_Garage), EditorGUIUtility.IconContent( managerTexturePath ) },
        { typeof(RTC_SceneManager), EditorGUIUtility.IconContent( managerTexturePath ) },
        { typeof(RTC_TrafficSpawner), EditorGUIUtility.IconContent( managerTexturePath ) },
        { typeof(CCDS_MissionObjective_CheckpointItem), EditorGUIUtility.IconContent( managerTexturePath ) },
        { typeof(CCDS_MissionObjective_TrailblazerItem), EditorGUIUtility.IconContent( managerTexturePath ) },
        { typeof(CCDS_MissionObjective_Race_Finisher), EditorGUIUtility.IconContent( managerTexturePath ) },
        { typeof(CCDS_SoundtrackManager), EditorGUIUtility.IconContent( managerTexturePath ) },
        { typeof(CCDS_MissionObjective_Race_Vehicle), EditorGUIUtility.IconContent( managerTexturePath ) },
        { typeof(CCDS_MissionObjective_Pursuit_Vehicle), EditorGUIUtility.IconContent( managerTexturePath ) },

    };

        GameObject go = EditorUtility.InstanceIDToObject(id) as GameObject;

        if (go != null) {

            foreach ((Type type, GUIContent typeIcon) in typeIcons) {

                if (go.GetComponent(type)) {

                    labeledObjects.Add(id, icon = typeIcon);
                    return true;

                }

            }

        }

        unlabeledObjects.Add(id);
        icon = default;
        return false;

    }

    static void UpdateObject(int id) {

        unlabeledObjects.Remove(id);
        labeledObjects.Remove(id);
        SortObject(id, out _);

    }

    const int MAX_SELECTION_UPDATE_COUNT = 3; // how many objects we want to allow to get updated on select/deselect

    static void OnSelectionChanged() {

        TryUpdateObjects(previousSelection); // update on deselect
        TryUpdateObjects(previousSelection = Selection.gameObjects); // update on select

    }

    static void TryUpdateObjects(GameObject[] objects) {

        if (objects != null && objects.Length > 0 && objects.Length <= MAX_SELECTION_UPDATE_COUNT) { // max of three to prevent performance hitches when selecting many objects

            foreach (GameObject go in objects) {
                UpdateObject(go.GetInstanceID());

            }

        }

    }

}