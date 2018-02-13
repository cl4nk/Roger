using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

[Serializable]
struct ObjectGeneration
{
    //TODO : Protect these values
    public GameObjectPool pool;
    public float density;

    public bool centerIt;
    public bool fitCell;
}

public class LevelGeneration : MonoBehaviour
{
    public UnityEvent OnLevelGeneration = new UnityEvent();
    public UnityEvent OnLevelGenerated = new UnityEvent();

    [SerializeField]
    private Vector3 cellSize = new Vector3(5, 5, 5);

    [Header("Prefabs")]
    //TODO : Allow rules for walls
    [SerializeField]
    private GameObjectPool wallPool;

    [SerializeField]
    private List<ObjectGeneration> objectList;

    public void InitNewMaze(Maze maze)
    {
        OnLevelGeneration.Invoke();

        CleanMaze();

        InitTabCells();

        Init();

        OnLevelGenerated.Invoke();
    }

    private void Init()
    {
        CloseMaze();

        RemoveUselessBorders();
        RemoveUselessWalls();
        RemoveUselessCorridors();

        GenerateDoors();
        GenerateSpawnPos();

        GenerateDecos();

        BuildMaze();
    }

    private void BuildMaze()
    {
        BuildTerrain();
        BuildRoof();

        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                Cell cell = tabCells[x, y];
                if (cell.GetCaseType() == Cell.Type.Closed)
                    continue;
                Vector2Int pos = new Vector2Int(x, y);
                if (cell.walls[(int) Direction.North])
                    BuildWall(pos, Direction.North);
                if (cell.walls[(int) Direction.East])
                    BuildWall(pos, Direction.East);
                if (cell.walls[(int) Direction.South])
                    BuildWall(pos, Direction.South);
                if (cell.walls[(int) Direction.West])
                    BuildWall(pos, Direction.West);

                BuildObject(pos);
            }
    }

    private void CleanMaze()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            if (child != transform && child != enemySpawners.transform && !poolObjects.IsInPool(child.gameObject))
                Destroy(child.gameObject);
        }
    }

    private void InitTabCells()
    {
        tabCells = new Cell[width, height];
        for (int i = 0; i < width; i++)
            for (int j = 0; j < height; j++)
                tabCells[i, j] = new Cell(i, j);
    }

    private void GenerateSpawnPos(int x = -1, int y = -1)
    {
        if (x >= 0 && y >= 0)
        {
            tabCells[x, y].obj = Cell.CASE_OBJECT.Spawn;
            spawnPos = new Vector2Int(x, y);
            return;
        }

        int roomIndex = Random.Range(0, rooms.Count);

        spawnRoom = roomIndex;

        Vector2Int spawnRoomPos = rooms[roomIndex];
        tabCells[spawnRoomPos.x, spawnRoomPos.y].obj = Cell.CASE_OBJECT.Spawn;
        spawnPos = new Vector2Int(spawnRoomPos.x, spawnRoomPos.y);
    }

    private void GenerateDecos()
    {
        if (decoPrefabs.Length == 0)
            return;
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                Cell.Type caseType = tabCells[x, y].GetCaseType();
                if (caseType != Cell.Type.Plain)
                    continue;
                if (tabCells[x, y].obj != Cell.CASE_OBJECT.None)
                    continue;
                if (HasObjNear(new Vector2Int(x, y)))
                    continue;
                if (Random.value < densityDeco)
                {
                    tabCells[x, y].obj = Cell.CASE_OBJECT.Decor;
                    tabCells[x, y].decoIndex = Random.Range(0, decoPrefabs.Length);
                }
            }
    }

    private void RemoveUselessCorridors()
    {
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                Cell cell = tabCells[x, y];
                Vector2Int pos = cell.Pos;
                if (cell.GetCaseType() != Cell.Type.Corridor)
                    continue;
                Direction dir = GetNearTypeDirection(pos, Cell.Type.Corridor);
                if (dir != Direction.None)
                {
                    Direction opposedDir = GetOpposedDirection(dir);
                    Cell other = GetCaseByDirection(pos, dir);
                    if (other == null)
                        continue;
                    if (!cell.walls[(int) dir] && !other.walls[(int) opposedDir])
                        continue;
                    cell.walls[(int) dir] = false;
                    other.walls[(int) opposedDir] = false;
                }
            }
    }

    private void RemoveUselessWalls()
    {
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                Cell cell = tabCells[x, y];
                Vector2Int pos = cell.Pos;
                if (cell.GetCaseType() == Cell.Type.Closed)
                    continue;
                foreach (Direction dir in Enum.GetValues(typeof(Direction)))
                {
                    if (dir == Direction.Count || dir == Direction.None)
                        continue;
                    Direction opposedDir = GetOpposedDirection(dir);
                    Cell other = GetCaseByDirection(pos, dir);
                    if (other == null)
                        continue;
                    if (other.GetCaseType() == Cell.Type.Closed)
                        continue;
                    if (cell.walls[(int) dir] && other.walls[(int) opposedDir])
                        cell.walls[(int) dir] = false;
                }
            }
    }

    private void RemoveUselessBorders()
    {
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                Vector2Int pos = new Vector2Int(x, y);
                Direction dir = GetNearTypeDirection(pos, Cell.Type.Border);
                if (dir != Direction.None)
                {
                    Direction opposedDir = GetOpposedDirection(dir);
                    Cell me = tabCells[x, y];
                    Cell other = GetCaseByDirection(pos, dir);
                    if (other == null)
                        continue;
                    if (me.walls[(int) dir] && other.walls[(int) opposedDir])
                    {
                        me.walls[(int) dir] = false;
                        other.walls[(int) opposedDir] = false;
                    }
                }
                else
                {
                    dir = GetNearTypeDirection(pos, Cell.Type.Plain);
                    if (dir == Direction.None)
                        return;
                    Cell me = tabCells[x, y];
                    if (me.walls[(int) dir])
                        me.walls[(int) dir] = false;
                }
            }
    }

    private Cell GetCaseByDirection(Vector2Int pos, Direction dir)
    {
        int x = pos.x;
        int y = pos.y;
        switch (dir)
        {
            case Direction.North:
                y++;
                break;
            case Direction.South:
                y--;
                break;
            case Direction.East:
                x++;
                break;
            case Direction.West:
                x--;
                break;
        }
        if (!IsInWidth(x) || !IsInWidth(y))
            return null;
        return tabCells[x, y];
    }

    private Direction GetOpposedDirection(Direction dir)
    {
        switch (dir)
        {
            case Direction.North:
                return Direction.South;
            case Direction.South:
                return Direction.North;
            case Direction.East:
                return Direction.West;
            case Direction.West:
                return Direction.East;
            default:
                return Direction.None;
        }
    }

    private bool IsInWidth(int x)
    {
        return x >= 0 && x < width;
    }

    private bool IsInHeight(int y)
    {
        return y >= 0 && y < height;
    }

    private float GetPercentOpenedCell()
    {
        int max = width * height;
        int count = 0;
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
                if (tabCells[x, y].GetCaseType() != Cell.Type.Closed)
                    count++;

        return count / (float) max;
    }

    private void CreatePlain(Vector2Int centerPos, Vector2Int size)
    {
        for (int i = centerPos.x - size.x / 2; i < centerPos.x + size.x / 2; i++)
            for (int j = centerPos.y - size.y / 2; j < centerPos.y + size.y / 2; j++)
            {
                if (!IsInHeight(i) || !IsInHeight(j))
                    continue;
                Cell cell = tabCells[i, j];
                if (j != centerPos.y + size.y / 2 - 1)
                    cell.walls[(int) Direction.North] = false;
                if (i != centerPos.x + size.x / 2 - 1)
                    cell.walls[(int) Direction.East] = false;
                if (i != centerPos.x - size.x / 2)
                    cell.walls[(int) Direction.West] = false;
                if (j != centerPos.y - size.y / 2)
                    cell.walls[(int) Direction.South] = false;
            }
    }

    private void GenerateDoors()
    {
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                Cell cell = tabCells[x, y];
                Cell.Type cellType = cell.GetCaseType();
                if (cellType == Cell.Type.Border || cellType == Cell.Type.Plain)
                {
                    Direction corridorDir = GetNearTypeDirection(new Vector2Int(x, y), Cell.Type.Corridor);
                    if (corridorDir != Direction.None)
                    {
                        Cell corridor = GetCaseByDirection(cell.Pos, corridorDir);
                        if (corridor.walls[(int) GetOpposedDirection(corridorDir)])
                            continue;
                        cell.obj = Cell.CASE_OBJECT.Door;
                        cell.objDirection = corridorDir;
                        cell.walls[(int) corridorDir] = false;
                    }
                }
            }
    }

    private bool HasObjNear(Vector2Int pos)
    {
        foreach (Cell.CASE_OBJECT objType in Enum.GetValues(typeof(Cell.CASE_OBJECT)))
        {
            if (objType == Cell.CASE_OBJECT.None)
                continue;
            if (objType == Cell.CASE_OBJECT.Count)
                continue;
            if (GetNearObjDirection(pos, objType) != Direction.None)
                return true;
        }
        return false;
    }

    private bool IsObjNear(Vector2Int pos, Cell.CASE_OBJECT objType)
    {
        return GetNearObjDirection(pos, objType) != Direction.None;
    }

    private Direction GetNearObjDirection(Vector2Int pos, Cell.CASE_OBJECT objType)
    {
        if (IsInWidth(pos.x + 1))
            if (tabCells[pos.x + 1, pos.y].obj == objType)
                return Direction.East;
        if (IsInWidth(pos.x - 1))
            if (tabCells[pos.x - 1, pos.y].obj == objType)
                return Direction.West;
        if (IsInHeight(pos.y + 1))
            if (tabCells[pos.x, pos.y + 1].obj == objType)
                return Direction.North;
        if (IsInHeight(pos.y - 1))
            if (tabCells[pos.x, pos.y - 1].obj == objType)
                return Direction.South;
        return Direction.None;
    }

    private Direction GetNearTypeDirection(Vector2Int pos, Cell.Type caseType)
    {
        if (IsInWidth(pos.x + 1))
            if (tabCells[pos.x + 1, pos.y].GetCaseType() == caseType)
                return Direction.East;
        if (IsInWidth(pos.x - 1))
            if (tabCells[pos.x - 1, pos.y].GetCaseType() == caseType)
                return Direction.West;
        if (IsInHeight(pos.y + 1))
            if (tabCells[pos.x, pos.y + 1].GetCaseType() == caseType)
                return Direction.North;
        if (IsInHeight(pos.y - 1))
            if (tabCells[pos.x, pos.y - 1].GetCaseType() == caseType)
                return Direction.South;
        return Direction.None;
    }

    private void BuildWall(Vector2Int pos, Direction dir)
    {
        Cell.Type caseType = tabCells[pos.x, pos.y].GetCaseType();
        if (caseType == Cell.Type.Corridor || caseType == Cell.Type.Border)
        {
            if (torchIndex == 0)
                BuildWallWithTorch(pos, dir);
            else
                BuildScaledObject(pos, dir, cubePrefab, normalWallScale, false, "Wall");
            torchIndex = (torchIndex + 1) % torchModulo;
        }
        else
        {
            BuildScaledObject(pos, dir, cubePrefab, normalWallScale, false, "Wall");
        }
    }

    private void BuildWallWithTorch(Vector2Int pos, Direction dir)
    {
        GameObject wall = BuildScaledObject(pos, dir, cubePrefab, normalWallScale, false, "Wall");

        GameObject torch = poolObjects.GetTorch(torchPrefab);
        torch.transform.position = wall.transform.position;
        torch.transform.rotation = wall.transform.rotation;
        torch.transform.Rotate(new Vector3(-20, 0));
        Vector3 torchPosition = torch.transform.position - torch.transform.forward * wall.transform.localScale.z * 0.6f;
        torch.transform.position = torchPosition;
        torch.transform.SetParent(transform);
    }

    private GameObject BuildOrientedObject(Vector2Int pos, Direction dir, GameObject prefab, bool centerIt,
        string name = "OrientedObj")
    {
        return BuildScalableOrientableObject(pos, dir, prefab, false, Vector3.one, centerIt, name);
    }

    private GameObject BuildScaledObject(Vector2Int pos, Direction dir, GameObject prefab, Vector3 scale,
        bool centerIt, string name = "OrientedObj")
    {
        return BuildScalableOrientableObject(pos, dir, prefab, true, scale, centerIt, name);
    }

    private GameObject BuildScalableOrientableObject(Vector2Int pos, Direction dir, GameObjectPool pool, bool scaleIt,
        Vector3 scale, bool centerIt, string name = "OrientedObj")
    {
        int x = pos.x, y = pos.y;
        GameObject obj = pool.Get();

        obj.name = name;
        obj.transform.SetParent(transform);
        Vector3 realPos = new Vector3(x * cellSize, 0, y * cellSize);
        if (scaleIt)
        {
            Vector3 defaultScale = obj.transform.localScale;
            Vector3 newScale = new Vector3(scale.x == 0 ? defaultScale.x : scale.x * cellSize,
                scale.y == 0 ? defaultScale.y : scale.y * cellSize,
                scale.z == 0 ? defaultScale.z : scale.z * cellSize);
            obj.transform.localScale = newScale;
            if (scale.y > 0)
                realPos.y = newScale.y / 2;
        }

        float rotation = 0;
        if (centerIt)
        {
            realPos.x += 0.5f * cellSize;
            realPos.z += 0.5f * cellSize;
        }

        switch (dir)
        {
            case Direction.North:
                if (!centerIt)
                {
                    realPos.x += 0.5f * cellSize;
                    realPos.z += cellSize;
                }
                break;
            case Direction.East:
                rotation = rotationWall;
                if (!centerIt)
                {
                    realPos.x += cellSize;
                    realPos.z += 0.5f * cellSize;
                }
                break;
            case Direction.South:
                rotation = rotationWall * 2;
                if (!centerIt)
                    realPos.x += 0.5f * cellSize;
                break;
            case Direction.West:
                rotation = rotationWall * 3;
                if (!centerIt)
                    realPos.z += 0.5f * cellSize;
                break;
            default:
                break;
        }

        obj.transform.Rotate(new Vector3(0, rotation));
        obj.transform.position = realPos;

        return obj;
    }

    private void BuildDeco(Cell cell)
    {
        Direction dir = decoRandomDir[cell.decoIndex]
            ? (Direction) Random.Range(0, (int) Direction.Count)
            : Direction.None;
        BuildScaledObject(cell.Pos, dir, decoPrefabs[cell.decoIndex], decoScales[cell.decoIndex], true,
            "Deco " + cell.decoIndex);
    }

    private GameObject BuildObject(Vector2Int pos, GameObject prefab, string name = "Obj")
    {
        int x = pos.x, y = pos.y;
        GameObject obj;
        if (name == "SpawnEnemy")
            obj = poolObjects.GetSpawn(prefab);
        else
            obj = Instantiate(prefab);

        obj.name = name;
        obj.transform.SetParent(transform);
        float defaultY = prefab.transform.position.y;
        Vector3 realPos = new Vector3((x + .5f) * cellSize, defaultY, (y + 0.5f) * cellSize);
        obj.transform.position = realPos;

        return obj;
    }


    private void BuildObject(Vector2Int pos)
    {
        Cell cell = tabCells[pos.x, pos.y];
        switch (cell.obj)
        {
            case Cell.CASE_OBJECT.Door:
                BuildScaledObject(pos, cell.objDirection, doorPrefab, normalWallScale, false, "Door");
                break;
            case Cell.CASE_OBJECT.Decor:
                BuildDeco(cell);
                return;
            default:
                return;
        }
    }

    private void CloseMaze()
    {
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                Cell cell = tabCells[x, y];
                if (y == 0)
                    cell.walls[(int) Direction.South] = true;
                if (x == 0)
                    cell.walls[(int) Direction.West] = true;
                if (y == width - 1)
                    cell.walls[(int) Direction.North] = true;
                if (x == height - 1)
                    cell.walls[(int) Direction.East] = true;
            }
    }

    private void OnDrawGizmos()
    {
        int gizmosWidth = width == 0 ? baseWidth : width;
        int gizmosHeight = height == 0 ? baseHeight : height;
        Gizmos.color = Color.black;
        for (int x = 0; x < gizmosWidth; x++)
            Gizmos.DrawLine(new Vector3(x * cellSize, 0, 0), new Vector3(x * cellSize, 0, gizmosHeight * cellSize));
        for (int y = 0; y < gizmosHeight; y++)
            Gizmos.DrawLine(new Vector3(0, 0, y * cellSize), new Vector3(gizmosWidth * cellSize, 0, y * cellSize));
    }
}