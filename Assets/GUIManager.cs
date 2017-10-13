using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour {

	public void OnClickRestartBtn()
    {
        GameManager.instance.RestartGame();
    }

    public void OnClickShowGameRuleBtn()
    {
        GameManager.instance.ShowGameRuleUI();
    }

    public void OnClickHideGameRuleBtn()
    {
        GameManager.instance.HideGameRuleUI();
    }
}
