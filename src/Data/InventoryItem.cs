using Classic.Data.Enums.Character;

namespace Classic.Data
{
    public class InventoryItem
    {
        public string Name { get; set; }
        public int DisplayID { get; set; }
        public ItemSlot ItemSlot { get; set; }
    }
}
