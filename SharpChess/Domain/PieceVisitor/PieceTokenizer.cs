namespace SharpChess
{
    public class PieceTokenizer : PieceVisitor
    {
        public string Tokens { get; private set; }

        public PieceTokenizer()
        {
            Tokens = "";
        }

        public override void VisitKing(King king)
        {
            Tokens += $"K:{king.Color.ToToken()},";
        }

        public override void VisitQueen(Queen queen)
        {
            Tokens += $"Q:{queen.Color.ToToken()},";
        }

        public override void VisitRook(Rook rook)
        {
            Tokens += $"r:{rook.Color.ToToken()},";
        }

        public override void VisitBishop(Bishop bishop)
        {
            Tokens += $"b:{bishop.Color.ToToken()},";
        }

        public override void VisitKnight(Knight knight)
        {
            Tokens += $"k:{knight.Color.ToToken()},";
        }

        public override void VisitPawn(Pawn pawn)
        {
            Tokens += $"p:{pawn.Color.ToToken()},";
        }

        public override void VisitNullPiece(NullPiece nullpiece)
        {
            Tokens += " : ,";
        }

        public void ClearTokens()
        {
            Tokens = "";
        }
    }
}
