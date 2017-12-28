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
        GameStateManager.Instance.Initialize();

        Destroy(gameObject);
    }
}
