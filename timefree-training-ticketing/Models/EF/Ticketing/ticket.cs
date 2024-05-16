using System;
using System.Collections.Generic;

namespace timefree_training_ticketing.Models.EF.Ticketing;

public partial class ticket
{
    public Guid guid { get; set; }

    public string? event_name { get; set; }

    public string? location { get; set; }

    public int? price { get; set; }

    public string? ticket_type { get; set; }

    public DateTime? date { get; set; }

    public bool _deleted { get; set; }

    public DateTime? date_created { get; set; }

    public Guid? created_by { get; set; }

    public string? created_by_ip { get; set; }

    public DateTime? date_modified { get; set; }

    public Guid? modified_by { get; set; }

    public string? modified_by_ip { get; set; }

    public virtual ICollection<order> order { get; set; } = new List<order>();
}
