using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApiCSharp.Models;
using WebApiCSharp.Services;

namespace WebApiCSharp.Controllers;

[ApiController]
[Route("[controller]")]
public class LocalVariableDBController : ControllerBase
{
    private readonly ILogger<LocalVariableDBController> _logger;

    public LocalVariableDBController(ILogger<LocalVariableDBController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public List<LocalVariableDB> GetAll()
    {
        return LocalVariableDBService.Get();
    }

    [HttpGet("{id}")]
    public ActionResult<LocalVariableDB> Get(int id)
    {
        var localVar = LocalVariableDBService.Get(id);
        if (localVar == null)
            return NotFound();
        return localVar;
    }

    [HttpPost]
    public IActionResult Create(LocalVariableDB localVar)
    {
        LocalVariableDB n = LocalVariableDBService.Add(localVar);
        if (n == null)
            return Conflict();
        return CreatedAtAction(nameof(Create), n);
    }

    [HttpPut("{id}")]
    public IActionResult Update(LocalVariableDB localVar)
    {
        LocalVariableDBService.Update(localVar);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(string id)
    {
        return NoContent();
    }
}
