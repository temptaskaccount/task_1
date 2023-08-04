using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class GridGenerator : MonoBehaviour
{

    [SerializeField] private RectTransform Content;
    
    private List<GameObject> GeneratedObjects = new List<GameObject>();
    private Vector2 ContentSize;
    public GridCell GridPrefab;
    
    private int InitialGridSize = 3;

    private void Start()
    {
       // GenerateGridCells(InitialGridSize);
    }

    private void GetContentSize()
    {
        ContentSize = Content.sizeDelta;
    }
    

    public void GenerateGridCells(int i)
    {
        GetContentSize();
        GeneratedObjects.Clear();
        
        List<RowContainer> Rows = new List<RowContainer>();
        
        float GridCellLengt = GetGridCellLengt(i);
        for (int row = 0; row < i;  row++)
        {
            RowContainer NewRow = new RowContainer();
            Rows.Add(NewRow);
            
            for (int colon = 0; colon < i; colon++)
            {
                
                GridCell created = Instantiate(GridPrefab ,Content.transform);
                
                created.Initilaize(GridCellLengt,GetGridPositionByGridSize(row  , colon , GridCellLengt),this);
                NewRow.GridCells.Add(created);

            }
        }
        
    }


    float GetGridCellLengt(int _sended)
    {
        return ContentSize.x / _sended;
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