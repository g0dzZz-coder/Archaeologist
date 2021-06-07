using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Archaeologist.Gameplay
{
    public class Field : MonoBehaviour
    {
        [Header("General")]
        [SerializeField] MatchController controller = null;
        [SerializeField] DropZone dropZone = null;
        [SerializeField] RectTransform canvas = null;
        [SerializeField] Vector2 size = new Vector2(10, 10);

        [Header("Cells")]
        [SerializeField] Transform cellRoot = null;
        [SerializeField] Cell cellPrefab = null;

        [Header("Treasures")]
        [SerializeField] Transform treasureRoot = null;
        [SerializeField] Treasure treasurePrefab = null;
        [Range(0f, 1f)]
        [SerializeField] float treasureSpawnRate = 0.2f;

        private bool IsPlayerCanDig => Player.Shovels > 0;

        private readonly List<Cell> cells = new List<Cell>();
        private readonly List<Treasure> treasures = new List<Treasure>();

        private void Awake()
        {
            Init();
        }

        private void OnEnable()
        {
            controller.MatchRestarted += Init;
        }

        private void OnDisable()
        {
            controller.MatchRestarted -= Init;
        }

        private void Init()
        {
            Clear();
            Fill();
        }

        private void Fill()
        {
            for (var i = 0; i < size.x; i++)
            {
                for (var j = 0; j < size.y; j++)
                {
                    var cell = Instantiate(cellPrefab, cellRoot);
                    cell.Clicked += OnCellClicked;

                    cells.Add(cell);
                }
            }
        }

        private void Clear()
        {
            foreach (Cell child in cells)
            {
                try { Destroy(child.gameObject); }
                catch { }
            }

            cells.Clear();

            foreach (Treasure child in treasures)
            {
                try { Destroy(child.gameObject); }
                catch { }
            }

            treasures.Clear();
        }

        private void OnCellClicked(Cell cell)
        {
            if (IsPlayerCanDig == false || cell.IsEmpty)
                return;

            cell.Dig();
            Player.RemoveShovel();

            if (IsTreasureNeedsToBeSpawned())
                SpawnTreasure(cell.transform);
        }

        private void OnTreasureDragEnded(PointerEventData eventData, Treasure treasure)
        {
            if (dropZone.IsPointerReleasedOver(eventData) == false)
                return;

            Destroy(treasure.gameObject);
            Player.IncreaseScore(treasure.Reward);
        }

        private void SpawnTreasure(Transform parent)
        {
            var treasure = Instantiate(treasurePrefab, parent);
            treasure.Init(canvas, treasureRoot);
            treasure.DragEnded += OnTreasureDragEnded;

            treasures.Add(treasure);
        }

        private bool IsTreasureNeedsToBeSpawned()
        {
            return Random.Range(0f, 1f) < treasureSpawnRate;
        }
    }
}