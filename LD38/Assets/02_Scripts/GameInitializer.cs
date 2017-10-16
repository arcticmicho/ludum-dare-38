using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInitializer : MonoSingleton<GameInitializer>
{
    [SerializeField]
    private ViewContainer m_initContainer;

	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private IEnumerator InitGame()
    {
        yield return SceneManager.LoadSceneAsync("Main");
        GameManager.Instance.Init();
        UIPartyManager.Instance.Init();
        
        GameManager.Instance.Serializer.DeserializeData();
        UIPartyManager.Instance.RequestView<MainMenuView>();
    }
}
