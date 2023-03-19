using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System;
using System.Linq;

public class GameManager : MonoBehaviour
{
    private Text placeHoldertext;
    private GameObject Ferraritext;
    private Text ferrariTextName;
    private GameObject Fordtext;
    private Text FordTextName;
    private Toggle soundOnOff;
    private AudioSource gmASource;
    private Slider musicVolslider;

    private static string ferrariname;
    private static string fordname;

    private static bool Toggle { get; set; }
    private static float SliderFloat{ get; set; }

    public string FerrariName {
        get => ferrariname;
        set {
            if (value == "") {
                ferrariname = "Ferrari Player";
            }
            else {
                ferrariname = value;
            }
        }
    }
    public string FordName {
        get => fordname;
        set {
            if (value == "") {
                fordname = "Ford Player";
            }
            else {
                fordname = value;
            }
        }
    }

    void Start()
    {
        ReadString();
        gmASource = GetComponent<AudioSource>();
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            soundOnOff = GameObject.Find("SoundOnOff").GetComponent<Toggle>();
            musicVolslider = GameObject.Find("Slider").GetComponent<Slider>();
        }
        SetOptions();
    }

    private void SetOptions() {
        if(SceneManager.GetActiveScene().buildIndex == 0) {
            musicVolslider.value = SliderFloat;
            soundOnOff.isOn = Toggle;
            if(Toggle == false) {
                gmASource.volume = 0;
            } 
        } else {
            if (Toggle == false) {
                gmASource.volume = 0;
            } else {
                gmASource.volume = SliderFloat;
            }
        }
    }

    private void WriteString()
    {
        string path = @"Assets/Resources/settings.txt";
        soundOnOff = GameObject.Find("SoundOnOff").GetComponent<Toggle>();
        musicVolslider = GameObject.Find("Slider").GetComponent<Slider>();

        FileInfo fi = new FileInfo(path);
        using (TextWriter tw = new StreamWriter(fi.Open(FileMode.Truncate)))
        {
            string soundOfOn = soundOnOff.isOn.ToString();
            Toggle = Convert.ToBoolean(soundOfOn);
            tw.WriteLine(soundOfOn);
            double musicLvl = musicVolslider.value;
            SliderFloat = musicVolslider.value;
            tw.WriteLine(musicLvl);
        }
    }

    private void ReadString()
    {
        string path = @"Assets/Resources/settings.txt";
        Toggle = Convert.ToBoolean(File.ReadLines(path).First());
        SliderFloat = (float)Convert.ToDecimal(File.ReadLines(path).ElementAt(1));
    }

    public void RemovePlaceHolder()
    {
        GameObject placeHolder = GameObject.Find("Placeholder");
        placeHoldertext = placeHolder.GetComponent<Text>();
        placeHoldertext.text.Remove(0);
    }
    public void create()
    {
        Ferraritext = GameObject.FindGameObjectWithTag("FerrariName");
        ferrariTextName = Ferraritext.GetComponent<Text>();
        FerrariName = ferrariTextName.text;

        Fordtext = GameObject.FindGameObjectWithTag("FordName");
        FordTextName = Fordtext.GetComponent<Text>();
        FordName = FordTextName.text;
    }

    public void LevelOne()
    {
        WriteString();
        ReadString();
        create();
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public IEnumerator LevelTwo()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(2);
    }

    public IEnumerator LevelThree()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(3);
    }

    public IEnumerator CreditScene()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(4);
    }

    public void Quit()
    {
        EditorApplication.Exit(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            if (!soundOnOff.isOn)
            {
                gmASource.volume = 0;
            }
            else
            {
                gmASource.volume = musicVolslider.value;
            }
        }

    }
}
