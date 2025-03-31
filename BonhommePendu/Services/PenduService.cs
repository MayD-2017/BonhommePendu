using BonhommePendu.Events;
using BonhommePendu.Models;
using System.Net;
using System.Text;

namespace BonhommePendu.Services
{
    public class PenduService
    {
        public const string LANGUAGE = "en";

        private GameData _gameData { get; set; }

        public GameData JoinGame()
        {
            if (_gameData != null && !_gameData.Won && !_gameData.Lost)
                return _gameData;
            return null;
        }

        public async Task<GameData?> StartGame()
        {
            if (_gameData == null || _gameData.Won || _gameData.Lost)
            {
                string randomWord = await GetRandomWord();
                _gameData = new GameData(randomWord);
                return _gameData;
            }
            return null;
        }

        public GameEvent? GuessLetter(char letter)
        {
            letter = char.Parse(letter.ToString().Normalize(NormalizationForm.FormD).ToLower());
            // On ne fait rien si on a déjà essayé cette lettre
            if (_gameData.GuessedLetters.Contains(letter))
            {
                return null;
            }

            return new GuessEvent(_gameData, letter);
        }

        private async Task<string> GetRandomWord()
        {
            HttpClient client = new HttpClient();
            int wordLength = 10;
            var result = await client.GetAsync("https://random-word.ryanrk.com/api/" + LANGUAGE + "/word/random/?length=" + wordLength);
            var randomWord = await result.Content.ReadAsStringAsync();
            // On se débarasse des charactères \" au début et à la fin 
            return randomWord.Substring(2, wordLength);
        }
    }
}
