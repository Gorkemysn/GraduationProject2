using UnityEngine;
using UnityEngine.UI;

public class PuzzlePiece : MonoBehaviour
{
    public Vector2Int CorrectPosition { get; private set; }
    public Vector2Int CurrentPosition { get; private set; }

    private PuzzleManager manager;
    private Image image;
    private Button button;

    public void Init(int x, int y, Sprite fullSprite, int gridSize)
    {
        CorrectPosition = new Vector2Int(x, y);
        CurrentPosition = CorrectPosition;

        image = GetComponent<Image>();
        button = GetComponent<Button>();
        manager = FindObjectOfType<PuzzleManager>();

        float width = fullSprite.texture.width / gridSize;
        float height = fullSprite.texture.height / gridSize;

        Rect rect = new Rect(x * width, (gridSize - y - 1) * height, width, height);
        image.sprite = Sprite.Create(fullSprite.texture, rect, new Vector2(0.5f, 0.5f));
        button.onClick.AddListener(() => manager.OnPieceClicked(this));
    }

    public void MoveTo(Vector2Int newPos)
    {
        CurrentPosition = newPos;
        GetComponent<RectTransform>().anchoredPosition = new Vector2(newPos.x * 100, -newPos.y * 100);
    }

    public bool IsInCorrectPosition()
    {
        return CurrentPosition == CorrectPosition;
    }

    public bool IsAdjacentTo(Vector2Int pos)
    {
        int dx = Mathf.Abs(CurrentPosition.x - pos.x);
        int dy = Mathf.Abs(CurrentPosition.y - pos.y);
        return (dx + dy) == 1;
    }
}
