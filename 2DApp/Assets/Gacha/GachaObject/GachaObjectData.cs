using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif


public class GachaObjectData : ScriptableObject
{
    public enum RARITY
    {
        C,
        UC,
        R,
        SR,
        SSR,
    }
    public RARITY eRarity; //レアリティ


    public string ObjectName = "ObjectName";//オブジェクトの名前

    public GameObject gObjectPrefab;//画像

    public Texture2D TopTexture;


#if UNITY_EDITOR

    [CustomEditor(typeof(GachaObjectData))]               //!< 拡張するときのお決まりとして書いてね
    public class CharacterEditor : Editor           //!< Editorを継承するよ！
    {

        public override void OnInspectorGUI()
        {
            GachaObjectData pGachaObjectData = target as GachaObjectData;


            EditorGUILayout.BeginHorizontal();
            {
                GUILayout.Box(pGachaObjectData.TopTexture, GUILayout.Width(128), GUILayout.Height(128));
                {
                    EditorGUILayout.BeginVertical();
                    pGachaObjectData.ObjectName = EditorGUILayout.TextField("オブジェクトネーム", pGachaObjectData.ObjectName);//オブジェクトの名前
                    pGachaObjectData.eRarity = (GachaObjectData.RARITY)EditorGUILayout.EnumPopup("設定する項目", pGachaObjectData.eRarity);//レアリティを選択
                    pGachaObjectData.gObjectPrefab = EditorGUILayout.ObjectField("オブジェクトのプレハブ", pGachaObjectData.gObjectPrefab, typeof(GameObject), true) as GameObject;
                    pGachaObjectData.TopTexture = EditorGUILayout.ObjectField("オブジェクト画像", pGachaObjectData.TopTexture, typeof(Texture2D), true) as Texture2D;

                }
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndHorizontal();
        }
    }
#endif
}
