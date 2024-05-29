using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Microsoft.VisualBasic;


public class HallLogic
{
    private List<HallModel> _halls { get; }
    public string path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"DataSources/halls.json"));

    public HallLogic(string? newPath = null)
    {
        if (newPath != null) {
            path = newPath;
        } 
        _halls = DataAccess<HallModel>.LoadAll(path);
    }

    public string? GetHallNameById(int id)
    {
        HallModel? hall = _halls.FirstOrDefault(h => h.hallID == id);
        return hall != null ? hall.hallName : null;
    }



    public int GetTotalHalls()
    {
        return _halls.Count;
    }

    public int GetSeatsCount(int id){
        HallModel? hall = _halls.FirstOrDefault(h => h.hallID == id);
        switch (hall.type.ToLower()){
            case "small":
                return 30;
            case "medium":
                return 50;
            case "large":
                return 80;
            default:
             return 0;
        }
    }

    public int[,] GetSeatsOfHall(int id){
        HallModel? hall = _halls.FirstOrDefault(h => h.hallID == id);
        switch (hall.type.ToLower()){
            case "small":
                int[,] seatsS = {
                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 9, 9},
                    {0, 0, 0, 0, 0, 0, 0, 0, 9, 9, 9, 9}
                    };
                return seatsS;
            case "medium":
                int[,] seatsM = {
                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 9, 9},
                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 9, 9, 9, 9},
                    {0, 0, 0, 0, 0, 0, 0, 0, 9, 9, 9, 9, 9, 9},
                    {0, 0, 0, 0, 0, 0, 9, 9, 9, 9, 9, 9, 9, 9}
                    };
                return seatsM;
            case "large":
                int[,] seatsL = {
                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 , 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 , 0, 0},
                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 , 9, 9},
                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 , 9, 9},
                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 9, 9, 9, 9 , 9, 9},
                    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 9, 9, 9, 9 , 9, 9},
                    };
                return seatsL;
            default:
                return null;
        }

    }

    public List<HallModel> GetList()
    {
        return _halls;
    }

    public void DisplayTable(bool showId = false)
    {
        Console.WriteLine($"{Color.Yellow}Table of all Halls:{Color.Reset}\n");

        if (showId == false)
        {
            Console.Write(Color.Blue);
            Console.WriteLine("{0,-15}{1,-10}{2,-15}", "Name", "Type", "Active");
            Console.WriteLine($"{Color.Reset}-----------------------------------");
            foreach (HallModel hall in _halls)
            {
                string actstr;
                if (hall.active)
                {
                    actstr = "Active";
                }
                else
                {
                    actstr = "Inactive";
                }
                Console.WriteLine("{0,-15}{1,-10}{2,-15}", hall.hallName, hall.type, actstr);
            }
        }
        else
        {
            Console.Write(Color.Blue);
            Console.WriteLine("{0, -5}{1,-15}{2,-10}{3,-15}", "ID", "Name", "Type", "Active");
            Console.WriteLine($"{Color.Reset}----------------------------------------");
            foreach (HallModel hall in _halls)
            {
                string actstr;
                if (hall.active)
                {
                    actstr = "Active";
                }
                else
                {
                    actstr = "Inactive";
                }
                Console.WriteLine("{0, -5}{1,-15}{2,-10}{3,-15}", hall.hallID, hall.hallName, hall.type, actstr);
            }
        }
    }

    public void insertHall(string name, string type)
    {

        int lastId = _halls.Last().hallID;
        int id = lastId + 1;
        HallModel newHall = new HallModel(id, name, type, true);
        _halls.Add(newHall);

        DataAccess<HallModel>.WriteAll(_halls, path);
    }

    public void UpdateList(HallModel hall)
    {
        //Find if there is already an model with the same id
        int index = _halls.FindIndex(s => s.hallID == hall.hallID);

        if (index != -1)
        {
            //update existing model
            _halls[index] = hall;
        }
        else
        {
            //add new model
            _halls.Add(hall);
        }
        DataAccess<HallModel>.WriteAll(_halls, path);

    }

    public int GetNewId()
    {
        int currentId = 0;
        foreach (var hall in _halls)
        {
            if (hall.hallID > currentId)
            {
                currentId = hall.hallID;
            }
        }
        return currentId + 1;
    }

    public bool Delete(int id)
    {
        HallModel locToRemove = _halls.Find(p => p.hallID == id);
        if (locToRemove != null)
        {
            _halls.Remove(locToRemove);
            DataAccess<HallModel>.WriteAll(_halls, path);
            return true;
        }
        return false;
    }

    public string getHallNamebyId(int id)
    {
        try
        {
            HallModel hall = _halls.Find(p => p.hallID == id);
            return hall.hallName;
        }
        catch (System.Exception)
        {
            return "";
        }
    }

}