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
        UIPartyManager.Instance.Init();
        UIPartyManager.Instance.LoadViews(m_initContainer);
        UIPartyManager.Instance.RequestView<LoadingView>();
        StartCoroutine(InitGame());
    }

    private IEnumerator InitGame()
    {
        yield return SceneManager.LoadSceneAsync("Main", LoadSceneMode.Additive);

        GameManager.Instance.Init();
        ResourceManager.Instance.Initialize();
        GameManager.Instance.Serializer.DeserializeData();
        InventoryManager.Instance.Initialize();
        CharactersManager.Instance.Initialize();
        GameStateManager.Instance.Initialize();
        GameManager.Instance.PostLoad();
        //UIPartyManager.Instance.RequestView<MainMenuView>();
    }
}
