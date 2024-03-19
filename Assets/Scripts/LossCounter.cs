using UnityEngine;
using UnityEngine.UI;

public class LossCounter : MonoBehaviour
{ 
    [SerializeField] private Text _text;
    private int _enemyLoss;
    private int _playerLoss;

    public void SetEnemyLoss(int loss)
    {
        _enemyLoss = loss;
        UpdateText();
    }

    public void SetPlayerLoss(int loss)
    { 
        _playerLoss = loss;
        UpdateText();
    }

    private void UpdateText()
    {
        _text.text = $"{_enemyLoss} - {_playerLoss}";
    }
}
