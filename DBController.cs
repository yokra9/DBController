using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework.Constraints;
using UnityEngine;

public class DBController : MonoBehaviour
{

	[SerializeField] public Transform[] Bones;
	[SerializeField, Range(0f, 1f)] public float Damping, Elasticity, Stiffness, Inert;

	// Use this for initialization
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
		}
	}
}
