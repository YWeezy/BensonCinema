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

    public MaterialsModel GetMaterial(string name, string type){
        MaterialsModel model = _materials.FirstOrDefault(_material => _material.material == name && _material.type == type);
        if (model == null){
            return null;
        }else{
            return model;
        }
    }

    

    public void insertMaterial(MaterialsModel material)
    {
        
        int index = _materials.FindIndex(_material => _material.material == material.material && _material.type == material.type);
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


    public void updateMaterial(int materialId, string newMaterial, int newQuantity, string newHall, string newType, List<Dictionary<string, object>> newOccupation = null)
    {
        var materialToUpdate = _materials.Find( m => m.material == newMaterial);
        if (materialToUpdate == null)
        {
            throw new Exception("Material not found");
        }

        materialToUpdate.quantity = newQuantity;
        materialToUpdate.currentHall = newHall;

        if (newOccupation != null)
        {
            materialToUpdate.occupation = newOccupation;
        }

        DataAccess<MaterialsModel>.WriteAll(_materials);
    
    }
}