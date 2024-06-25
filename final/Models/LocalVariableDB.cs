using MongoDB.Bson;  
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
namespace WebApiCSharp.Models;
//add for new feature

public class LocalVariableDB
{
    [BsonElement("_id")]
    public BsonObjectId Id{get;set;}


    [BsonElement("VarName")]
    public string Name{get;set;}


    [BsonElement("Value")]
    public object Value{get;set;}

    [BsonElement("Module")]
    public string Module{get;set;} 
}