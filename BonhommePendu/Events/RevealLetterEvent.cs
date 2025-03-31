using BonhommePendu.Models;

namespace BonhommePendu.Events
{
    // Un événement à créer pour CHAQUE positon d'une lettre que l'on veut révéler (alors il faut faire plusieurs événements si la lettre est là plusieurs fois!)
    public class RevealLetterEvent : GameEvent
    {
        public override string EventType { get { return "RevealLetter"; } }
        public int index { get; set; }
        public char letter { get; set; }

        public RevealLetterEvent(GameData gameData, char letter, int index)
        {
            this.index = index;
            // Conseil: Vous pouvez utiliser gameData.RevealLetter mettre à jour gameData
            this.letter = gameData.RevealLetter(index);
            // Conseil: Vous pouvez utiliser gameData.HasGuessedTheWord pour savoir si c'est une victoire
            if (gameData.HasGuessedTheWord) Events.Add(new WinEvent(gameData));
        }
    }
}
