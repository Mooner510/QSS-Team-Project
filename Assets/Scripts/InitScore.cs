using UnityEngine;
using UnityEngine.UI;

public class InitScore : MonoBehaviour
{
    private void Start()
    {
        gameObject.GetComponent<Text>().text = $"{ScoreBoard.GetScore()}";
    }
}