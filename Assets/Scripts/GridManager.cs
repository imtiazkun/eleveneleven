using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace zeltatech
{
    public class GridManager : MonoBehaviour
    {

        public static GridManager instance;


        public Block[,] grid = new Block[11, 11];

        [SerializeField]
        Block gridEmptyBlock;

        [SerializeField]
        Transform Board;

        [SerializeField]
        Vector2 offset;


        public static Action OnGridComplete;


        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            GenerateGrid();
        }

        int count = 0;

        [Button]
        public void GenerateGrid()
        {
            foreach (Transform _child in Board)
            {
                Destroy(_child.gameObject);
            }

            for (int row = 0; row < grid.GetLength(0); row++)
            {
                for (int col = 0; col < grid.GetLength(1); col++)
                {
                    StartCoroutine(GenerateBlock(col, row, count));
                    count++;
                }
            }
        }

        IEnumerator GenerateBlock(int col, int row, int _count)
        {
            yield return new WaitForSeconds(_count * 0.02f);
            Block block = Instantiate(gridEmptyBlock, new Vector2(col, row) + offset, Quaternion.identity);
            block.gameObject.name = $"block[{col}, {row}]";

            block.Init(new Vector2(col, row));

            block.transform.SetParent(Board);
            grid[col, row] = block;


            if (_count >= 120)
            {
                OnGridComplete?.Invoke();
            }
        }


        public void ClearCompletedRows()
        {
            for (int row = 0; row < grid.GetLength(0); row++)
            {
                if (CheckifRowClearable(row) == true)
                {
                    ClearCompletedRow(row);
                }
            }
        }

        public bool CheckifRowClearable(int _row)
        {
            for (int col = 0; col < grid.GetLength(1); col++)
            {
                if (grid[col, _row].isFilled == false)
                    return false;
            }

            return true;
        }

        public void ClearCompletedRow (int _row)
        {
            for (int col = 0;col < grid.GetLength(1); col++)
            {
                grid[col, _row].isFilled = false;
                grid[col, _row].SetColor(grid[col, _row].blockColor);
                GameManager.instance.UpdateScore();
            }
        }

        // Debug
        [Button]
        public void PrintGridCellCount()
        {
            Debug.Log(count);
        }
    }

}