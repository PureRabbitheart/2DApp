using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

public class GachaDataCreate : EditorWindow
{

    private bool[] isTab = { false, false };

    private GachaData pGachaData;
    public List<GachaData.Data> GachaContents;
    GachaData FileData;

    //MenuItemを付ける事で上部メニューに項目を追加
    [MenuItem("Tools/Gacha/GachaDataCreate")]
    private static void Create()
    {

        var window = GetWindow<GachaDataCreate>("GachaDataCreateWindow");
        window.minSize = new Vector2(640, 320);

    }

    private void OnGUI()
    {

        if (pGachaData == null)
        {
            pGachaData = new GachaData();
        }

        FileData = EditorGUILayout.ObjectField(FileData, typeof(GachaData), true) as GachaData;//データを編集するなら
        if (FileData != null)//中身が入ったら
        {
            pGachaData = FileData;
        }

        pGachaData.GachaName = EditorGUILayout.TextField("ガチャのタイトル", pGachaData.GachaName);//オブジェクトの名前

        EditorGUILayout.Space();//改行スペース
        EditorGUILayout.Space();//改行スペース

        pGachaData.TopTexture = EditorGUILayout.ObjectField(pGachaData.TopTexture, typeof(Texture2D), true) as Texture2D;

        EditorGUILayout.Space();//改行スペース
        EditorGUILayout.Space();//改行スペース


        EditorGUILayout.BeginVertical(GUI.skin.box);
        {


            ///////////////////////　　ピックアップ　　//////////////////////////////////////////////////

            if (isTab[0] = EditorGUILayout.Foldout(isTab[0], "Pickup"))//Pickupのタブ
            {
                for (int i = 0; i < pGachaData.GachaContents.Count; ++i)//表示
                {
                    if (pGachaData.GachaContents[i].isPickup == true)
                    {
                        GachaData.Data tmpData = pGachaData.GachaContents[i];
                        EditorGUILayout.BeginHorizontal();
                        {
                            tmpData.DataFile = EditorGUILayout.ObjectField(pGachaData.GachaContents[i].DataFile, typeof(GachaObjectData), true) as GachaObjectData;

                            if (GUILayout.Button("除外", GUILayout.Width(80), GUILayout.Height(15)))
                            {
                                tmpData.isPickup = !pGachaData.GachaContents[i].isPickup;
                            }
                            if (GUILayout.Button("削除", GUILayout.Width(50), GUILayout.Height(15)))
                            {
                                pGachaData.GachaContents.Remove(pGachaData.GachaContents[i]);
                            }
                        }
                        EditorGUILayout.EndHorizontal();
                        pGachaData.GachaContents[i] = tmpData;
                    }

                }


                GachaData.Data data = new GachaData.Data();
                data.DataFile = EditorGUILayout.ObjectField("追加", null, typeof(GachaObjectData), true) as GachaObjectData;//追加

                if (data.DataFile != null)//データが入ったら
                {
                    data.isPickup = true;
                    pGachaData.GachaContents.Add(data);
                }
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////





            ///////////////////////　　はずれの中身　　////////////////////////////////////////////////////////
            if (isTab[1] = EditorGUILayout.Foldout(isTab[1], "中身"))//Pickupのタブ
            {
                for (int i = 0; i < pGachaData.GachaContents.Count; ++i)//表示
                {
                    if (pGachaData.GachaContents[i].isPickup == false)
                    {
                        GachaData.Data tmpData = pGachaData.GachaContents[i];
                        EditorGUILayout.BeginHorizontal();
                        {
                            tmpData.DataFile = EditorGUILayout.ObjectField(pGachaData.GachaContents[i].DataFile, typeof(GachaObjectData), true) as GachaObjectData;

                            if (GUILayout.Button("ピックアップ", GUILayout.Width(80), GUILayout.Height(15)))
                            {
                                tmpData.isPickup = !pGachaData.GachaContents[i].isPickup;
                            }

                            if (GUILayout.Button("削除", GUILayout.Width(50), GUILayout.Height(15)))
                            {
                                pGachaData.GachaContents.Remove(pGachaData.GachaContents[i]);
                            }
                        }
                        EditorGUILayout.EndHorizontal();
                        pGachaData.GachaContents[i] = tmpData;
                    }
                }


                GachaData.Data data = new GachaData.Data();
                data.DataFile = EditorGUILayout.ObjectField("追加", null, typeof(GachaObjectData), true) as GachaObjectData;//追加

                if (data.DataFile != null)//データが入ったら
                {
                    data.isPickup = false;
                    pGachaData.GachaContents.Add(data);
                }
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////


        }
        EditorGUILayout.EndVertical();



        EditorGUILayout.BeginHorizontal();
        {
            if (FileData == null && GUILayout.Button("ファイル保存"))
            {
                for (int i = (int)GachaObjectData.RARITY.SR; i <= (int)GachaObjectData.RARITY.SSR; i++)
                {
                    Calculation(i);
                }
                if (pGachaData.GachaName != "Name")
                {
                    AssetDatabase.CreateAsset(pGachaData, "Assets/Gacha/GachaData/" + pGachaData.GachaName + ".asset");//ファイル書き出し
                    Debug.Log(pGachaData.GachaName + "作成しました");
                }
                else
                {
                    Debug.Log("ファイル名をちゃんと入力してね");

                }
            }
            else if (FileData != null && GUILayout.Button("上書き"))
            {
                Debug.Log("上書き完了");

            }

            if (GUILayout.Button("新規作成"))
            {
                FileData = null;
                pGachaData = new GachaData();
                Debug.Log("新規作成しました");
            }

        }
        EditorGUILayout.EndHorizontal();
    }


    void Calculation(int rarity)//確率計算
    {
        List<GachaData.Data> tmp = new List<GachaData.Data>();

        for (int i = 0; i < pGachaData.GachaContents.Count; i++)//ガチャから出てくる分回す
        {
            if ((int)pGachaData.GachaContents[i].DataFile.eRarity == rarity)//対象のレアリティだったら
            {
                tmp.Add(pGachaData.GachaContents[i]);//必要な情報だけ保存
            }
        }


        //Pickupが何個あるか探す
        int ObjectCount = 0;

        for (int j = 0; j < tmp.Count; j++)
        {
            ObjectCount++;
            if (tmp[j].isPickup == true)//Pickup+レアリティの数を足す
            {
                ObjectCount++;
            }
        }

        //100を数で割る
        float probability = 100.0f / (float)ObjectCount;
        Debug.Log(tmp.Count);
        //Pickupは普通の二倍の確立にする

        for (int m = 0; m < pGachaData.GachaContents.Count; m++)//ガチャから出てくる分回す
        {
            if ((int)pGachaData.GachaContents[m].DataFile.eRarity == rarity)//対象のレアリティだったら
            {
                GachaData.Data tmpData = pGachaData.GachaContents[m];

                if (pGachaData.GachaContents[m].isPickup == true)
                {
                    tmpData.fProbability = probability * 2;
                }
                else
                {
                    tmpData.fProbability = probability;
                }
                pGachaData.GachaContents[m] = tmpData;

            }
        }



    }
}
