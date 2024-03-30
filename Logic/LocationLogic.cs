using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Microsoft.VisualBasic;


class LocationLogic
{
    private List<LocationModel> _locations;
    
    public LocationLogic(){
        _locations = LocationAccess.Locationget();
    }

    public void insertLocation(string name, string type){

        int lastId = _locations.Last().locationID;
        int id = lastId + 1;
        LocationModel newLocation = new LocationModel(id, name, type);
        _locations.Add(newLocation);

        LocationAccess.WriteAll(_locations);
    }
}