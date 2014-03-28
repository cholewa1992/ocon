﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Build.Framework;

namespace ContextawareFramework
{
    public interface ISituation : ISerializable
    {
        Guid Id { get; }
        string Description { get; set; }
        bool State { get; set; }
        Predicate<ICollection<IEntity>> SituationPredicate { get; set; }
        Guid SubscribersAddresse { get; set; }
    }
}
