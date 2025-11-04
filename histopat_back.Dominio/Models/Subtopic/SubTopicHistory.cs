using histopat_back.Dominio.Models.Base;
using histopat_back.Dominio.Models.Module;
using histopat_back.Dominio.Models.User;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace histopat_back.Dominio.Models.Subtopic;

public class SubTopicHistory : BaseHistory
{    public int IdSubTopic { get; set; }

    public Subtopic Subtopic { get; set; } = null!;
}
