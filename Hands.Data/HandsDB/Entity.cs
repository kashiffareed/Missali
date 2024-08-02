using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackableEntities;

namespace Hands.Data.HandsDB
{
    public abstract class Entity : ITrackable,IEntity
    {
        [NotMapped]
        public TrackingState TrackingState { get; set; }
        [NotMapped]
        public ICollection<string> ModifiedProperties { get; set; }

        public bool IsActive { get; set; }
    }
}

