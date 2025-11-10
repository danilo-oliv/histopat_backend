
using histopat_back.Context;
using histopat_back.Dominio.Models.Topic;
using histopat_back.Services.Interfaces;
using histopat_back.ViewModel.Module;
using histopat_back.ViewModel.Topic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace histopat_back.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TopicController : ControllerBase
{
    private ITopicService _topicService;
    public TopicController(ITopicService topicService)
    {
        this._topicService = topicService;
    }

    // GET: api/Topic/module/5
    [HttpGet("module/{moduleId}")]
    public async Task<ActionResult> FindAllTopicsByModuleId(int moduleId)
    {
        var topics = await _topicService.FindAllTopicsByModuleId(moduleId);

        return Ok(topics);
    }

    // GET: api/Module/5
    [HttpGet("{topicId}")]
    public async Task<ActionResult> findById(int topicId)
    {
        var topic = await _topicService.FindById(topicId);

        return Ok(topic);
    }

    // POST: api/Topic
    [HttpPost]
    public async Task<ActionResult> saveTopic(TopicPost topicPost)
    {
        await _topicService.SaveTopic(topicPost);

        return Created();
    }

    // PUT: api/Topic/5
    [HttpPut("{topicId}")]
    public async Task<IActionResult> EditTopic(TopicEdit topicEdit, int topicId)
    {

        await _topicService.EditTopic(topicEdit, topicId);

        return NoContent();
    }

    // DELETE: api/Topic/5
    [HttpDelete("{topicId}")]
    public async Task<IActionResult> DeleteTopic(int topicId)
    {
        await _topicService.DeleteTopic(topicId);

        return NoContent();
    }
}
