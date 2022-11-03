using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Engine))]
public class EngineEditor : Editor
{
	Engine engine;

	private void OnEnable()
    {
		engine = target as Engine;
	}

	public override void OnInspectorGUI()
	{
		GUI.color = Color.cyan;

		EditorGUI.BeginDisabledGroup(true);

		EditorGUILayout.TextField(engine.encryptData.sim_geo);
		EditorGUILayout.TextField(engine.encryptData.bundle);
		EditorGUILayout.TextField(engine.encryptData.amidentificator);
		EditorGUILayout.TextField(engine.encryptData.afidentificator);
		EditorGUILayout.TextField(engine.encryptData.googleid);
		EditorGUILayout.TextField(engine.encryptData.subcodename);
		EditorGUILayout.TextField(engine.encryptData.appref);
		EditorGUILayout.TextField(engine.encryptData.dl_url);
		EditorGUILayout.TextField(engine.encryptData.dl_url2);
		EditorGUILayout.TextField(engine.encryptData.bundle_prop);
		EditorGUILayout.TextField(engine.encryptData.subcodename_prop);
		EditorGUILayout.TextField(engine.encryptData.domen_prop);
		EditorGUILayout.TextField(engine.encryptData.space_prop);
		EditorGUILayout.TextField(engine.encryptData.requestCampaign_prop);
		EditorGUILayout.TextField(engine.container.initData.appmetricaAppId_prop);
		EditorGUILayout.TextField(engine.container.initData.oneSignalAppId_prop);
		EditorGUILayout.TextField(engine.container.initData.appsFlyerAppId_prop);

		EditorGUI.EndDisabledGroup();

		EditorUtility.SetDirty(target);
	}
}
