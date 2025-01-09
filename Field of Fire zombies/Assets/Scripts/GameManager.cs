using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text pointsUiText;
    [SerializeField] private float Points;
    public float scoreMultiplier = 1f;

    private void Start()
    {
        pointsUiText.SetText(Points.ToString());
    }

    public void AddScore(int pointsAmount)
    {
        Points += Mathf.RoundToInt(pointsAmount * scoreMultiplier);
        pointsUiText.SetText($"{Points}");
    }
}
