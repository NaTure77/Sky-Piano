using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaySelfManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        //DataManager.MakeCurrent();
        DataManager.LoadPlayerData();
        /*DataManager.toneLevel = DataManager.userData.tone;
        DataManager.metronomeBpm = DataManager.userData.bpm;
        DataManager.instrumentType = DataManager.userData.instrumentType*/
        //각 매니저에서 초기화 함수에 ref로 playdata 넘기기.
    }
    private void Start()
    {
        InitSystem();
        
    }
    public void InitSystem()
    {
        SoundManager.instance.InitToneList();
        KeyboardManager.instance.Init();
        SoundManager.instance.ChangeKeyTone(DataManager.userData.tone);
        SoundManager.instance.SetInstrument(DataManager.userData.instrumentType);
        Metronome.instance.Init(DataManager.userData.bpm);
        KeyboardManager.instance.SetPlayMode();
        UIManager.menu.exitButton.onClick.AddListener(() => Application.Quit());

    }
    public void SaveSetting()
    {
        DataManager.userData.tone = SoundManager.instance.toneLevel;
        DataManager.userData.bpm = Metronome.instance.bpm;
        DataManager.userData.instrumentType = SoundManager.instance.instrumentType;
        DataManager.SavePlayerData();
    }

    public void SelectScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Check_instrumentData()
    {
        DirectoryInfo di = new DirectoryInfo(Application.persistentDataPath + "/Instruments/Piano");
        BetterStreamingAssets.Initialize();
        if (di.Exists == false)
        {
            di.Create();
            string sourcePath = "/Piano/";
            string targetPath = Application.persistentDataPath + "/Instruments/Piano/";         
            for (int i = 0; i < 15; i++)
            {
                File.WriteAllBytes(targetPath + i + ".WAV", BetterStreamingAssets.ReadAllBytes(sourcePath + i + ".WAV"));
            }
        }
         di = new DirectoryInfo(Application.persistentDataPath + "/Instruments/Ukulele");
        if (di.Exists == false)
        {
            di.Create();
            string sourcePath = "/Ukulele/";
            string targetPath = Application.persistentDataPath + "/Instruments/Ukulele/";
            for (int i = 0; i < 15; i++)
            {
                File.WriteAllBytes(targetPath + i + ".WAV", BetterStreamingAssets.ReadAllBytes(sourcePath + i + ".WAV"));
            }
        }
    }
}
