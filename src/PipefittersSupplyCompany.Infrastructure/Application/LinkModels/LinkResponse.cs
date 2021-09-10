using System;
using System.Collections.Generic;
using PipefittersSupplyCompany.SharedKernel;

namespace PipefittersSupplyCompany.Infrastructure.Application.LinkModels
{
    public class LinkResponse
    {
        public bool HasLinks { get; set; }

        public List<Entity<Guid>> ShapedEntities { get; set; }

        public LinkCollectionWrapper<Entity<Guid>> LinkedEntities { get; set; }

        public LinkResponse()
        {
            ShapedEntities = new List<Entity<Guid>>();
            LinkedEntities = new LinkCollectionWrapper<Entity<Guid>>();
        }
    }
}