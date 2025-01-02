using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenURL : MonoBehaviour
{
	[SerializeField] private string _privacyPolicyURL; // "https://sites.google.com/view/openwoldgames/%D0%B3%D0%BB%D0%B0%D0%B2%D0%BD%D0%B0%D1%8F";
	
	public void Open()
	{
		Application.OpenURL(_privacyPolicyURL);
	}
}
