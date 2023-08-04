using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridCell : MonoBehaviour
{
    [SerializeField]private RectTransform rectTransform;
    public GameObject Xobject;
    private Button selftButton;
    private GridGenerator gridGenerator;
    private List<GridCell> neigbourGrids = new List<GridCell>();
    
    public void Initilaize(float _lengt,Vector2 _position,GridGenerator _gridGenerator)
    {
        selftButton = GetComponent<Button>();
        selftButton.onClick.AddListener(OnClick);
        gridGenerator = _gridGenerator;
        rectTransform.sizeDelta = new Vector2(_lengt,_lengt);
        rectTransform.anchoredPosition = _position;
    }

    public void OnClick()
    {
        Xobject.SetActive(true);
    }

}
