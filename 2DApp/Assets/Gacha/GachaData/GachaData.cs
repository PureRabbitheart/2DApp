using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif


public class GachaData : ScriptableObject
{

    public string GachaName = "Name";//ガチャの名前


    [System.Serializable]
    public struct Data
    {
        public GachaObjectData DataFile;//オブジェクトのデータファイル
        public bool isPickup;//Pickup対象か
        public float fProbability;//確率
    }

    public List<Data> GachaContents = new List<Data>();//何が出るかのデータが入ったList

    public Texture2D TopTexture;

    //#if UNITY_EDITOR

    //    [CustomEditor(typeof(GachaObjectData))]               //!< 拡張するときのお決まりとして書いてね
    //    public class CharacterEditor : Editor           //!< Editorを継承するよ！
    //    {

    //        public override void OnInspectorGUI()
    //        {
    //            GachaObjectData pGachaObjectData = target as GachaObjectData;


    //            EditorGUILayout.BeginHorizontal();
    //            {
    //                GUILayout.Box(pGachaObjectData.test, GUILayout.Width(128), GUILayout.Height(128));
    //                {
    //                    EditorGUILayout.BeginVertical();
    //                    pGachaObjectData.ObjectName = EditorGUILayout.TextField("オブジェクトネーム", pGachaObjectData.ObjectName);//オブジェクトの名前
    //                    pGachaObjectData.eRarity = (GachaObjectData.RARITY)EditorGUILayout.EnumPopup("設定する項目", pGachaObjectData.eRarity);//レアリティを選択
    //                    pGachaObjectData.gObjectPrefab = EditorGUILayout.ObjectField("オブジェクトのプレハブ", pGachaObjectData.gObjectPrefab, typeof(GameObject), true) as GameObject;
    //                    pGachaObjectData.test = EditorGUILayout.ObjectField("オブジェクト画像", pGachaObjectData.test, typeof(Texture2D), true) as Texture2D;

    //                }
    //                EditorGUILayout.EndVertical();
    //            }
    //            EditorGUILayout.EndHorizontal();
    //        }
    //    }
    //#endif
}
