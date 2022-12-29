using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject HomeCanvas;
    public GameObject ReaderCanvas;
    public GameObject SettingsCanvas;
    public GameObject ReaderSideMenu;

    private void OnEnable()
    {
        EnableHomeCanvas();
        DisableReaderCanvas();
        DisableSettingsCanvas();
        DisableReaderSideMenu();

    }
    public void EnableHomeCanvas()
    {
        HomeCanvas.SetActive(true);
    }
    public void DisableHomeCanvas()
    {
        HomeCanvas.SetActive(false);
    }
    public void EnableReaderCanvas()
    {
        ReaderCanvas.SetActive(true);
    }
    public void DisableReaderCanvas()
    {
        ReaderCanvas.SetActive(false);
    }
    public void EnableSettingsCanvas()
    {
        SettingsCanvas.SetActive(true);
    }
    public void DisableSettingsCanvas()
    {
        SettingsCanvas.SetActive(false);
    }
    public void EnableReaderSideMenu()
    {
        ReaderSideMenu.SetActive(true);
    }
    public void DisableReaderSideMenu()
    {
        ReaderSideMenu.SetActive(false);
    }
    public void ReaderSideMenuSwitch()
    {
        if (ReaderSideMenu.activeSelf) DisableReaderSideMenu();
        else EnableReaderSideMenu();

    }

}
