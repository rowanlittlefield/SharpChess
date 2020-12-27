using System;
namespace SharpChess
{
    public class MovePieceBoardMemento : BoardMemento
    {
        public (int, int) EndCoordinates { get; protected set; }
        public Piece Piece { get; protected set; }
        public (int, int) StartCoordinates { get; protected set; }

        public MovePieceBoardMemento(Piece piece, (int, int) startCoordinates, (int, int) endCoordinates)
        {
            StartCoordinates = startCoordinates;
            EndCoordinates = endCoordinates;
            IsTurnOver = true;
            Piece = piece;
        }
    }
}
