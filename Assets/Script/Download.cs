using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

//Desenglozar el link que se copia desde el google drive
//https://drive.google.com/file/d/1-LuNPDeWdwhV16x8t70oZ9_zjFHjEVEe/view?usp=sharing
//https://drive.google.com/file/d//view?usp=sharing
//1-LuNPDeWdwhV16x8t70oZ9_zjFHjEVEe
//Cuando este desenglozado se le agregan estas primeras lineas de htt
//https://drive.google.com/uc?export=download&id=
//Despues se une el desenglobe con la linea de htt, quedando asi
//https://drive.google.com/uc?export=download&id=1-LuNPDeWdwhV16x8t70oZ9_zjFHjEVEe

public class Download : MonoBehaviour
{
    public string miUrl = "https://drive.google.com/uc?export=download&id=1-LuNPDeWdwhV16x8t70oZ9_zjFHjEVEe";
    public bool mClearChace = false;

    private VideoPlayer mVideoPlayer = null;
    private AssetBundle mBundle = null;

    void Awake()
    {
        mVideoPlayer = GetComponent<VideoPlayer>();
        Caching.compressionEnabled = false;

        if (mClearChace)
            Caching.ClearCache();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(DownloadAndPlay());
        }
    }

    public void PlayVideoPlayer()
    { 
        StartCoroutine(DownloadAndPlay());
	}

    IEnumerator DownloadAndPlay()
    {
        yield return GetBundle();

        if(!mBundle)
        {
            print("Bundle failed");
            yield break;
        }
        VideoClip newVideoClip = mBundle.LoadAsset<VideoClip>("Videoadn");
        mVideoPlayer.clip = newVideoClip;
        mVideoPlayer.Play();
    }
    IEnumerator GetBundle()
    {
        WWW request = WWW.LoadFromCacheOrDownload(miUrl,0);

        while(!request.isDone)
        {
            print(request.progress);
            yield return null;
        }

        if(request.error == null)
        {
            mBundle = request.assetBundle;
            print("Success");
        }
        else
        {
            print(request.error);
        }
        
    }

}
