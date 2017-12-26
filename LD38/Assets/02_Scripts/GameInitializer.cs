using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInitializer :MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _preloadManagers;

    [SerializeField]
    private List<GameObject> _postLoadManagers;

    private void Start()
    {
        foreach (var manager in _preloadManagers)
        {
            GameObject managerGameObject = Instantiate(manager) as GameObject;
            managerGameObject.name = manager.name;
        }

        StartCoroutine(InitGame());
    }

    private IEnumerator InitGame()
    {
        yield return SceneManager.LoadSceneAsync("Main", LoadSceneMode.Additive);

        foreach (var manager in _postLoadManagers)
        {
            GameObject managerGameObject = Instantiate(manager) as GameObject;
            managerGameObject.name = manager.name;
        }

        ResourceManager.Instance.Initialize();
        InventoryManager.Instance.Initialize();
        CharactersManager.Instance.Initialize();
        GameStateManager.Instance.Initialize();

        GameManager.Instance.Serializer.DeserializeData();
        GameManager.Instance.PostLoad();

        Destroy(gameObject);
    }
}
