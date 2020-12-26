﻿using SharpChess;

namespace SharpChess
{
    public enum UserAction
    {
        Up,
        Right,
        Down,
        Left,
        Enter,
        ToggleTheme,
        FlipBoard,
        Null,
    }
}

static class UserActionMethods
{
    public static UserAction FlipVertically(this UserAction action)
    {
        switch (action)
        {
            case UserAction.Up:
                return UserAction.Down;
            case UserAction.Down:
                return UserAction.Up;
            default:
                return action;
        }
    }
}
