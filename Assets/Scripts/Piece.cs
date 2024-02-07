using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

namespace zeltatech
{
    public class Piece : MonoBehaviour
    {

        public int id;


        Block[] _childBlocks;

        bool canMove = true;
        Vector3 origin;



        public static Action OnPieceAction;

        private void OnDestroy()
        {
            GridManager.instance.ClearCompletedRows();
        }


        private void Awake()
        {
            origin = transform.position;
            _childBlocks = GetComponentsInChildren<Block>();

            Color _color = GameManager.instance.GetRandomColor();
            _color.a = 1;
            foreach (Transform child in transform)
            {
                child.GetComponent<Block>().SetColor(_color);
            }
        }

        private void OnEnable()
        {
            Debug.Log("Spawned");
        }


        private void OnMouseDrag()
        {
            Vector3 cameraPos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);

            if (canMove == true)
                transform.position = cameraPos;



            for (int row = 0; row < GridManager.instance.grid.GetLength(0); row++)
            {
                for (int col = 0; col < GridManager.instance.grid.GetLength(1); col++)
                {
                    if (GridManager.instance.grid[col, row].isFilled == false)
                    {
                        GridManager.instance.grid[col, row].SetColor(GridManager.instance.grid[col, row].blockColor);
                    }
                }
            }

            for (int row = 0; row < GridManager.instance.grid.GetLength(0); row++)
            {
                for (int col = 0; col < GridManager.instance.grid.GetLength(1); col++)
                {
                    foreach (Block item in _childBlocks)
                    {
                        if (GameManager.instance.isValidPosition(_childBlocks))
                        {
                            Color _clr = item.GetColor();
                            _clr.a = 0.5f;
                            GridManager.instance.grid[(int)Mathf.Floor(item.blockPos.x), (int)Mathf.Floor(item.blockPos.y)].SetColor(_clr);
                        }
                    }
                }
            }

        }

        private void OnMouseDown()
        {
            transform.localScale = Vector3.one;

            OnPieceAction?.Invoke();
        }

        private void OnMouseUp()
        {

            if (canMove == true)
            {
                if (GameManager.instance.isValidPosition(_childBlocks))
                {
                    transform.localScale = Vector3.one;

                    for (int i = 0; i < _childBlocks.Length; i++)
                    {
                        _childBlocks[i].transform.position = _childBlocks[i].blockPos;
                        GridManager.instance.grid[(int)_childBlocks[i].blockPos.x, (int)_childBlocks[i].blockPos.y].isFilled = true;
                        Color _clr = GridManager.instance.grid[(int)_childBlocks[i].blockPos.x, (int)_childBlocks[i].blockPos.y].GetColor();
                        _clr.a = 1;
                        GridManager.instance.grid[(int)_childBlocks[i].blockPos.x, (int)_childBlocks[i].blockPos.y].SetColor(_clr);
                    }

                    AudioManager.instance.Play("click");
                    OnPieceAction?.Invoke();
                    canMove = false;
                    GameManager.instance.Check(id);
                }
                else
                {
                    transform.localScale = Vector3.one * 0.5f;
                    transform.position = origin;
                }
            }
        }


    }

}