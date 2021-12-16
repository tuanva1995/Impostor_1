using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/**
  * Scene:Splash
  * Object:Main Camera
  * Description: F-ja zaduzena za ucitavanje MainScene-e, kao i vizuelni prikaz inicijalizaije CrossPromotion-e i ucitavanja scene
  **/
public class SplashScene : MonoBehaviour {
	
	int appStartedNumber;
	AsyncOperation progress = null;
	public Image progressBar;
	float myProgress=0;
	string sceneToLoad;
	// Use this for initialization
	void Start ()
	{

		sceneToLoad = "GamePlay";
        appStartedNumber = 0;
		appStartedNumber++;
		StartCoroutine(LoadScene());
	}
	
	/// <summary>
	/// Coroutine koja ceka dok se ne inicijalizuje CrossPromotion, menja progres ucitavanja CrossPromotion-a, kao i progres ucitavanje scene, i taj progres se prikazuje u Update-u
	/// </summary>
	IEnumerator LoadScene()
	{

        
        Debug.Log("interstitialInitialized");
        while (myProgress < 1)
        {
            myProgress += Time.deltaTime;
            progressBar.fillAmount = myProgress;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        Debug.Log("offerwallInitialized");


		//LoadSceneSetFakeLoading();
		GoFade();


	}
    
	void LoadSceneSetFakeLoading()
    {
        Application.LoadLevel(sceneToLoad);
    }
	public Color loadToColor = Color.blue;
	public void GoFade()
	{
		Initiate.Fade(sceneToLoad, loadToColor, 1f);
	}


}
