using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInitState : GenericState<GameStateManager, GameStateData>
{
    private bool m_finishLoading;

    public override void OnEnter(GameStateData data)
    {
        base.OnEnter(data);
        GameStateManager.Instance.StartCoroutine(InitGame());
    }

    private IEnumerator InitGame()
    {
        yield return SceneManager.LoadSceneAsync("Main", LoadSceneMode.Additive);

        ResourceManager.Instance.Initialize();
        InventoryManager.Instance.Initialize();
        CharactersManager.Instance.Initialize();

        yield return GameManager.Instance.Serializer.DeserializeData();

        GameManager.Instance.PostLoad(GameManager.Instance.Serializer.IsNewGame);

        m_finishLoading = true;
    }

    public override StateTransition<GameStateData> EvaluateTransition()
    {
        if(m_finishLoading)
        {
            return new StateTransition<GameStateData>(typeof(GameIdleState), null);
        }
        return base.EvaluateTransition();
    }
}
