﻿using Framework.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LookUp.Domain.EventEnvelopes.Attribute
{
    public record AttributeCreated(
        Guid Id,
        AttributeTypeEnum Type,
        string Unit,
        RecordStatusEnum Status
     )
    {
        public static AttributeCreated Created(Guid id, AttributeTypeEnum type, string unit, RecordStatusEnum status)
                => new(id, type, unit, status);
    }
}
