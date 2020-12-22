using SharpChess;

namespace SharpChess
{
    public enum PieceColor
    {
        Black,
        White,
        Null,
    }
}

static class PieceColorMethods
{
    public static PieceColor GetOpposingColor(this PieceColor color)
    {
        switch (color)
        {
            case PieceColor.White:
                return PieceColor.Black;
            case PieceColor.Black:
                return PieceColor.White;
            default:
                return PieceColor.Null;
        }
    }
}
