using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DBController : MonoBehaviour
{
	public Transform[] Bones;
	[Range(0f, 1f)] public float Damping, Elasticity, Stiffness, Inert;
	public float UpdateRate, Radius;
	public AnimationCurve DanpingDistrib, ElasticityDistrib, StiffnessDistrib, InertDistrib, RadiusDistrib;
	public bool UseColliderSettings;
	public enum FreezeAxis
	{
		None, X, Y, Z
	}
	public FreezeAxis m_FreezeAxis = FreezeAxis.None;
	public List<DynamicBoneColliderBase> Colliders;
	public enum UpdateMode
	{
		Normal,
		AnimatePhysics,
		UnscaledTime,
		Default
	}
	public UpdateMode m_UpdateMode = UpdateMode.Default;
	
	// Use this for initialization
	void Reset()
	{
		var db = GetComponent<DynamicBone>();
		if (!db) return;
		UpdateRate = db.m_UpdateRate;
		Damping = db.m_Damping;
		Elasticity = db.m_Elasticity;
		Stiffness = db.m_Stiffness;
		Inert = db.m_Inert;
		Radius = db.m_Radius;
		DanpingDistrib = db.m_DampingDistrib;
		ElasticityDistrib = db.m_ElasticityDistrib;
		StiffnessDistrib = db.m_StiffnessDistrib;
		InertDistrib = db.m_InertDistrib;
		RadiusDistrib = db.m_RadiusDistrib;
		if (UseColliderSettings) Colliders = db.m_Colliders;
	}

	void OnValidate() {
		var dbs = GetComponents<DynamicBone>();
		
		// アタッチされていないボーンがあれば追加
		var attachedBones = dbs.Select(db => db.m_Root);
		var unAttachedBones = Bones.Except(attachedBones).ToArray();
		foreach (var bone in unAttachedBones)
		{
			if (bone == null) break;
			var db = gameObject.AddComponent<DynamicBone>();
			db.m_Root = bone;
			db.m_DistantDisable = true;
		}
		// 追加した場合コンポーネントをリロード
		if (unAttachedBones.Length > 0)
		{
			dbs = GetComponents<DynamicBone>();
		}
		
		foreach (var dynamicBone in dbs)
		{
			dynamicBone.m_UpdateRate = UpdateRate;
			dynamicBone.m_Damping = Damping;
			dynamicBone.m_Elasticity = Elasticity;
			dynamicBone.m_Stiffness = Stiffness;
			dynamicBone.m_Inert = Inert;
			dynamicBone.m_Radius = Radius;
			dynamicBone.m_DampingDistrib = DanpingDistrib;
			dynamicBone.m_ElasticityDistrib = ElasticityDistrib;
			dynamicBone.m_StiffnessDistrib = StiffnessDistrib;
			dynamicBone.m_InertDistrib = InertDistrib;
			dynamicBone.m_RadiusDistrib = RadiusDistrib;
			
			if (UseColliderSettings) dynamicBone.m_Colliders = Colliders;
			
			switch(m_FreezeAxis){
				case FreezeAxis.X:
					dynamicBone.m_FreezeAxis = DynamicBone.FreezeAxis.X;
					break;
				case FreezeAxis.Y:
					dynamicBone.m_FreezeAxis = DynamicBone.FreezeAxis.Y;
					break;
				case FreezeAxis.Z:
					dynamicBone.m_FreezeAxis = DynamicBone.FreezeAxis.Z;
					break;
				case FreezeAxis.None:
					dynamicBone.m_FreezeAxis = DynamicBone.FreezeAxis.None;
					break;
			}
			
			switch(m_UpdateMode){
				case UpdateMode.Normal:
					dynamicBone.m_UpdateMode = DynamicBone.UpdateMode.Normal;
					break;
				case UpdateMode.AnimatePhysics:
					dynamicBone.m_UpdateMode = DynamicBone.UpdateMode.AnimatePhysics;
					break;
				case UpdateMode.UnscaledTime:
					dynamicBone.m_UpdateMode = DynamicBone.UpdateMode.UnscaledTime;
					break;
				case UpdateMode.Default:
					dynamicBone.m_UpdateMode = DynamicBone.UpdateMode.Default;
					break;
			}
		}
	}
}
