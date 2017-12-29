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

        yield return GameManager.Instance.Serializer.DeserializeData();

        PostLoad();

        m_finishLoading = true;
    }

    private void PostLoad()
    {
        bool newGame = GameManager.Instance.Serializer.IsNewGame;
        GameManager.Instance.PostLoad(newGame);
        InventoryManager.Instance.PostLoad(newGame);
        CharactersManager.Instance.PostLoad(newGame);
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
