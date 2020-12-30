namespace SharpChess
{
    public abstract class PieceVisitor
    {
        public PieceVisitor()
        {
        }

        public abstract void VisitKing(King king);

        public abstract void VisitQueen(Queen queen);

        public abstract void VisitRook(Rook rook);

        public abstract void VisitBishop(Bishop bishop);

        public abstract void VisitKnight(Knight knight);

        public abstract void VisitPawn(Pawn pawn);

        public abstract void VisitNullPiece(NullPiece nullpiece);
    }
}
