using System;
using System.Collections.Generic;

namespace hnc_diaz.Models;

public partial class InfoHnc
{
    public int IdInfo { get; set; }

    public string MatriculaInfo { get; set; } = null!;

    public string FechaInfo { get; set; } = null!;

    public string HoraInfo { get; set; } = null!;

    public string EstadoInfo { get; set; } = null!;
}
