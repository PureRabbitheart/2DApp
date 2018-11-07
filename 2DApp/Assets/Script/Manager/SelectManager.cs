using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System.Text;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class SelectManager : MonoBehaviour
{
    enum MODE
    {
        Select,
        StageInfo,
    }

    [SerializeField]
    private MODE eSceneMode;
    [SerializeField]
    private ActionPointManager pAPManager;
    [SerializeField]
    private TextMeshProUGUI InfoText;
    [SerializeField]
    private TextMeshProUGUI LemonText;//レモンの数
    [SerializeField]
    private TextAsset StageInfoFile;//ステージの情報が入ったファイル
    [SerializeField]
    private GameObject gStageInfo;//ステージの情報ウィンドウ
    [SerializeField]
    private NextScene pNextScene;


    private List<string[]> StageInfoList = new List<string[]>();//ステージ情報を管理するリスト
    private int PlayStageNum;//プレイするステージ番号


    void Awake()
    {
        StageInfoList = ResourceLoad(StageInfoFile.name);
        LemonValueRender();
    }

    void Update()
    {
        InfoWindow();
    }

    void LemonValueRender()//レモンの数を表示する
    {
        int lemonValue = PlayerPrefs.GetInt("Lemon");
        LemonText.text = "× " + lemonValue.ToString();
    }

    public void StageInfoRender(int StageNum)//ステージの詳細を押したら
    {
        eSceneMode = MODE.StageInfo;
        InfoText.text = "消費APは" + StageInfoList[StageNum][1].ToString();
        PlayStageNum = StageNum;
    }

    public void StageSelectBack()//StageSelectに戻る
    {
        eSceneMode = MODE.Select;
    }

    public void PlayScene()//選んだステージで遊ぶ
    {

        if(pAPManager.UseActionPoint(int.Parse(StageInfoList[PlayStageNum][1])))
        {
            PlayerPrefs.SetString("RcoveryTime", System.DateTime.Now.ToString());
            PlayerPrefs.SetInt("AP", pAPManager.nowPoint);
            pNextScene.StageScene(PlayStageNum);
            Debug.Log("ロード");
        }
        else
        {
            Debug.Log("スタミナないわ(#･∀･)");
        }
    }

    void InfoWindow()
    {
        if (eSceneMode == MODE.Select)
        {
            gStageInfo.SetActive(false);
        }
        else
        {
            gStageInfo.SetActive(true);
        }
    }


    List<string[]> ResourceLoad(string FileName)//データの読み込み
    {
        List<string[]> tmpFile = new List<string[]>();
        TextAsset CSVFile = Resources.Load(FileName) as TextAsset; /* Resouces/CSV下のCSV読み込み */

        StringReader reader = new StringReader(CSVFile.text);

        while (reader.Peek() > -1)
        {
            string line = reader.ReadLine();
            tmpFile.Add(line.Split(',')); // リストに入れる
        }
        return tmpFile;
    }

    void ResourceSave(List<string[]> file, string fileName)//データ出力
    {
        StreamWriter p_StreamWriter = new StreamWriter(Application.dataPath + "/Resources/" + fileName + ".csv", false, Encoding.UTF8);//第一引数path、第二引数追記するか最初からやるか
                                                                                                                                       // データ出力
        for (int i = 0; i < file.Count; i++)
        {
            string tmpstring = string.Join(",", file[i]);
            p_StreamWriter.WriteLine(tmpstring);

        }
        p_StreamWriter.Flush();
        p_StreamWriter.Close();
#if UNITY_EDITOR
        AssetDatabase.Refresh();
#endif
    }
}
