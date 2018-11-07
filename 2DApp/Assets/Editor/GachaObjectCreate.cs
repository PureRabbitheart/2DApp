using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

public class GachaObjectCreate : EditorWindow
{

    //MenuItemを付ける事で上部メニューに項目を追加
    [MenuItem("Tools/Gacha/GachaObjectCreate")]
    private static void Create()
    {

        var window = GetWindow<GachaObjectCreate>("ObjectCreateWindow");
        window.minSize = new Vector2(640, 320);

    }

    GachaObjectData pGachaObjectData;//オブジェクトのデータを管理するポインタ

    private void OnGUI()
    {

        if (pGachaObjectData == null)//ScriptableObjectのインスタンスを作成
        {
            pGachaObjectData = ScriptableObject.CreateInstance<GachaObjectData>();
        }


        EditorGUILayout.LabelField("ガチャのオブジェクトファイル作成");//タイトル

        EditorGUILayout.Space();//改行スペース
        EditorGUILayout.Space();//改行スペース

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


        EditorGUILayout.BeginHorizontal(GUI.skin.box);
        {
            if (GUILayout.Button("ファイル保存"))
            {
                AssetDatabase.CreateAsset(pGachaObjectData, "Assets/Gacha/GachaObject/" + pGachaObjectData.eRarity.ToString() + "/" + pGachaObjectData.ObjectName + ".asset");//ファイル書き出し
                Debug.Log(pGachaObjectData.ObjectName + "作成しました");
            }
            if (GUILayout.Button("新規作成"))
            {
                pGachaObjectData = ScriptableObject.CreateInstance<GachaObjectData>();
            }
        }
        EditorGUILayout.EndHorizontal();
    }
}
