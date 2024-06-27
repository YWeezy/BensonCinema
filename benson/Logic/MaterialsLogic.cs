using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Microsoft.VisualBasic;


public class MaterialsLogic
{
    private List<MaterialsModel> _materials { get; }

    public MaterialsLogic()
    {
        
        _materials = DataAccess<MaterialsModel>.LoadAll();
        
        
    }





    public List<MaterialsModel> GetList()
    {
        return _materials;
    }

    

    public void insertMaterial(MaterialsModel material)
    {
       
        int index = _materials.FindIndex(_material => _material.material == material.material);
        if (index != -1){
            _materials[index].quantity = _materials[index].quantity + material.quantity;
        }
        else
        {
            // Add new model
            _materials.Add(material);
        }

        DataAccess<MaterialsModel>.WriteAll(_materials);
    }

    public void delete(int index){
        _materials.RemoveAt(index);
        DataAccess<MaterialsModel>.WriteAll(_materials);
    }
}