using System.Linq;
using UnityEngine;

public class DBController : MonoBehaviour
{
	public Transform[] Bones;
	[Range(0f, 1f)] public float Damping, Elasticity, Stiffness, Inert;
	public float Radius;
	public AnimationCurve DanpingDistrib, ElasticityDistrib, StiffnessDistrib, InertDistrib, RadiusDistrib;

	// Use this for initialization
	void Reset()
	{
		var db = GetComponent<DynamicBone>();
		if (!db) return;
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
		}
	}
}
