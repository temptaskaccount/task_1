using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GridController : MonoBehaviour
{
    public static  GridController Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    public GridGenerator GridGenerator;

    private void Start()
    {
        GridGenerator.OnNewGridRequest += OnGridsRefreshed;
    }
    void OnGridsRefreshed()
    {
        markedGridsCells.Clear();
        
    }

    private List<GridCell> markedGridsCells = new List<GridCell>();

    public void ControlCellsForCombination()
    {
        List<GridCell> _succesfullGrids = new List<GridCell>();
        foreach (GridCell item in markedGridsCells)
        {
            foreach (GridCell successfulGrid in item.GetSuccessfulGridCells())
            {
                _succesfullGrids.Add(successfulGrid);
            }
        }
        
        
        foreach (GridCell item in  _succesfullGrids.Distinct())
        {
            item.RefreshGridCell();
        }
    }
    public void AddMarkedGridCell(GridCell cell)
    {
        markedGridsCells.Add(cell);
        ControlCellsForCombination();
    }

    public void RemoveMarkedGridCell(GridCell cell)
    {
        markedGridsCells.Remove(cell);
    }

    private void OnDestroy()
    {
        GridGenerator.OnNewGridRequest -= OnGridsRefreshed;
    }
}
