using System; 
using MongoDB.Driver;
using MongoDB.Bson;  
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using WebApiCSharp.Models;
using System.Collections.Generic;

namespace WebApiCSharp.Services
{
    public class LocalVariableService : ServiceBase
    {
        public static IMongoCollection<LocalVariable> LocalVarCollection = dbAOS.GetCollection<LocalVariable>(Globals.LOCAL_VARIABLES_COLLECTION_NAME);
        public static List<LocalVariable> Get()
        {
            try
            {
                var c = LocalVarCollection.Find<LocalVariable>(c=> true).ToList();
                return c;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex.Message);
                return null;
            }
        }

        public static LocalVariable Get(int id)
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

        public static LocalVariable Add(LocalVariable item)
        {
            try
            { 
                LocalVarCollection.InsertOneAsync(item).GetAwaiter().GetResult();
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

        public static LocalVariable Update(LocalVariable item)
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

        public static bool Delete(LocalVariable item)
        { 
            // var result = LocalVarCollection.DeleteOne(doc => doc.Id == item.Id);
            // return result.IsAcknowledged;
            return true;
        }
    }
}