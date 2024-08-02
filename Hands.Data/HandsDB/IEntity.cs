using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hands.Data.HandsDB
{
    public interface IEntity
    {

          bool IsActive { get; set; } 
    }
}
