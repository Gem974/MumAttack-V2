using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MumEscape_GenerationProcedural : MonoBehaviour
{
    //Variables 
    private MumEscape_Cells[,] _grid;
    private List<MumEscape_Cells> _gridCells = new List<MumEscape_Cells>();
    private int _counterOfToys, _counterOfObstacles;
    private GameObject _emptyRoomParent;
    [Header("Procedural Prefabs")]
    public GameObject _cellsPrefab;
    public GameObject  _goalPrefab, _bedPrefab, _spawnJ1, _spawnJ2, _obstaclePrefab, _emptyRoom;
    public GameObject[] _toysPrefabs;
    public Vector3 _toysOffset;
    [Header("Procedural Parameters")]
    public int _lengthOfGrid;
    public int _largeOfGrid, _maxObject, _maxObstacles;

    // When you click in the button on the editor :
    public void GenerateRoom()
    {
        ClearRoom();
        CreateEmptyRoomParent();
        InitGrid();
        CreateBed();
        CreatePlayerSpawners();
        GenerateObstacle();
        GenerateToys();
    }

    //Game Object Parent creation
    public void CreateEmptyRoomParent()
    {
        _emptyRoomParent = Instantiate(_emptyRoom, transform.position, Quaternion.identity,gameObject.transform);
        _emptyRoomParent.name = "Room";
        _emptyRoomParent.GetComponent<MumEscape_Level>()._large = _largeOfGrid;
        _emptyRoomParent.GetComponent<MumEscape_Level>()._lenght = _lengthOfGrid;
    }

    //Initialize starting grid
    public void InitGrid()
    {
        _grid = new MumEscape_Cells[ _lengthOfGrid, _largeOfGrid];
        for (int i = 0; i < _lengthOfGrid; i++)
        {
            for (int j = 0; j < _largeOfGrid; j++)
            {
                Vector3 offset = new Vector3(i, 0, j);
                Vector3 pos = gameObject.transform.position + offset;
                GameObject cellsGO;
                if(i == (int)_lengthOfGrid/2 && j == 0)
                {
                    cellsGO = Instantiate(_goalPrefab, pos, Quaternion.identity);
                    cellsGO.name = "Goal" + i + "x" + j;
                    
                }
                else
                {
                    cellsGO = Instantiate(_cellsPrefab, pos, Quaternion.identity);
                    cellsGO.name = "Cells" + i + "x" + j;
                }
                cellsGO.transform.parent = _emptyRoomParent.transform;
                MumEscape_Cells cells = cellsGO.GetComponent<MumEscape_Cells>();  
                cells.setX(i);
                cells.setY(j);
                _grid[i, j] = cells;
                _gridCells.Add(cells);
            }
        }
    }

    //Put the bed
    private void CreateBed()
    {
        foreach (var cells in _gridCells)
        {
            if(cells.getX() == _lengthOfGrid /2 && cells.getY() == _largeOfGrid -1 )
            {
                var go = Instantiate(_bedPrefab, cells.gameObject.transform.position + Vector3.up, Quaternion.identity, _emptyRoomParent.transform);
                go.name = "Bed";
                cells._isEmpty = false;
                var voisins = GetVoisins(cells);
                for (int i = 0; i < voisins.Count; i++)
                {
                    if(voisins[i].getY() == cells.getY())
                    {
                        voisins[i]._isEmpty = false;
                    }
                }
            }
        }
        
    }

    //Put Player spawners
    private void CreatePlayerSpawners()
    {
        foreach (var cells in _gridCells)
        {
            if ((cells.getX() == (_lengthOfGrid / 2) - 2) && cells.getY() == _largeOfGrid - 1)
            {
               
                var spawn = Instantiate(_spawnJ1, cells.gameObject.transform.position + Vector3.up, Quaternion.identity, _emptyRoomParent.transform);
                spawn.name = "Spawner J1";
                cells._isEmpty = false;
                var voisins = GetVoisins(cells);
                for (int i = 0; i < voisins.Count; i++)
                {
                    voisins[i]._isEmpty = false;
                }
            }

            if ((cells.getX() == (_lengthOfGrid / 2) +2) && cells.getY() == _largeOfGrid - 1)
            {

                var spawn = Instantiate(_spawnJ2, cells.gameObject.transform.position + Vector3.up, Quaternion.identity, _emptyRoomParent.transform);
                spawn.name = "Spawner J2";
                cells._isEmpty = false;
                var voisins = GetVoisins(cells);
                for (int i = 0; i < voisins.Count; i++)
                {
                    voisins[i]._isEmpty = false;
                }
            }
        }
    }

    //Put the Obstacle
    private void GenerateObstacle()
    {
        _counterOfObstacles = 0;
        while(_counterOfObstacles < _maxObstacles)
        {
            var emptyCells = GetEmptyCells();
            int cellID = Random.Range(0, (emptyCells.Count / 2)-_lengthOfGrid);

            if(emptyCells[cellID].getY() == 0)
            {
                while(emptyCells[cellID].getY() == 0)
                {
                    cellID = Random.Range(0, emptyCells.Count /2 - _lengthOfGrid);
                }
            }
            
            var go = Instantiate(_obstaclePrefab, new Vector3(emptyCells[cellID].getX(), 1, emptyCells[cellID].getY() - 0.5f), Quaternion.identity, _emptyRoomParent.transform);
            go.name = "Meuble";
            emptyCells[cellID]._isEmpty = false;
            emptyCells[cellID - 1]._isEmpty = false;
            _counterOfObstacles++;
        }    
    }


    //Put Toys and Cloths
    private void GenerateToys()
    {
        var emptyCells = GetEmptyCells();
        _counterOfToys = 0;
        while (_counterOfToys < _maxObject)
        {
            for (int i = 0; i < emptyCells.Count; i++)
            {
                if (_counterOfToys < _maxObject)
                {
                    int pop = Random.Range(0, 100);
                    if (pop >= 80)
                    {
                        int id = Random.Range(0, _toysPrefabs.Length);
                         var go = Instantiate(_toysPrefabs[id], new Vector3(emptyCells[i].getX(), 0, emptyCells[i].getY()) + _toysOffset, Quaternion.identity, _emptyRoomParent.transform);
                        go.name = _toysPrefabs[id].name;
                        _counterOfToys++;
                        emptyCells[i]._isEmpty = false;
                        emptyCells.Remove(emptyCells[i]);
                    }
                }
            }
        }
    }


    //Clear the old level
    private void ClearRoom()
    {
        Transform[] childrens = gameObject.GetComponentsInChildren<Transform>();

        //Destroy the old room in hiearchy
        for (int i = 1; i < childrens.Length; i++)
        {
            if(childrens[i] != null)
                DestroyImmediate(childrens[i].gameObject);
        }

        //Clear List
        if (_gridCells.Count > 0)
        {
            _gridCells.Clear();
        }
    }

    private List<MumEscape_Cells> GetEmptyCells()
    {
        var emptyCells = new List<MumEscape_Cells>();
        foreach (var cell in _gridCells)
        {
            if (cell._isEmpty)
            {
                emptyCells.Add(cell);
            }
        }

        return emptyCells;
    }

    // Function that allows to retrieve the list of neighbors of a MumEscape_Cells
    public List<MumEscape_Cells> GetVoisins(MumEscape_Cells cell)
    {
        List<MumEscape_Cells> voisins = new List<MumEscape_Cells>();
        int x = cell.getX();
        int y = cell.getY();

        //Les MumEscape_Cellss avec 2 voisins
        if (x == 0 && y == 0)
        {
            MumEscape_Cells voisin1 = _grid[1, 0];
            MumEscape_Cells voisin2 = _grid[0, 1];
            voisins.Add(voisin1);
            voisins.Add(voisin2);
        }
        else if (x == 0 && y == _largeOfGrid - 1)
        {
            MumEscape_Cells voisin1 = _grid[0, _largeOfGrid - 2];
            MumEscape_Cells voisin2 = _grid[1, _largeOfGrid - 1];
            voisins.Add(voisin1);
            voisins.Add(voisin2);
        }
        else if (x == _lengthOfGrid - 1 && y == _largeOfGrid - 1)
        {
            MumEscape_Cells voisin1 = _grid[_lengthOfGrid - 2, _largeOfGrid - 1];
            MumEscape_Cells voisin2 = _grid[_lengthOfGrid - 1, _largeOfGrid - 2];
            voisins.Add(voisin1);
            voisins.Add(voisin2);
        }
        else if (x == _lengthOfGrid - 1 && y == 0)
        {
            MumEscape_Cells voisin1 = _grid[_lengthOfGrid - 2, 0];
            MumEscape_Cells voisin2 = _grid[_lengthOfGrid - 1, 1];
            voisins.Add(voisin1);
            voisins.Add(voisin2);
        }
        //Les MumEscape_Cellss avec 3 voisins
        else if (x == 0)
        {
            MumEscape_Cells voisin1 = _grid[0, y + 1];
            MumEscape_Cells voisin2 = _grid[1, y];
            MumEscape_Cells voisin3 = _grid[0, y - 1];
            voisins.Add(voisin1);
            voisins.Add(voisin2);
            voisins.Add(voisin3);
        }
        else if (x == _lengthOfGrid - 1)
        {
            MumEscape_Cells voisin1 = _grid[_lengthOfGrid - 1, y + 1];
            MumEscape_Cells voisin2 = _grid[_lengthOfGrid - 2, y];
            MumEscape_Cells voisin3 = _grid[_lengthOfGrid - 1, y - 1];
            voisins.Add(voisin1);
            voisins.Add(voisin2);
            voisins.Add(voisin3);
        }
        else if (y == 0)
        {
            MumEscape_Cells voisin1 = _grid[x - 1, 0];
            MumEscape_Cells voisin2 = _grid[x, 1];
            MumEscape_Cells voisin3 = _grid[x + 1, 0];
            voisins.Add(voisin1);
            voisins.Add(voisin2);
            voisins.Add(voisin3);
        }
        else if (y == _largeOfGrid - 1)
        {
            MumEscape_Cells voisin1 = _grid[x + 1, _largeOfGrid - 1];
            MumEscape_Cells voisin2 = _grid[x, _largeOfGrid - 2];
            MumEscape_Cells voisin3 = _grid[x - 1, _largeOfGrid - 1];
            voisins.Add(voisin1);
            voisins.Add(voisin2);
            voisins.Add(voisin3);
        }// Les MumEscape_Cellss avec 4 voisins
        else
        {
            MumEscape_Cells voisin1 = _grid[x + 1, y];
            MumEscape_Cells voisin2 = _grid[x - 1, y];
            MumEscape_Cells voisin3 = _grid[x, y + 1];
            MumEscape_Cells voisin4 = _grid[x, y - 1];
            voisins.Add(voisin1);
            voisins.Add(voisin2);
            voisins.Add(voisin3);
            voisins.Add(voisin4);
        }
        return voisins;
    }
}
