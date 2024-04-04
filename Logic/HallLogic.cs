using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Microsoft.VisualBasic;


class HallLogic
{
    private List<HallModel> _halls {get;}
    
    public HallLogic(){
        _halls = HallAccess.Hallget();
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

    public List<HallModel> GetList() {
        return _halls;
    }

    public void DisplayTable(bool showId = false) {
        Console.WriteLine("Table of all halls\n");

        if (showId == false) {
            Console.WriteLine("{0,-15}{1,-10}{2,-15}", "Name", "Type", "Active");
            Console.WriteLine("-----------------------------------");
            foreach (HallModel hall in _halls)
            {
                string actstr;
                if (hall.active)
                {
                    actstr = "active";
                }else{
                    actstr = "inactive";
                }
                Console.WriteLine("{0,-15}{1,-10}{2,-15}", hall.hallName, hall.type, actstr);
            }
        } else {
            Console.WriteLine("{0, -5}{1,-15}{2,-10}{3,-15}", "ID", "Name", "Type", "Active");
            Console.WriteLine("----------------------------------------");
            foreach (HallModel hall in _halls)
            {
                string actstr;
                if (hall.active)
                {
                    actstr = "active";
                }else{
                    actstr = "inactive";
                }
                Console.WriteLine("{0, -5}{1,-15}{2,-10}{3,-15}", hall.hallID, hall.hallName, hall.type, actstr);
            }
        }
    }

    public void insertHall(string name, string type){

        int lastId = _halls.Last().hallID;
        int id = lastId + 1;
        HallModel newHall = new HallModel(id, name, type, true);
        _halls.Add(newHall);

        HallAccess.WriteAll(_halls);
    }

    public void UpdateList(HallModel hall)
    {
        //Find if there is already an model with the same id
        int index = _halls.FindIndex(s => s.hallID == hall.hallID);

            //update existing model
        _halls[index] = hall;
        
        HallAccess.WriteAll(_halls);

    }

    public bool Delete(int id) {
        HallModel locToRemove = _halls.Find(p => p.hallID == id);
        if (locToRemove != null)
        {
            _halls.Remove(locToRemove);
            HallAccess.WriteAll(_halls);
            return true;
        }
        return false;
    }

}