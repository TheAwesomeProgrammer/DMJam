using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BabyIconUiManager : MonoBehaviour
{
    [SerializeField]
    private BabyIcon _babyIconPrefab;

    [SerializeField]
    private Transform _babyIconParent;

    [SerializeField]
    private Player _player;

    private List<BabyIcon> _babyIcons;

    private void Awake()
    {
        _babyIcons = new List<BabyIcon>();
        SpawnBabyIcons();
        _player.KilledABaby += KilledABaby;
    }

    private void KilledABaby()
    {
        BabyIcon aliveBabyIcon = _babyIcons.First(item => item.IsAlive);
        aliveBabyIcon.BabyDied();
    }

    private void SpawnBabyIcons()
    {
        for(int i = 0; i < Player.BABY_KILL_QUOTA; i++)
        {
            _babyIcons.Add(Instantiate<BabyIcon>(_babyIconPrefab, _babyIconParent));
        }
    }


}
