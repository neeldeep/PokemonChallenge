using System.Threading.Tasks;

namespace Pokemon.Services.Interfaces
{
    public interface ITranslateService
    {
        Task<string> GetShakespeareText(string inputText);
    }
}
