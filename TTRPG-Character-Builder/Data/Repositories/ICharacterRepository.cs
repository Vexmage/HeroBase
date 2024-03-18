using System.Collections.Generic;
using System.Threading.Tasks;
using TTRPG_Character_Builder.Models;

namespace TTRPG_Character_Builder.Data.Repositories
{
    public interface ICharacterRepository
    {
        Task<IEnumerable<Character>> GetAllCharactersAsync();
        Task<Character> GetCharacterByIdAsync(int id);
        Task AddCharacterAsync(Character character);
        Task UpdateCharacterAsync(Character character);
        Task DeleteCharacterAsync(int id);
    }
}
