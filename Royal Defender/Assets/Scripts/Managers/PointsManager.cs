using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsManager : MonoBehaviour
{
    public Text pointsText;
    private static int points;
    private static PointsManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        points = 0;
    }

    public static PointsManager GetInstance()
    {
        return instance;
    }

    public void AddPoints(int amount)
    {
        points += amount;
        UpdatePointsUI();
    }

    public void SubtractPoints(int amount)
    {
        points -= amount;
        UpdatePointsUI();
    }

    public int GetPoints()
    {
        return points;
    }

    private void UpdatePointsUI()
    {
        pointsText.text = points.ToString();
    }
}
