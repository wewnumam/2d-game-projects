using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public Sprite backgroundImage;
    public Sprite[] puzzlesImage;
    public List<Sprite> puzzles = new List<Sprite>();
    public List<Button> buttons = new List<Button>();

    private bool hasPickFirstGuess, hasPickSecondGuess;
    private int guessesCount, correctGuessesCount, gameGuesses, firstGuessIndex, secondGuessIndex;
    private string firstGuessPuzzle, secondGuessPuzzle;

    void Awake()
    {
        puzzlesImage = Resources.LoadAll<Sprite>("Sprites/Candy");
        GetButtons();
        AddListeners();
        AddPuzzles();
        Shuffle(puzzles);
        gameGuesses = puzzles.Count / 2;
    }

    void GetButtons()
    {
        GameObject[] puzzleButons = GameObject.FindGameObjectsWithTag("PuzzleButton");

        for (int i = 0; i < puzzleButons.Length; i++)
        {
            buttons.Add(puzzleButons[i].GetComponent<Button>());
            buttons[i].image.sprite = backgroundImage;
        }
    }

    void AddListeners()
    {
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(() => PickAPuzzle());
        }
    }

    void AddPuzzles()
    {
        int puzzlesImageIndex = 0;

        for (int i = 0; i < buttons.Count; i++)
        {
            if (puzzlesImageIndex == buttons.Count / 2) puzzlesImageIndex = 0;

            puzzles.Add(puzzlesImage[puzzlesImageIndex]);

            puzzlesImageIndex++;
        }
    }

    void PickAPuzzle()
    {
        if (!hasPickFirstGuess )
        {
            hasPickFirstGuess = true;
            firstGuessIndex = int.Parse(EventSystem.current.currentSelectedGameObject.name);
            firstGuessPuzzle = puzzles[firstGuessIndex].name;
            buttons[firstGuessIndex].image.sprite = puzzles[firstGuessIndex];
        }
        else if (!hasPickSecondGuess)
        {
            hasPickSecondGuess = true;
            secondGuessIndex = int.Parse(EventSystem.current.currentSelectedGameObject.name);
            secondGuessPuzzle = puzzles[secondGuessIndex].name;
            buttons[secondGuessIndex].image.sprite = puzzles[secondGuessIndex];
            guessesCount++;

            StartCoroutine(CheckPuzzleMatch());
        }
    }

    IEnumerator CheckPuzzleMatch()
    {
        yield return new WaitForSeconds(1f);

        if (firstGuessPuzzle == secondGuessPuzzle)
        {
            yield return new WaitForSeconds(0.5f);

            buttons[firstGuessIndex].interactable = false;
            buttons[secondGuessIndex].interactable = false;

            buttons[firstGuessIndex].image.color = new Color(0f, 0f, 0f, 0f);
            buttons[secondGuessIndex].image.color = new Color(0f, 0f, 0f, 0f);

            CheckGameFinished();
        }
        else
        {
            buttons[firstGuessIndex].image.sprite = backgroundImage;
            buttons[secondGuessIndex].image.sprite = backgroundImage;
        }

        yield return new WaitForSeconds(0.5f);
        
        hasPickFirstGuess = hasPickSecondGuess = false;
    }

    void CheckGameFinished()
    {
        correctGuessesCount++;
        if (correctGuessesCount == gameGuesses)
        {
            Debug.Log($"GAME FINISHED IN {guessesCount} GUESSES");
        }
    }

    void Shuffle(List<Sprite> sprites)
    {
        for (int i = 0; i < sprites.Count; i++)
        {
            Sprite currentSprite = sprites[i];
            int randomIndex = Random.Range(i, sprites.Count);
            sprites[i] = sprites[randomIndex];
            sprites[randomIndex] = currentSprite;
        }
    }
}
