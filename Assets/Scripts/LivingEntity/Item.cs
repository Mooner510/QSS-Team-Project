using UnityEngine;
using Random = UnityEngine.Random;

namespace LivingEntity
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private Sprite[] sprites;
        private ItemType _type;

        private void Start() => SetItemType((ItemType) Random.Range(0, 5));

        public ItemType GetItemType() => _type;

        private void SetItemType(ItemType type)
        {
            GetComponent<SpriteRenderer>().sprite = sprites[(int) type];
            _type = type;
        }
    }
}