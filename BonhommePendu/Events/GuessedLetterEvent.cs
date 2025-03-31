using BonhommePendu.Models;
using System;
using System.Security.Cryptography.X509Certificates;

namespace BonhommePendu.Events
{
    // Un événement à créer peu importe si la lettre est dans le mot ou pas!
    public class GuessedLetterEvent : GameEvent
    {
        public override string EventType { get { return "GuessedLetter"; } }
        public char letter { get; set; }
        // TODO: Compléter
        public GuessedLetterEvent(GameData gameData, char letter)
        {
            this.letter = letter;
            gameData.GuessedLetters.Add(letter);
            var wordlower = gameData.Word.ToLower();
            if (wordlower.Contains(letter))
            {
                for (int i = 0; i < gameData.Word.Length; i++)
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
