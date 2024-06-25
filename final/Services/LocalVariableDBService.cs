using System; 
using MongoDB.Driver;
using MongoDB.Bson;  
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using WebApiCSharp.Models;
using System.Collections.Generic;
//add for new feature
namespace WebApiCSharp.Services;

public class LocalVariableDBService:ServiceBase
{
    public static IMongoCollection<LocalVariableDB> LocalVarDBCollection = dbAOS.GetCollection<LocalVariableDB>(Globals.LOCAL_VARIABLES_COLLECTION_NAME);
    public static List<LocalVariableDB> Get()
    {
        try
        {
            var c = LocalVarDBCollection.Find<LocalVariableDB>(c=> true).ToList();
            return c;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error:" + ex.Message);
            return null;
        }
    }
    
    public static LocalVariableDB Get(int id)
    {
        try
        {
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error:" + ex.Message);
            return null;
        }
    }
    public static LocalVariableDB Add(LocalVariableDB item)
    {
        try
        { 
            LocalVarDBCollection.InsertOneAsync(item).GetAwaiter().GetResult();
            return item;
        }
        catch (MongoWriteException mwx)
        {
            if (mwx.WriteError.Category == ServerErrorCategory.DuplicateKey)
            {
                // mwx.WriteError.Message contains the duplicate key error message
            }
            return null;
        }
    }
    
    public static LocalVariableDB Update(LocalVariableDB item)
    {
        try
        { 
            return null;
        }
        catch (MongoWriteException mwx)
        {
            if (mwx.WriteError.Category == ServerErrorCategory.DuplicateKey)
            {
                // mwx.WriteError.Message contains the duplicate key error message
            }
            return null;
        }
    }
    
    public static bool Delete(LocalVariableDB item)
    { 
        // var result = LocalVarCollection.DeleteOne(doc => doc.Id == item.Id);
        // return result.IsAcknowledged;
        return true;
    }
}