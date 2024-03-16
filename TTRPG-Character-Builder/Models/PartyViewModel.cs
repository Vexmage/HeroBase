namespace TTRPG_Character_Builder.Models
{
    public class PartyViewModel
    {
        public int PartyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Character> Characters { get; set; } // list characters in a party
    }
}
