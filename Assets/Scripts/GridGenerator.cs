using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class GridGenerator : MonoBehaviour
{

    [SerializeField] private RectTransform content;
    private List<GameObject> generatedObjects = new List<GameObject>();
    private Vector2 contentSize;
    public GridCell GridPrefab;
    public UnityAction OnNewGridRequest;
    
    private  int InitialGridSize = 3;

    private void Start()
    {
        GenerateGridCells(InitialGridSize);
    }

    private void GetContentSize()
    {
        contentSize = content.sizeDelta;
    }
    

    public void GenerateGridCells(int i)
    {
        GetContentSize();
        OnNewGridRequest?.Invoke();
        generatedObjects.Clear();
        
        List<RowContainer> Rows = new List<RowContainer>();
        
        float GridCellLengt = GetGridCellLengt(i);
        for (int row = 0; row < i;  row++)
        {
            RowContainer NewRow = new RowContainer();
            Rows.Add(NewRow);
            
            for (int colon = 0; colon < i; colon++)
            {
                
                GridCell created = Instantiate(GridPrefab ,content.transform);
                
                created.Initilaize(GridCellLengt,GetGridPositionByGridSize(row  , colon , GridCellLengt),this);
                NewRow.GridCells.Add(created);
                AddNeigbours(created,row,colon);

            }
        }
        
        
        void AddNeigbours(GridCell _gridCell,int _row,int _colonum)
        {
            GridCell gridCell1 = null;
            if (_row > 0)
            {
                gridCell1 = Rows[_row - 1].GridCells[_colonum];
                _gridCell.AddNeigbour(gridCell1);
                gridCell1.AddNeigbour(_gridCell);
                
            }
            if (_row < Rows.Count - 1)
            {
                gridCell1 = Rows[_row + 1].GridCells[_colonum];
                _gridCell.AddNeigbour(gridCell1);
                gridCell1.AddNeigbour(_gridCell);
            }
            if (_colonum > 0)
            {
                gridCell1 = Rows[_row].GridCells[_colonum - 1];
                _gridCell.AddNeigbour(gridCell1);
                gridCell1.AddNeigbour(_gridCell);
            }
            if (_colonum < Rows[_row].GridCells.Count - 1)
            {
                gridCell1 = Rows[_row].GridCells[_colonum + 1];
                _gridCell.AddNeigbour(gridCell1);
                gridCell1.AddNeigbour(_gridCell);
            }
        }
        
    }


    float GetGridCellLengt(int _sended)
    {
        return contentSize.x / _sended;
    }
    Vector2 GetGridPositionByGridSize(int row,int colonum,float gridCellSideLengt)
    {
        return new Vector2(colonum * gridCellSideLengt  , row * -gridCellSideLengt);
    }
}
[System.Serializable]
public class RowContainer
{
    public List<GridCell> GridCells = new List<GridCell>();
}