using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;



public class ScreenShot : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Photo Taker")]
    [SerializeField] private Image PhotoDisplayArea;
    [SerializeField] private GameObject photoFrame;

    [Header("Flash Effect")]
    [SerializeField] private GameObject cameraflash;
    [SerializeField] private float flashTime;
    private Texture2D screen_capture;

    [Header("Audio")]
    [SerializeField] private AudioSource Audio;

    private bool viewingPhoto;
    private void Start()
    {
        screen_capture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(!viewingPhoto)
                StartCoroutine(CapturePhoto());
            else
                RemovePhoto();
        }   
    }

    IEnumerator CapturePhoto()
    {
        viewingPhoto = true;

        yield return new WaitForEndOfFrame();

        Rect regionToRead = new Rect(0,0, Screen.width, Screen.height);

        screen_capture.ReadPixels(regionToRead, 0,0, false );
        screen_capture.Apply();
        ShowPhoto();
    }

    void ShowPhoto()
    {
        Sprite photoSprite = Sprite.Create(screen_capture, new Rect(0.0f,0.0f, screen_capture.width,screen_capture.height), new Vector2(0.5f,0.5f), 100.0f);
        PhotoDisplayArea.sprite = photoSprite;

        StartCoroutine(CameraFlashEffect());
        
        
        
    }

    IEnumerator CameraFlashEffect()
    {
        Audio.Play();
        yield return new WaitForSeconds(1f);
        cameraflash.SetActive(true);
        yield return new WaitForSeconds(flashTime);
        cameraflash.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        photoFrame.SetActive(true);
    }

    void RemovePhoto()
    {
        viewingPhoto = false;
        photoFrame.SetActive(false);
    }
}
