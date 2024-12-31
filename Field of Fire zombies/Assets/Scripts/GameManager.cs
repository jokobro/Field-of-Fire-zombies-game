using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text pointsUiText;
    [SerializeField] private float Points;
    private int scoreMultiplier = 2;

    private void Start()
    {
      string pointsUiText = Points.ToString();
    }

    private void Update()
    {
        pointsUiText.SetText("" + Points);
    }

    public void AddScore(int pointsAmount)
    {
        Points += Mathf.RoundToInt(pointsAmount * scoreMultiplier);
        pointsUiText.SetText("" + Points);
    }


}
