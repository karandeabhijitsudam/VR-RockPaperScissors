using UnityEngine;
using UnityEngine.UI;
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

    [Header("Spin Animation")]
    public Image playerSpinDisplay;
    public Image aiSpinDisplay;

    public Sprite rockSprite;
    public Sprite paperSprite;
    public Sprite scissorsSprite;

    public GameObject playAgainButton;

    private Sprite[] gestureSprites;
    private bool isSpinning = true;
    private float spinTimer = 0f;
    private float spinSpeed = 0.05f;
    private bool canPlay = true;


    void Start()
    {
        gestureSprites = new Sprite[] { rockSprite, paperSprite, scissorsSprite };
        //ResetGame(); // optional
    }

    void Update()
    {
        if (canPlay && isSpinning)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) MakeMove(Move.Rock);
            if (Input.GetKeyDown(KeyCode.Alpha2)) MakeMove(Move.Paper);
            if (Input.GetKeyDown(KeyCode.Alpha3)) MakeMove(Move.Scissors);

        }
        

        if (isSpinning)
        {
            spinTimer += Time.deltaTime;
            if (spinTimer >= spinSpeed)
            {
                spinTimer = 0f;

                int playerIndex = Random.Range(0, gestureSprites.Length);
                int aiIndex = Random.Range(0, gestureSprites.Length);

                playerSpinDisplay.sprite = gestureSprites[playerIndex];
                aiSpinDisplay.sprite = gestureSprites[aiIndex];
            }
        }

    }

    void MakeMove(Move move)
    {
        canPlay = false;
        isSpinning = false;
        playerSpinDisplay.gameObject.SetActive(false);
        aiSpinDisplay.gameObject.SetActive(false);

        playerMove = move;
        aiMove = (Move)Random.Range(0, 3);

        aiChoiceText.text = $"AI chose: {aiMove}";
        playerChoiceText.text = $"You chose: {move}";
        resultText.text = $"Result: {GetResult()}";
        ShowSprites();

        playAgainButton.SetActive(true);


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

    void HideAllSprites()
    {
        playerRockImage.SetActive(false);
        playerPaperImage.SetActive(false);
        playerScissorsImage.SetActive(false);
        aiRockImage.SetActive(false);
        aiPaperImage.SetActive(false);
        aiScissorsImage.SetActive(false);
    }

    

    public void ResetGame()
    {
        canPlay = true;
        isSpinning = true;
        spinTimer = 0f;

        // Hide previous result texts
        resultText.text = "Press 1 - Rock, 2 - Paper, or 3 - Scissors to play";
        aiChoiceText.text = "AI Choice:";
        playerChoiceText.text = "Your Choice:";

        // Hide all gesture visuals
        HideAllSprites();

        // Show spin displays
        playerSpinDisplay.gameObject.SetActive(true);
        aiSpinDisplay.gameObject.SetActive(true);

        // Hide the Play Again button
        playAgainButton.SetActive(false);
    }


}
