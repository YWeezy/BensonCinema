using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Microsoft.VisualBasic;


class HallLogic
{
    private List<LocationModel> _locations {get;}
    
    public HallLogic(){
        _locations = LocationAccess.Locationget();
    }

    public List<LocationModel> GetList() {

        

        return _locations;
    }

    public void insertLocation(string name, string type){

        int lastId = _locations.Last().locationID;
        int id = lastId + 1;
        LocationModel newLocation = new LocationModel(id, name, type);
        _locations.Add(newLocation);

        LocationAccess.WriteAll(_locations);
    }

    public bool Delete(int id) {
        LocationModel locToRemove = _locations.Find(p => p.locationID == id);
        if (locToRemove != null)
        {
            _locations.Remove(locToRemove);
            LocationAccess.WriteAll(_locations);
            return true;
        }
        return false;
    }
}