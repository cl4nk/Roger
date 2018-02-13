using UnityEngine;

public class Cell
{
    public enum Type
    {
        Plain,
        Border,
        Corridor,
        Trap,
        Closed
    }

    public enum Direction
    {
        North,
        South,
        East,
        West,
        Count,
        None = -1
    }
    public Type CellType { get; private set; }

    public readonly bool[] walls;

    public void BuildWall (int index, bool build)
    {
        walls[index] = build;
        CellType = GetCaseType();
    }

    public Cell(int x, int y)
    {
        walls = new bool[(int) Direction.Count];
        Close();
        Pos = new Vector2Int(x, y);
    }

    public Vector2Int Pos { get; private set; }

    private Type GetCaseType()
    {
        int wallsCount = 0;
        int countDirection = (int) Direction.Count;
        for (int i = 0; i < countDirection; ++i)
        {
            if (walls[i])
            {
                ++wallsCount;
            }
        }
            
        switch (wallsCount)
        {
            case 0:
                return Type.Plain;
            case 1:
                return Type.Border;
            case 2:
                if (walls[(int) Direction.East] && walls[(int) Direction.West])
                    return Type.Corridor;
                if (walls[(int) Direction.North] && walls[(int) Direction.South])
                    return Type.Corridor;
                return Type.Border;
            case 3:
                return Type.Trap;
            case 4:
                return Type.Closed;
            default:
                return Type.Closed;
        }
    }

    public void Close()
    {
        for (int i = 0; i < (int) Direction.Count; ++i)
        {
            walls[i] = true;
        }
        CellType = Type.Closed;
    }

    public void Open()
    {
        for (int i = 0; i < (int) Direction.Count; ++i)
        {
            walls[i] = false;
        }
        CellType = Type.Plain;
    }
}
