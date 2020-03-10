using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealthWithWaveManager : IHealth
{
    void SetWaveManager(WaveManager waveManager);
}
