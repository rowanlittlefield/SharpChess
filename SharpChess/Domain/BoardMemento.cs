namespace SharpChess
{
    public class BoardMemento
    {
        public (int, int) EndCoordinates { get; protected set; }
        public Piece Piece { get; protected set; }
        public (int, int) StartCoordinates { get; protected set; }

        public BoardMemento(Piece piece, (int, int) startCoordinates, (int, int) endCoordinates)
        {
            StartCoordinates = startCoordinates;
            EndCoordinates = endCoordinates;
            Piece = piece;
        }
    }
}
