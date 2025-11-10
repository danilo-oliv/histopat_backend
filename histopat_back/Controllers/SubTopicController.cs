using histopat_back.Context;
using histopat_back.Dominio.Models.Subtopic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using histopat_back.Services.ServicesImpl;
using Microsoft.AspNetCore.Http.HttpResults;
using histopat_back.ViewModel.SubTopic;
using histopat_back.Services.Interfaces;

namespace histopat_back.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubTopicController : ControllerBase
{
    private ISubTopicService _subTopicService;
    public SubTopicController(ISubTopicService subTopicService)
    {
        this._subTopicService = subTopicService;
    }

    // GET: api/SubTopic/topic/2
    [HttpGet("topic/{topicId}")]
    public async Task<ActionResult> FindAllSubTopicsByTopicId(int topicId)
    {
        var subTopics = await _subTopicService.FindAllSubTopicsByTopicId(topicId);

        return Ok(subTopics);
    }

    // GET: api/SubTopic/5
    [HttpGet("{id}")]
    public async Task<ActionResult> FindById(int id)
    {
        var subTopic = await _subTopicService.findById(id);

        return Ok(subTopic);
    }

    // POST: api/SubTopic
    [HttpPost]
    public async Task<ActionResult> SaveSubTopic(SubTopicPost subTopic)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        await _subTopicService.SaveSubTopic(subTopic);

        return Created();
    }

    // PUT: api/SubTopic/5
    [HttpPut("{subTopicId}")]
    public async Task<IActionResult> EditSubTopic(SubTopicEdit subTopicEdit, int subTopicId)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        await _subTopicService.EditSubTopic(subTopicEdit, subTopicId);

        return NoContent();
    }

    // DELETE: api/SubTopic/5
    [HttpDelete("{subTopicId}")]
    public async Task<IActionResult> DeleteSubTopic(int subTopicId)
    {
        await _subTopicService.DeleteSubTopic(subTopicId);

        return NoContent();
    }
}
