using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class PuzzleManager : MonoBehaviour
{
    public int gridSize = 3;
    public Sprite puzzleImage;
    public GameObject puzzlePiecePrefab;
    public Transform puzzleParent;
    public TextMeshProUGUI solvedText;
    public GameObject puzzlePanel;
    public Button shuffleButton;
    public Button closeButton;

    public TextMeshProUGUI moveCounterText;
    private int moveCount = 0;

    private List<PuzzlePiece> pieces = new List<PuzzlePiece>();
    private Vector2Int emptySlot;
    private bool isShuffled = false;
    private bool rewardGiven = false;

    [Header("Puzzle Reward Settings")]
    [SerializeField] private int rewardSouls = 50; // Inspector üzerinden ayarlanabilir hale getirildi

    void Start()
    {
        solvedText.gameObject.SetActive(false);
        closeButton.gameObject.SetActive(false);
        shuffleButton.gameObject.SetActive(true);

        shuffleButton.onClick.AddListener(ShufflePuzzle);
        closeButton.onClick.AddListener(ClosePuzzle);
        GeneratePuzzle();
    }

    void GeneratePuzzle()
    {
        float size = 100f;
        Texture2D tex = puzzleImage.texture;
        int total = gridSize * gridSize;
        emptySlot = new Vector2Int(gridSize - 1, gridSize - 1);

        for (int y = 0; y < gridSize; y++)
        {
            for (int x = 0; x < gridSize; x++)
            {
                if (x == gridSize - 1 && y == gridSize - 1) continue;

                GameObject obj = Instantiate(puzzlePiecePrefab, puzzleParent);
                obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(x * size, -y * size);

                PuzzlePiece piece = obj.GetComponent<PuzzlePiece>();
                piece.Init(x, y, puzzleImage, gridSize);
                pieces.Add(piece);
            }
        }
    }

    void ShufflePuzzle()
    {
        if (isShuffled) return;
        isShuffled = true;

        for (int i = 0; i < 50; i++)
        {
            List<PuzzlePiece> movable = pieces.FindAll(p => p.IsAdjacentTo(emptySlot));
            if (movable.Count > 0)
            {
                PuzzlePiece selected = movable[Random.Range(0, movable.Count)];
                Vector2Int prevPos = selected.CurrentPosition;
                selected.MoveTo(emptySlot);
                emptySlot = prevPos;
            }
        }

        shuffleButton.gameObject.SetActive(false);
    }

    public void OnPieceClicked(PuzzlePiece piece)
    {
        if (!isShuffled) return;

        if (piece.IsAdjacentTo(emptySlot))
        {
            Vector2Int prev = piece.CurrentPosition;
            piece.MoveTo(emptySlot);
            emptySlot = prev;

            moveCount++;
            UpdateMoveCounter();

            if (IsSolved() && !rewardGiven)
            {
                solvedText.gameObject.SetActive(true);
                closeButton.gameObject.SetActive(true);
                SoulManager.instance.AddSouls(rewardSouls); // Artýk inspector'dan ayarlanýyor
                rewardGiven = true;
            }
        }
    }

    void UpdateMoveCounter()
    {
        if (moveCounterText != null)
        {
            moveCounterText.text = "" + moveCount;
        }
    }

    bool IsSolved()
    {
        foreach (var p in pieces)
        {
            if (!p.IsInCorrectPosition())
                return false;
        }
        return true;
    }

    void ClosePuzzle()
    {
        Time.timeScale = 1f;
        solvedText.gameObject.SetActive(false);
        closeButton.gameObject.SetActive(false);
        puzzlePanel.SetActive(false);
        shuffleButton.gameObject.SetActive(true);
        moveCount = 0;
        UpdateMoveCounter();
        rewardGiven = false;
    }
}
