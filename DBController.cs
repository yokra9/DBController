using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBController : MonoBehaviour
{
	
	private float _timeleft;
	private DynamicBone[] _dbs;

	[SerializeField, Range(0f, 1f)] public float Damping, Elasticity, Stiffness, Inert;

	// Use this for initialization
	void OnValidate() {
		_dbs = GetComponents<DynamicBone>();
		foreach (var dynamicBone in _dbs)
		{
			dynamicBone.m_Damping = Damping;
			dynamicBone.m_Elasticity = Elasticity;
			dynamicBone.m_Stiffness = Stiffness;
			dynamicBone.m_Inert = Inert;
		}
	}
}
