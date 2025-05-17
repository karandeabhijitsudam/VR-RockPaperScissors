using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI playerChoiceText;
    public TextMeshProUGUI aiChoiceText;
    public TextMeshProUGUI resultText;

    private enum Move { Rock, Paper, Scissors }
    private Move playerMove, aiMove;

    [Header("Player Gesture Sprites")]
    public GameObject playerRockImage;
    public GameObject playerPaperImage;
    public GameObject playerScissorsImage;

    [Header("AI Gesture Sprites")]
    public GameObject aiRockImage;
    public GameObject aiPaperImage;
    public GameObject aiScissorsImage;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) MakeMove(Move.Rock);
        if (Input.GetKeyDown(KeyCode.Alpha2)) MakeMove(Move.Paper);
        if (Input.GetKeyDown(KeyCode.Alpha3)) MakeMove(Move.Scissors);
    }

    void MakeMove(Move move)
    {
        playerMove = move;
        aiMove = (Move)Random.Range(0, 3);

        aiChoiceText.text = $"AI chose: {aiMove}";
        playerChoiceText.text = $"You chose: {move}";
        resultText.text = $"Result: {GetResult()}";
        ShowSprites();
    }

    string GetResult()
    {
        if (playerMove == aiMove) return "Draw";

        if ((playerMove == Move.Rock && aiMove == Move.Scissors) ||
            (playerMove == Move.Paper && aiMove == Move.Rock) ||
            (playerMove == Move.Scissors && aiMove == Move.Paper))
        {
            return "You Win!";
        }

        return "You Lose!";
    }

    void ShowSprites()
    {
        // Hide all sprites first
        playerRockImage.SetActive(false);
        playerPaperImage.SetActive(false);
        playerScissorsImage.SetActive(false);

        aiRockImage.SetActive(false);
        aiPaperImage.SetActive(false);
        aiScissorsImage.SetActive(false);

        // Show player's selected move
        switch (playerMove)
        {
            case Move.Rock: playerRockImage.SetActive(true); break;
            case Move.Paper: playerPaperImage.SetActive(true); break;
            case Move.Scissors: playerScissorsImage.SetActive(true); break;
        }

        // Show AI's selected move
        switch (aiMove)
        {
            case Move.Rock: aiRockImage.SetActive(true); break;
            case Move.Paper: aiPaperImage.SetActive(true); break;
            case Move.Scissors: aiScissorsImage.SetActive(true); break;
        }
    }

}
