using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GridCell : MonoBehaviour
{
    [SerializeField]private RectTransform rectTransform;
    private List<GridCell> neigbourGrids = new List<GridCell>();
    public GameObject Xobject;
    private Button selftButton;
    private GridGenerator gridGenerator;
    private GridController gridController;
    private bool marked = false;
    Tween scaleTween;
    public void Initilaize(float _lengt,Vector2 _position,GridGenerator _gridGenerator)
    {
        selftButton = GetComponent<Button>();
        selftButton.onClick.AddListener(OnClick);
        gridGenerator = _gridGenerator;
        gridController = GridController.Instance;
        gridGenerator.OnNewGridRequest += OnGridsRefreshed;
        rectTransform.sizeDelta = new Vector2(_lengt,_lengt);
        rectTransform.anchoredPosition = _position;
        Xobject.transform.localScale = Vector3.zero;
    }

    public void OnClick()
    {
        if(IsMarked())
            return;

        scaleTween?.Kill();
        
        Xobject.SetActive(true);
        marked = true;
        gridController.AddMarkedGridCell(this);
        Xobject.transform.localScale = Vector3.one;

    }
    
    void OnGridsRefreshed()
    {
        gridGenerator.OnNewGridRequest -= OnGridsRefreshed;
        Destroy(gameObject);
    }

    public bool IsMarked()
    {
        return marked;
    }
    public List<GridCell> GetSuccessfulGridCells()
    {
        int MarkedNeigbours = 0;
        List<GridCell> MarkeCells = new List<GridCell>();
        
        foreach (GridCell item in neigbourGrids)
        {
            if (item.IsMarked())
            {
                MarkeCells.Add(item);
                MarkedNeigbours++;
            }
        }

        if (MarkedNeigbours < 2)
            MarkeCells.Clear();
        else
        {
            MarkeCells.Add(this);
        }
        
        return MarkeCells;
    
    }
    public void AddNeigbour(GridCell _gridCell)
    {
        neigbourGrids.Add(_gridCell);
    }

    public void SuccesEffect()
    {
        
        scaleTween = Xobject.transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.OutBounce).OnComplete(() => Xobject.SetActive(false));
    }

    public void RefreshGridCell()
    {
        gridController.RemoveMarkedGridCell(this);
        marked = false;
        SuccesEffect();
    }

}
