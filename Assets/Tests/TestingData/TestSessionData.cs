using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Evacuation.Model;

public class TestSessionData : MonoBehaviour
{
    public bool isReset = false;
    public bool isSaving = false;
    public bool isLoading = false;

    // Update is called once per frame
    void Update()
    {
        ResetHangar();
        SaveHanger();
        LoadHangar();
    }

    public void ResetHangar()
    {
        if (!isReset) return;

        SessionData.instance.ResetAllSaves();
        Debug.Log("Has Reset");
        isReset = false;
    }

    public void SaveHanger()
    {
        if (!isSaving) return;

        SessionData.instance.Save();
        isSaving = false;
    }

    public void LoadHangar()
    {
        if (!isLoading) return;

        SessionData.instance.Load();
        isLoading = false;
    }
}
