using BonhommePendu.Models;

namespace BonhommePendu.Events
{
    // Un événement à créer peu importe si la lettre est dans le mot ou pas!
    public class GuessedLetterEvent : GameEvent
    {
        public override string EventType { get { return "GuessedLetter"; } }

        // TODO: Compléter
        public GuessedLetterEvent(GameData gameData, char letter)
        {
            gameData.GuessedLetters.Add(letter);
            if (gameData.Word.Contains(letter))
            {
                for (int i = 0; i <= gameData.Word.Length; i++)
                {
                    if (gameData.HasSameLetterAtIndex(letter, i))
                    {
                        Events.Add(new RevealLetterEvent(gameData, letter, i));
                    }
                }
            }
            else
            {
                Events.Add(new WrongGuessEvent(gameData));
            }
            

        }
    }
}
