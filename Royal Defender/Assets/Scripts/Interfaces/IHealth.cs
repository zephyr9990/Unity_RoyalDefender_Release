using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth
{
    void TakeDamage(int amount);
    void RestoreHealth(int amount);

    bool IsGreaterThanZero();
}
