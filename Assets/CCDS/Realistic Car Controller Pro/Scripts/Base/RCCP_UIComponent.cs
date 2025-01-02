//----------------------------------------------
//        Realistic Car Controller Pro
//
// Copyright � 2014 - 2024 BoneCracker Games
// https://www.bonecrackergames.com
// Ekrem Bugra Ozdoganlar
//
//----------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Base class for all modular components.
/// </summary>
public class RCCP_UIComponent : MonoBehaviour {

    public RCCP_Settings RCCPSettings {

        get {

            if (_RCCPSettings == null)
                _RCCPSettings = RCCP_Settings.Instance;

            return _RCCPSettings;

        }

    }
    private RCCP_Settings _RCCPSettings;

}
