using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace zeltatech
{
    public class Block : MonoBehaviour
    {

        [SerializeField, ReadOnly]
        public Vector3 blockPos;

        [SerializeField]
        public Color blockColor;

        public Vector2 blockId;

        public bool isFilled = false;

        public bool IsEmptyCell = false;


        private void Awake()
        {
            if (IsEmptyCell)
            {
                transform.localScale = new Vector3(0.05f, 0.05f);
                transform.DOScale(0.09f, 1);
            }
        }


        public void Init(Vector2 id)
        {
            GetComponent<SpriteRenderer>().color = blockColor;
            blockId = id;
            IsEmptyCell = true;
        }

        public void SetColor(Color _color)
        {
            GetComponent<SpriteRenderer>().color = _color;
        }

        public Color GetColor ()
        {
            return GetComponent<SpriteRenderer>().color;    
        }

        private void Update()
        {
            blockPos = new Vector3(math.floor(transform.position.x), math.floor(transform.position.y), math.floor(transform.position.z));
        }
    }
}