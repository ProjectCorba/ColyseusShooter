using Colyseus.Schema;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyCharacter _character;
    [SerializeField] private EnemyGun _gun;
    private Player _player;
    private List<float> _receiveTimeInterval = new List<float> { 0, 0, 0, 0, 0 };
    private float AverageInterval
    {
        get {
            int receiveTimeIntervalCount = _receiveTimeInterval.Count;
            float summ = 0f;
            for (int i = 0; i < receiveTimeIntervalCount; i++){
                summ += _receiveTimeInterval[i];
            }
            return summ / receiveTimeIntervalCount;
        }
    }
    private float _lastReceiveTime = 0f;
    

    public void Init(string key, Player player)
    {
        this._player = player;
        _character.SetSpeed(player.speed);
        _character.SetMaxHP(player.maxHP);
        _character.Init(key);
        _player.OnChange += OnChange;
    }

    public void Shoot(in ShootInfo info)
    {
        Vector3 position = new Vector3(info.pX, info.pY, info.pZ);
        Vector3 velocity = new Vector3(info.dX, info.dY, info.dZ);
        _gun.Shoot(position, velocity);
    }

    public void Destroy()
    {
        _player.OnChange -= OnChange;
        Destroy(gameObject);
    }

    private void SaveReceiveTime()
    {
        float interval = Time.time - _lastReceiveTime;
        _lastReceiveTime = Time.time;

        _receiveTimeInterval.Add(interval);
        _receiveTimeInterval.RemoveAt(0);
    }
    internal void OnChange(List<DataChange> changes)
    {
        SaveReceiveTime();

        Vector3 position = _character.targetPosition;
        Vector3 velocity = _character.Velocity;

        foreach (var dataChange in changes) {
            switch (dataChange.Field)
            {
                case "loss":
                    MultiplayerManager.Instance._lossCounter.SetEnemyLoss((byte)dataChange.Value);
                    break;
                case "currentHP":
                    if ((sbyte)dataChange.Value > (sbyte)dataChange.PreviousValue)
                    {
                        _character.RestorHP((sbyte)dataChange.Value);
                    }
                    break;
                case "pX":
                    position.x = (float)dataChange.Value;
                    break;
                case "pY":
                    position.y = (float)dataChange.Value;
                    break;
                case "pZ":
                    position.z = (float)dataChange.Value;
                    break;
                case "vX":
                    velocity.x = (float)dataChange.Value;
                    break;
                case "vY":
                    velocity.y = (float)dataChange.Value;
                    break;
                case "vZ":
                    velocity.z = (float)dataChange.Value;
                    break;
                case "rX":
                    _character.SetRotateX((float)dataChange.Value);
                    break;
                case "rY":
                    _character.SetRotateY((float)dataChange.Value);
                    break;
                default:
                    Debug.LogWarning(dataChange.Field+": не обрабатываются изменения.");
                    break;
            }

        }
        _character.SetMovement(position, velocity, AverageInterval);
    }
}
